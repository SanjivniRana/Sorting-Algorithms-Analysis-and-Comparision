using System;
using System.Web;
using System.Linq;
using System.Web.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _5311_Project_sxr0277.Models
{
    public class CompareAlgoInput
    {
        //[Required(ErrorMessage = "Please enter the input size")]
        public string inputSize { get; set; }
        public string input { get; set; }

        public bool isBubble { get; set; }
        public bool isSelection { get; set; }
        public bool isInsertion { get; set; }
        public bool isHeap { get; set; }
        public bool isMerge { get; set; }
        public bool isQuickPivot { get; set; }
        public bool isQuickMedian { get; set; }

        public string sortedArray1 { get; set; }
        public string sortedArray2 { get; set; }
        public string sortedArray3 { get; set; }
        public string sortedArray4 { get; set; }
        public string sortedArray5 { get; set; }
        public string sortedArray6 { get; set; }
        public string sortedArray7 { get; set; }

        public string pivotUsed { get; set; }

        public string executionTimeBubble { get; set; }
        public string executionTimeSelection { get; set; }
        public string executionTimeInsertion { get; set; }
        public string executionTimeHeap { get; set; }
        public string executionTimeMerge { get; set; }
        public string executionTimeQuickPivot { get; set; }
        public string executionTimeQuickMedian { get; set; }

    }
}