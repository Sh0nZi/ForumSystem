namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Areas.Administration.Models;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    
    public class PostsAdministrationController : AdminController
    {
        public PostsAdministrationController(IDeletableEntityRepository<Post> posts) : base(posts)
        {
        }

        // GET: Administration/PostsAdministration
        public ActionResult Index()
        {
            return this.View();
        }
        
        public ActionResult Read([DataSourceRequest]
                                 DataSourceRequest request)
        {
            var posts =
                this.Posts.All().Project().To<PostAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(posts, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest]
                                   DataSourceRequest request, PostAdminViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbmodel = this.GetById(model.Id);

                this.Posts.Delete(dbmodel);
                this.Posts.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]
                                   DataSourceRequest request, PostAdminViewModel model) 
        {
            var dbmodel = this.GetById(model.Id);
            dbmodel.AuthorId = this.User.Identity.GetUserId();
            this.Posts.Add(dbmodel);
            this.Posts.SaveChanges();
            if (dbmodel != null)
            {
                model.Id = dbmodel.Id;
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]
                                   DataSourceRequest request, PostAdminViewModel model)
        {
            var dbmodel = this.GetById(model.Id);
            dbmodel.Title = model.Title;
            dbmodel.Content = model.Content;
            this.Posts.Update(dbmodel);
            this.Posts.SaveChanges();
            return this.GridOperation(model, request);
        }

        private Post GetById(int id)
        {
            return this.Posts.GetById(id);
        }
        
        private JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
        {
            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }
    }
}