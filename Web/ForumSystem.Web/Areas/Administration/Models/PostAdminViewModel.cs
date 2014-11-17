namespace ForumSystem.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class PostAdminViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)] 
        public int Id { get; set; }

        public string Title { get; set; }

        [HiddenInput(DisplayValue = false)] 
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss}")]
        public DateTime DateCreated { get; set; }

        public string Content { get; set; }

        [HiddenInput(DisplayValue = false)] 
        public bool IsDeleted { get; set; }

        [HiddenInput(DisplayValue = false)] 
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss}")]
        public DateTime CreatedOn { get; set; }

        [HiddenInput(DisplayValue = false)] 
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss}")]
        public DateTime? ModifiedOn { get; set; }

        [HiddenInput(DisplayValue = false)] 
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss}")]
        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Post, PostAdminViewModel>()
                         .ForMember(m => m.AuthorName, opt => opt.MapFrom(u => u.Author.Email));
        }
    }
}