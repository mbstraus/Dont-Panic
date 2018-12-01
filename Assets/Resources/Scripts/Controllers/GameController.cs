using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public Player Player;

    public bool IsAnimatingGoWindow = false;

    public GameState CurrentGameState;

    void Awake() {
        instance = this;
        CurrentGameState = new GameState();
    }

    public void UpdateGameState() {
        CurrentGameState.RecalculateStats();
    }

    public void KillEnemy() {
        CurrentGameState.KilledEnemies += 1;

        CurrentGameState.AddScore();
    }

    public void StartNewWave() {
        IsAnimatingGoWindow = true;
        UIController.instance.StartReadyAnimation();
    }

    public void GameOver() {
        CurrentGameState.IsGameOver = true;
        UIController.instance.GameOver();
    }

    public void RestartGame() {
        CurrentGameState = new GameState();
        SpawnController.instance.StartNextWave();
        UIController.instance.RestartGame();
    }

    public void TakePlayerDamage() {
        if (CurrentGameState.CurrentShieldStrength > 0) {
            CurrentGameState.CurrentShieldStrength -= 1;
        } else {
            CurrentGameState.CurrentHullStrength -= 1;
        }
        CurrentGameState.ResetScoreMultiplier();
    }
}
