using UnityEngine;
using System.Collections;
using System;

public class Manager_Environment : MonoBehaviour
{
    public bool RandomSeed;
    public LayerMask GroundLayer;
    public EnvironmentSetDefition[] Definitions;
    public Transform Collector;
    public ScriptableEvent FinishedGeneration;

    private void Start()
    {
        if (Collector == null)
            Debug.LogError("Collector transform is not set of the environment generation.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    public void Generate()
    {
        Clean();

        float minX = transform.position.x - (transform.localScale.x / 2f);
        float maxX = transform.position.x + (transform.localScale.x / 2f);
        float minZ = transform.position.z - (transform.localScale.z / 2f);
        float maxZ = transform.position.z + (transform.localScale.z / 2f);

        // TODO: do it in a better way
        foreach (var definition in Definitions)
        {
            int generatedNum = (int)(definition.Density * transform.localScale.x * transform.localScale.y);
            int generatorSeed = definition.Seed;

            if (RandomSeed)
                generatorSeed = RandomHelper.Range(0, 10000);

            UnityEngine.Random.InitState(generatorSeed);

            for (int i = 0; i < generatedNum; i++)
            {
                float x = UnityEngine.Random.Range(minX, maxX);
                float z = UnityEngine.Random.Range(minZ, maxZ);
                float perlinX = ((maxX + x) / definition.Scale) + generatorSeed;
                float perlinZ = ((maxZ + z) / definition.Scale) + generatorSeed;
                if (Mathf.PerlinNoise(perlinX, perlinZ) >= definition.Strenght)
                {
                    Vector3 position = new Vector3(x, GetYPos(x, z), z);
                    Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0f, 359f), 0);
                    Transform parent = Collector;
                    Instantiate(definition.Prefabs.GetRandom(), position, rotation, parent);
                }
            }
        }

        FinishedGeneration?.Invoke();
    }

    private float GetYPos(float x, float z)
    {
        Ray ray = new Ray(new Vector3(x, 10f, z), Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, GroundLayer))
        {
            return hit.point.y;
        }

        return 0f;
    }

    private void Clean()
    {
        foreach (Transform child in Collector.transform)
        {
            Destroy(child.gameObject);
        }
    }

    [System.Serializable]
    public class EnvironmentSetDefition
    {
        public GameObject[] Prefabs;
        public int Seed = 0;
        public float Strenght = .5f;
        public float Scale = 1f;
        public float Density = 1f;
    }
}
