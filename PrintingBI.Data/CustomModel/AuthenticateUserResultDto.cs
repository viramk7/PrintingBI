namespace PrintingBI.Data.CustomModel
{
    public class AuthenticateUserResultDto
    {
        public int UserId { get; set; }

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

        /// <summary>
        /// Refresh Token for User
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Full Name for authenticated user
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// User Name for authenticated user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email for authenticated user
        /// </summary>
        public string Email { get; set; }
    }
}
