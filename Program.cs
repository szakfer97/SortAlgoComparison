namespace SortAlgoComparison
{
    class Program
    {
        public delegate void SortMethod(int[] list);

        public static Random rnd = new Random();

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the initial array size:");
            int x = int.Parse(Console.ReadLine());
            int[] list = new int[x];
            int select;
            do
            {
                Console.WriteLine("Make a selection:");
                Console.WriteLine("\t1: Insertion Sort");
                Console.WriteLine("\t2: Selection Sort");
                Console.WriteLine("\t3: Bubble Sort");
                Console.WriteLine("\t4: Quick Sort");
                Console.WriteLine($"\t5: Change the array size. Currently {x}");
                Console.WriteLine("\t0: Quit");
                Console.Write("You choose: ");
                select = int.Parse(Console.ReadLine());
                FillRandom(list, x);
                switch (select)
                {
                    case 1:
                        ShowSortingTimes("Insertion Sort", InsertionSort, list);
                        break;
                    case 2:
                        ShowSortingTimes("Selection Sort", SelectionSort, list);
                        break;
                    case 3:
                        ShowSortingTimes("Bubble Sort", BubbleSort, list);
                        break;
                    case 4:
                        ShowSortingTimes("Quick Sort", QuickSort, list);
                        break;
                    case 5:
                        do
                        {
                            Console.WriteLine("New array size: ");
                            x = int.Parse(Console.ReadLine());
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

        public static void QuickSort(int[] a)
        {
            QuickSortRecursive(a, 0, a.Length);
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

        public static int Partition(int[] arr, int x)
        {
            int lowMark = 0, highMark = arr.Length - 1;

            while (true)
            {
                while (lowMark < arr.Length && arr[lowMark] <= x)
                    lowMark++;
                while (highMark >= 0 && arr[highMark] > x)
                    highMark--;
                if (lowMark > highMark)
                    return highMark;
                Swap(arr, lowMark, highMark);
            }
        }
    }
}