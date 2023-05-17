using System;
using System.Collections.Generic;

namespace MovieNowApp.Models
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Poster { get; set; }
        public string Year { get; set; }
    }
}
