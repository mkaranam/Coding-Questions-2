using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class CircusTower
    {
        Person[] Persons;
        Person[] inWeightOrder;
        Person[] inHeightOrder;
        int Count;

        public CircusTower()
        {
            Count = 10;
            Persons = new Person[10];
            for (int i = 0; i < 10; i++) Persons[i] = new Person();
        }

        public CircusTower(int[] Height, int[] Weight)
        {
            Count = Height.Length;
            Persons = new Person[Count];
            for (int i = 0; i < Count; i++)
            {
                Persons[i] = new Person();
                Persons[i].weight = Weight[i];
                Persons[i].height = Height[i];
                Persons[i].ID = i;
            }
            inWeightOrder = new Person[Count];
            inHeightOrder = new Person[Count];
            Persons.CopyTo(inWeightOrder, 0);
            Persons.CopyTo(inHeightOrder, 0);
            sortHeightOrder();
            sortWeightOrder();
            
        }

        public void sortWeightOrder()
        {
            Array.Sort(inWeightOrder, Person.weightComparison);
        }

        public void sortHeightOrder()
        {
            Array.Sort(inHeightOrder, Person.heightComparison);
        }

        public String printInWeightOrder()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Person p in inWeightOrder) sb.Append(p.ID + "(" + p.weight + ") ");
            return sb.ToString();
        }

        public String printInHeightOrder()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Person p in inHeightOrder) sb.Append(p.ID + "(" + p.height + ") ");
            return sb.ToString();
        }

        public int maxHeight()
        {
            int[] height = new int[Count];
            for (int i = 0; i < Count; i++) height[i] = maxHeight(height, i);
            return height.Max();
        }

        private int maxHeight(int[] input, int p)
        {
            if (input[p] > 0) return input[p];

            int height = 0;
            int wNum = find(inWeightOrder, p, 0, Count);
            int hNum = find(inHeightOrder, p, 0, Count);
            if (wNum == 0 && hNum == 0) return 1;
            HashSet<int> hs = new HashSet<int>();
            for (int i = wNum; i >= 0; i--) hs.Add(i);
            for (int i = hNum; i >= 0; i--)
            {
                if (hs.Contains(i))
                {
                    if (input[i] == 0) input[i] = maxHeight(input, i);
                    height = Math.Max(input[i], height);
                }
            }
            return height + 1;
        }

        private int find(Person[] per, int p, int low, int high)
        {
            if (low > high) return -1;
            int mid = (low + high) / 2;
            if (per[mid].ID == p)
            {
                return mid;
            }

            if (p < per[mid].ID) return find(per, p, low, mid - 1);
            else return find(per, p, mid + 1, high);
        }

       

        
    }

    class Person 
    {
        public int ID { get; set; }
        public int weight {get; set;}
        public int height { get; set; }

        public static Comparison<Person> heightComparison = delegate(Person p1, Person p2)
        {
            return p1.height.CompareTo(p2.height);
        };

        public static Comparison<Person> weightComparison = delegate(Person p1, Person p2)
        {
            return p1.weight.CompareTo(p2.weight);
        };
    }
}
