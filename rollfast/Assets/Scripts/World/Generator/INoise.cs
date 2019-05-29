using UnityEngine;

namespace World.Generator
{
    public interface INoise
    {
        float coherentNoise(Vector3 pos, int octaves, int multiplier, float amplitude, float lacunarity, float persistence);
        float coherentNoise(float x, float y, float z, int octaves, int multiplier, float amplitude, float lacunarity, float persistence);
    }
}