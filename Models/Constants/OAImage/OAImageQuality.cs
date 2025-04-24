namespace ImaGen_BE.Models.Constants.OAImage
{
    public class OAImageQuality
    {
        public const string Standard = "standard";
        public const string Hd = "hd";

        public static readonly HashSet<string> AllowedValues =
        [
            Standard,
            Hd
        ];
    }
}
