using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace pxg.DataStructure
{
    public class Mesh
    {
        public Mesh(IEnumerable<MarchingCube> cubes, float tau)
        {
            Vertices = new List<Vector3>();
            Triangles = new List<int>();

            foreach (var cube in cubes)
            {
                CreateTriangle(cube.Points, cube.Values, tau);
            }

            //var s = false;
            for (var i = 0; i < Vertices.Count-3; i += 3)
            {
                Triangles.Add(i);
                Triangles.Add(i + 2);
                Triangles.Add(i + 1);
            }            
        }

        public List<Vector3> Vertices { get; }
        public List<int> Triangles { get; }

        private void CreateTriangle(IReadOnlyList<Vector3> points, IReadOnlyList<float> values, float tau)
        {
            var caseIndex = 0;
            for (var i = 0; i < values.Count; i++)
            {
                if (values[i] > tau)
                {
                    caseIndex += (int) Math.Pow(2, i);
                }
            }

            var edges = new int[15];
            Array.Copy(CaseDictionary.Faces, caseIndex * 15, edges, 0, 15);
            foreach (var e in edges)
            {
                if (e == -1) continue;

                var p1i = EdgeToPoint.GetPointIndices(e)[0];
                var p2i = EdgeToPoint.GetPointIndices(e)[1];

                var p = ComputePosition(points[p1i], points[p2i], values[p1i], values[p2i], tau);
                Vertices.Add(p);
            }
        }

        private static Vector3 ComputePosition(Vector3 p1, Vector3 p2, float v1, float v2, float tau)
        {
            var t = (tau-v1) / (v2 - v1);
            var p = new Vector3(0, 0, 0);
            p = p + (p1 * (1f - t));
            p = p + (p2 * t);

            return p;
        }
    }
}