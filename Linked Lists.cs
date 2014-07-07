using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeeksForGeeks;

namespace LinkedLists
{
    class Program
    {
        public LinkList<int> createList(int[] input)
        {
            LinkList<int> l = new LinkList<int>();
            foreach (int temp in input) l.putLast(temp);
            return l;
        }

        public void removeDups(LinkList<int> input)
        {
            if (input == null) return;
            HashSet<int> hs = new HashSet<int>();
            Node<int> node = input.head;
            Node<int> prev = node;
            while (node != null)
            {
                if (hs.Contains(node.data))
                {
                    prev.next = node.next;
                }
                else
                {
                    hs.Add(node.data);
                    prev = node;
                }
                node = node.next;
                
            }
        }

        public void removeDupsNoExtraSpace(LinkList<int> input)
        {
            if (input == null) return;
            Node<int> current = input.head;
            
            while (current != null)
            {
                Node<int> prev = current;
                Node<int> node = current.next;
                while (node != null)
                {
                    if (prev != null && current.data == node.data)
                    {
                        prev.next = node.next;
                    }
                    else
                    {
                        prev = node;
                    }
                    
                    node = node.next;
                }
                prev = current;
                current = current.next;
            }
        }

        public int? kthToLastElement(LinkList<int> input, int k)
        {
            if (input == null) return null;
            if (k < 1) return null;
            Node<int> p1 = input.head;
            Node<int> p2 = input.head;
            int counter = 0;
            while (p1 != null)
            {
                if (counter >= k)
                {
                    p2 = p2.next;
                }
                p1 = p1.next;
                counter++;
            }
            return p2.data;
        }

        public void deleteNode(LinkList<int> input, int k)
        {
            if (input == null || k > input.Size || k < 0) return;
            Node<int> node = input.findK(k);
            if (node.next == null) return;
            node.data = node.next.data;
            node.next = node.next.next;
        }

        public LinkList<int> partitionX(LinkList<int> input, int X)
        {
            if (input == null || input.Size == 1) return input;
            LinkList<int> low = new LinkList<int>();
            LinkList<int> high = new LinkList<int>();
            Node<int> current = input.head;
            while (current != null)
            {
                if (current.data <= X) low.putFirst(current.data);
                else high.putFirst(current.data);
                current = current.next;
            }
            low.tail.next = high.head;
            return low;
        }

        public LinkList<int> sumOfLists(LinkList<int> l1, LinkList<int> l2)
        {
            if (l1 == null && l2 == null) return null;
            Node<int> c1, c2;
            c1 = c2 = null;
            if(l1 !=null) c1 = l1.head;
            if(l2 != null) c2 = l2.head;
            LinkList<int> output = new LinkList<int>();
            int carry = 0;
            while (c1 != null & c2 != null)
            {
                int temp = c1.data + c2.data + carry;
                if (temp > 10)
                {
                    temp -= 10;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }
                c1 = c1.next;
                c2 = c2.next;
                output.putLast(temp);
            }
            while (c1 != null)
            {
                if (carry > 0)
                {
                    output.putLast(c1.data + carry);
                    carry = 0;
                }
                output.putLast(c1.data);
                c1 = c1.next;
            }
            while (c2 != null)
            {
                if (carry > 0)
                {
                    output.putLast(c2.data + carry);
                    carry = 0;
                }
                output.putLast(c2.data);
                c2 = c2.next;
            }
            if (carry > 0) output.putLast(1);
            return output;
        }

        public int sumOfListsReverseOrder(LinkList<int> l1, LinkList<int> l2)
        {
            if (l1 == null && l2 == null) return 0;
            Node<int> c1, c2;
            c1 = c2 = null;
            if (l1 != null) c1 = l1.head;
            if (l2 != null) c2 = l2.head;
            int sum1 = 0, sum2 = 0;
            while (c1 != null)
            {
                sum1 = sum1 * 10 + c1.data;
                c1 = c1.next;
            }
            while (c2 != null)
            {
                sum2 = sum2 * 10 + c2.data;
                c2 = c2.next;
            }
            return (sum1 + sum2);
        }

        public int? loopStart(LinkList<int> input)
        {
            if (input == null) return null;
            Node<int> node = input.findK(3);
            Console.WriteLine("Node link to: {0}", node.data);
            input.tail.next = node;

            Node<int> slow = input.head;
            Node<int> fast = input.head;
            bool met = false;
            while (true)
            {
                if (slow.next == null || fast.next == null || fast.next.next == null) return null;
                slow = slow.next;
                if (!met) fast = fast.next.next;
                else fast = fast.next;

                if (slow == fast)
                {
                    if (met) return slow.data;
                    else
                    {
                        met = true;
                        slow = input.head;
                    }
                }
                
            }
        }
    }
}
