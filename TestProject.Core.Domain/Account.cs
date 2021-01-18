using TestProject.Storage.Core;

namespace TestProject.Core.Domain
{
    public class Account : IEntity<long>
    {
        public long Id { get; set; }
        public string Iban { get; set; }
        public int UserId { get; set; }
    }
}