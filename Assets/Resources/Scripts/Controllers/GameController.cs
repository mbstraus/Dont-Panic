using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public Player Player;

    public bool IsAnimatingGoWindow = false;

    public GameState CurrentGameState;

    public IRouletteItem[] RouletteItems;

    void Awake() {
        instance = this;

        CurrentGameState = new GameState(RouletteItems);
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
        CurrentGameState = new GameState(RouletteItems);
        SpawnController.instance.StartNextWave();
        UIController.instance.RestartGame();
    }

    public bool TakePlayerDamage() {
        bool isTakingShieldDamage = false;
        if (CurrentGameState.CurrentShieldStrength > 0) {
            CurrentGameState.CurrentShieldStrength -= 1;
            isTakingShieldDamage = true;
        } else {
            CurrentGameState.CurrentHullStrength -= 1;
        }
        CurrentGameState.ResetScoreMultiplier();
        return isTakingShieldDamage;
    }

    public int GetNumberOfAvailableRouletteItems() {
        return CurrentGameState.AvailableRouletteItems.Count;
    }

    public void RemoveRouletteItem(IRouletteItem item) {
        item.gameObject.SetActive(false);
        CurrentGameState.AvailableRouletteItems.Remove(item);
    }

    public IRouletteItem GetRouletteItem(int index) {
        return CurrentGameState.AvailableRouletteItems[index];
    }

    public void DisableRouletteItems() {
        foreach(IRouletteItem item in CurrentGameState.AvailableRouletteItems) {
            item.gameObject.SetActive(false);
        }
    }
}
