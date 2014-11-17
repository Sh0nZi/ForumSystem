namespace ForumSystem.Web.ViewModels.Feedbacks
{
    using System;
    using AutoMapper;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;
   
    public class FeedbackDisplayViewModel : IMapFrom<Feedback>, IHaveCustomMappings
    {        
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Feedback, FeedbackDisplayViewModel>()
                         .ForMember(m => m.AuthorName, opt => opt.MapFrom(u => u.Author.UserName));
        }
    }
}