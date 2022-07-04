using System;

namespace Worigo.API.Model
{
    public class RandomGeneration
    {
        public static int RandomVertificationCodeCreate(int sayi)
        {
            var random = new Random();
            var result = 0;
            for (int i = 0; i < sayi; i++)
            {
                result += random.Next(0, 10);
            }
            return result;
        }
    }
}
