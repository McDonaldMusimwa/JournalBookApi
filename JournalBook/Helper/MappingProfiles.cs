using AutoMapper;
using JournalBook.Models;
using JournalBook.Dto;

namespace JournalBook.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Story, StoryDto>();
             CreateMap<StoryDto, Story>();
            CreateMap<Owner, OwnerDto>();
            // Mapping from OwnerDto to Owner
            CreateMap<OwnerDto, Owner>();
             
          
        }
    }
}
