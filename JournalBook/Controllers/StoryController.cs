using Microsoft.AspNetCore.Mvc;
using JournalBook.Interface;
using JournalBook.Models;
using AutoMapper;
using JournalBook.Dto;
using JournalBook.Repository;

namespace JournalBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController:Controller
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IMapper _mapper;
        public StoryController(IStoryRepository storyrepository,IMapper mapper)
        {
            this._storyRepository = storyrepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Story>))]
        public IActionResult GetStories() 
        {
            var stories = _mapper.Map<List<StoryDto>>(_storyRepository.GetStories());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(stories);
        }


        [HttpGet("{storyId}")]
        [ProducesResponseType(200, Type = typeof(Story))]
        [ProducesResponseType(400)]
        public IActionResult GetStory(int storyId)
        {
            if (!_storyRepository.StoryExists(storyId))
                return NotFound();

            var story = _mapper.Map<StoryDto>(_storyRepository.GetStory(storyId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(story);

        }

        [HttpGet("title/{storytitle}")]
        [ProducesResponseType(200, Type = typeof(Story))]
        [ProducesResponseType(400)]
        public IActionResult GetStoryByTitle(string storytitle)
        {
            if (!_storyRepository.StoryExistsByTitle(storytitle))
                return NotFound();

            var story = _mapper.Map<StoryDto>(_storyRepository.GetStory(storytitle));
            return Ok(story);
        }

        [HttpPost("create-story")]
        [ProducesResponseType(201, Type = typeof(StoryDto))]
        [ProducesResponseType(400)]
        public IActionResult CreateStory([FromBody] StoryDto storyCreate)
        {
            if (storyCreate == null)
                return BadRequest(ModelState);

            // You can add additional validation logic for the incoming storyCreate DTO here.

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var story = _mapper.Map<Story>(storyCreate);

            if (!_storyRepository.CreateStory(story))
            {
                ModelState.AddModelError("", "Something went wrong while saving the story");
                return StatusCode(500, ModelState);
            }

            // After successfully creating the story, you can fetch the newly created story and return it.
            var createdStory = _storyRepository.GetStory(story.Id); // Modify this method according to your repository.
            var storyDto = _mapper.Map<StoryDto>(createdStory);

            return CreatedAtAction("GetStory", new { storyId = storyDto.Id }, storyDto);
        }

        [HttpPut("update-story/{storyId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ModifyStory(int storyId, [FromBody] StoryDto storyUpdate)
        {
            if (storyUpdate == null)
                return BadRequest(ModelState);

            // You can add additional validation logic for the incoming storyUpdate DTO here.

            if (!_storyRepository.StoryExists(storyId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingStory = _storyRepository.GetStory(storyId);
            if (existingStory == null)
                return NotFound();

            // Update the properties of the existingStory with the values from storyUpdate
            existingStory.Title = storyUpdate.Title;
            existingStory.Text = storyUpdate.Text;
            // Update any other properties you want to modify.

            if (!_storyRepository.ModifyStory(existingStory))
            {
                ModelState.AddModelError("", "Something went wrong while updating the story");
                return StatusCode(500, ModelState);
            }

            return NoContent(); // 204 No Content response.
        }

        [HttpDelete("{storyId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(Story story)
        {
            if (!_storyRepository.StoryExists(story.Id))
                return NotFound();

            if (_storyRepository.DeleteStory(story))
                return NoContent(); // Owner successfully deleted.

            ModelState.AddModelError("", "Something went wrong while deleting.");
            return StatusCode(500, ModelState);
        }

    }



}
