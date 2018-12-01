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

    public GameObject ReadyText;
    public GameObject GoText;
    public GameObject ReadyOverlayPanel;

    public GameObject GameOverScreen;

    void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update () {
        ShieldsText.text = GameController.instance.CurrentGameState.CurrentShieldStrength.ToString();
        HullText.text = GameController.instance.CurrentGameState.CurrentHullStrength.ToString();
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
            RouletteModifierDescription.text = RouletteController.instance.CurrentRouletteItem.Description();
        }
    }

    public void StartReadyAnimation() {
        StartCoroutine(PlayReadyAnimation());
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
    }

    public void RestartGame() {
        GameOverScreen.SetActive(false);
    }
}
