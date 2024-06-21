namespace LeagueApp.Settings
{
    public class FileSettings
    {
        public const string ImagePath = "/Images/Logos";

        public const string AllowedExtensions = ".jpg,.jpeg,.png";

        public const int MaxFileSiezInMB = 1;
        public const int MaxFileSiezInBytes = MaxFileSiezInMB * 1024 * 1024;
    }
}
