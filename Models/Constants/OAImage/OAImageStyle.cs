namespace ImaGen_BE.Models.Constants.OAImage
{
    public class OAImageStyle
    {
        public const string Vivid = "vivid";
        public const string Natural = "natural";

        public static readonly HashSet<string> AllowedValues =
        [
            Vivid,
            Natural
        ];
    }
}
