using JournalBook.Models;
namespace JournalBook.Interface
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int id);
        Owner GetOwnerByEmail(string email);
        ICollection<Owner> GetOwnerOfStory(int storyId);
        ICollection <Story> GetStoryByOwner(int ownerId);
        bool OwnerExistsByEmail(string email);
        bool OwnerExists(int ownerId);

        bool CreateOwner(Owner owner);
        bool Save();

        bool DeleteOwner(Owner owner);

        bool UpdateOwner(Owner owner);
        //bool ModifyOwner(Owner owner);
    }
}
