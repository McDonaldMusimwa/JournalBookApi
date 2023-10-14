using JournalBook.Models;
using JournalBook.Data;
using JournalBook.Interface;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace JournalBook.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        public readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(o=>o.Id).ToList();
        }

       public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(o=>o.Id == ownerId).FirstOrDefault();
        }

        public Owner GetOwnerByEmail(string email)
        {
            return _context.Owners.Where(o => o.email == email).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfStory(int storyId)
        {
            var owners = _context.Stories
         .Where(s => s.Id == storyId)
         .Select(s => s.Owner)
         .ToList();

            return owners;
        }
        public ICollection<Story>GetStoryByOwner(int ownerId)
        {
            return _context.Stories.Where(s=>s.OwnerId==ownerId).ToList();
        }

        public bool OwnerExistsByEmail(string email)
        {
            return _context.Owners.Any(o => o.email == email);
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }

        public bool CreateOwner(Owner owner)
        {
            //change tracker "Add,update,modify"
            _context.Add(owner);
            return Save();
        }

        public bool Save() { 
        var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


        public bool DeleteOwner(Owner owner)
        {
            var ownerToDelete = _context.Owners.SingleOrDefault(o => o.Id == owner.Id);

            if (ownerToDelete == null)
                return false; // Owner with the given ID doesn't exist.

            _context.Owners.Remove(ownerToDelete);
            _context.SaveChanges(); // Save changes to the database.

            return true; // Owner successfully deleted.
        }

        public bool UpdateOwner(Owner owner)
        {
            var ownerToUpdate = _context.Owners.SingleOrDefault(o => o.Id == owner.Id);

            if (ownerToUpdate == null)
                return false; // Owner with the given ID doesn't exist.

            // Update owner properties with values from the updatedOwner object.
            ownerToUpdate.FirstName = owner.FirstName;
            ownerToUpdate.LastName = owner.LastName;
            ownerToUpdate.email = owner.email;

            _context.SaveChanges(); // Save changes to the database.

            return true; // Owner successfully updated.
        }
    }
}
