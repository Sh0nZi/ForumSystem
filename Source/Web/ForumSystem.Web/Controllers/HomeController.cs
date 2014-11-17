namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.ViewModels.Home;
    using Microsoft.AspNet.Identity;

    public class HomeController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;

        private readonly IDeletableEntityRepository<Vote> votes;

        public HomeController(IDeletableEntityRepository<Post> posts, IDeletableEntityRepository<Vote> votes)
        {
            this.posts = posts;
            this.votes = votes;
        }

        public ActionResult Index()
        {
            var model = this.posts.All().Project().To<IndexBlogPostViewModel>();

            return this.View(model);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult UpVote(int id)
        {
            var post = this.posts.All().Project().To<IndexBlogPostViewModel>().FirstOrDefault(p => p.Id == id);

            if (!post.Votes.Any(v => v.VoterId == this.User.Identity.GetUserId()))
            {
                var vote = new Vote()
                {
                    IsUpvote = true,
                    PostId = id,
                    VoterId = this.User.Identity.GetUserId()
                };
                this.votes.Add(vote);
                this.votes.SaveChanges();
            }

            return this.PartialView("_VotePartial", post);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult DownVote(int id)
        {
            var post = this.posts.All().Project().To<IndexBlogPostViewModel>().FirstOrDefault(p => p.Id == id);

            if (!post.Votes.Any(v => v.VoterId == this.User.Identity.GetUserId()))
            {
                var vote = new Vote()
                {
                    IsUpvote = false,
                    PostId = id,
                    VoterId = this.User.Identity.GetUserId()
                };
                this.votes.Add(vote);
                this.votes.SaveChanges();
            }

            this.posts.Dispose();
            return this.PartialView("_VotePartial", post);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult CancelVote(int id)
        {
            var post = this.posts.All().Project().To<IndexBlogPostViewModel>().FirstOrDefault(p => p.Id == id);

            if (post.Votes.Any(v => v.VoterId == this.User.Identity.GetUserId()))
            {
                var vote = post.Votes.FirstOrDefault(v => v.VoterId == this.User.Identity.GetUserId() && v.PostId == id);

                this.votes.Delete(vote);

                this.votes.SaveChanges();
            }

            return this.PartialView("_VotePartial", post);
        }
    }
}