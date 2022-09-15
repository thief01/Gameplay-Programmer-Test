using System;
using System.Collections;
using System.Collections.Generic;
using Patterns;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public enum GameState
    {
        playing,
        waitForRestart
    }

    public class GameManager : Singleton<GameManager>
    {
        public GameState GameState { get; private set; }
        public UnityEvent OnGameStateChanged = new UnityEvent();

        public int Points => points;

        private int points=0;

        private PlayerInput player;
        private void Awake()
        {
            player = FindObjectOfType<PlayerInput>();
        }

        public void AddScore()
        {
            points++;
        }

        public void SetGameState(GameState gameState)
        {
            if (gameState == GameState.waitForRestart)
            {
                Time.timeScale = 0;
            }

            if (gameState == GameState.playing)
            {
                Time.timeScale = 1;
                points = 0;
            }

            GameState = gameState;
            OnGameStateChanged.Invoke();
        }
    }
}