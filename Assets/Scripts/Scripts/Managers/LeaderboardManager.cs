using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour, ILeaderboardManager
{
    private List<float> scores;
    public List<float> Scores() => scores;

    private static ILeaderboardManager instance;
    public static ILeaderboardManager Instance() => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Leaderboard instance assigned");
            instance = this;
        }
    }

    private void Start()
    {
        scores = new List<float>();
        GetSavedScores();
    }

    private void GetSavedScores()
    {
        scores = new List<float>();
        for(int i = 1; i <= 10; i++)
        {
            scores.Add(PlayerPrefs.GetFloat("Score" + i, 5940.999f));
        }
        UIManager.Instance().UpdateLeaderboardUI();
    }

    private void SaveScores()
    {
        for(int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetFloat("Score" + (i + 1), scores[i]);
        }
    }

    public bool IsHighScore(float seconds)
    {
        bool ans = false;

        foreach(float aux in scores)
        {
            if (seconds < aux)
            {
                ans = true;
                break;
            }
        }

        return ans;
    }

    public void RegisterHighScore(float seconds)
    {
        for(int i=0; i<10; i++)
        {
            if (seconds < scores[i])
            {
                scores.Insert(i, seconds);
                break;
            }
        }

        if (scores.Count > 10)
        {
            scores.RemoveAt(10);
        }

        SaveScores();
    }

    private void OnDestroy()
    {
        if (instance != null && instance == this)
        {
            Debug.Log("Leaderboard instance destroyed");
            instance = null;
        }
    }
}
