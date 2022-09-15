using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILeaderboardManager
{
    List<float> Scores();
    bool IsHighScore(float seconds);
    void RegisterHighScore(float seconds);
}
