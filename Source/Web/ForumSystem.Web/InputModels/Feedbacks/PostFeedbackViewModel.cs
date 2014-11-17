namespace ForumSystem.Web.InputModels.Feedbacks
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class PostFeedbackViewModel : IMapFrom<Feedback>
    {
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }
    }
}