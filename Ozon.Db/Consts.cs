using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Ozon.Db
{
    public static class Consts
    {
        public const string AdminRoleName = "Admin";
        public static string UserRoleName { get; } = "User";
        public static string AdminEmail { get; } = "admin@ozon-team.ru";
        public static string Password { get; } = "111QQQqqq+++";
        public static string ProductImageFolder { get; } = "products";
        public static string UserImageFolder { get; } = "user_images";
        public static string? GetAttributeName(Enum value)
        {
            return value.GetType()
                .GetMember(value.ToString())?
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();
        }
    }
}
