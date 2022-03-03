using System;
using System.Web;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace _5311_Project_sxr0277.Models
{
    public class InsertionSortModel
    {
        [Required(ErrorMessage = "Please enter the input size")]
        public string InsertionInputSize { get; set; }
        public string InsertionInput { get; set; }
        public string InsertionSortedArray { get; set; }
        public string InsertionTime { get; set; }
    }
}