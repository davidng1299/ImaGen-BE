namespace ImaGen_BE.Models.Constants.OAImage
{
    public static class OAImageSize
    {
        public const string Size256 = "256x256";
        public const string Size512 = "512x512";
        public const string Size1024 = "1024x1024";
        public const string SizeW1792 = "1792x1024";
        public const string SizeH1792 = "1024x1792";

        public static readonly HashSet<string> AllowedModel2Values =
        [
            Size256,
            Size512,
            Size1024
        ];

        public static readonly HashSet<string> AllowedModel3Values =
        [
            Size1024,
            SizeW1792,
            SizeH1792
        ];


        public static readonly HashSet<string> AllowedValues = AllowedModel2Values.Concat(AllowedModel3Values).ToHashSet();
    }
}
