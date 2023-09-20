namespace Domain.Extensions
{
    public sealed class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(Guid id) : base($"The customer id {id} was not found.")
        {            
        }
    }
}
