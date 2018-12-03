using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

    public static UIController instance;

    public Text ShieldsText;
    public Text HullText;
    public Text EnemiesRemainingText;
    public Text ScoreText;
    public Text GameOverScoreText;
    public Text ScoreMultiplierText;
    public Text RouletteModifierDescription;
    public Text RouletteModifierTitle;

    public GameObject GunJammedPanel;
    public GameObject ReadyText;
    public GameObject GoText;
    public GameObject ReadyOverlayPanel;
    public GameObject NewGameMenuPanel;
    public GameObject ControlsMenuPanel;
    public GameObject OptionsMenuPanel;
    public GameObject TopHud;
    public GameObject BottomHud;
    public GameObject RoulettePanel;
    public GameObject GameOverScreen;

    void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update () {
        if (GameController.instance == null || GameController.instance.CurrentGameState == null) {
            return;
        }
        ShieldsText.text = GameController.instance.CurrentGameState.CurrentShieldStrength.ToString();
        HullText.text = Mathf.Clamp(GameController.instance.CurrentGameState.CurrentHullStrength, 0, int.MaxValue).ToString();
        EnemiesRemainingText.text = Mathf.Clamp(
                GameController.instance.CurrentGameState.NumberOfEnemiesToSpawn - GameController.instance.CurrentGameState.KilledEnemies,
                0,
                GameController.instance.CurrentGameState.NumberOfEnemiesToSpawn).ToString();
        string scoreText = GameController.instance.CurrentGameState.Score.ToString().PadLeft(12, '0');

        ScoreText.text = scoreText;
        GameOverScoreText.text = scoreText;

        int scoreMultiplier = GameController.instance.CurrentGameState.ScoreMultiplier + GameController.instance.CurrentGameState.ScoreMultiplerFromSlots - 2;
        ScoreMultiplierText.text = "x" + scoreMultiplier;

        if (RouletteController.instance.CurrentRouletteItem != null) {
            RouletteModifierDescription.text = RouletteController.instance.CurrentRouletteItem.GetDescription();
            RouletteModifierTitle.text = RouletteController.instance.CurrentRouletteItem.GetTitle();
        }
    }

    public void ResetRouletteText() {
        RouletteModifierDescription.text = "Engaging Improbability Drive...";
        RouletteModifierTitle.text = "Calculating Unknown Certainties...";
    }

    public void StartReadyAnimation() {
        StartCoroutine(PlayReadyAnimation());

        TopHud.SetActive(true);
        BottomHud.SetActive(true);
    }

    IEnumerator PlayReadyAnimation() {
        ReadyOverlayPanel.SetActive(true);

        ReadyText.SetActive(true);
        GoText.SetActive(false);

        yield return new WaitForSeconds(1);

        ReadyText.SetActive(false);
        GoText.SetActive(true);

        yield return new WaitForSeconds(1);

        ReadyText.SetActive(false);
        GoText.SetActive(false);
        ReadyOverlayPanel.SetActive(false);

        GameController.instance.IsAnimatingGoWindow = false;
    }

    public void GameOver() {
        GameOverScreen.SetActive(true);
        GunJammedPanel.SetActive(false);

        TopHud.SetActive(false);
        BottomHud.SetActive(false);
    }

    public void RestartGame() {
        NewGameMenuPanel.SetActive(false);
    }

    public void ShowGunJamPanel() {
        GunJammedPanel.SetActive(true);
    }

    public void HideGunJamPanel() {
        GunJammedPanel.SetActive(false);
    }

    public void ShowNewGameScreen() {
        GameOverScreen.SetActive(false);
        NewGameMenuPanel.SetActive(true);

        TopHud.SetActive(false);
        BottomHud.SetActive(false);
    }

    public void ShowControlsScreen() {
        NewGameMenuPanel.SetActive(false);
        ControlsMenuPanel.SetActive(true);
    }

    public void HideControlsScreen() {
        NewGameMenuPanel.SetActive(true);
        ControlsMenuPanel.SetActive(false);
    }

    public void ShowOptionsScreen() {
        NewGameMenuPanel.SetActive(false);
        OptionsMenuPanel.SetActive(true);
    }

    public void HideOptionsScreen() {
        NewGameMenuPanel.SetActive(true);
        OptionsMenuPanel.SetActive(false);
    }

    public void ShowRoulette() {
        RoulettePanel.SetActive(true);

        TopHud.SetActive(false);
        BottomHud.SetActive(false);
    }

    public void HideRoulette() {
        RoulettePanel.SetActive(false);

        TopHud.SetActive(true);
        BottomHud.SetActive(true);

        ResetRouletteText();
    }
}
