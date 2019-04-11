using System;
using UnityEngine;
using World.Generator;

public class Chunk
{
    public float[,,] Values { get; set; }
    public Vector3 Dimension { get; set; }

    public void generateChunk(SimplexNoiseGenerator simplexNoiseGenerator, Vector3 chunkCoordinates, Vector3 chunkSize, NoisePresetConfig noisePreset)
    {
        int chunkX = (int) chunkCoordinates.x * (int) chunkSize.x;
        int chunkY = (int) chunkCoordinates.y * (int) chunkSize.y;
        int chunkZ = (int) chunkCoordinates.z * (int) chunkSize.z;

        Values = new float[(int) chunkSize.x +2, (int) chunkSize.y +2, (int) chunkSize.z +2];
        Dimension = chunkSize;
        
        for (int x = 0; x < chunkSize.x +2; x++)
        {
            for (int y = 0; y < chunkSize.y +2; y++)
            {
                for (int z = 0; z < chunkSize.z +2; z++)
                {
                    float noiseValue = (simplexNoiseGenerator.coherentNoise(chunkX + x, chunkY + y, chunkZ + z, noisePreset.octaves, noisePreset.multiplier, noisePreset.amplitude, noisePreset.lacunarity, noisePreset.persistence) + 1) / 2;
                    Values[x, y, z] = noiseManipulation(x, y, z, noiseValue);
                }
            }
        }
    }

    public float getValue(float x, float y, float z)
    {
        if (x >= Dimension.x + 1 || y >= Dimension.y + 1 || z >= Dimension.z + 1) return 1;

        return Values[(int) x, (int) y, (int) z];
    }
    
    public float getValue(Vector3 pos)
    {
        if (pos.x >= Dimension.x + 2 || pos.y >= Dimension.y + 2 || pos.z >= Dimension.z + 2)
        {
            return 1;
        }

        return Values[(int) pos.x, (int) pos.y, (int) pos.z];
    }

    private float noiseManipulation(float x, float y, float z, float noiseValue)
    {
        return noiseValue * y / 4;
    }
}