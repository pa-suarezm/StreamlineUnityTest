using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIManager
{
    void UpdateGameOverScreen();
    void UpdateJetpackUI();
    void UpdateScoreUI();
    void UpdateBestScoreUI();
    void UpdateLeaderboardUI();
}
