using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Helpers;
using _5311_Project_sxr0277.Models;

namespace _5311_Project_sxr0277.Controllers
{
    public class QuickSortController : Controller
    {
        //
        // GET: /QuickSort/

        public ActionResult QuickSortPivot()
        {
            var model = new QuickSortModel();
            return View(model);
        }

        #region "Quick Sort Using Pivot"
        [HttpPost]
        public ActionResult QuickSortPivot(QuickSortModel pivotInputInfo)
        {
            try
            {

                if (pivotInputInfo.QuickInputSize != null)
                {
                    var sizeOfInput = Convert.ToInt32(pivotInputInfo.QuickInputSize);
                    Session["InputSize"] = sizeOfInput;
                    int[] pivotInputArr = new int[sizeOfInput];               // initializing array to the size of the input length
                    //if no input data is provided, the program will auto-generate a string
                    if (pivotInputInfo.QuickPivotInput == null)
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
                        pivotInputArr = Array.ConvertAll(inputStringB.Split(','), int.Parse);
                    }
                    //converting string to array when input data is provided
                    else
                    {
                        pivotInputArr = Array.ConvertAll(pivotInputInfo.QuickPivotInput.Split(','), int.Parse);
                    }
                    pivotInputInfo.QuickPivotUsed = pivotInputArr[sizeOfInput - 1].ToString();
                    var watchPivot = System.Diagnostics.Stopwatch.StartNew();
                    int[] pivotResultArr = QuickSortingPivot(pivotInputArr, 0, sizeOfInput - 1);                                          //call to the Bubble Sort function           
                    watchPivot.Stop();
                    pivotInputInfo.QuickPivotTime = watchPivot.Elapsed.TotalSeconds.ToString("0.000000");
                    Session["pivotET"] = watchPivot.Elapsed.TotalSeconds.ToString("0.000000");
                    if (sizeOfInput > 20)
                    {
                        Array.Resize(ref pivotResultArr, 20);
                    }
                    string pivotResultStr = string.Join(",", pivotResultArr);
                    pivotInputInfo.QuickPivotSortedArray = pivotResultStr;
                    return View(pivotInputInfo);
                }
                else
                {
                    ModelState.AddModelError("QuickInputSize", "Enter input");
                    return View(pivotInputInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //quick sort function using pivot as the last element
        private int[] QuickSortingPivot(int[] array, int low, int high)
        {
            if (low < high)
            {

                /* part is partitioning index, pivot is the last element */
                int part = Partition(array, low, high);

                // recursively sort the elements both before and after the partition 
                QuickSortingPivot(array, low, part - 1);
                QuickSortingPivot(array, part + 1, high);
            }
            return array;
        }

        private int Partition(int[] array2, int low, int high)
        {
            int pivot = array2[high];

            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (array2[j] < pivot)
                {
                    i++;

                    // swaping elements array2[i] and array2[j] 
                    int temp;
                    temp = array2[i];
                    array2[i] = array2[j];
                    array2[j] = temp;
                }
            }

            // swaping elements array2[i+1] and pivot
            int temp1;
            temp1 = array2[i + 1];
            array2[i + 1] = array2[high];
            array2[high] = temp1;

            //returns the pivot index
            return i + 1;
        }

        public void PivotChart()
        {
            var SizeQP = Session["InputSize"];
            var quickPivotExcTime = Session["pivotET"];
            var pivotChart = new Chart(width: 500, height: 300)
                                .AddTitle("Quick Sort Using Pivot Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeQP.ToString() },
                                yValues: new[] { quickPivotExcTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();

            Session.Remove("InputSize");
            Session.Remove("pivotET");
        }
        #endregion

        #region "Quick Sort Using Median"

        public ActionResult QuickSortMedian()
        {
            var model = new QuickSortModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult QuickSortMedian(QuickSortModel medianInputInfo)
        {
            try
            {
                
                if (medianInputInfo.QuickInputSize != null)
                {
                    var sizeOfInput = Convert.ToInt32(medianInputInfo.QuickInputSize);
                    Session["InputSize"] = sizeOfInput;
                    int[] medianInputArr = new int[sizeOfInput];               // initializing array to the size of the input length
                    //if no input data is provided, the program will auto-generate a string
                    if (medianInputInfo.QuickMedianInput == null)
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
                        medianInputArr = Array.ConvertAll(inputStringB.Split(','), int.Parse);
                    }
                    //converting string to array when input data is provided
                    else
                    {
                        medianInputArr = Array.ConvertAll(medianInputInfo.QuickMedianInput.Split(','), int.Parse);
                    }
                    var watchMedian = System.Diagnostics.Stopwatch.StartNew();
                    int[] medianResultArr = QuickSortingMedian(medianInputArr, 0, sizeOfInput);                                          //call to the Bubble Sort function           
                    watchMedian.Stop();
                    medianInputInfo.QuickMedianTime = watchMedian.Elapsed.TotalSeconds.ToString("0.000000");
                    Session["medianET"] = watchMedian.Elapsed.TotalSeconds.ToString("0.000000");
                    if (sizeOfInput > 20)
                    {
                        Array.Resize(ref medianResultArr, 20);
                    }
                    string medianResultStr = string.Join(",", medianResultArr);
                    medianInputInfo.QuickMedianSortedArray = medianResultStr;
                    return View(medianInputInfo);
                }
                else
                {
                    ModelState.AddModelError("QuickInputSize", "Enter input size");
                    return View(medianInputInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int[] QuickSortingMedian(int[] inputArray, int firstElementPosition, int lastElementPosition)
        {
            if (firstElementPosition < lastElementPosition)
            {

                int pivotElementPostion = PartitionInputArray(inputArray, firstElementPosition, lastElementPosition);

                QuickSortingMedian(inputArray, firstElementPosition, pivotElementPostion);
                QuickSortingMedian(inputArray, pivotElementPostion + 1, lastElementPosition);

            }
            return inputArray;
        }

        private int PartitionInputArray(int[] inputArray, int firstElementPosition, int lastElementPosition)
        {
            var pivot = inputArray[firstElementPosition];
            if (lastElementPosition - firstElementPosition > 2)
            {
                int MiddleElementPosition = (int)Math.Floor((Convert.ToDouble(lastElementPosition) - Convert.ToDouble(firstElementPosition)) / 2);
                int[] sortingArr = { inputArray[firstElementPosition], inputArray[firstElementPosition + MiddleElementPosition], inputArray[lastElementPosition - 1] };
                Array.Sort(sortingArr);
                if (sortingArr[1] != inputArray[firstElementPosition])
                {
                    int temp = inputArray[firstElementPosition];
                    if (sortingArr[1] == inputArray[firstElementPosition + MiddleElementPosition])
                    {
                        inputArray[firstElementPosition] = inputArray[firstElementPosition + MiddleElementPosition];
                        inputArray[firstElementPosition + MiddleElementPosition] = temp;
                    }
                    else
                    {
                        inputArray[firstElementPosition] = inputArray[lastElementPosition - 1];
                        inputArray[lastElementPosition - 1] = temp;
                    }
                }
                pivot = sortingArr[1];
            }


            int numOfElementsLowerThanPivot = firstElementPosition;
            for (int j = firstElementPosition; j < lastElementPosition; j++)
            {
                if (inputArray[j] < pivot)
                {
                    numOfElementsLowerThanPivot++;
                    int temp = inputArray[j];
                    inputArray[j] = inputArray[numOfElementsLowerThanPivot];
                    inputArray[numOfElementsLowerThanPivot] = temp;
                }
            }

            inputArray[firstElementPosition] = inputArray[numOfElementsLowerThanPivot];
            inputArray[numOfElementsLowerThanPivot] = pivot;


            return numOfElementsLowerThanPivot;
        }

        public void MedianChart()
        {
            var SizeQM = Session["InputSize"];
            var quickMedianExcTime = Session["MedianET"];
            var medianChart = new Chart(width: 500, height: 300)
                                .AddTitle("Quick Sort Using Median Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeQM.ToString() },
                                yValues: new[] { quickMedianExcTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();

            Session.Remove("InputSize");
            Session.Remove("MedianET");
        }

        #endregion

    }
}
