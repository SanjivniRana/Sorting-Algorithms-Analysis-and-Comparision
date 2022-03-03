using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Helpers;
using _5311_Project_sxr0277.Models;

namespace _5311_Project_sxr0277.Controllers
{
    public class BubbleSortController : Controller
    {
        //
        // GET: /SortingAlgorithms/

        public ActionResult BubbleSort()
        {
            var model = new BubbleSortModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult BubbleSort(BubbleSortModel bubbleInputInfo)
        {
            try
            {

                if (bubbleInputInfo.BubbleInputSize != null)
                {
                    var sizeOfInput = Convert.ToInt32(bubbleInputInfo.BubbleInputSize);
                    Session["InputSize"] = sizeOfInput;
                    int[] bubbleInputArr = new int[sizeOfInput];               // initializing array to the size of the input length
                    //if no input data is provided, the program will auto-generate a string
                    if (bubbleInputInfo.BubbleInput == null)
                    {
                        var ranNum = 0;
                        var randomNumber = new Random();
                        var builder = new StringBuilder();
                        for (var i = 1; i <= sizeOfInput; i++)
                        {
                            ranNum = randomNumber.Next(0, 100);
                            builder.Append(ranNum + ",");
                        }
                        var inputStringB = builder.ToString().Remove(builder.ToString().Length - 1);
                        bubbleInputArr = Array.ConvertAll(inputStringB.Split(','), int.Parse);
                    }
                    //converting string to array when input data is provided
                    else
                    {
                        bubbleInputArr = Array.ConvertAll(bubbleInputInfo.BubbleInput.Split(','), int.Parse);
                    }
                    var watchBubble = System.Diagnostics.Stopwatch.StartNew();
                    int[] bubbleResultArr = BubbleSorting(bubbleInputArr);
                    watchBubble.Stop();
                    bubbleInputInfo.BubbleTime = watchBubble.Elapsed.TotalSeconds.ToString("0.000000");
                    Session["bubbleET"] = watchBubble.Elapsed.TotalSeconds.ToString("0.000000");
                    if (sizeOfInput > 20)
                    {
                        Array.Resize(ref bubbleResultArr, 20);
                    }
                    string bubbleResultStr = string.Join(",", bubbleResultArr);
                    bubbleInputInfo.BubbleSortedArray = bubbleResultStr;
                    return View(bubbleInputInfo);
                }
                else
                {
                    ModelState.AddModelError("BubbleInputSize", "Enter input size");
                    return View(bubbleInputInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //bubble sort function
        public int[] BubbleSorting(int[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length - 1; i++)
            {
                //this for loop will traverse through the array
                for (int j = 0; j < length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        //compare and swap the elements
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            return array;
        }

        public void BubbleSortChart()
        {
            var SizeB = Session["InputSize"];
            var bubbleExcTime = Session["bubbleET"];
            var bubbleSortChart = new Chart(width: 500, height: 300)
                                .AddTitle("Bubble Sort Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeB.ToString() },
                                yValues: new[] { bubbleExcTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();

            Session.Remove("InputSize");
            Session.Remove("bubbleET");
        }

    }
}
