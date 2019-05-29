using System.Collections.Generic;
using UnityEngine;

namespace World.Generator
{
    public class CachedNoise: INoise
    {
        private Dictionary<string, float> cachedNoise;
        private INoise generator;

        public CachedNoise()
        {
            generator = new SimplexNoiseGenerator();
            cachedNoise = new Dictionary<string, float>();
        }
        
        public float coherentNoise(Vector3 pos, int octaves, int multiplier, float amplitude, float lacunarity, float persistence)
        {
            return coherentNoise(pos.x, pos.y, pos.z, octaves, multiplier, amplitude, lacunarity, persistence);
        }

        public float coherentNoise(float x, float y, float z, int octaves, int multiplier, float amplitude, float lacunarity, float persistence)
        {
            var key = (int)x + ":" + (int)y + ":" + (int)z;
            if (cachedNoise.ContainsKey(key))
            {
                //TODO if parameters changed we should recalculate
                return cachedNoise[key];
            }

            var v = generator.coherentNoise(x, y, z, octaves, multiplier, amplitude, lacunarity, persistence);
            cachedNoise.Add(key, v);

            return v;
        }
    }
}