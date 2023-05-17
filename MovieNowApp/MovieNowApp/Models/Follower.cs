using System;
using System.Collections.Generic;

namespace MovieNowApp.Models
{
    public partial class Follower
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowerId { get; set; }

        public virtual User User { get; set; }
    }
}
