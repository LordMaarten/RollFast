using System;
using System.Collections.Generic;
using UnityEngine;
using World.Generator;

namespace World.DataStructure
{
    public class MarchingCube
    {
        private MarchingCube(List<Vector3> p, List<float> v)
        {
            Points = p;
            Values = v;
        }

        public List<Vector3> Points { get; }

        public List<float> Values { get; }

        public static IEnumerable<MarchingCube> GenerateCubes(int res, Vector3 chunkSize, Vector3 chunkPos, INoise generator)
        {
            var cubes = new List<MarchingCube>();

            var step = chunkSize.x / 4;
            for (var x = 0f; x < chunkSize.x; x += step)
            {
                for (var y = 0f; y < chunkSize.y; y += step)
                {
                    for (var z = 0f; z < chunkSize.z; z += step)
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

                        var values = new List<float>(8);
                        foreach (var point in points)
                        {
                            var val = (generator.coherentNoise(point + chunkPos, 4, 15, 3, 1, 1) + 1) / 2 * (point.y * chunkPos.y) / 4;
                            values.Add(val);
                        }

                        cubes.Add(new MarchingCube(points, values));
                    }
                }
            }

            return cubes;
        }

        public static IEnumerable<MarchingCube> GenerateCubes(int res, Chunk chunk, INoise generator)
        {
            var cubes = new List<MarchingCube>();

            var chunkSize = chunk.Dimension;

            var step = (int) Math.Max(Math.Max(chunkSize.x, chunkSize.y), chunkSize.z) / res;
            for (var x = 0f; x <= chunkSize.x; x += step)
            {
                for (var y = 0f; y <= chunkSize.y; y += step)
                {
                    for (var z = 0f; z <= chunkSize.z; z += step)
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

                        var values = new List<float>(8);
                        foreach (var point in points)
                        {
                            values.Add(chunk.getValue(point));
                        }

                        cubes.Add(new MarchingCube(points, values));
                    }
                }
            }

            return cubes;
        }
    }
}