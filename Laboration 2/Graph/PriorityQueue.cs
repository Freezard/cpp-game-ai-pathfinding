using System.Collections.Generic;

// PriorityQueue.cs
// A priorityqueue sorted by total
// cost to a node
namespace Laboration_2
{
    class PriorityQueue
    {
        private List<double> nodeKeys;
        private List<int> heap;
        private List<int> invHeap;
        private int size;
        private int maxSize;

        private void Swap(int a, int b)
        {
            int temp = heap[a];
            heap[a] = heap[b];
            heap[b] = temp;

            invHeap[heap[a]] = a;
            invHeap[heap[b]] = b;
        }

        private void ReorderUpwards(int n)
        {
            while (n > 1 && nodeKeys[heap[n / 2]] > nodeKeys[heap[n]])
            {
                Swap(n / 2, n);
                n /= 2;
            }
        }

        private void ReorderDownwards(int n, int heapSize)
        {
            while (2 * n <= heapSize)
            {
                int child = 2 * n;

                if (child < heapSize && nodeKeys[heap[child]] > nodeKeys[heap[child + 1]])
                    ++child;

                if (nodeKeys[heap[n]] > nodeKeys[heap[child]])
                {
                    Swap(child, n);
                    n = child;
                }
                else break;
            }
        }

        public PriorityQueue(List<double> keys, int maxSize)
        {
            nodeKeys = keys;
            this.maxSize = maxSize;
            heap = new List<int>();
            invHeap = new List<int>();

            for (int i = 0; i < maxSize + 1; i++)
            {
                heap.Add(0);
                invHeap.Add(0);
            }
        }

        public bool Empty()
        {
            return size == 0;
        }

        public void insert(int index)
        {
            //assert

            ++size;
            heap[size] = index;
            invHeap[index] = size;
            ReorderUpwards(size);
        }

        public int Pop()
        {
            Swap(1, size);

            ReorderDownwards(1, size - 1);

            return heap[size--];
        }

        public void ChangePriority(int index)
        {
            ReorderUpwards(invHeap[index]);
        }
    }
}
