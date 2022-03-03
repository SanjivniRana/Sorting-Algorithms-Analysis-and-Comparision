using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Helpers;
using _5311_Project_sxr0277.Models;

namespace _5311_Project_sxr0277.Controllers
{
    public class HeapSortController : Controller
    {
        //
        // GET: /HeapSort/

        public ActionResult HeapSort()
        {
            var model = new HeapSortModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult HeapSort(HeapSortModel heapInputInfo)
        {
            try
            {

                if (heapInputInfo.HeapInputSize != null)
                {
                    var sizeOfInput = Convert.ToInt32(heapInputInfo.HeapInputSize);
                    Session["InputSize"] = sizeOfInput;
                    int[] heapInputArr = new int[sizeOfInput];               // initializing array to the size of the input length
                    //if no input data is provided, the program will auto-generate a string
                    if (heapInputInfo.HeapInput == null)
                    {
                        var ranNum = 0;
                        var randomNumber = new Random();
                        var builder = new StringBuilder();
                        for (var i = 1; i <= sizeOfInput; i++)
                        {
                            ranNum = randomNumber.Next(0, 100);
                            builder.Append(ranNum + ",");
                        }
                        var inputStringH = builder.ToString().Remove(builder.ToString().Length - 1);
                        heapInputArr = Array.ConvertAll(inputStringH.Split(','), int.Parse);
                    }
                    //converting string to array when input data is provided
                    else
                    {
                        heapInputArr = Array.ConvertAll(heapInputInfo.HeapInput.Split(','), int.Parse);
                    }
                    var watchHeap = System.Diagnostics.Stopwatch.StartNew();
                    int[] heapResultArr = HeapSorting(heapInputArr);                                          //call to the Bubble Sort function           
                    watchHeap.Stop();
                    heapInputInfo.HeapTime = watchHeap.Elapsed.TotalSeconds.ToString("0.000000");
                    Session["heapExTime"] = watchHeap.Elapsed.TotalSeconds.ToString("0.000000");
                    if (sizeOfInput > 20)
                    {
                        Array.Resize(ref heapResultArr, 20);
                    }
                    string heapResultStr = string.Join(",", heapResultArr);
                    heapInputInfo.HeapSortedArray = heapResultStr;
                    return View(heapInputInfo);
                }
                else
                {
                    ModelState.AddModelError("HeapInputSize", "Enter input size");
                    return View(heapInputInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //heap sort function
        private int[] HeapSorting(int[] array)
        {
            int length = array.Length;

            //MaxHeapify will rearrange the array and build heap with each iteration
            for (int i = length / 2 - 1; i >= 0; i--)
            {
                MaxHeapify(array, length, i);
            }

            //extracting elements from the heap one by one
            for (int i = length - 1; i > 0; i--)
            {
                // moving root element to the end
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;

                // performing max-heapify on the reduced heap
                MaxHeapify(array, i, 0);
            }
            return array;
        }

        private void MaxHeapify(int[] array, int n, int i)
        {
            // Initialize root as the largest
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            // when the left child is comparatively larger than the root
            if (left < n && array[left] > array[largest])
                largest = left;

            // when the right child is comparatively larger than the largest
            if (right < n && array[right] > array[largest])
                largest = right;

            // when the root element is not the largest
            if (largest != i)
            {
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;

                // Recursively max-heapify the affected sub-tree
                MaxHeapify(array, n, largest);
            }
        }

        public void HeapSortChart()
        {
            var SizeH = Session["InputSize"];
            var heapExcTime = Session["heapExTime"];
            var heapSortChart = new Chart(width: 500, height: 300)
                                .AddTitle("Heap Sort Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeH.ToString() },
                                yValues: new[] { heapExcTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();

            Session.Remove("InputSize");
            Session.Remove("heapExTime");
        }

    }
}
