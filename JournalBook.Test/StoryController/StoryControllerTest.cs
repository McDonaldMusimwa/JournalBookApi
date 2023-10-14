using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using JournalBook.Controllers;
using JournalBook.Dto;
using JournalBook.Interface;
using JournalBook.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JournalBook.Tests
{
    public class StoryControllerTests
    {
        [Fact]
        public void GetStories_ReturnsOkResultWithStoryDtoList()
        {
            // Arrange
            var mockStoryRepository = new Mock<IStoryRepository>();
            var mockMapper = new Mock<IMapper>();
            var controller = new StoryController(mockStoryRepository.Object, mockMapper.Object);

            var stories = new List<Story>
            {
                new Story { Id = 1, Title = "Story 1", Text = "Text 1" },
                new Story { Id = 2, Title = "Story 2", Text = "Text 2" },
            };

            mockStoryRepository.Setup(repo => repo.GetStories()).Returns(stories);

            var storyDtos = stories.Select(s => new StoryDto { Id = s.Id, Title = s.Title, Text = s.Text }).ToList();
            mockMapper.Setup(mapper => mapper.Map<List<StoryDto>>(stories)).Returns(storyDtos);

            // Act
            var result = controller.GetStories() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var returnedStoryDtos = result.Value as List<StoryDto>;
            Assert.NotNull(returnedStoryDtos);
            Assert.Equal(storyDtos, returnedStoryDtos);
        }

        // Add more test methods for other controller actions.
    }
}
