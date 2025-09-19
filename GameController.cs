using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button startButton;
    public float delayBeforeStart = 5f;
    public float spawnInterval = 1f;

    public ISpawner spawner;
    public ITimer timer;

    private bool gameRunning = false;

    void Start()
    {
        startButton.onClick.AddListener(() => StartCoroutine(StartGame()));
    }

    IEnumerator StartGame()
    {
        startButton.interactable = false;
        yield return new WaitForSeconds(delayBeforeStart);

        gameRunning = true;
        timer.StartTimer(EndGame);
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (gameRunning)
        {
            spawner.SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void EndGame()
    {
        gameRunning = false;
        spawner.ClearObjects();
    }
}