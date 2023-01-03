using System;

namespace Worigo.Core.Exceptions
{
    public class AuthorizationException:Exception
    {
        public AuthorizationException(string message):base(message) 
        {

        }
    }
}
