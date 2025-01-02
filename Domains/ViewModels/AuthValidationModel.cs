using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.ViewModels
{
    public class AuthValidationModel
    {
        public int Status { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string ValidResult { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

    }
}
