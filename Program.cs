namespace SortAlgoComparison
{
    class Program
    {
        public delegate void SortMethod(int[] list);

        public static Random rnd = new Random();

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the initial array size:");
            int x = int.Parse(Console.ReadLine()!);
            int[] list = new int[x];
            int select;
            do
            {
                Console.WriteLine("Make a selection:");
                Console.WriteLine("\t1: Bubble Sort");
                Console.WriteLine("\t2: Insertion Sort");
                Console.WriteLine("\t3: Selection Sort");
                Console.WriteLine("\t4: Quick Sort");
                Console.WriteLine("\t5: Merge Sort");
                Console.WriteLine("\t6: Radix Sort");
                Console.WriteLine($"\t7: Change the array size. Currently {x}");
                Console.WriteLine("\t0: Quit");
                Console.Write("You choose: ");
                select = int.Parse(Console.ReadLine()!);
                FillRandom(list, x);
                switch (select)
                {
                    case 1:
                        ShowSortingTimes("Bubble Sort", BubbleSort, list);
                        break;
                    case 2:
                        ShowSortingTimes("Insertion Sort", InsertionSort, list);
                        break;
                    case 3:
                        ShowSortingTimes("Selection Sort", SelectionSort, list);
                        break;
                    case 4:
                        ShowSortingTimes("Quick Sort", QuickSort, list);
                        break;
                    case 5:
                        ShowSortingTimes("Merge Sort", MergeSort, list);
                        break;
                    case 6:
                        ShowSortingTimes("Radix Sort", RadixSort, list);
                        break;
                    case 7:
                        do
                        {
                            Console.WriteLine("New array size: ");
                            x = int.Parse(Console.ReadLine()!);
                        } while (x < 0);
                        list = new int[x];
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            } while (select != 0);
        }

        public static void ShowSortingTimes(String methodName, SortMethod method, int[] list)
        {
            double sortTime;
            Console.WriteLine($"{methodName} of {list.Length} items:");
            FillRandom(list, 10000);
            sortTime = GetSortingTime(method, list);
            Console.WriteLine($"\t{sortTime} seconds for a scrambled list");
            sortTime = GetSortingTime(method, list);
            Console.WriteLine($"\t{sortTime} seconds for a sorted list\n");
        }

        public static double GetSortingTime(SortMethod method, int[] list)
        {
            int startTime, stopTime;
            startTime = Environment.TickCount;
            method(list);
            stopTime = Environment.TickCount;
            return (stopTime - startTime) / 1000.0;
        }

        public static void FillRandom(int[] arr, int max)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rnd.Next(max + 1);
        }

        public static int FindMax(int[] arr, int last)
        {
            int maxIndex = 0;
            for (int i = 1; i <= last; i++)
            {
                if (arr[i] > arr[maxIndex])
                    maxIndex = i;
            }
            return maxIndex;
        }

        public static void Swap(int[] arr, int m, int n)
        {
            int tmp = arr[m];
            arr[m] = arr[n];
            arr[n] = tmp;
        }

        public static void BubbleSort(int[] list)
        {
            for (int i = list.Length - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (list[j] > list[j + 1])
                        Swap(list, j, j + 1);
                }
            }
        }

        public static void InsertionSort(int[] list)
        {
            for (int i = 1; i < list.Length; i++)
            {
                if (list[i] < list[i - 1])
                {
                    int temp = list[i];
                    int j;
                    for (j = i; j > 0 && list[j - 1] > temp; j--)
                        list[j] = list[j - 1];
                    list[j] = temp;
                }
            }
        }

        public static void SelectionSort(int[] list)
        {
            int last = list.Length - 1;
            do
            {
                int biggest = FindMax(list, last);
                Swap(list, biggest, last);
                last--;
            } while (last > 0);
            return;
        }

        public static void QuickSort(int[] list)
        {
            QuickSortRecursive(list, 0, list.Length);
        }

        public static void QuickSortRecursive(int[] a, int low, int high)
        {
            if (high - low <= 1) return;
            int pivot = a[high - 1];
            int split = low;
            for (int i = low; i < high - 1; i++)
                if (a[i] < pivot)
                    Swap(a, i, split++);
            Swap(a, high - 1, split);
            QuickSortRecursive(a, low, split);
            QuickSortRecursive(a, split + 1, high);
            return;
        }

        public static void MergeSort(int[] list)
        {
            SortMerge(list);
        }

        public static int[] SortMerge(int[] array)
        {
            int[] left;
            int[] right;
            int[] result = new int[array.Length];
            if (array.Length <= 1)
                return array;
            int midPoint = array.Length / 2;
            left = new int[midPoint];
            if (array.Length % 2 == 0)
                right = new int[midPoint];
            else
                right = new int[midPoint + 1];
            for (int i = 0; i < midPoint; i++)
                left[i] = array[i];
            int x = 0;
            for (int i = midPoint; i < array.Length; i++)
            {
                right[x] = array[i];
                x++;
            }
            left = SortMerge(left);
            right = SortMerge(right);
            result = MergeArrays(left, right);
            return result;
        }

        public static int[] MergeArrays(int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];
            int indexLeft = 0, indexRight = 0, indexResult = 0;
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    if (left[indexLeft] <= right[indexRight])
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
            return result;
        }

        public static void RadixSort(int[] list)
        {
            {
                int i, j;
                int[] tmp = new int[list.Length];
                for (int shift = 31; shift > -1; --shift)
                {
                    j = 0;
                    for (i = 0; i < list.Length; ++i)
                    {
                        bool move = (list[i] << shift) >= 0;
                        if (shift == 0 ? !move : move)
                            list[i - j] = list[i];
                        else
                            tmp[j++] = list[i];
                    }
                    Array.Copy(tmp, 0, list, list.Length - j, j);
                }
            }
        }
    }
}
