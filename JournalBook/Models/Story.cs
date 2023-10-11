namespace JournalBook.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
            
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}

        // Add a foreign key property to reference the owner
        public int OwnerId { get; set; }

        // Navigation property to represent the owner
        public Owner Owner { get; set; }

    }
}
