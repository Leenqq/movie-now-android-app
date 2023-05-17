using System;
using System.Collections.Generic;

namespace MovieNowApp.Models
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int? RatingNumber { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}
