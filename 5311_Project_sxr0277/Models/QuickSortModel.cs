using System;
using System.Web;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace _5311_Project_sxr0277.Models
{
    public class QuickSortModel
    {
        [Required(ErrorMessage = "Please enter the input size")]
        public string QuickInputSize { get; set; }
        public string QuickPivotInput { get; set; }
        public string QuickPivotSortedArray { get; set; }
        public string QuickPivotUsed { get; set; }
        public string QuickPivotTime { get; set; }
        public string QuickMedianInput { get; set; }
        public string QuickMedianSortedArray { get; set; }
        public string QuickMedianTime { get; set; }
    }
}