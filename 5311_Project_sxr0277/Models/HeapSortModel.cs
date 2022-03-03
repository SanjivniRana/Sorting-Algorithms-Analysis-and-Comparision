using System;
using System.Web;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace _5311_Project_sxr0277.Models
{
    public class HeapSortModel
    {
        [Required(ErrorMessage = "Please enter the input size")]
        public string HeapInputSize { get; set; }
        public string HeapInput { get; set; }
        public string HeapSortedArray { get; set; }
        public string HeapTime { get; set; }
    }
}