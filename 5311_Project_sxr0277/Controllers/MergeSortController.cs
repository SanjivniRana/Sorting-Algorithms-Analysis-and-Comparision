using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Helpers;
using _5311_Project_sxr0277.Models;

namespace _5311_Project_sxr0277.Controllers
{
    public class MergeSortController : Controller
    {
        //
        // GET: /MergeSort/

        public ActionResult MergeSort()
        {
            var model = new MergeSortModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult MergeSort(MergeSortModel mergeInputInfo)
        {
            try
            {

                if (mergeInputInfo.MergeInputSize != null)
                {
                    var sizeOfInput = Convert.ToInt32(mergeInputInfo.MergeInputSize);
                    Session["InputSize"] = sizeOfInput;
                    int[] mergeInputArr = new int[sizeOfInput];               // initializing array to the size of the input length
                    //if no input data is provided, the program will auto-generate a string
                    if (mergeInputInfo.MergeInput == null)
                    {
                        var ranNum = 0;
                        var randomNumber = new Random();
                        var builder = new StringBuilder();
                        for (var i = 1; i <= sizeOfInput; i++)
                        {
                            ranNum = randomNumber.Next(0, 100);
                            builder.Append(ranNum + ",");
                        }
                        var inputStringM = builder.ToString().Remove(builder.ToString().Length - 1);
                        mergeInputArr = Array.ConvertAll(inputStringM.Split(','), int.Parse);
                    }
                    //converting string to array when input data is provided
                    else
                    {
                        mergeInputArr = Array.ConvertAll(mergeInputInfo.MergeInput.Split(','), int.Parse);
                    }
                    var watchMerge = System.Diagnostics.Stopwatch.StartNew();
                    int[] mergeResultArr = MergeSorting(mergeInputArr, 0, sizeOfInput-1);                                          //call to the Bubble Sort function           
                    watchMerge.Stop();
                    mergeInputInfo.MergeTime = watchMerge.Elapsed.TotalSeconds.ToString("0.000000");
                    Session["mergeExTime"] = watchMerge.Elapsed.TotalSeconds.ToString("0.000000");
                    if (sizeOfInput > 20)
                    {
                        Array.Resize(ref mergeResultArr, 20);
                    }
                    string mergeResultStr = string.Join(",", mergeResultArr);
                    mergeInputInfo.MergeSortedArray = mergeResultStr;
                    return View(mergeInputInfo);
                }
                else
                {
                    ModelState.AddModelError("MergeInputSize", "Enter input");
                    return View(mergeInputInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //merge sort function
        private int[] MergeSorting(int[] array, int left, int right)
        {
            if (left < right)
            {
                // finding mid point of the array
                int mid = (left + right) / 2;

                // sorting the first and second halves of the input
                MergeSorting(array, left, mid);
                MergeSorting(array, mid + 1, right);

                // merging the sorted halves once all elements are sorted
                Merge(array, left, mid, right);
            }
            return array;
        }

        private void Merge(int[] array2, int l, int mid, int r)
        {
            //finding the sizes of two subarrays to be merged
            int n1 = mid - l + 1;
            int n2 = r - mid;

            // creating temp arrays for sorting
            int[] L = new int[n1];
            int[] R = new int[n2];
            int i, j;

            // copying data to the temp arrays for further sorting
            for (i = 0; i < n1; ++i)
                L[i] = array2[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = array2[mid + 1 + j];

            // merging the temp arrays

            i = 0;
            j = 0;
            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    array2[k] = L[i];
                    i++;
                }
                else
                {
                    array2[k] = R[j];
                    j++;
                }
                k++;
            }

            // Copy any remaining elements of array L[]
            while (i < n1)
            {
                array2[k] = L[i];
                i++;
                k++;
            }

            // Copy any remaining elements of array R[]
            while (j < n2)
            {
                array2[k] = R[j];
                j++;
                k++;
            }
        }

        public void MergeSortChart()
        {
            var SizeM = Session["InputSize"];
            var mergeExecTime = Session["mergeExTime"];
            var mergeSortChart = new Chart(width: 500, height: 300)
                                .AddTitle("Merge Sort Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeM.ToString() },
                                yValues: new[] { mergeExecTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();

            Session.Remove("InputSize");
            Session.Remove("mergeExTime");
        }

    }
}
