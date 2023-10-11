using JournalBook.Models;
using JournalBook.Data;

namespace JournalBook.Repository
{
    public class OwnerRepository
    {
        public readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(p=>p.Id).ToList();
        }
    }
}
