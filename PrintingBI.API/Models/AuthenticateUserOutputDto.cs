namespace PrintingBI.API.Models
{
    /// <summary>
    /// Model provided if the user is authenticated
    /// </summary>
    public class AuthenticateUserOutputDto
    {
        public AuthenticateUserOutputDto(bool isSystemGeneratedPassword, string token, int expiresTime)
        {
            IsSystemGeneratedPassword = isSystemGeneratedPassword;
            Token = token;
            ExpiresTime = expiresTime;
        }

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
    }
}
