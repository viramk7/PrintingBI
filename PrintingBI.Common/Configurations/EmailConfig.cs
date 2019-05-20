
namespace PrintingBI.Common
{
    public class EmailConfig : IEmailConfig
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public int PortNumber { get; set; }
    }
}
