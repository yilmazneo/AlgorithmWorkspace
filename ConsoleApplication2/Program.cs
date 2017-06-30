using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            //HashTable ht = new HashTable(20);
            //ht.Insert(10, "10");
            //ht.Insert(30, "10 - 2");
            //ht.Search(12);
            //string s = "Hello";

            //int[] l = Enumerable.Range(1, 100).ToArray<int>().Reverse<int>().ToArray();
            Algorithms.Item[] items = new Algorithms.Item[] {
                new Algorithms.Item{number=4,order=1},
                new Algorithms.Item{number=1,order=2},
                new Algorithms.Item{number=6,order=3},
                new Algorithms.Item{number=4,order=4},
                new Algorithms.Item{number=2,order=5},
                new Algorithms.Item{number=11,order=6},
                new Algorithms.Item{number=6,order=7},
                new Algorithms.Item{number=7,order=8},
                new Algorithms.Item{number=5,order=9},
                new Algorithms.Item{number=3,order=10}
            };

            //Sorts.QuickSort(items, 0, items.Length - 1);

            //Sorts.MergeSort(items, 0, items.Length - 1);

            //Sorts.HeapSort(items);

            /*
            PriorityQueue pq = new PriorityQueue();
            foreach (var i in items)
            {
                pq.Insert(i);
            }

            pq.IncreaseKey(pq.size - 1, 23);
            pq.ExtractMax();
            pq.Delete(3);

            foreach (Algorithms.Item i in items)
            {
                Console.WriteLine(i.number + " - " + i.order);
            }
            */
            /*
            BinarySearchTree tree = new BinarySearchTree();
            tree.AddIterative(6);
            tree.AddIterative(5);
            tree.AddIterative(7);
            tree.AddIterative(2);
            tree.AddIterative(5);
            tree.AddIterative(8);
            tree.AddIterative(1);
            tree.InorderTraversal();
            Console.WriteLine("minimum is " + tree.Minimum.data);
            Console.WriteLine("maximum is " + tree.Maximum.data);
            while (1 == 1)
            {
                int keyToSearch = int.Parse(Console.ReadLine());
                Console.WriteLine(tree.IterativeSearch(keyToSearch) == null ? "False" : "True");
            }
             */
            BinarySearchTree tree = new BinarySearchTree();
            tree.ReconstructFromPreorderTraversal(new int[] { 50, 39, 28, 18, 38, 47, 40, 48, 90, 80, 100 });
            //tree.root = tree.construct(new int[] { 50, 39, 28, 18, 38, 47, 40, 48, 90, 80, 100 },0,null);
            //tree.root = tree.construct(new int[] { 50, 40,30,45,80,70,90 }, 0, null);
            tree.IterativePreorderTraversal(tree.root);
            Console.WriteLine();
            tree.printTree(tree.root);
            //tree.InorderTraversal();
            //Node lca = tree.GetLowestCommonAncestor(8, 48, tree.root, null);

            //LinkedList ll = new LinkedList();
            //ll.Flatten(ll.root, null);
            //ll.Print();
        }




    }
}
