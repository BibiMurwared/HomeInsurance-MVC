namespace HomeInsurance_MVC.Constants
{
    /// <summary>
    /// Stores validation constants used across models.
    /// </summary>
    public static class ValidationConstants
    {
        /// <summary>
        /// Defines max length for full name.
        /// </summary>
        public const int UserFullNameMaxLength = 100;

        /// <summary>
        /// Defines max length for email fields.
        /// </summary>
        public const int UserEmailMaxLength = 150;

        /// <summary>
        /// Defines max length for phone fields.
        /// </summary>
        public const int PhoneMaxLength = 25;

        /// <summary>
        /// Defines max length for item name.
        /// </summary>
        public const int ItemNameMaxLength = 100;

        /// <summary>
        /// Defines max length for item category.
        /// </summary>
        public const int ItemCategoryMaxLength = 50;

        /// <summary>
        /// Defines max length for item description.
        /// </summary>
        public const int ItemDescriptionMaxLength = 500;

        /// <summary>
        /// Defines max length for room location.
        /// </summary>
        public const int RoomLocationMaxLength = 100;

        /// <summary>
        /// Defines max length for serial number.
        /// </summary>
        public const int SerialNumberMaxLength = 100;

        /// <summary>
        /// Defines max length for image URL.
        /// </summary>
        public const int ImageUrlMaxLength = 500;

        /// <summary>
        /// Defines max password length.
        /// </summary>
        public const int PasswordMaxLength = 100;

        /// <summary>
        /// Defines min password length.
        /// </summary>
        public const int PasswordMinLength = 8;

        /// <summary>
        /// Defines minimum allowed item value.
        /// </summary>
        public const string MinEstimatedValue = "0";

        /// <summary>
        /// Defines maximum allowed item value.
        /// </summary>
        public const string MaxEstimatedValue = "999999999.99";
    }
}
