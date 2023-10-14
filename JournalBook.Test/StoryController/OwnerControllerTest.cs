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
    public class OwnerControllerTests
    {
        [Fact]
        public void GetOwners_ReturnsOkResultWithOwnerList()
        {
            // Arrange
            var mockOwnerRepository = new Mock<IOwnerRepository>();
            var mockStoryRepository = new Mock<IStoryRepository>();
            var mockMapper = new Mock<IMapper>();
            var controller = new OwnerController(mockOwnerRepository.Object, mockStoryRepository.Object, mockMapper.Object);

            var owners = new List<Owner>
            {
                new Owner { Id = 1, FirstName = "John", LastName = "Doe", email = "john@example.com" ,password="Password1"},
                new Owner { Id = 2, FirstName = "Jane", LastName = "Smith", email = "jane@example.com",password="Password1" },
            };

            mockOwnerRepository.Setup(repo => repo.GetOwners()).Returns(owners);

            var ownerDtos = owners.Select(o => new OwnerDto
            {
                //Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                email = o.email
            }).ToList();
            mockMapper.Setup(mapper => mapper.Map<List<OwnerDto>>(owners)).Returns(ownerDtos);

            // Act
            var result = controller.GetOwners() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var returnedOwnerDtos = result.Value as List<OwnerDto>;
            Assert.NotNull(returnedOwnerDtos);
            Assert.Equal(ownerDtos, returnedOwnerDtos);
        }

        // Add more test methods for other controller actions.
    }
}
