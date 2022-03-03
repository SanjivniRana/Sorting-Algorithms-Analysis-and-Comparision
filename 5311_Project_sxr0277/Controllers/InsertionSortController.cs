using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Helpers;
using _5311_Project_sxr0277.Models;

namespace _5311_Project_sxr0277.Controllers
{
    public class InsertionSortController : Controller
    {
        //
        // GET: /InsertionSort/

        public ActionResult InsertionSort()
        {
            var model = new InsertionSortModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult InsertionSort(InsertionSortModel insertionInputInfo)
        {
            try
            {

                if (insertionInputInfo.InsertionInputSize != null)
                {
                    var sizeOfInput = Convert.ToInt32(insertionInputInfo.InsertionInputSize);
                    Session["InputSize"] = sizeOfInput;
                    int[] insertionInputArr = new int[sizeOfInput];               // initializing array to the size of the input length
                    //if no input data is provided, the program will auto-generate a string
                    if (insertionInputInfo.InsertionInput == null)
                    {
                        var ranNum = 0;
                        var randomNumber = new Random();
                        var builder = new StringBuilder();
                        for (var i = 1; i <= sizeOfInput; i++)
                        {
                            ranNum = randomNumber.Next(0, 100);
                            builder.Append(ranNum + ",");
                        }
                        var inputStringI = builder.ToString().Remove(builder.ToString().Length - 1);
                        insertionInputArr = Array.ConvertAll(inputStringI.Split(','), int.Parse);
                    }
                    //converting string to array when input data is provided
                    else
                    {
                        insertionInputArr = Array.ConvertAll(insertionInputInfo.InsertionInput.Split(','), int.Parse);
                    }
                    var watchInsertion = System.Diagnostics.Stopwatch.StartNew();
                    int[] insertionResultArr = InsertionSorting(insertionInputArr);                                          //call to the Bubble Sort function           
                    watchInsertion.Stop();
                    insertionInputInfo.InsertionTime = watchInsertion.Elapsed.TotalSeconds.ToString("0.000000");
                    Session["insertionET"] = watchInsertion.Elapsed.TotalSeconds.ToString("0.000000");
                    if (sizeOfInput > 20)
                    {
                        Array.Resize(ref insertionResultArr, 20);
                    }
                    string insertionResultStr = string.Join(",", insertionResultArr);
                    insertionInputInfo.InsertionSortedArray = insertionResultStr;
                    return View(insertionInputInfo);
                }
                else
                {
                    ModelState.AddModelError("InsertionInputSize", "Enter input size");
                    return View(insertionInputInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //insertion sort function
        public int[] InsertionSorting(int[] array)
        {
            int length = array.Length;
            for (int i = 1; i < length; ++i)
            {
                //initializing key element
                int key = array[i];
                int j = i - 1;

                //this while loop will move the elements of the array larger than the key to one position ahead of its current position
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }
                array[j + 1] = key;
            }
            return array;
        }

        public void InsertionSortChart()
        {
            var SizeI = Session["InputSize"];
            var insertionExcTime = Session["insertionET"];
            var insertionSortChart = new Chart(width: 500, height: 300)
                                .AddTitle("Insertion Sort Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeI.ToString() },
                                yValues: new[] { insertionExcTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();

            Session.Remove("InputSize");
            Session.Remove("insertionET");
        }

    }
}
