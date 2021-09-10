using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesTT.Models
{
    public class Category
    {
        public int page { get; set; }
        public List<Movie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
