using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

    public static UIController instance;

    public Text StatusText;
    public Text EnemiesRemainingText;

    public GameObject ReadyText;
    public GameObject GoText;
    public GameObject ReadyOverlayPanel;

    void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update () {
        StatusText.text = "Shields: " + GameController.instance.CurrentGameState.CurrentShieldStrength
            + " - Hull: " + GameController.instance.CurrentGameState.CurrentHullStrength;
        EnemiesRemainingText.text = "Enemies Remaining: "
            + Mathf.Clamp(GameController.instance.CurrentGameState.NumberOfEnemiesToSpawn - GameController.instance.CurrentGameState.KilledEnemies,
                0,
                GameController.instance.CurrentGameState.NumberOfEnemiesToSpawn);
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
}
