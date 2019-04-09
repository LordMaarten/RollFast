using System;
using UnityEngine;
using Random = System.Random;

namespace pxg.Generator
{
    public class Rock: MonoBehaviour
    {
        [Range(1, 100)] public int range = 1;

        public int x = 10, y= 10, z = 10;
        public int filterHLength = 2;
        [Range(1,100)] public int filterStrength = 50;
        
        public string seed;

        private int[,,] map = new int[0,0,0];

        private int[,,] gen()
        {
            var rand = new Random(this.seed.GetHashCode());
            var map = new int[x,y,z];
            
            for (var x = 0; x < this.x; x++)
            {
                for (var y = 0; y < this.y; y++)
                {
                    for (var z = 0; z < this.z; z++)
                    {
                        map[x,y,z] = rand.Next(100) < this.range ? 1 : 0;
                    }
                }
            }

            return map;
        }

        private int[,,] filter(int[,,] map)
        {
            var filteredMap = new int[x, y, z];
            for (var x = 0; x < this.x; x++)
            {
                for (var y = 0; y < this.y; y++)
                {
                    for (var z = 0; z < this.z; z++)
                    {
                        filteredMap[x, y, z] = applyFilter(map, x, y, z);
                    }
                }
            }

            return filteredMap;
        }

        private int applyFilter(int[,,] map, int x, int y, int z)
        {
            int one = 0, zero = 0;

            if (x - this.filterHLength < 0 || x + this.filterHLength > this.x
                || y - this.filterHLength < 0 || y + this.filterHLength > this.y
                || z - this.filterHLength < 0 || z + this.filterHLength > this.z
                || this.filterHLength == 0)
            {
                return map[x, y, z];
            }
            
            for (int xIndex = x - this.filterHLength; xIndex < x + this.filterHLength; xIndex++)
            {
                for (int yIndex = y - this.filterHLength; yIndex < y + this.filterHLength; yIndex++)
                {
                    for (int zIndex = z - this.filterHLength; zIndex < z + this.filterHLength; zIndex++)
                    {
                        if (map[xIndex, yIndex, zIndex] == 1)
                        {
                            one++;
                        }
                        else
                        {
                            zero++;
                        }
                    }
                }
            }

            return one > zero ? 1 : 0;
        }

        private void OnDrawGizmos()
        {
            var map = this.gen();   
            map = this.filter(map);
            for (int xIndex = 0; xIndex < this.x; xIndex++)
            {
                for (int yIndex = 0; yIndex < this.y/2; yIndex++)
                {
                    for (int zIndex = 0; zIndex < this.z; zIndex++)
                    {
                        if (map[xIndex,yIndex,zIndex] > 0)
                        {
                            Gizmos.color = Color.red;

                            Gizmos.DrawCube(new Vector3(xIndex,yIndex,zIndex), new Vector3(1f,1f,1f) );
                            continue;
                        }

                        //Gizmos.color = Color.black;
                        //Gizmos.DrawWireCube(new Vector3(xIndex,yIndex,zIndex),new Vector3(.2f,.2f,.2f) );
                    }
                }
            }
        }
    }
}