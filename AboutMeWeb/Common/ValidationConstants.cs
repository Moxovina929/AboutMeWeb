namespace AboutMeWeb.Common
{
    public static class ValidationConstants
    {
        // Service
        public const int ServiceTitleMinLength = 3;
        public const int ServiceTitleMaxLength = 80;
        public const int ServiceDescriptionMinLength = 10;
        public const int ServiceDescriptionMaxLength = 1000;
        public const int ServicePriceMinValue = 0;
        public const int ServicePriceMaxValue = 10000;

        // Review
        public const int ReviewRatingMinValue = 1;
        public const int ReviewRatingMaxValue = 5;
        public const int ReviewCommentMinLength = 5;
        public const int ReviewCommentMaxLength = 500;

        // User
        public const int UserNameMinLength = 3;
        public const int UserNameMaxLength = 20;
    }
}
