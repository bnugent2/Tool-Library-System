namespace CAB301_Assignment
{
    /// <summary>
    /// Performs Decending Quick Sort Algorithm for a given array of Tools
    /// </summary>
    class QuickSort
    {
        public static int partition(Tool[] arr, int left, int right)
        {
            int pivot = arr[left].NoBorrowings;
            int i = left;
            for (int j = left + 1; j <= right; j++)
            {
                if (arr[j].NoBorrowings > pivot)
                {
                    i = i + 1;
                    Tool temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            Tool temp1 = arr[i];
            arr[i] = arr[left];
            arr[left] = temp1;

            return i;

        }

        public static void Sort(Tool[] arr, int left, int right)
        {
            if (left < right)
            {
                int q = partition(arr, left, right);
                Sort(arr, left, q);
                Sort(arr, q + 1, right);
            }
        }
    }
}
