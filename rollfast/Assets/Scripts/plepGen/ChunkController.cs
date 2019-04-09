using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private Vector3 chunksRendered = new Vector3(1, 1, 1);
    [SerializeField] private Vector3 chunkSize = new Vector3(16f, 16f, 16f);
    [SerializeField] private NoisePresetConfig noisePreset;
    [SerializeField] private GameObject playerTransform;
    [SerializeField] private Material mat;
    [SerializeField] private GameObject _chunk;
    [SerializeField] private float _colorTimer = 0f;

    private SimplexNoiseGenerator SimplexNoiseGenerator = new SimplexNoiseGenerator();

    private List<Vector3> chunkPos = new List<Vector3>();
    private List<string> colors = new List<string>();
    private string _currentColor;

    public void Start()
    {
        colors.Add("#1abc9c");
        colors.Add("#2ecc71");
        colors.Add("#3498db");
        colors.Add("#9b59b6");
        colors.Add("#34495e");
        colors.Add("#f1c40f");
        colors.Add("#e67e22");
        colors.Add("#e74c3c");
        colors.Add("#bdc3c7");
        colors.Add("#7f8c8d");

        Random random = new Random();
        _currentColor = colors[random.Next(colors.Count)];


        if (noisePreset == null)
        {
            throw new Exception("plox provide a NoisePresetConfig");
        }

        newColorTimer();
    }

    private void FixedUpdate()
    {
        generateChunks();
    }

    private void newColorTimer()
    {
        Random random = new Random();
        float changeTime = (float) random.NextDouble() * (10 - 2) + 2;

        _colorTimer = Time.time + changeTime;
    }

    private void generateChunks()
    {
        if (_colorTimer <= Time.time)
        {
            Random random = new Random();
            _currentColor = colors[random.Next(colors.Count)];
            newColorTimer();
        }

        float chunkOffsetX = playerTransform.transform.position.x != 0f ? (float) Math.Floor((playerTransform.transform.position.x) / chunkSize.x) : 0f;
        float chunkOffsetY = 0f;
        float chunkOffsetZ = playerTransform.transform.position.z != 0f ? (float) Math.Floor((playerTransform.transform.position.z) / chunkSize.z) : 0f;

        for (int x = 0; x < chunksRendered.x; x++)
        {
            for (int y = 0; y < chunksRendered.y; y++)
            {
                for (int z = 0; z < chunksRendered.z; z++)
                {
                    float chunkX = (chunkSize.x) * (x + chunkOffsetX);
                    float chunkY = (chunkSize.y) * (y + chunkOffsetY);
                    float chunkZ = (chunkSize.z) * (z + chunkOffsetZ);

                    if (!chunkPos.Contains(new Vector3(chunkX, chunkY, chunkZ)))
                    {
                        StartCoroutine(generateChunk(x, chunkOffsetX, z, chunkOffsetZ, y, chunkOffsetY, chunkX, chunkY, chunkZ));
                    }
                }
            }
        }
    }

    IEnumerator generateChunk(int x, float chunkOffsetX, int z, float chunkOffsetZ, int y, float chunkOffsetY, float chunkX, float chunkY, float chunkZ)
    {
        //Chunk chunk = new Chunk();
        //chunk.generateChunk(SimplexNoiseGenerator, new Vector3(x + chunkOffsetX, y + chunkOffsetY, z + chunkOffsetZ), chunkSize, noisePreset);
        //var m = pxg.Generator.Mesh.GenerateMesh((int) chunkSize.x, chunk, noisePreset.tau, SimplexNoiseGenerator);
        var pos = new Vector3((x + chunkOffsetX) * chunkSize.x, (y + chunkOffsetY) * chunkSize.y, (z + chunkOffsetZ) * chunkSize.z);
        var m = pxg.Generator.Mesh.GenerateMesh((int) chunkSize.x, chunkSize, pos, noisePreset.tau, SimplexNoiseGenerator);
        chunkPos.Add(new Vector3(chunkX, chunkY, chunkZ));
        var go = Instantiate(_chunk, new Vector3(chunkX - (chunkSize.x / 2 * chunksRendered.x), chunkY - (chunkSize.y / 2 * chunksRendered.y), chunkZ - (chunkSize.z / 2 * chunksRendered.z)), Quaternion.identity, gameObject.transform);
        go.GetComponent<MeshCollider>().sharedMesh = m;
        go.GetComponent<MeshFilter>().mesh = m;

        Color newColor = new Color();
        ColorUtility.TryParseHtmlString(_currentColor, out newColor);
        Material newMat = new Material(mat);
        newMat.color = newColor;
        go.GetComponent<MeshRenderer>().material = newMat;

        yield return new WaitForFixedUpdate();
    }
}