using UnityEngine;
using World.DataStructure;

namespace World.Generator
{
    public static class Mesh
    {
        public static UnityEngine.Mesh GenerateMesh(int res, Vector3 chunkSize, Vector3 chunkPos, float tau, SimplexNoiseGenerator generator)
        {
            var cubes = MarchingCube.GenerateCubes(res, chunkSize, chunkPos, generator);
            var m = new DataStructure.Mesh(cubes, tau);

            var mesh = new UnityEngine.Mesh {vertices = m.Vertices.ToArray(), triangles = m.Triangles.ToArray()};
            mesh.RecalculateNormals();
            return mesh;
        }

        public static UnityEngine.Mesh GenerateMesh(int res, Chunk chunk, float tau, SimplexNoiseGenerator generator)
        {
            var cubes = MarchingCube.GenerateCubes(res, chunk, generator);
            var m = new DataStructure.Mesh(cubes, tau);

            var mesh = new UnityEngine.Mesh {vertices = m.Vertices.ToArray(), triangles = m.Triangles.ToArray()};
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}