using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    private Transform playerTransform;

    // Tiles prefabs array
    public GameObject[] tilesPrefabs;

    public float tileLength = 40f;          // size tile in z
    private int amnTilesOnScreen = 10;      // amount of tiles permited in scene
    private int lastPrefabIndex = 0;        // index last tile spawned
    private int tileCount = 0;              // amount of tiles placed

    private List<GameObject> activeTiles;   // Tile list in scene

    private float spawnZ = -2.0f;           // z position to spawn next tile
    private float safeZone = 50.0f;         // safeZone to spawn tiles with no obstacles

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // Init list
        activeTiles = new List<GameObject>();

        FirstTiles();
    }


    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
            SumoUnTile();
        }
    }


    private void FirstTiles()
    {
        // primer puesta en escena de la cantidad maxima de tiles permitidos
        for (int i = 0; i < amnTilesOnScreen; i++)
        {
            // los tres primeros seeran el indice 0 que debe corresponer a uno sin obstaculos
            if (i < 3)
            {
                SpawnTile(0);
                SumoUnTile();
            }
            else
            {
                SpawnTile();
                SumoUnTile();
            }
        }
    }


    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
            go = Instantiate(tilesPrefabs[RandomPrefabIndex()]) as GameObject;
        else
            go = Instantiate(tilesPrefabs[prefabIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    void SumoUnTile()
    {
        tileCount++;
    }

    private int RandomPrefabIndex()
    {
        // Si solo fuese un prefab
        if (tilesPrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilesPrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
