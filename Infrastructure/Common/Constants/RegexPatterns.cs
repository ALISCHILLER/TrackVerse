namespace Infrastructure.Common.Constants
{
    /// <summary>
    /// کلاس برای نگهداری الگوهای عبارات منظم (Regex) کاربردی.
    /// </summary>
    public static class RegexPatterns
    {
        /// <summary>
        /// الگوی برای تطبیق با ایمیل‌ها.
        /// </summary>
        public const string Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        /// <summary>
        /// الگوی برای تطبیق با شماره تلفن‌های ایرانی (با پیش شماره ۰۹).
        /// </summary>
        public const string PhoneNumber = @"^09\d{9}$";

        /// <summary>
        /// الگوی برای تطبیق با URL‌های معتبر.
        /// </summary>
        public const string Url = @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";

        /// <summary>
        /// الگوی برای تطبیق با شناسه ملی ایرانی (۱۰ رقم).
        /// </summary>
        public const string NationalId = @"^\d{10}$";

        /// <summary>
        /// الگوی برای تطبیق با کد پستی ایران (۱۰ رقم).
        /// </summary>
        public const string PostalCode = @"^\d{10}$";

        /// <summary>
        /// الگوی برای تطبیق با شماره کارت بانکی (۱۶ رقم).
        /// </summary>
        public const string CreditCard = @"^\d{16}$";

        /// <summary>
        /// الگوی برای تطبیق با تاریخ در فرمت YYYY-MM-DD.
        /// </summary>
        public const string Date = @"^\d{4}-\d{2}-\d{2}$";

        /// <summary>
        /// الگوی برای تطبیق با ساعت در فرمت HH:mm (۲۴ ساعته).
        /// </summary>
        public const string Time = @"^([01]?[0-9]|2[0-3]):([0-5]?[0-9])$";

        /// <summary>
        /// الگوی برای تطبیق با شناسه کاربری (کاراکترهای مجاز شامل حروف، اعداد، و زیرخط).
        /// </summary>
        public const string Username = @"^[a-zA-Z0-9_]{3,16}$";

        /// <summary>
        /// الگوی برای تطبیق با پسورد (حداقل یک عدد، یک حرف بزرگ، یک حرف کوچک و یک علامت خاص).
        /// </summary>
        public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

        /// <summary>
        /// الگوی برای تطبیق با آدرس IPv4.
        /// </summary>
        public const string IPv4 = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        /// <summary>
        /// الگوی برای تطبیق با آدرس IPv6.
        /// </summary>
        public const string IPv6 = @"^([0-9a-fA-F]{1,4}:){7}([0-9a-fA-F]{1,4})$";

        /// <summary>
        /// الگوی برای تطبیق با شناسه محصول (SKU).
        /// </summary>
        public const string ProductId = @"^[A-Za-z0-9-]{10,20}$";

        /// <summary>
        /// الگوی برای تطبیق با آدرس ایمیل دانشگاهی.
        /// </summary>
        public const string UniversityEmail = @"^[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9.-]+\.)?edu$";

        /// <summary>
        /// الگوی برای تطبیق با تاریخ تولد (در فرمت YYYY/MM/DD).
        /// </summary>
        public const string BirthDate = @"^(19|20)\d\d/(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])$";

        /// <summary>
        /// الگوی برای تطبیق با کد امنیتی کارت (CVV) (۳ رقم).
        /// </summary>
        public const string Cvv = @"^\d{3}$";
    }
}
