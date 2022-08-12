using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADFSPlugin
{
    public class PasswordValidationService
    {
        private static bool IsSuccess = true;
        public static readonly PasswordValidationService Instance = new PasswordValidationService();
        private PasswordValidationService() {}

        public bool Validate(string username, string password)
        {
            IsSuccess = !IsSuccess;
            return IsSuccess;
        }
    }
}
