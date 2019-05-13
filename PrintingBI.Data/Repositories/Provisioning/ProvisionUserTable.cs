namespace PrintingBI.Data.Repositories.Provisioning
{
    public class ProvisionUserTable : IProvisionTable
    {
        public string ErrorMessage => "Could not create User table";

        public bool Create()
        {
            // Create the User Table here
            return false;
        }
    }
}
