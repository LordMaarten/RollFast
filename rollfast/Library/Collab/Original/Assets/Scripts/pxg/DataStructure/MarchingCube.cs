using System;
using System.Collections.Generic;
using UnityEngine;

namespace pxg.DataStructure
{
    public class MarchingCube
    {
        private MarchingCube(List<Vector3> p,  List<float> fl)
        {
            Points = p;
            Values = fl;
        }

        public List<Vector3> Points { get; }

        public List<float> Values { get; }

        public static IEnumerable<MarchingCube> GenerateCubes(int res,  Vector3 chunkSize, SimplexNoiseGenerator simplexNoiseGenerator)
        {
            var cubes = new List<MarchingCube>();

            var step = (int) Math.Max(Math.Max(chunkSize.x, chunkSize.y), chunkSize.z) / res;
            for (var x = 0f; x <= chunkSize.x +1f; x += step)
            {
                for (var y = 0f; y <= chunkSize.y +1f; y += step)
                {
                    for (var z = 0f; z <= chunkSize.z +1f; z += step)
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

                        List<float> fl = new List<float>(8);
                        foreach (var p in points)
                        {
                            fl.Add(simplexNoiseGenerator.coherentNoise(p.x, p.y, p.z));
                        }


                        cubes.Add(new MarchingCube(points, fl));
                    }
                }
            }

            return cubes;
        }
    }
}
