using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Helpers;
using _5311_Project_sxr0277.Models;

namespace _5311_Project_sxr0277.Controllers
{
    public class SelectionSortController : Controller
    {
        //
        // GET: /SelectionSort/

        public ActionResult SelectionSort()
        {
            var model = new SelectionSortModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult SelectionSort(SelectionSortModel selectionInputInfo)
        {
            try
            {

                if (selectionInputInfo.SelectionInputSize != null)
                {
                    var sizeOfInput = Convert.ToInt32(selectionInputInfo.SelectionInputSize);
                    Session["InputSize"] = sizeOfInput;
                    int[] selectionInputArr = new int[sizeOfInput];               // initializing array to the size of the input length
                    //if no input data is provided, the program will auto-generate a string
                    if (selectionInputInfo.SelectionInput == null)
                    {
                        var ranNum = 0;
                        var randomNumber = new Random();
                        var builder = new StringBuilder();
                        for (var i = 1; i <= sizeOfInput; i++)
                        {
                            ranNum = randomNumber.Next(0, 100);
                            builder.Append(ranNum + ",");
                        }
                        var inputStringS = builder.ToString().Remove(builder.ToString().Length - 1);
                        selectionInputArr = Array.ConvertAll(inputStringS.Split(','), int.Parse);
                    }
                    //converting string to array when input data is provided
                    else
                    {
                        selectionInputArr = Array.ConvertAll(selectionInputInfo.SelectionInput.Split(','), int.Parse);
                    }

                    var watchSelection = System.Diagnostics.Stopwatch.StartNew();
                    int[] selectionResultArr = SelectionSorting(selectionInputArr);                                          //call to the Selection Sort function           
                    watchSelection.Stop();
                    selectionInputInfo.SelectionTime = watchSelection.Elapsed.TotalSeconds.ToString("0.000000");
                    Session["selectionET"] = watchSelection.Elapsed.TotalSeconds.ToString("0.000000");
                    if (sizeOfInput > 20)
                    {
                        Array.Resize(ref selectionResultArr, 20);
                    }
                    string selectionResultStr = string.Join(",", selectionResultArr);
                    selectionInputInfo.SelectionSortedArray = selectionResultStr;
                    return View(selectionInputInfo);
                }
                else
                {
                    ModelState.AddModelError("SelectionInputSize", "Enter input");
                    return View(selectionInputInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //selection sort function
        public int[] SelectionSorting(int[] array)
        {
            int length = array.Length;

            //this for loop will traverse through the array 
            for (int i = 0; i < length - 1; i++)
            {
                //this for loop will find the index of the minimum element in the unsorted array 
                int min = i;
                for (int j = i + 1; j < length; j++)
                {
                    if (array[j] < array[min])
                    {
                        min = j;
                    }
                }

                //moving minumum element to the first element
                int temp;
                temp = array[min];
                array[min] = array[i];
                array[i] = temp;
            }
            return array;
        }

        public void SelectionSortChart()
        {
            var SizeS = Session["InputSize"];
            var selectionExcTime = Session["selectionET"];
            var selectionSortChart = new Chart(width: 500, height: 300)
                                .AddTitle("Selection Sort Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeS.ToString() },
                                yValues: new[] { selectionExcTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();

            Session.Remove("InputSize");
            Session.Remove("selectionET");
        }

    }
}
