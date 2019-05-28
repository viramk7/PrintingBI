using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Data.CustomModel
{
    public class AuthenticateUserResultDto
    {
        /// <summary>
        /// Is User Authenticated ?
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// Is User Super Admin ?
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// Is User First Time Pass Change Done ?
        /// </summary>
        public bool IsPasswordChange { get; set; }
    }
}
