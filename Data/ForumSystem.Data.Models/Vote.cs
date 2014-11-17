namespace ForumSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using ForumSystem.Data.Common.Models;

    public class Vote : IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string VoterId { get; set; }

        public bool IsUpvote { get; set; }

        public virtual ApplicationUser Voter { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}