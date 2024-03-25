using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;

    [SerializeField] private int mapWidth = 256;
    private int mapDepth = 256;
    [SerializeField] private float mapScale = 10;

    [SerializeField] private Material[] mapMaterials;
    [SerializeField] private List<TileFactoryData> tileFactories;

    private GameObject[,] map;
    private float offsetX, offsetZ;

    private GameObject mapHolder;

    private void Awake()
    {
        mapDepth = mapWidth;
        map = new GameObject[mapWidth, mapDepth];
    }

    private void Start()
    {
        Generate();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
            GenerateTexture();

        if (Input.GetKeyDown(KeyCode.G))
            Generate();

        if (Input.GetKeyDown(KeyCode.M))
            GenerateMap();

        if (Input.GetKeyDown(KeyCode.Return))
            mapHolder.SetActive(!mapHolder.activeInHierarchy);
    }

    [ContextMenu("Generate")]
    private void Generate()
    {
        GenerateTexture();
        GenerateMap();
    }

    [ContextMenu("Generate Map")]
    private void GenerateMap()
    {
        mapDepth = mapWidth;
        Destroy(mapHolder);
        mapHolder = new GameObject("MapHolder");
        //string debugMsg = string.Empty;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0 ; z < mapDepth; z++)
            {
                map[x, z] = Instantiate(tilePrefab, new Vector3(x, 0, z), tilePrefab.transform.rotation, mapHolder.transform);
                var perlinValue = GeneratePerlinNoiseValue(x, z);
          //      debugMsg += perlinValue + ", ";
                ChangePerlinMaterial(map[x, z], perlinValue);
            }
        }
        //Debug.Log(debugMsg);
    }
    
    [ContextMenu("Generate Texture")]
    private void GenerateTexture()
    {
        offsetX = Random.Range(-5000, 5000);
        offsetZ = Random.Range(-5000, 5000);
        mapDepth = mapWidth;
        var perlinTexture = GenerateNoiseTexture(mapWidth, mapDepth);
        GetComponent<Renderer>().material.mainTexture = perlinTexture;
    }

    private Texture2D GenerateNoiseTexture(int width, int depth)
    {
        var texture = new Texture2D(width, depth);

        for (int x = 0; x < width; x++)
        {
            for(int z = 0;z < depth; z++)
            {
                Color color = ApplyNoise(x,z);
                texture.SetPixel(x, z, color);
            }
        }

        texture.Apply();
        return texture;
    }

    private float GeneratePerlinNoiseValue(int width, int depth)
    {
        float x = (width + offsetX) / mapWidth * mapScale;
        float z = (depth + offsetZ) / mapDepth * mapScale;
        return Mathf.Clamp01(Mathf.PerlinNoise(x, z));
    }

    private void ChangePerlinMaterial(GameObject go, float perlinValue)
    {
        Material material = null;
        switch (perlinValue)
        {
            case <= 0.5f:  material = mapMaterials[0]; break;  // City
            case <= 0.75f:   material = mapMaterials[1]; break;  // Grass
            case <= 0.9f:  material = mapMaterials[2]; break;  // Water
            default:        material = mapMaterials[3]; break;  // Lava
        }
        go.GetComponent<Renderer>().material = material;
    }

    private Color ApplyNoise(int x, int z)
    {
        float coordX = (x + offsetX) / mapWidth * mapScale;
        float coordZ = (z + offsetZ) / mapDepth * mapScale;
        //float coordX = (float)x / mapWidth * mapScale;
        //float coordZ = (float)z / mapDepth * mapScale;
        float sample = Mathf.PerlinNoise(coordX, coordZ);
        return new Color(sample, sample, sample);
    }
}

[System.Serializable]
public class TileFactoryData
{
    public string factoryName;
    public AbstractFactoryTile factory;
}