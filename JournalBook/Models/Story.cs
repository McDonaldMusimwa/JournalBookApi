namespace JournalBook.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
            
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}


        
    }
}
