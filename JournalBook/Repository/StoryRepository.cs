using JournalBook.Data;
using JournalBook.Interface;
using JournalBook.Models;

namespace JournalBook.Repository
{

    public class StoryRepository:IStoryRepository
    {
        public readonly DataContext _context;

        public StoryRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Story>GetStories()
        {
            return _context.Stories.OrderBy(p=>p.Id).ToList();
        }
    }
}
