using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Final.Models
{
    public class LanguageTypeViewModel
    {
        public List<Tutor>? Tutors { get; set; }
        public SelectList? Languages { get; set; }
        public string? LanguageType { get; set; }
        public string? Search { get; set; }
    }
}