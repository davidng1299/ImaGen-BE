using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using ImaGen_BE.DTOs.OAImageGeneration;
using ImaGen_BE.Models.Constants.OAImage;
using ImaGen_BE.DTOs.OAImageSave;
using ImaGen_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace ImaGen_BE.Services
{
    public class OAImageService : IOAImageService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly DataContext _context;
        private readonly S3Service _s3Service;

        public OAImageService(HttpClient httpClient, IConfiguration config, DataContext context, S3Service s3Service)
        {
            _httpClient = httpClient;
            _config = config;
            _context = context;
            _s3Service = s3Service;
        }

        /// <summary>
        /// Generate an image using the OpenAI API.
        /// </summary>
        /// <param name="prompt">The textual prompt describing the image to generate.</param>
        /// <param name="size">The image size, currently supporting these formats: "256x256", "512x512", or "1024x1024".</param>
        /// <param name="style">The image style, i.e. "natural" or "vivid".</param>
        /// <returns>The generated image object</returns>
        public async Task<OAImageGenerationResponseDto> GenerateImageAsync(OAImageGenerationRequestDto requestDto)
        {
            var apiKey = _config["OpenAI:ApiKey"];
            var requestBody = new
            {
                model = GetDallEModel(requestDto),
                prompt = requestDto.Prompt,
                n = 1,
                size = requestDto.Size,
                style = requestDto.Style,
                response_format = "b64_json"
            };

            var requestJson = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/images/generations");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            var json = await JsonDocument.ParseAsync(stream);
            var base64String = json.RootElement
                .GetProperty("data")[0].GetProperty("b64_json").GetString();


            return new OAImageGenerationResponseDto
            {
                Base64String = base64String ?? string.Empty,
                Size = requestDto.Size,
            };
        }

        public async Task<Image> SaveImageAsync(SaveOAImageRequestDto requestDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var image = new Image
            {
                UserId = requestDto.UserId
            };
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            var fileName = $"gen_{image.Id}.png";
            var imageUrl = await _s3Service.UploadImage(requestDto.Base64String, fileName);

            // 3️⃣ Step 3: Update DB record with URL
            image.Url = imageUrl;
            image.Size = ConvertSizeToInt(requestDto.Size);
            _context.Images.Update(image);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return image;
        }

        public async Task<List<Image>> GetImagesAsync(string userId)
        {
            var images = await _context.Images.Where(i => i.UserId == userId).ToListAsync();
            return images;
        }

        /// <summary>
        /// Some image options are only avalaible for newer dall-e model. Determine which model to use according to the API docs: https://platform.openai.com/docs/api-reference/images/create.
        /// </summary>
        /// <returns>The Dall-e model to use.</returns>
        private string GetDallEModel(OAImageGenerationRequestDto requestDto)
        {
            var DallEModel = OAImageModel.DallE2;

            if (!string.IsNullOrEmpty(requestDto.Style) || requestDto.Quality == OAImageQuality.Hd)
            {
                DallEModel = OAImageModel.DallE3;
            }

            if (DallEModel == OAImageModel.DallE2 && OAImageSize.AllowedModel2Values.Contains(requestDto.Size) || DallEModel == OAImageModel.DallE3 && OAImageSize.AllowedModel3Values.Contains(requestDto.Size))
            {
                return DallEModel;
                
            }
            else
            {
                throw new ArgumentException("The selected size is not supported for DALL-E 3 model.");
            }
        }

        private int ConvertSizeToInt(string size)
        {
            if (size.Equals(OAImageSize.Size256))
            {
                return 1;
            }
            else if (size.Equals(OAImageSize.Size512))
            {
                return 2;
            }
            else if (size.Equals(OAImageSize.Size1024))
            {
                return 3;
            }
            else if (size.Equals(OAImageSize.SizeW1792))
            {
                return 4;
            }
            else if (size.Equals(OAImageSize.SizeH1792))
            {
                return 5;
            }
            else
            {
                throw new ArgumentException("Invalid size");
            }
        }
    }
}
