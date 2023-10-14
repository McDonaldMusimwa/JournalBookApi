using JournalBook.Data;
using JournalBook.Interface;
using JournalBook.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
        public Story GetStory(int id)
        {
            return _context.Stories.Where(p => p.Id == id).FirstOrDefault();
        }

        public Story GetStory(string title)
        {
            return _context.Stories.Where(p => p.Title == title).FirstOrDefault(); 

        }

        public bool StoryExists(int storyId)
        { return _context.Stories.Any(p => p.Id == storyId);}

        public bool StoryExistsByTitle(string title)
        {
            return _context.Stories.Any(p => p.Title == title);
        }

        public bool CreateStory(Story story)
        {
            //change tracker "Add,update,modify"
            _context.Add(story);
            return Save();
        }

        public bool Save()
        {
            var saved   = _context.SaveChanges();
            return saved  > 0 ? true : false;
        }

        public bool ModifyStory(Story story)
        {
            var storyToUpdate = _context.Stories.SingleOrDefault(o => o.Id == story.Id);

            if (storyToUpdate == null)
                return false; // Owner with the given ID doesn't exist.

            // Update owner properties with values from the updatedOwner object.
            storyToUpdate.Title = story.Title;
            storyToUpdate.Text = story.Text;
            storyToUpdate.UpdatedDate = story.UpdatedDate;

            _context.SaveChanges(); // Save changes to the database.

            return true; // Owner successfully updated.
        }

        public bool DeleteStory(Story story)
        {
            var storyToDelete = _context.Stories.SingleOrDefault(s => s.Id == story.Id);

            if (storyToDelete == null)
                return false; // Owner with the given ID doesn't exist.

            _context.Stories.Remove(storyToDelete);
            _context.SaveChanges(); // Save changes to the database.

            return true; // Owner successfully deleted.
        }
    }
}
