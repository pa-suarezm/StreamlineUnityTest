using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IUIManager
{
    private static IUIManager instance;
    public static IUIManager Instance() => instance;

    private IPlayer player;

    [Header("Score")]
    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtBestScore;

    [Header("Jetpack")]
    [SerializeField] private Slider jetpackFuelBar;

    [Header("Game Over Screen")]
    [SerializeField] private GameObject gameOverScreen;

    [Header("Leaderboard")]
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private Text txtLeaderboard;

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
        //Hides cursor on focus
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        Application.targetFrameRate = 144;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        jetpackFuelBar.minValue = 0;
        jetpackFuelBar.maxValue = player.MaxFuel();
        UpdateGameOverScreen();
        UpdateBestScoreUI();
        leaderboard.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleLeaderboard();
        }
    }

    private void ToggleLeaderboard()
    {
        leaderboard.SetActive(!leaderboard.activeInHierarchy);
    }

    public void UpdateGameOverScreen()
    {
        gameOverScreen.SetActive(GameStateManager.Instance().IsGameOver());
    }

    public void UpdateScoreUI()
    {
        txtScore.text = SecondsToTimerText(GameStateManager.Instance().Timer());
    }

    public void UpdateBestScoreUI()
    {
        txtBestScore.text = "Best: " + SecondsToTimerText(GameStateManager.Instance().BestTime());
    }

    public void UpdateLeaderboardUI()
    {
        txtLeaderboard.text = "";
        int i = 1;
        Debug.Log("Leaderboard instance is null " + (LeaderboardManager.Instance() == null).ToString());
        foreach(float scoreAux in LeaderboardManager.Instance().Scores())
        {
            txtLeaderboard.text += (i + ". " + SecondsToTimerText(scoreAux) + "\n");
            i++;
        }
    }

    private string SecondsToTimerText(float seconds)
    {
        if (seconds == 0)
        {
            return "";
        }

        string[] rawTimer = seconds.ToString().Split('.');

        //Miliseconds
        string txtMiliseconds = "";
        if (rawTimer.Length == 2)
        {
            txtMiliseconds = rawTimer[1].Substring(0, Mathf.Min(3, rawTimer[1].Length));
        }
        while (txtMiliseconds.Length < 3)
        {
            txtMiliseconds += "0";
        }

        //Seconds
        int onlySeconds = ((int)(seconds % 60));
        string txtSeconds = "";
        if (onlySeconds < 10)
        {
            txtSeconds = "0";
        }
        txtSeconds += onlySeconds.ToString();

        //Minutes
        int onlyMinutes = ((int)Mathf.Floor(seconds / 60));
        string txtMinutes = "";
        if (onlyMinutes < 10)
        {
            txtMinutes = "0";
        }
        txtMinutes += onlyMinutes.ToString();

        //Answer
        return (txtMinutes + ":" + txtSeconds + ":" + txtMiliseconds);
    }

    public void UpdateJetpackUI()
    {
        jetpackFuelBar.value = player.CurrentFuel();
    }

    private void OnDestroy()
    {
        if (instance != null && instance == this)
        {
            instance = null;
        }
    }

}
