using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    struct Item
    {
        public int key { get; set; }
        public string data { get; set; }
    }

    public class HashTable
    {
        int capacity;
        int size = 0;
        Item?[] items;

        public HashTable(int cp)
        {
            this.capacity = cp;
            items = new Item?[capacity];
        }

        private int GetHashCode(int key)
        {
            return key % capacity;
        }

        public void Insert(int key, string data)
        {
            if (size == capacity)
            {
                throw new Exception("Hash table is full");
            }

            Item i = new Item() { key = key, data = data };

            int hashIndex = GetHashCode(key);

            while (items[hashIndex] != null || (items[hashIndex].HasValue && items[hashIndex].Value.key != -1))
            {
                hashIndex++;

                hashIndex %= capacity;
            }

            items[hashIndex] = i;

        }

        public string Search(int key)
        {
            int hashIndex = GetHashCode(key);

            while (items[hashIndex] != null || (items[hashIndex].HasValue && items[hashIndex].Value.key != -1))
            {
                if (items[hashIndex].Value.key == key)
                {
                    return items[hashIndex].Value.data;
                }

                hashIndex++;
                hashIndex %= capacity;
            }

            return null;
        }


        public int Partition(List<int> items, int s, int e)
        {
            int i = s - 1;
            int pivot = items[e];
            int temp;
            for(int j=s;j<e;j++){
                if (items[j] <= items[e])
                {
                    i += 1;
                    temp = items[i];
                    items[i] = items[j];
                    items[j] = items[i];
                }
                j += 1;
            }
            temp = items[i + 1];
            items[i + 1] = items[e];
            items[e] = temp;

            return i + 1;
            
        }

        public void QuickSort(List<int> items,int s,int e)
        {
            if (s < e)
            {
                int pivot = Partition(items, s, e);
                QuickSort(items, s, pivot - 1);
                QuickSort(items, pivot + 1, e);
            }
        }


    }
}
