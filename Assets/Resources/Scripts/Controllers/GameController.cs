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
    }

    public void StartNewWave() {
        IsAnimatingGoWindow = true;
        UIController.instance.StartReadyAnimation();
    }
}
