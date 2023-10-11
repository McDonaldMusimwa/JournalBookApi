using Microsoft.AspNetCore.Mvc;
using JournalBook.Interface;
using JournalBook.Models;

namespace JournalBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController:Controller
    {
        private readonly IStoryRepository _storyRepository;
        public StoryController(IStoryRepository storyrepository)
        {
            this._storyRepository = storyrepository;
            
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Story>))]
        public IActionResult GetStories() 
        {
            var stories = _storyRepository.GetStories();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(stories);
        }
    }
}
