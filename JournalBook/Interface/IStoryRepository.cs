using JournalBook.Models;

namespace JournalBook.Interface
{
    public interface IStoryRepository
    {
        ICollection<Story> GetStories();
        Story GetStory(int id);
        Story GetStory(string id);
        bool StoryExists(int storyId);

        bool StoryExistsByTitle(string storytitle);

        bool CreateStory(Story story);
        bool Save();

        bool ModifyStory(Story story);

        bool DeleteStory(Story story);
    }
}
