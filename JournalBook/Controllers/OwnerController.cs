using AutoMapper;
using JournalBook.Dto;
using JournalBook.Interface;
using JournalBook.Models;
using Microsoft.AspNetCore.Mvc;
using JournalBook.Repository;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController:Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IStoryRepository _storyRepository;

        private readonly IMapper _mapper;
        public OwnerController(IOwnerRepository ownerrepository,IStoryRepository storyRepository, IMapper mapper)
        {
            this._ownerRepository = ownerrepository;
            this._storyRepository = storyRepository;
            this._mapper = mapper;

        }
        [HttpGet("owners")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            var stories = _ownerRepository.GetOwners();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(stories);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(OwnerDto))]
        [ProducesResponseType(404)]
        public IActionResult GetOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //return Ok(story);

            return Ok(owner);
        }

        [HttpGet("ownerByEmail/{email}")]
        [ProducesResponseType(200, Type = typeof(OwnerDto))]
        [ProducesResponseType(404)]
        public IActionResult GetOwnerByEmail(string email)
        {
            if (!_ownerRepository.OwnerExistsByEmail(email))
                return NotFound();

            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwnerByEmail(email));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //return Ok(story);

            return Ok(owner);
        }

        [HttpGet("story/{storyId}")]
        [ProducesResponseType(200, Type = typeof(OwnerDto))]
        [ProducesResponseType(404)]
        public IActionResult GetOwnerOfStory(int storyId)
        {
            if (!_storyRepository.StoryExists(storyId))
                return NotFound();

            //var story = _storyRepository.GetStory(storyId);
            
            
            var ownerDto = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwnerOfStory(storyId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //return Ok(story);

            return Ok(ownerDto);
        }

        [HttpGet("storiesByOwner/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(StoryDto))]
        [ProducesResponseType(404)]
        public IActionResult GetStoryByOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            //var story = _storyRepository.GetStory(storyId);
            
            var storyDto = _mapper.Map<List<StoryDto>>(_ownerRepository.GetStoryByOwner(ownerId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //return Ok(story);

            return Ok(storyDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);

            var owners = _ownerRepository.GetOwners()
                .Where(c => c.email.Trim().ToUpper() == ownerCreate.email.Trim().ToUpper())
                .FirstOrDefault();

            if (owners != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = _mapper.Map<Owner>(ownerCreate);


            if (!_ownerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpDelete("{ownerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(Owner owner)
        {
            if (!_ownerRepository.OwnerExists(owner.Id))
                return NotFound();

            if (_ownerRepository.DeleteOwner(owner))
                return NoContent(); // Owner successfully deleted.

            ModelState.AddModelError("", "Something went wrong while deleting.");
            return StatusCode(500, ModelState);
        }

        [HttpPut("{ownerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerDto ownerUpdate)
        {
            if (ownerUpdate == null)
                return BadRequest(ModelState);

            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            var owner = _mapper.Map<Owner>(ownerUpdate);
            owner.Id = ownerId; // Set the owner's ID based on the route parameter.

            if (_ownerRepository.UpdateOwner(owner))
                return NoContent(); // Owner successfully updated.

            ModelState.AddModelError("", "Something went wrong while updating.");
            return StatusCode(500, ModelState);
        }

        

    }
}
