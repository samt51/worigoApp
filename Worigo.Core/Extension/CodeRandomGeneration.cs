using System;

namespace Worigo.Core.Extension
{
    public class CodeRandomGeneration
    {
        public static int RandomVertificationCodeCreate(int sayi)
        {
            var random = new Random();
            var result = "";
            for (int i = 0; i < sayi; i++)
            {
                result += random.Next(0, 10);
            }
            return Convert.ToInt32(result);
        }
    }
}
