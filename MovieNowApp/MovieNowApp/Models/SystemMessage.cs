using System;
using System.Collections.Generic;

namespace MovieNowApp.Models
{
    public partial class SystemMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }

        public virtual User User { get; set; }
    }
}
