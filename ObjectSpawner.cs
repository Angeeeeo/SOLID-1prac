using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour, ISpawner
{
    public GameObject[] fallingPrefabs;
    public Material blueMaterial;
    public Material redMaterial;

    public Transform spawnArea;
    public float spawnAreaSize = 5f;
    public float spawnHeight = 3f;

    public Transform floor;
    public Transform basket;

    public ICatchHandler catchHandler; // Инъекция зависимости

    private List<GameObject> activeObjects = new List<GameObject>();

    public void SpawnObject()
    {
        GameObject prefab = fallingPrefabs[Random.Range(0, fallingPrefabs.Length)];
        Vector3 randomPos = spawnArea.position + new Vector3(Random.Range(-spawnAreaSize, spawnAreaSize), 0, Random.Range(-spawnAreaSize, spawnAreaSize));

        GameObject obj = Instantiate(prefab, randomPos + Vector3.up * spawnHeight, Quaternion.identity);
        activeObjects.Add(obj);

        bool isBlue = Random.value > 0.5f;
        var renderer = obj.GetComponent<Renderer>();
        renderer.material = isBlue ? blueMaterial : redMaterial;

        var behavior = obj.AddComponent<FallingObject>();
        behavior.Init(catchHandler, floor.position.y, basket, isBlue);
    }

    public void ClearObjects()
    {
        foreach (var obj in activeObjects)
        {
            if (obj != null)
                Destroy(obj);
        }
        activeObjects.Clear();
    }
}