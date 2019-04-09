using UnityEngine;

namespace pxg.Generator
{
    public static class Mesh
    {
        public static UnityEngine.Mesh GenerateMesh(int res, Vector3 chunkSize, float tau, SimplexNoiseGenerator simplexNoiseGenerator)
        {
            var cubes = DataStructure.MarchingCube.GenerateCubes(res, chunkSize, simplexNoiseGenerator);
            var m = new DataStructure.Mesh(cubes, tau);

            var mesh = new UnityEngine.Mesh {vertices = m.Vertices.ToArray(), triangles = m.Triangles.ToArray()};
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}