namespace PrintingBI.API.Models
{
    /// <summary>
    /// Model provided if the user is authenticated
    /// </summary>
    public class AuthenticateUserOutputDto
    {
        /// <summary>
        /// Initialize all the fields
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isSystemGeneratedPassword"></param>
        /// <param name="token"></param>
        /// <param name="expiresTime"></param>
        /// <param name="refreshToken"></param>
        /// <param name="fullName"></param>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        public AuthenticateUserOutputDto(int userId, bool isSystemGeneratedPassword, string token, int expiresTime, string refreshToken, string fullName, string userName, string email)
        {
            UserId = userId;
            IsSystemGeneratedPassword = isSystemGeneratedPassword;
            Token = token;
            ExpiresTime = expiresTime;
            RefreshToken = refreshToken;
            FullName = fullName;
            UserName = userName;
            Email = email;
        }


        /// <summary>
        /// user id of the authenticated user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// True if the user password is created by system 
        /// Force user to change password if true
        /// </summary>
        public bool IsSystemGeneratedPassword { get; set; }

        /// <summary>
        /// Authentication token. 
        /// Send this token in header of the every request for accessing the apis
        /// ex. Authorization: Bearer XXXac...xyz
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Token expire time
        /// </summary>
        public int ExpiresTime { get; set; }

        /// <summary>
        /// Refresh token - it will helps to validates user with refresh token
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
