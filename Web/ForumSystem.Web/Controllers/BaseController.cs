namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;
    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure;

    public class BaseController : Controller
    {
        public BaseController(IDeletableEntityRepository<Feedback> feedbacks, ISanitizer sanitizer)
        {
            this.Feedbacks = feedbacks;
            this.Sanitizer = sanitizer;
        }

        protected IDeletableEntityRepository<Feedback> Feedbacks { get; set; }

        protected ISanitizer Sanitizer { get; set; }
    }
}