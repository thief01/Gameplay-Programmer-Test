using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILose : MonoBehaviour
{
    [SerializeField] private GameObject restartPanel;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
    }

    public void OnGameStateChanged()
    {
        restartPanel.SetActive(GameManager.Instance.GameState == GameState.waitForRestart);
    }

    public void RestartGame()
    {
        GameManager.Instance.SetGameState(GameState.playing);
    }
}
