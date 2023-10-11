using JournalBook.Models;
namespace JournalBook.Interface
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
    }
}
