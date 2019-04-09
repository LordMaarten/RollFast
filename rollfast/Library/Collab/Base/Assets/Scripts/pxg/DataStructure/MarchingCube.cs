using System;
using System.Collections.Generic;
using UnityEngine;

namespace pxg.DataStructure
{
    public class MarchingCube
    {
        private MarchingCube(List<Vector3> p, Chunk chunk)
        {
            Points = p;
            Values = new List<float>(8);
            foreach (var point in p)
            {
                Values.Add(chunk.getValue(point.x, point.y, point.z));
            }
        }

        public List<Vector3> Points { get; }

        public List<float> Values { get; }

        public static IEnumerable<MarchingCube> GenerateCubes(int res, Chunk chunk)
        {
            var cubes = new List<MarchingCube>();

            var step = (int) Math.Max(Math.Max(chunk.Dimension.x, chunk.Dimension.y), chunk.Dimension.z) / res;
            for (var x = 0f; x <= chunk.Dimension.x +1f; x += step)
            {
                for (var y = 0f; y <= chunk.Dimension.y +1f; y += step)
                {
                    for (var z = 0f; z <= chunk.Dimension.z +1f; z += step)
                    {
                        var points = new List<Vector3>(8)
                        {
                            new Vector3(x, y, z + step),
                            new Vector3(x + step, y, z + step),
                            new Vector3(x + step, y + step, z + step),
                            new Vector3(x, y + step, z + step),
                            new Vector3(x, y, z),
                            new Vector3(x + step, y, z),
                            new Vector3(x + step, y + step, z),
                            new Vector3(x, y + step, z),
                        };


                        cubes.Add(new MarchingCube(points, chunk));
                    }
                }
            }

            return cubes;
        }
    }
}
