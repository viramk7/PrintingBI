namespace PrintingBI.Common
{
    public interface IEmailConfig
    {
        string Email { get; set; }
        string HostName { get; set; }
        string Password { get; set; }
        int PortNumber { get; set; }
    }
}