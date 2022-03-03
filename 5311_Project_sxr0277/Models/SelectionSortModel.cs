using System;
using System.Web;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace _5311_Project_sxr0277.Models
{
    public class SelectionSortModel
    {
        [Required(ErrorMessage = "Please enter the input size")]
        public string SelectionInputSize { get; set; }
        public string SelectionInput { get; set; }
        public string SelectionSortedArray { get; set; }
        public string SelectionTime { get; set; }
    }
}