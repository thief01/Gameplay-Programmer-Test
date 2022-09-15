using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    playing,
    waitForRestart
}
public class GameManager : Singleton<GameManager>
{
    public GameState GameState { get; set; }
    
    public int Points { get; }

    private int points=0;

    public void AddScore()
    {
        points++;
    }
}
