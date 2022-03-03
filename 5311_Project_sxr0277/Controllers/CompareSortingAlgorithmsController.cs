using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Helpers;
using _5311_Project_sxr0277.Models;

namespace _5311_Project_sxr0277.Controllers
{
    public class CompareSortingAlgorithmsController : Controller
    {
        //
        // GET: /CompareSortingAlgorithms/

        public ActionResult Index()
        {
            //initializing the object for our sorting model
            var model = new CompareAlgoInput();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CompareAlgoInput inputInfo)
        {
            try
            {
                //the code is programed to work only when the input size is provided
                if (inputInfo.inputSize != null)
                {
                    var sizeOfInput = Convert.ToInt32(inputInfo.inputSize);
                    Session["InputSize"] = sizeOfInput;
                    Session["InputComp"] = sizeOfInput;      //using different value in session for comparision graph
                    if (inputInfo.isBubble || inputInfo.isSelection || inputInfo.isInsertion || inputInfo.isHeap || inputInfo.isMerge || inputInfo.isQuickPivot || inputInfo.isQuickMedian)
                    {
                        int[] inputArray = new int[sizeOfInput];               // initializing array to the size of the input length
                        //if no input data is provided, the program will auto-generate a string
                        if (inputInfo.input == null)
                        {
                            var ranNum = 0;
                            var randomNumber = new Random();
                            var builder = new StringBuilder();
                            for (var i = 1; i <= sizeOfInput; i++)
                            {
                                ranNum = randomNumber.Next(0, 100);
                                builder.Append(ranNum + ",");
                            }
                            var inputString = builder.ToString().Remove(builder.ToString().Length - 1);
                            inputArray = Array.ConvertAll(inputString.Split(','), int.Parse);
                        }
                        //converting string to array when input data is provided
                        else
                        {
                            inputArray = Array.ConvertAll(inputInfo.input.Split(','), int.Parse);
                        }

                        //executes the comparision of each of the selected algorithms
                        if (inputInfo.isBubble)
                        {
                            //initializing a new array for bubble sorting
                            int[] bsInput = new int[sizeOfInput];
                            Array.Copy(inputArray, bsInput, sizeOfInput);
                            //using stopwatch to measure the runtime of the sorting function
                            var watch1 = System.Diagnostics.Stopwatch.StartNew();
                            int[] resultArr1 = BubbleSort(bsInput);       //this will call the bubble sort function and return a sorted array
                            watch1.Stop();
                            //if the input size is too large, the array will be resized to 20 and only display the first 20 sorted index of the array
                            if (sizeOfInput > 20)
                            {
                                Array.Resize(ref resultArr1, 20);  
                            }
                            //storing the execution time to display in the result and chart
                            inputInfo.executionTimeBubble = watch1.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["bubbleExTime"] = watch1.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["bubbleETime"] = watch1.Elapsed.TotalSeconds.ToString("0.000000");
                            string resultString1 = string.Join(",", resultArr1);
                            inputInfo.sortedArray1 = resultString1;
                        }
                        if (inputInfo.isSelection)
                        {
                            int[] ssInput = new int[sizeOfInput];
                            Array.Copy(inputArray, ssInput, sizeOfInput);
                            var watch2 = System.Diagnostics.Stopwatch.StartNew();
                            int[] resultArr2 = SelectionSort(ssInput);           
                            watch2.Stop();
                            inputInfo.executionTimeSelection = watch2.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["selectionExTime"] = watch2.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["selectionETime"] = watch2.Elapsed.TotalSeconds.ToString("0.000000");
                            if (sizeOfInput > 20)
                            {
                                Array.Resize(ref resultArr2, 20);   
                            }
                            string resultString2 = string.Join(",", resultArr2);
                            inputInfo.sortedArray2 = resultString2;
                        }
                        if (inputInfo.isInsertion)
                        {
                            int[] isInput = new int[sizeOfInput];
                            Array.Copy(inputArray, isInput, sizeOfInput);
                            var watch3 = System.Diagnostics.Stopwatch.StartNew();
                            int[] resultArr3 = InsertionSort(isInput);           
                            watch3.Stop();
                            inputInfo.executionTimeInsertion = watch3.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["insertionExTime"] = watch3.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["insertionETime"] = watch3.Elapsed.TotalSeconds.ToString("0.000000");
                            if (sizeOfInput > 20)
                            {
                                Array.Resize(ref resultArr3, 20);  
                            }
                            string resultString3 = string.Join(",", resultArr3);
                            inputInfo.sortedArray3 = resultString3;
                        }
                        if (inputInfo.isHeap)
                        {
                            int[] hsInput = new int[sizeOfInput];
                            Array.Copy(inputArray, hsInput, sizeOfInput);
                            var watch4 = System.Diagnostics.Stopwatch.StartNew();
                            int[] resultArr4 = HeapSort(hsInput);           
                            watch4.Stop();
                            inputInfo.executionTimeHeap = watch4.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["heapExTime"] = watch4.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["heapETime"] = watch4.Elapsed.TotalSeconds.ToString("0.000000");
                            if (sizeOfInput > 20)
                            {
                                Array.Resize(ref resultArr4, 20);  
                            }
                            string resultString4 = string.Join(",", resultArr4);
                            inputInfo.sortedArray4 = resultString4;
                        }
                        if (inputInfo.isMerge)
                        {
                            int[] msInput = new int[sizeOfInput];
                            Array.Copy(inputArray, msInput, sizeOfInput);
                            var watch5 = System.Diagnostics.Stopwatch.StartNew();
                            int[] resultArr5 = MergeSort(msInput, 0, msInput.Length - 1);           
                            watch5.Stop();
                            inputInfo.executionTimeMerge = watch5.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["mergeExTime"] = watch5.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["mergeETime"] = watch5.Elapsed.TotalSeconds.ToString("0.000000");
                            if (sizeOfInput > 20)
                            {
                                Array.Resize(ref resultArr5, 20);
                            }
                            string resultString5 = string.Join(",", resultArr5);
                            inputInfo.sortedArray5 = resultString5;
                        }
                        if (inputInfo.isQuickPivot)
                        {
                            int[] qspInput = new int[sizeOfInput];
                            Array.Copy(inputArray, qspInput, sizeOfInput);
                            var watch6 = System.Diagnostics.Stopwatch.StartNew();
                            int[] resultArr6 = QuickSortPivot(qspInput, 0, qspInput.Length - 1);            
                            watch6.Stop();
                            inputInfo.executionTimeQuickPivot = watch6.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["quickPivotExTime"] = watch6.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["quickPivotETime"] = watch6.Elapsed.TotalSeconds.ToString("0.000000");
                            if (sizeOfInput > 20)
                            {
                                Array.Resize(ref resultArr6, 20); 
                            }
                            string resultString6 = string.Join(",", resultArr6);
                            //storing the last element of the array as the pivot element used for sorting
                            inputInfo.pivotUsed = inputArray[inputArray.Length - 1].ToString();
                            inputInfo.sortedArray6 = resultString6;
                        }
                        if (inputInfo.isQuickMedian)
                        {
                            int[] qsmInput = new int[sizeOfInput];
                            Array.Copy(inputArray, qsmInput, sizeOfInput);
                            var watch7 = System.Diagnostics.Stopwatch.StartNew();
                            int[] resultArr7 = QuickSortMedian(qsmInput, 0, qsmInput.Length);       
                            watch7.Stop();
                            inputInfo.executionTimeQuickMedian = watch7.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["quickMedianExTime"] = watch7.Elapsed.TotalSeconds.ToString("0.000000");
                            Session["quickMedianETime"] = watch7.Elapsed.TotalSeconds.ToString("0.000000");
                            if (sizeOfInput > 20)
                            {
                                Array.Resize(ref resultArr7, 20);  
                            }
                            string resultString7 = string.Join(",", resultArr7);
                            inputInfo.sortedArray7 = resultString7;
                        }
                        return View(inputInfo);
                    }
                    else
                    {
                        ModelState.AddModelError("isBubble", "Enter one or more sorting algorithms below");
                        return View(inputInfo);
                    }
                }
                else
                {
                    ModelState.AddModelError("inputSize", "Please enter the input size");
                    return View(inputInfo);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region "Sorting Functions"

        //bubble sort function
        public int[] BubbleSort(int[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length-1; i++)
            {
                //this for loop will traverse through the array
                for (int j = 0; j < length-i-1; j++)
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

        //selection sort function
        public int[] SelectionSort(int[] array)
        {
            int length = array.Length;

            //this for loop will traverse through the array 
            for (int i = 0; i < length-1; i++)
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

        //insertion sort function
        public int[] InsertionSort(int[] array)
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

        //heap sort function
        private int[] HeapSort(int[] array)
        {
            int length = array.Length;

            //MaxHeapify will rearrange the array and build heap with each iteration
            for (int i = length/2 - 1; i >= 0; i--)
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

        //merge sort function
        private int[] MergeSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                // finding mid point of the array
                int mid = (left + right) / 2;

                // sorting the first and second halves of the input
                MergeSort(array, left, mid);
                MergeSort(array, mid + 1, right);

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

        //quick sort function using pivot as the last element
        private int[] QuickSortPivot(int[] array, int low, int high)
        {
            if (low < high)
            {

                /* part is partitioning index, pivot is the last element */
                int part = Partition(array, low, high);

                // recursively sort the elements both before and after the partition 
                QuickSortPivot(array, low, part - 1);
                QuickSortPivot(array, part + 1, high);
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

        private int[] QuickSortMedian(int[] inputArray, int firstElementPosition, int lastElementPosition)
        {
            if (firstElementPosition < lastElementPosition)
            {

                int pivotElementPostion = PartitionInputArray(inputArray, firstElementPosition, lastElementPosition);

                QuickSortMedian(inputArray, firstElementPosition, pivotElementPostion);
                QuickSortMedian(inputArray, pivotElementPostion + 1, lastElementPosition);

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

        #endregion

        #region "Sorting Algorithm Chart Representation Functions"

        public void BubbleChart()
        {
            var SizeB = Session["InputSize"];
            var bubbleExecTime = Session["bubbleExTime"];
            var bubbleChart = new Chart(width: 500, height: 300)
                                .AddTitle("Bubble Sort Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeB.ToString() },
                                yValues: new[] { bubbleExecTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();
        }

        public void SelectionChart()
        {
            var SizeS = Session["InputSize"];
            var selectionExecTime = Session["selectionExTime"];
            var selectionChart = new Chart(width: 500, height: 300)
                                .AddTitle("Selection Sort Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeS.ToString() },
                                yValues: new[] { selectionExecTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();
        }

        public void InsertionChart()
        {
            var SizeI = Session["InputSize"];
            var insertionExecTime = Session["insertionExTime"];
            var insertionChart = new Chart(width: 500, height: 300)
                                .AddTitle("Insertion Sort Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeI.ToString() },
                                yValues: new[] { insertionExecTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();
        }

        public void HeapChart()
        {
            var SizeH = Session["InputSize"];
            var heapExecTime = Session["heapExTime"];
            var heapChart = new Chart(width: 500, height: 300)
                                .AddTitle("Heap Sort Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeH.ToString() },
                                yValues: new[] { heapExecTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();
        }

        public void MergeChart()
        {
            var SizeM = Session["InputSize"];
            var mergeExecTime = Session["mergeExTime"];
            var mergeChart = new Chart(width: 500, height: 300)
                                .AddTitle("Merge Sort Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeM.ToString() },
                                yValues: new[] { mergeExecTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();
        }

        public void QuickPivotChart()
        {
            var SizeQP = Session["InputSize"];
            var quickPivotExecTime = Session["quickPivotExTime"];
            var quickPivotChart = new Chart(width: 500, height: 300)
                                .AddTitle("Quick Sort Using Pivot Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeQP.ToString() },
                                yValues: new[] { quickPivotExecTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();
        }

        public void QuickMedianChart()
        {
            var SizeQM = Session["InputSize"];
            var quickMedianExecTime = Session["quickMedianExTime"];
            var quickMedianChart = new Chart(width: 500, height: 300)
                                .AddTitle("Quick Sort Using Median Execution Runtime Graph")
                                .AddSeries(chartType: "column",
                                xValue: new[] { SizeQM.ToString() },
                                yValues: new[] { quickMedianExecTime })
                                .SetXAxis(title: "Input Size")
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();
        }

        public void ComparisionChart()
        {
            var bubbleExecTime = Session["bubbleETime"];
            var selectionExecTime = Session["selectionETime"];
            var insertionExecTime = Session["insertionETime"];
            var heapExecTime = Session["heapETime"];
            var mergeExecTime = Session["mergeETime"];
            var quickPivotExecTime = Session["quickPivotETime"];
            var quickMedianExecTime = Session["quickMedianETime"];
            var Size = Session["InputComp"];

            string bs = null; string ss = null; string ins = null; string hs = null; string ms = null; string qsp = null; string qsm = null;

            if (bubbleExecTime != null)
            {
                bs = "Bubble sort";
            }
            if (selectionExecTime != null)
            {
                ss = "Selection sort";
            }
            if (insertionExecTime != null)
            {
                ins = "Insertion sort";
            }
            if (heapExecTime != null)
            {
                hs = "Heap sort";
            }
            if (mergeExecTime != null)
            {
                ms = "Merge sort";
            }
            if (quickPivotExecTime != null)
            {
                qsp = "Quick sort(Pivot)";
            }
            if (quickMedianExecTime != null)
            {
                qsm = "Quick sort(Median)";
            }

            var comparisionChart = new Chart(width: 500, height: 300);

            comparisionChart.AddTitle("Comparision Graph For Runtime of Sorting Algorithms")
                                .AddSeries(chartType: "column",
                                xValue: new[] { bs,ss,ins,hs,ms,qsp,qsm},
                                yValues: new[] { bubbleExecTime, selectionExecTime, insertionExecTime, heapExecTime, mergeExecTime, quickPivotExecTime, quickMedianExecTime })
                                .SetXAxis(title: "Input Size:"+ Size.ToString())
                                .SetYAxis(title: "Execution Time (in seconds)")
                                .Write();

            Session.Remove("bubbleETime");
            Session.Remove("selectionETime");
            Session.Remove("insertionETime");
            Session.Remove("heapETime");
            Session.Remove("mergeETime");
            Session.Remove("quickPivotETime");
            Session.Remove("quickMedianETime");
            Session.Remove("InputComp");

        }

        #endregion

    }
}
