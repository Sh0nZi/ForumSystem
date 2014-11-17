namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure;
    using ForumSystem.Web.InputModels.Feedbacks;
    using Microsoft.AspNet.Identity;

    [ValidateInput(false)]
    public class FeedbackController : BaseController
    {
        public FeedbackController(IDeletableEntityRepository<Feedback> feedbacks, ISanitizer sanitizer) : base(feedbacks, sanitizer)
        {
        }

        // GET: Feedback
        public ActionResult Create()
        {
            var model = new PostFeedbackViewModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostFeedbackViewModel input)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();

                var dbmodel = new Feedback()
                {
                    Title = input.Title,
                    Content = this.Sanitizer.Sanitize(input.Content),
                    AuthorId = userId
                };

                this.Feedbacks.Add(dbmodel);
                this.Feedbacks.SaveChanges();
                this.TempData["Success"] = "Feedback successfully send!";
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(input);
        }
    }
}