using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NoisePresetConfig", menuName = "NoisePresetConfig", order = 1)]
public class NoisePresetConfig : ScriptableObject
{
    public int octaves = 4;
    public int multiplier = 15;
    public float amplitude = 0.25f;
    public float lacunarity = 2;
    public float persistence = 0.5f;
    public float tau = 0.47f;
}
