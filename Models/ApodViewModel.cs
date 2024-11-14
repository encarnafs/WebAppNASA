using System.ComponentModel.DataAnnotations;

namespace WebAppNASA.Models
{
    public class ApodViewModel
    {
        public string? Copyright { get; set; }

        public DateTime Date { get; set; }

        public string? Explanation { get; set; }

        public string? Title { get; set; }

        public string? Url { get; set; }
    }
}
