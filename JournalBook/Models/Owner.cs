namespace JournalBook.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }    
        public ICollection <Story> Stories { get; set; }
        
    }
}
