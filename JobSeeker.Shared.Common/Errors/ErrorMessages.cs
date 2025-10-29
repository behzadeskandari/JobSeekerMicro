using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Common.Errors
{

    public static partial class ErrorMessages
    {
        public static string DuplicateEmail = "Duplicate Email Error";
        public static string UserNotFound = "User Not Found";
        public static string InvalidPassword = "Invalid Password";
        public static string InvalidUser = "Invalid User";
        public static string ErrorInLogout = "Error In LogOut";

        public static string? NotFound = "Not Found";

        public static string? IdMissMatch = "ID in the URL does not match the ID in the request body";

        public static string? BadRequest = "Url or Body is not valid, please check your request";
    }

    public static partial class ErrorMessages
    {
        public static string DuplicateEmailFA = "ایمیل قبلا ثبت شده است";
        public static string UserNotFoundFA = "کاربر پیدا نشد";
        public static string InvalidPasswordFA = "پسورد اشتباه میباشد";
        public static string InvalidUserFA = "کاربر اشتباه میباشد";
        public static string? NotFoundFA = "مورد پیدا نشد";
        public static string? IdMissMatchFA = "ایدی ها یکی نمیباشد";
        public static string? BadRequestFA = "بدنه ارسالی صحیح نمیباشد";
    }

    public static partial class SuccessMessages
    {
        public static string AccountCreated = "Account Created Successfuly";
        public static string LoginSuccess = "Login Sucessfuly";
        public static string ErrorInGoogleSignIn = "Login Error In GoogleSign In";

        public static string DeletedRecord  = "Deleted Record";
        public static string LogOutSucess = "LogOut Sucessfuly";
    }

    public static partial class SuccessMessages
    {
        public static string AccountCreatedFA = "حساب با موفقیت ایجاد شد";
        public static string LoginSuccessFA = "ورود با موفقیت";
        public static string ErrorInGoogleSignInFA = "خطای ورود در Google ";
        public static string DeletedRecordFA = "رکورد با موفقیت حذف گردید";

        public static string LogOutSucessFA = "با موفقیت خارج شدید";
        public static string ErrorInLogoutFA = "خطا در خروج";
    }
}
