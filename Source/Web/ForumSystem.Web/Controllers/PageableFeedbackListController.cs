namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure;
    using ForumSystem.Web.ViewModels.Feedbacks;
    using PagedList;

    [Authorize]
    public class PageableFeedbackListController : BaseController
    {
        private const int PageSize = 4;

        public PageableFeedbackListController(IDeletableEntityRepository<Feedback> feedbacks, ISanitizer sanitizer) : base(feedbacks, sanitizer)
        {
        }

         // GET: PageableFeedbackList
         ////[OutputCache(Duration=0)]
        [OutputCache(Duration = 15 * 60)]
        public ActionResult Display(int? page)
        {
            var feedback = Feedbacks.All()
                                    .Project()
                                    .To<FeedbackDisplayViewModel>()
                                    .OrderBy(f => f.CreatedOn);
            return this.View(feedback.ToPagedList(page ?? 1, PageSize));
        }
    }
}