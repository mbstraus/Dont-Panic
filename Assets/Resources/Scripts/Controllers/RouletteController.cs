using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RouletteController : MonoBehaviour {

    public static RouletteController instance;

    public GameObject RoulettePanel;
    public float SpinAnimationDurationSec = 2f;

    public IRouletteItem CurrentRouletteItem;

    void Awake() {
        instance = this;
    }

    public void InitiateRouletteSpin() {
        int rouletteResult = Random.Range(0, GameController.instance.GetNumberOfAvailableRouletteItems());

        GameController.instance.DisableRouletteItems();
        float animationDuration = SpinAnimationDurationSec;

        IRouletteItem result = GameController.instance.GetRouletteItem(rouletteResult);

        result.gameObject.SetActive(true);
        CurrentRouletteItem = result;
    }

    public void ShowRoulette() {
        GameController.instance.CurrentGameState.IsDoingRoulette = true;
        RoulettePanel.SetActive(true);

        InitiateRouletteSpin();
    }

    public void AcceptRoulette() {
        IRouletteItem item = (IRouletteItem) CurrentRouletteItem.GetComponent(typeof(IRouletteItem));
        item.ApplyResult();

        if (item.IsOneTime()) {
            GameController.instance.RemoveRouletteItem(item);
        }

        CurrentRouletteItem = null;
        GameController.instance.CurrentGameState.IsDoingRoulette = false;
        SpawnController.instance.StartNextWave();
        GameController.instance.UpdateGameState();
        RoulettePanel.SetActive(false);

        UIController.instance.ResetRouletteText();
    }

    public void RejectRoulette() {
        GameController.instance.CurrentGameState.NumberOfEnemiesMultiplier += 1;
        GameController.instance.CurrentGameState.DelayBetweenSpawnsSec /= 2;

        CurrentRouletteItem = null;
        GameController.instance.CurrentGameState.IsDoingRoulette = false;
        SpawnController.instance.StartNextWave();
        GameController.instance.UpdateGameState();
        RoulettePanel.SetActive(false);

        GameController.instance.CurrentGameState.ScoreMultiplerFromSlots += 2;

        UIController.instance.ResetRouletteText();
    }
}
