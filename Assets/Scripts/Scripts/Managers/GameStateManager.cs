using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour, IGameStateManager
{
    private float timer;
    public float Timer() => timer;

    private static float bestTime;
    public float BestTime() => bestTime;

    private bool gameOver;
    public bool IsGameOver() => gameOver;

    private int collectedItems;
    public int CollectedItems() => collectedItems;

    private int maxItems = 1;
    public int MaxItems() => maxItems;

    private static IGameStateManager instance;
    public static IGameStateManager Instance() => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        gameOver = false;
        maxItems = GameObject.FindGameObjectsWithTag("Collectible").Length;
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        if (Input.GetAxis("Restart") != 0)
        {
            RestartLevel();
        }
    }

    IEnumerator StartTimer()
    {
        timer = 0;
        while(!gameOver)
        {
            yield return null;
            timer += Time.deltaTime;
            UIManager.Instance().UpdateScoreUI();
        }

        if (LeaderboardManager.Instance().IsHighScore(timer))
        {
            LeaderboardManager.Instance().RegisterHighScore(timer);
            UIManager.Instance().UpdateLeaderboardUI();
        }

        yield return true;
    }

    public void ItemCollected()
    {
        collectedItems++;
        if (collectedItems == maxItems)
        {
            gameOver = true;
            UIManager.Instance().UpdateGameOverScreen();
        }
    }

    public void RestartLevel()
    {
        gameOver = false;
        SceneManager.UnloadScene(SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private void OnDestroy()
    {
        if (instance != null && instance == this)
        {
            instance = null;
        }
    }
}
