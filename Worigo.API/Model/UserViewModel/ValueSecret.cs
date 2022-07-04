using System;

namespace Worigo.API.Model.UserViewModel
{
    public class ValueSecret
    {
        public static int integer(object val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            { return 0; }
        }
    }
}
