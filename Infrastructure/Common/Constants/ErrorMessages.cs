namespace Infrastructure.Common.Constants
{
    /// <summary>
    /// کلاس برای نگهداری پیام‌های خطای استاندارد شده در پروژه.
    /// </summary>
    public static class ErrorMessages
    {
        /// <summary>
        /// پیام خطا برای زمانی که یک ورودی نادرست ارسال می‌شود.
        /// </summary>
        public const string InvalidInput = "ورودی نامعتبر است. لطفاً دوباره تلاش کنید.";

        /// <summary>
        /// پیام خطا برای زمانی که کاربر مجوز دسترسی به یک عملیات را ندارد.
        /// </summary>
        public const string Unauthorized = "شما دسترسی لازم برای انجام این عملیات را ندارید.";

        /// <summary>
        /// پیام خطا برای زمانی که یک کاربر یافت نمی‌شود.
        /// </summary>
        public const string UserNotFound = "کاربر مورد نظر یافت نشد.";

        /// <summary>
        /// پیام خطا برای زمانی که رمز عبور نادرست وارد می‌شود.
        /// </summary>
        public const string InvalidPassword = "رمز عبور وارد شده صحیح نیست.";

        /// <summary>
        /// پیام خطا برای زمانی که عملیات به دلیل نقض قوانین مسدود شده است.
        /// </summary>
        public const string Forbidden = "دسترسی شما به این منبع مسدود شده است.";

        /// <summary>
        /// پیام خطا برای زمانی که یک منبع درخواست شده یافت نمی‌شود.
        /// </summary>
        public const string NotFound = "منبع مورد نظر یافت نشد.";

        /// <summary>
        /// پیام خطا برای زمانی که یک درخواست تایید نشده است.
        /// </summary>
        public const string UnverifiedAccount = "حساب کاربری شما تایید نشده است.";

        /// <summary>
        /// پیام خطا برای زمانی که درخواست زمان‌بر بوده و درخواست تایم‌اوت شده است.
        /// </summary>
        public const string RequestTimeout = "درخواست شما به دلیل زمان‌بر بودن، به پایان رسید.";

        /// <summary>
        /// پیام خطا برای زمانی که یک اشتباه داخلی سرور رخ می‌دهد.
        /// </summary>
        public const string InternalServerError = "خطای داخلی در سرور رخ داده است. لطفاً بعداً تلاش کنید.";

        /// <summary>
        /// پیام خطا برای زمانی که داده‌های ارسالی معتبر نیستند.
        /// </summary>
        public const string InvalidData = "داده‌های ارسال شده معتبر نیستند.";

        /// <summary>
        /// پیام خطا برای زمانی که یک منبع از پیش موجود است.
        /// </summary>
        public const string ResourceAlreadyExists = "منبع با این مشخصات از پیش وجود دارد.";

        /// <summary>
        /// پیام خطا برای زمانی که درخواست شامل پارامترهای اجباری نباشد.
        /// </summary>
        public const string MissingRequiredParameters = "پارامترهای ضروری در درخواست شما وجود ندارد.";

        /// <summary>
        /// پیام خطا برای زمانی که توکن یا مجوز نامعتبر است.
        /// </summary>
        public const string InvalidToken = "توکن شما نامعتبر است. لطفاً دوباره وارد شوید.";

        /// <summary>
        /// پیام خطا برای زمانی که مقدار ورودی خیلی طولانی است.
        /// </summary>
        public const string InputTooLong = "ورودی شما خیلی طولانی است.";

        /// <summary>
        /// پیام خطا برای زمانی که عملیات قطع شده است.
        /// </summary>
        public const string OperationCancelled = "عملیات شما لغو شد.";

        /// <summary>
        /// پیام خطا برای زمانی که در اعتبارسنجی فرمت داده‌ها مشکلی وجود دارد.
        /// </summary>
        public const string InvalidFormat = "فرمت داده‌های وارد شده نادرست است.";

        /// <summary>
        /// پیام خطا برای زمانی که به دلیل قوانین امنیتی عملیات رد شده است.
        /// </summary>
        public const string SecurityViolation = "عملیات شما به دلیل نقض قوانین امنیتی رد شد.";

        /// <summary>
        /// پیام خطا برای زمانی که برنامه از حالت نامناسبی برخوردار است.
        /// </summary>
        public const string InvalidState = "وضعیت فعلی برنامه نامناسب است.";

        /// <summary>
        /// پیام خطا برای زمانی که نیاز به ثبت‌نام کاربر است.
        /// </summary>
        public const string RegistrationRequired = "برای انجام این عملیات نیاز به ثبت‌نام دارید.";

        /// <summary>
        /// پیام خطا برای زمانی که کاربر قبلاً ثبت‌نام کرده است.
        /// </summary>
        public const string UserAlreadyRegistered = "این کاربر قبلاً ثبت‌نام کرده است.";

        /// <summary>
        /// پیام خطا برای زمانی که در فرآیند پرداخت یا تراکنش مشکلی پیش آمده است.
        /// </summary>
        public const string PaymentError = "خطا در پردازش پرداخت. لطفاً دوباره تلاش کنید.";

        /// <summary>
        /// پیام خطا برای زمانی که در ذخیره‌سازی داده‌ها مشکلی پیش آمده است.
        /// </summary>
        public const string DataStorageError = "خطا در ذخیره‌سازی داده‌ها. لطفاً بعداً تلاش کنید.";

        /// <summary>
        /// پیام خطا برای زمانی که در پردازش درخواست مشکلی پیش آمده است.
        /// </summary>
        public const string RequestProcessingError = "خطا در پردازش درخواست. لطفاً دوباره تلاش کنید.";
    }
}
