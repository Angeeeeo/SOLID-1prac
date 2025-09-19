using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] fallingPrefabs;
    public Material blueMaterial;
    public Material redMaterial;

    [Header("Game Settings")]
    public Transform spawnArea;
    public float spawnAreaSize = 5f;
    public float spawnHeight = 3f;
    public float delayBeforeStart = 5f;
    public float gameDuration = 15f;
    public float spawnInterval = 1f;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public Button startButton;

    [Header("World References")]
    public Transform floor;
    public Transform basket;

    private int score = 0;
    private bool gameRunning = false;
    private List<GameObject> activeObjects = new List<GameObject>();

    void Start()
    {
        startButton.onClick.AddListener(() => StartCoroutine(StartGame()));
        UpdateScore();
        UpdateTimer(0);
    }

    IEnumerator StartGame()
    {
        startButton.interactable = false;
        yield return new WaitForSeconds(delayBeforeStart);

        gameRunning = true;
        StartCoroutine(SpawnObjects());
        StartCoroutine(GameTimer());
    }

    IEnumerator SpawnObjects()
    {
        while (gameRunning)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        GameObject prefab = fallingPrefabs[Random.Range(0, fallingPrefabs.Length)];
        Vector3 randomPos = spawnArea.position + new Vector3(Random.Range(-spawnAreaSize, spawnAreaSize), 0, Random.Range(-spawnAreaSize, spawnAreaSize));

        GameObject obj = Instantiate(prefab, randomPos + Vector3.up * spawnHeight, Quaternion.identity);
        activeObjects.Add(obj);

        bool isBlue = Random.value > 0.5f;
        var renderer = obj.GetComponent<Renderer>();
        renderer.material = isBlue ? blueMaterial : redMaterial;

        var behavior = obj.AddComponent<FallingObject>();
        behavior.Init(this, floor.position.y, basket, isBlue);
    }

    IEnumerator GameTimer()
    {
        float remaining = gameDuration;
        while (remaining > 0)
        {
            UpdateTimer(remaining);
            yield return new WaitForSeconds(1f);
            remaining -= 1f;
        }

        UpdateTimer(0);
        EndGame();
    }

    void UpdateTimer(float timeLeft)
    {
        timerText.text = "Осталось: " + Mathf.CeilToInt(timeLeft).ToString() + " сек";
    }

    void EndGame()
    {
        gameRunning = false;

        foreach (var obj in activeObjects)
        {
            if (obj != null)
                Destroy(obj);
        }
        activeObjects.Clear();
    }

    public void RegisterCatch(bool correct)
    {
        score += correct ? 1 : -2;
        if (score < 0) score = 0;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Очки: " + score;
    }
}
