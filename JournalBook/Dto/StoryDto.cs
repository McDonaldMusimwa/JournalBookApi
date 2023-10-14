using JournalBook.Models;

namespace JournalBook.Dto
{
    public class StoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int OwnerId { get; set; }

        // Navigation property to represent the owner
        //public Owner Owner { get; set; }
    }
}
