using System;
using System.Web;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace _5311_Project_sxr0277.Models
{
    public class BubbleSortModel
    {
        [Required(ErrorMessage = "Please enter the input size")]
        public string BubbleInputSize { get; set; }
        public string BubbleInput { get; set; }
        public string BubbleSortedArray { get; set; }
        public string BubbleTime { get; set; }
    }
}