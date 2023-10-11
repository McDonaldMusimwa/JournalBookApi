using JournalBook.Models;

namespace JournalBook.Interface
{
    public interface IStoryRepository
    {
        ICollection<Story> GetStories();
    }
}
