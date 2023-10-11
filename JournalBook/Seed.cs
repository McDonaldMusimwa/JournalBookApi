using Microsoft.EntityFrameworkCore;
using JournalBook.Data;
using JournalBook.Models;

namespace JournalBook.Data
{
    public  class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            // Check if there is already data in the database
            
            if(!dataContext.Owners.Any() ||!dataContext.Stories.Any())
            {
                // Seed Owners
                var owners = new List<Owner>
            {
                new Owner { FirstName = "John", LastName = "Doe", email = "john@example.com",password = "password" },
                new Owner { FirstName = "Jane", LastName = "Smith", email = "jane@example.com", password = "password" },
                // Add more owners here
            };
                dataContext.Owners.AddRange(owners);
                dataContext.SaveChanges();

                // Seed Stories
                var stories = new List<Story>
            {
                new Story { Title = "First Story", Text = "This is the text of the first story.", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, OwnerId = 1 },
                new Story { Title = "Second Story", Text = "This is the text of the second story.", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, OwnerId = 2 },
                // Add more stories here
            };
                dataContext.Stories.AddRange(stories);
                dataContext.SaveChanges();
            }
            
            
        }
    }
}
