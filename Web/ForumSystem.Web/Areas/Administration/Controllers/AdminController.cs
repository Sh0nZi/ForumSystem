namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public AdminController(IDeletableEntityRepository<Post> posts)
        {
            this.Posts = posts;
        }

        protected IDeletableEntityRepository<Post> Posts { get; set; }
    }
}