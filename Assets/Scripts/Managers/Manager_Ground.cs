using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

public class Manager_Ground : MonoBehaviour
{
    public float ZoomLevel;
    public float Elevation;
    public int Seed;
    public bool RandomSeed;
    public int MeshSizeRatio;
    public Vector2 Size;
    public Vector2Int TextureSize;
    public ScriptableEvent FinishedGeneration;

    public MeshFilter _Filter;
    public MeshCollider _Collider;
    public MeshCollider _NavMeshCollider;
    public Renderer _Renderer;
    public GameObject _ReplacementGround;
    private Material _Material;

    private int _generationSeed;

    private void Start()
    {
        _ReplacementGround.SetActive(false);
        Initialize();
        Generate();
    }

    private void Initialize()
    {
        _Material = _Renderer.material; // NoiseTexture
        _generationSeed = RandomSeed ? (int)UnityEngine.Random.Range((int)0, (int)10000) : Seed;
    }

    private void Generate()
    {
        float[,] heightTexture = GetTexture();
        Mesh mesh = new Mesh();
        Vector2Int meshSize = TextureSize / MeshSizeRatio;

        Vector3[] vertices = new Vector3[meshSize.x * meshSize.y];
        int[] tris = new int[((meshSize.x - 1) * (meshSize.y - 1)) * 6];
        Vector2[] uvs = new Vector2[vertices.Length];

        for (int x = 0; x < meshSize.x; x++)
        {
            float xPos = (x * (Size.x / meshSize.x)) - (Size.x / 2);

            for (int y = 0; y < meshSize.y; y++)
            {
                float zPos = (y * (Size.y / meshSize.y)) - (Size.y / 2);
                Vector3 position = new Vector3(xPos, (1 - heightTexture[x * MeshSizeRatio, y * MeshSizeRatio]) * Elevation, zPos) + transform.position;
                vertices[(x * meshSize.y) + y] = position;
                uvs[(x * meshSize.y) + y] = new Vector2((x / (float)meshSize.x), (y / (float)meshSize.y));
            }
        }

        int v = 0;
        for (int i = 0; i < tris.Length; i += 6)
        {
            tris[i] = v;
            tris[i + 1] = tris[i] + meshSize.y + 1;
            tris[i + 2] = tris[i] + meshSize.y;
            tris[i + 3] = tris[i];
            tris[i + 4] = tris[i] + 1;
            tris[i + 5] = tris[i] + meshSize.y + 1;
            v++;

            if ((v + 1) % meshSize.y == 0)
                v++;
        }

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.uv = uvs;

        mesh.RecalculateNormals();

        _Filter.mesh = mesh;
        _Collider.sharedMesh = mesh;
        _NavMeshCollider.sharedMesh = mesh;

        FinishedGeneration?.Invoke();
    }

    private float[,] GetTexture()
    {
        Texture2D noiseTex = new Texture2D(TextureSize.x, TextureSize.y);
        Color[] pix = new Color[noiseTex.width * noiseTex.height];
        float[,] values = new float[noiseTex.width, noiseTex.height];

        int y = 0;

        while (y < noiseTex.height)
        {
            int x = 0;
            while (x < noiseTex.width)
            {
                float xCoord = _generationSeed + (((float)x / (float)noiseTex.width) * ZoomLevel);
                float yCoord = _generationSeed + (((float)y / (float)noiseTex.height) * ZoomLevel);
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                pix[y * noiseTex.width + x] = new Color(sample, sample, sample);
                values[x, y] = sample;
                x++;
            }
            y++;
        }

        // Copy the pixel data to the texture and load it into the GPU.
        noiseTex.SetPixels(pix);
        noiseTex.Apply();

        _Material.SetTexture("NoiseTexture", noiseTex);
        return values;
    }
}
