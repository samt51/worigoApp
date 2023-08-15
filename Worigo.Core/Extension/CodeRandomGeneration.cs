using System;

namespace Worigo.Core.Extension
{
    public class CodeRandomGeneration
    {
        public static string RandomVertificationCodeCreate()
        {
            var random = new Random();
            var result = "";
            for (int i = 0; i < 6; i++)
            {
                result += random.Next(0, 10);
            }
            return result;
        }
    }
}
