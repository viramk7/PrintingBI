namespace PrintingBI.Data.Infrastructure
{
    public interface ICustomerDbContext
    {
        PrintingBIDbContext Context { get; }
    }
}