using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameStateManager
{
    int CollectedItems();
    void ItemCollected();
    int MaxItems();
    float Timer();
    float BestTime();
    bool IsGameOver();
    void RestartLevel();
}
