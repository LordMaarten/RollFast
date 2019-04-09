using System.Collections.Generic;

namespace pxg.DataStructure
{
    public static class EdgeToPoint
    {
        public static List<int> GetPointIndices(int edge)
        {
            var l = new List<int>();

            switch (edge)
            {
                case 0:
                    l.Add(0);
                    l.Add(1);
                    break;
                case 1:
                    l.Add(1);
                    l.Add(2);
                    break;
                case 2:
                    l.Add(2);
                    l.Add(3);
                    break;
                case 3:
                    l.Add(3);
                    l.Add(0);
                    break;
                case 4:
                    l.Add(4);
                    l.Add(5);
                    break;
                case 5:
                    l.Add(5);
                    l.Add(6);
                    break;
                case 6:
                    l.Add(6);
                    l.Add(7);
                    break;
                case 7:
                    l.Add(4);
                    l.Add(7);
                    break;
                case 8:
                    l.Add(4);
                    l.Add(0);
                    break;
                case 9:
                    l.Add(5);
                    l.Add(1);
                    break;
                case 10:
                    l.Add(3);
                    l.Add(7);
                    break;
                case 11:
                    l.Add(6);
                    l.Add(2);
                    break;
            }

            return l;
        }
    }
}