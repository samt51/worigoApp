using System;
using System.Text;

namespace Worigo.Entity.Encryption
{
    public class EntityCommonMethods
    {
        public static string Key = "adef@@kfxbv@6+4123213asdasdsa";
        public static string ConvertDecrypt(string passwordBytes)
        {
            if (string.IsNullOrEmpty(passwordBytes)) return "";
            var base64String = Convert.FromBase64String(passwordBytes);
            var result = Encoding.UTF8.GetString(base64String);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
    }
}
