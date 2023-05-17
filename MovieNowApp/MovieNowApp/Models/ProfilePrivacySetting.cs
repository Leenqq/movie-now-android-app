using System;
using System.Collections.Generic;


namespace MovieNowApp.Models
{
    public partial class ProfilePrivacySetting
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Private { get; set; }
        public bool FollowersOnly { get; set; }
        public bool Public { get; set; }

        public virtual User User { get; set; }
    }
}
