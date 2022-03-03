using System;
using System.Web;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace _5311_Project_sxr0277.Models
{
    public class MergeSortModel
    {
        [Required(ErrorMessage = "Please enter the input size")]
        public string MergeInputSize { get; set; }
        public string MergeInput { get; set; }
        public string MergeSortedArray { get; set; }
        public string MergeTime { get; set; }
    }
}