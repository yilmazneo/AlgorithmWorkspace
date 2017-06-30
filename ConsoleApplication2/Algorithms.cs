using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    #region Notes

    /*
     * In linked list problems, try to find a way to use 2 pointers. For example, when finding whether given list is cyclic or 
     * acyclic, use 2 pointers and advance them at different speeds. If fast one reaches null, then no cycle, but if fast
     * one gets over the slow one, then there is a cycle.
     */

    #endregion



    #region PriorityQueue

    /*
     * Uses heap data structure.
     * Only needed operations are Max,ExtractMax,IncreaseKey,Insert
     */

    public class PriorityQueue
    {
        public int size{get;set;}
        public List<Algorithms.Item> pq;

        public PriorityQueue()
        {
            size = 0;
            pq = new List<Algorithms.Item>();
        }

        private int GetLeftChildIndex(int i)
        {
            return (i * 2) + 1;
        }

        private int GetRightChildIndex(int i)
        {
            return (i * 2) + 2;
        }

        private int GetParentIndex(int i)
        {
            return (i - 1) / 2;
        }

        private void MaxHeapify(int i)
        {
            int l = GetLeftChildIndex(i);
            int r = GetRightChildIndex(i);
            int max = i;

            if (l < size && pq[l].number > pq[max].number)
            {
                max = l;
            }
            if (r < size && pq[r].number > pq[max].number)
            {
                max = r;
            }

            if (i != max)
            {
                Algorithms.Item temp = pq[i];
                pq[i] = pq[max];
                pq[max] = temp;
                MaxHeapify(max);
            }
        }

        public Algorithms.Item Max
        {
            get { return pq[0]; }
            set { pq[0] = value; }
        }

        public Algorithms.Item ExtractMax()
        {
            if (size < 1)
            {
                throw new Exception("Heap Underflow");
            }

            Algorithms.Item max = Max;
            Max = pq[size - 1];
            size--;
            MaxHeapify(0);

            return max;
        }

        public void IncreaseKey(int i, int key)
        {
            if (pq[i].number > key)
            {
                throw new Exception("new value must be greater");
            }
            pq[i] = new Algorithms.Item() { number = key, order = pq[i].order };
            while (i > 0 && pq[GetParentIndex(i)].number < pq[i].number)
            {
                Algorithms.Item temp = pq[i];
                pq[i] = pq[GetParentIndex(i)];
                pq[GetParentIndex(i)] = temp;
                i = GetParentIndex(i);
            }
        }

        /*
         * Add to the end, set its value to smallest possible, and then call increase key on it with
         * the actual value.
         */
        public void Insert(Algorithms.Item newItem)
        {
            size++;
            pq.Add(new Algorithms.Item());
            pq[size - 1] = new Algorithms.Item() { number = int.MinValue,order = -1};
            IncreaseKey(size - 1, newItem.number);
        }

        public void Delete(int i)
        {
            IncreaseKey(i, int.MaxValue);
            ExtractMax();
        }

    }

    #endregion

    #region LinkedListNode with Flattening

    public class LinkedListNode
    {
        public LinkedListNode next { get; set; }
        public LinkedListNode pre { get; set; }
        public LinkedListNode child { get; set; }
        public int data { get; set; }
    }

    public class LinkedList
    {
        public LinkedListNode root;

        public LinkedList()
        {
            LinkedListNode n1 = new LinkedListNode() { data = 1 };
            root = n1;
            LinkedListNode n2 = new LinkedListNode() { data = 2 };
            LinkedListNode n3 = new LinkedListNode() { data = 3 };
            LinkedListNode n4 = new LinkedListNode() { data = 4 };
            LinkedListNode n5 = new LinkedListNode() { data = 5 };
            LinkedListNode n6 = new LinkedListNode() { data = 6 };
            LinkedListNode n7 = new LinkedListNode() { data = 7 };
            LinkedListNode n8 = new LinkedListNode() { data = 8 };
            LinkedListNode n9 = new LinkedListNode() { data = 9 };

            //n1.next = n2;
            //n2.pre = n1;
            //n2.next = n6;
            //n6.pre = n2;
            //n2.child = n3;
            //n3.next = n4;
            //n4.pre = n3;
            //n4.next = n5;
            //n5.pre = n4;
            //n6.child = n7;
            //n7.child = n8;
            //n6.next = n9;
            //n9.pre = n6;
            n1.child = n2;
            n2.child = n3;
            n3.child = n4;
            n4.child = n5;
            n5.next = n6;
            n6.next = n7;
            n2.next = n8;
            n8.child = n9;

        }

        public void Print()
        {
            LinkedListNode cur = root;
            while (cur != null)
            {
                Console.Write(cur.data + " - ");
                cur = cur.next;
            }
            Console.Read();
        }

        public void Flatten(LinkedListNode current, LinkedListNode next)
        {
            // If current node has a child
            if (current.child != null)
            {
                // if there is no next
                if (next == null)
                {
                    next = current.next; // then assign next to current element's next
                }
                current.next = current.child; // make current next its child
                current.child.pre = current; 
                Flatten(current.child, next); // Flatten the child list
            }
            // if current has no next,it means end of list
            // since lists can appear in multiple levels continue connecting it to next
            else if (current.next == null)
            {
                current.next = next; // connect end of list to available next
                // if there is next
                if (next != null)
                {
                    next.pre = current; 
                    Flatten(next, null); // flatten next
                }
            }
            // if no child and there is next
            else
            {
                Flatten(current.next, next); // flatten next 
            }
        }

    }

    #endregion


    #region BinarySearchTree (Inorder Traversal Gives Sorted Order)

    public class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }
        public int data { get; set; }
    }

    /*
     * Property is Left Child <= Parent
     * Right Child >= Parent
     * In Heap, Parent is either larger or equal - smaller or equal than both left and right
     * depending whether max or min heap.
     * Besides Search, supported operations are Min,Max,Predecessor,Successor
     * All these operations take O(h) time where h is the height of the tree
     * Tree being balanced or not determines the height.
     */
    public class BinarySearchTree{
        public Node root { get; set; }

        public void InorderTraversal()
        {
            InorderTraversal(root);
        }

        


        private Node GetNode(int value)
        {
            Node n = new Node();
            n.data = value;
            n.Left = null;
            n.Right = null;
            return n;
        }

        // This is efficient one. Complexity O(n)
        // c - current , p - previous
        public Node TreeFromPreorder(int s,int[] preorder,int? pre)
        {
            Node n = GetNode(preorder[s]);

            if (pre == null)
            {
                TreeFromPreorder(s + 1, preorder, 0); 
            }

            if (s + 1 < preorder.Length)
            {
                if (pre != null && preorder[pre.Value] > preorder[s])
                {
                    n.Left = TreeFromPreorder(s + 1, preorder,s);
                }
                else if (pre != null && preorder[pre.Value] < preorder[s])
                {
                    n.Right = TreeFromPreorder(s + 1, preorder,s);
                }
            }

            return n;
        }

        public Node construct(int[] array, int index, Int32? pre)
        {
            if (index == array.Length)
                return null;
            Node node = GetNode(array[index]);
            if (pre == null || pre > array[index])
                node.Left = construct(array, index + 1, array[index]);
            else if (pre == null || pre < array[index])
                node.Right = construct(array, index + 1, array[index]);
            return node;
        }

        public void printTree(Node node)
        {
            if (node == null) return;
            Console.Write(node.data + " ");
            printTree(node.Left);
            printTree(node.Right);
        }


        public Node GetLowestCommonAncestor(int v1, int v2, Node cur, Node pre)
        {
            if (cur.data == v1 || cur.data == v2)
            {
                return pre;
            }
            else if (cur == null)
            {
                return null;
            }
            else
            {
                if (cur.data > v1 && cur.data > v2)
                {
                    return GetLowestCommonAncestor(v1, v2, cur.Left,cur);
                }
                else if (cur.data < v1 && cur.data < v2)
                {
                    return GetLowestCommonAncestor(v1, v2, cur.Right, cur);
                }
                else
                {
                    return cur;
                }
            }

        }

#region Inefficient Reconstruct from Preorder O(n^2)

        public void ReconstructFromPreorderTraversal(int[] pre)
        {
            root = CreateFromPreorder(pre,0,pre.Length - 1);
        }

        private int GetFirstHigherValuedIndex(int[] pre, int s, int e)
        {
            int index = -1;

            if (s != e)
            {
                for (int i = s + 1; s <= e; i++)
                {
                    if (pre[i] > pre[s])
                    {
                        return i;
                    }
                }
            }

            return index;

        }
        // This is inefficient one. In worst case, complexity O(n^2)
        // Worst case is when for example 90 - 80 - 70 - 60 - 50
        private Node CreateFromPreorder(int[] pre,int s,int e)
        {
            Node n = GetNode(pre[s]);
            int bi = GetFirstHigherValuedIndex(pre, s, e);

            if (bi != -1)
            {
                n.Left = CreateFromPreorder(pre, s + 1, bi - 1);
                n.Right = CreateFromPreorder(pre, bi, e);
            }

            return n;
        }

#endregion

        /*
         * Time Complexity n
         */
        public void InorderTraversal(Node root)
        {
            if (root != null)
            {
                InorderTraversal(root.Left);
                Console.WriteLine(root.data);
                InorderTraversal(root.Right);
            }
        }

        public void IterativePreorderTraversal(Node root)
        {
            Stack<Node> s = new Stack<Node>();
            s.Push(root);
            while (s.Count > 0)
            {
                Node c = s.Pop();
                Console.Write(c.data + " - ");
                if (c.Right != null)
                {
                    s.Push(c.Right);
                }
                if (c.Left != null)
                {
                    s.Push(c.Left);
                }
            }

        }

        /*
         * First go down from root according to binary search tree property until you hit an end
         * Then you know it's where the new node needs to be inserted at.
         * Check if the tree was empty or not.
         * If it was empty, add the new node as the root.
         * Otherwise check whether new node is left or right child and update references accordingly.
         * Runs in O(h) where h is the height of the tree. h is not necessarily log(n) since the binary search
         * tree can be unbalanced unlike heap.
         */
        public void AddIterative(int data)
        {
            Node y = null;
            Node x = root;

            while (x != null)
            {
                y = x;
                if (data <= x.data)
                    x = x.Left;
                else
                    x = x.Right;
            }

            if (y == null)
            {
                root = new Node() { data = data };
            }
            else
            {
                Node n = GetNode(data, y);
                if (data <= y.data)
                {
                    y.Left = n;
                }
                else
                {
                    y.Right = n;
                }
            }

        }

        public void Add(int data)
        {
            if (root == null)
            {
                root = GetNode(data, null);               
            }
            else
            {
                AddRecursive(data, root);
            }
        }

        private Node GetNode(int data, Node parent)
        {
            Node node = new Node();
            node.data = data;
            node.Left = null;
            node.Right = null;
            node.Parent = parent;
            return node;
        }

        private void AddRecursive(int data,Node node)
        {
            if (data <= node.data)
            {
                if (node.Left != null)
                {
                    AddRecursive(data, node.Left);
                }
                else
                {
                    Node newNode = GetNode(data, node);
                    node.Left = newNode;
                }
            }
            else
            {
                if (node.Right != null)
                {
                    AddRecursive(data, node.Right);
                }
                else
                {
                    Node newNode = GetNode(data, node);
                    node.Right = newNode;
                }
            }
        }

        /*
         * Moves v as the subtree of where u is. 
         */ 
        private void Transplant(Node u, Node v)
        {
            if (u.Parent == null)
            {
                root = v;
            }
            else if (u == u.Parent.Left)
            {
                u.Parent.Left = v;
            }
            else if (u == u.Parent.Right)
            {
                u.Parent.Right = v;
            }

            if (v != null)
            {
                v.Parent = u.Parent;
            }
        }

        public void Delete(Node node)
        {
            // If no left child, then replace node to delete with its right subtree
            if (node.Left == null)
            {
                Transplant(node, node.Right);
            }
            // If no right child, then replace node to delete with its left subtree
            else if (node.Right == null)
            {
                Transplant(node, node.Left);
            }
            // if node to delete has both left and right children
            else
            {                
                Node y = GetMin(node.Right);
                // if the right child has no left child, then replace node with its right subtree                
                // The way to check is to call get min with right child. If min element has node
                // as its parent then, then there is no left subtree of the right child.
                if (y.Parent != node) // If yes, then there is left subtree
                {
                    Transplant(y, y.Right);
                    y.Right = node.Right;
                    y.Right.Parent = y;
                }
                Transplant(node, y);
                y.Left = node.Left;
                y.Left.Parent = y;
            }
        }

        public Node Search(int key)
        {
            return Search(root, key);
        }

        public Node Search(Node node,int key)
        {
            if (node == null)
                return null;

            if (node.data == key)
                return node;
            else if (key <= node.data)
                return Search(node.Left, key);
            else
                return Search(node.Right, key);

        }

        public Node IterativeSearch(int key)
        {
            Node x = root;
            while (x != null && x.data != key)
            {
                if (key <= x.data)
                    x = x.Left;
                else
                    x = x.Right;
            }
            return x;
        }

        public Node GetMin(Node s)
        {
            Node x = s;
            while (x != null)
            {
                if (x.Left != null)
                    x = x.Left;
                else
                    break;
            }
            return x;
        }

        public Node Minimum
        {
            get
            {
                Node x = root;
                while (x != null)
                {
                    if (x.Left != null)
                        x = x.Left;
                    else
                        break;
                }
                return x;
            }
        }

        public Node Maximum
        {
            get
            {
                Node x = root;
                while (x != null)
                {
                    if (x.Right != null)
                        x = x.Right;
                    else
                        break;
                }
                return x;
            }
        }

    }

    #endregion


    public static class Algorithms
    {
        public struct Item
        {
            public int number { get; set; }
            public int order { get; set; }
        }

        #region Quick Sort (Unstable-In Place No Extra Space)

        /*
         * Runs in n(log n) in average case and its constant factor is small and it makes it ideal for large inputs.
         * In the worst case, it performs n square. For example, when the input is already completely sorted, each partition
         * is left having all and right having nothing. Causes unbalanced partitioning.
         * Quick Sort is not stable because if a number with the same value is encountered before and the current insert
         * position is at that item and we encounter an item with less than or equal to the pivot, then the number switches
         * places with the small number and moving to later position than the one with the same number.
         * Example: 4,1,6,4,2,11,3
         * Loop Start(Pivot 3):
         * 1: 4,1,6,4,2,11,3 No Switch since 4 > 3, Current Insert Position: 0,Current Checked Index = 0
         * 2: 1,4,6,4,2,11,3 Switch since 1 < 3, Current Insert Position: 1,Current Checked Index = 1
         * 3: 1,4,6,4,2,11,3 No Switch since 6 > 3, Current Insert Position: 1,Current Checked Index = 2
         * 4: 1,4,6,4,2,11,3 No Switch since 4 > 3, Current Insert Position: 1,Current Checked Index = 3
         * 5: 1,4,6,4,2,11,3 Switch since 2 < 3, Current Insert Position: 1,Current Checked Index = 4
         * 5: 1,2,6,4,4,11,3 Switch since 2 < 3, Current Insert Position: 2,Current Checked Index = 5
         * At this point, the 4 which was the first 4 in order moved past the second 4 in order. 
         * They switched positions.
         */

        public static int Partition(Item[] items, int s, int e)
        {
            int i = s - 1;
            int pivot = items[e].number;
            Item temp;
            for (int j = s; j < e; j++)
            {
                if (items[j].number <= pivot)
                {
                    i += 1;
                    temp = items[i];
                    items[i] = items[j];
                    items[j] = temp;
                }
            }
            temp = items[i + 1];
            items[i + 1] = items[e];
            items[e] = temp;

            return i + 1;

        }

        public static void QuickSort(Item[] items, int s, int e)
        {
            if (s < e)
            {
                int pivot = Partition(items, s, e);
                QuickSort(items, s, pivot - 1);
                QuickSort(items, pivot + 1, e);
            }
        }
        #endregion

        #region Merge Sort (Stable - Not In Place Uses Extra Space)

        public static void MergeSort(Item[] items,int l,int r){
            if (r > l)
            {
                int m = (l + r) / 2;
                MergeSort(items, l, m);
                MergeSort(items, m + 1, r);
                Merge(items, l, m, r);
            }
        }

        public static void Merge(Item[] items, int l, int m, int r)
        {
            int cl = m - l + 1;
            int cr = r - m;
            Item[] left = new Item[cl + 1];
            Item[] right = new Item[cr + 1];
            for (int i = 0; i < cl; i++)
            {
                left[i] = items[l + i];
            }
            for (int j = 0; j < cr; j++)
            {
                right[j] = items[m + 1 + j]; 
            }
            left[cl].number = int.MaxValue;
            right[cr].number = int.MaxValue;

            int li = 0,ri = 0;
            for (int k = l; k < r+1; k++)
            {
                if (left[li].number <= right[ri].number)
                {
                    items[k] = left[li];
                    li++;
                }
                else
                {
                    items[k] = right[ri];
                    ri++;
                }
            }

        }


        #endregion

        #region HeapSort (Unstable - In place, No extra space)

        public static int GetLeftChildIndex(int i)
        {
            return (i * 2) + 1;
        }

        public static int GetRightChildIndex(int i)
        {
            return (i * 2) + 2;
        }

        public static int GetParentIndex(int i)
        {
            return (i - 1) / 2;
        }

        /*
         * log(n)
         * I passed the size as an argument because this is called from HeapSort method
         * and that changes the heap size on each call to MaxHeapify.
         */
        public static void MaxHeapify(Item[] arr,int i,int size)
        {
            int l = GetLeftChildIndex(i);
            int r = GetRightChildIndex(i);
            int largest = i;

            if (l < size && arr[l].number > arr[i].number)
            {
                largest = l;
            }

            if (r < size && arr[r].number > arr[largest].number)
            {
                largest = r;
            }

            if (largest != i)
            {
                Item temp = arr[i];
                arr[i] = arr[largest];
                arr[largest] = temp;
                MaxHeapify(arr,largest,size);
            }
        }

        /*
         * The leaves are already have the heap property.
         * Just run max heapify on the remaining bottom up.
         * n log(n). It calls MaxHeapify n times which takes log n.
         */
        public static void BuildFrom(Item[] arr)
        {
            int nonLeafStartIndex = (arr.Length / 2) - 1;
            for (int i = nonLeafStartIndex; i >= 0; i--)
            {
                MaxHeapify(arr,i,arr.Length);
            }
        }

        /*
         * Runs in n log(n).
         * Switch the last and root. Then call heapify on root.
         * Call heapify with current loop counter because it is the
         * current size of the heap.
         */
        public static void HeapSort(Item[] arr)
        {
            BuildFrom(arr);
            int size = arr.Length;
            for (int i = size - 1; i > 0; i--)
            {
                Item temp = arr[i];
                arr[i] = arr[0];
                arr[0] = temp;                
                MaxHeapify(arr,0,i);
            }
        }

        #endregion

    }
}
