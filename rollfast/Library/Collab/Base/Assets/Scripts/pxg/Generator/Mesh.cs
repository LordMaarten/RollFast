namespace pxg.Generator
{
    public static class Mesh
    {
        public static UnityEngine.Mesh GenerateMesh(int res, Chunk chunk, float tau)
        {
            var cubes = DataStructure.MarchingCube.GenerateCubes(res, chunk);
            var m = new DataStructure.Mesh(cubes, tau);

            var mesh = new UnityEngine.Mesh {vertices = m.Vertices.ToArray(), triangles = m.Triangles.ToArray()};
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}