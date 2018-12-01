using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RouletteController : MonoBehaviour {

    public static RouletteController instance;

    public GameObject SlotSpinner;
    public GameObject[] RouletteResultGameObjects;
    public GameObject RoulettePanel;
    public float SpinAnimationDurationSec = 2f;

    private GameObject currentRouletteItem;

    void Awake() {
        instance = this;
    }

    public void DoRouletteSpin() {
        int rouletteResult = Random.Range(0, RouletteResultGameObjects.Length);
        StartCoroutine(doRouletteSpin(RouletteResultGameObjects[rouletteResult]));
    }

    IEnumerator doRouletteSpin(GameObject result) {
        foreach (GameObject go in RouletteResultGameObjects) {
            go.SetActive(false);
        }
        float animationDuration = SpinAnimationDurationSec;
        SlotSpinner.SetActive(true);

        while (animationDuration >= 0) {
            animationDuration -= Time.deltaTime;
            yield return null;
        }

        SlotSpinner.SetActive(false);

        // TODO: Here is where we would set some state based on the result.
        result.SetActive(true);
        currentRouletteItem = result;
    }

    public void ShowRoulette() {
        GameController.instance.CurrentGameState.IsDoingRoulette = true;
        RoulettePanel.SetActive(true);

        DoRouletteSpin();
    }

    public void AcceptRoulette() {
        IRouletteItem item = (IRouletteItem) currentRouletteItem.GetComponent(typeof(IRouletteItem));
        item.ApplyResult();

        currentRouletteItem = null;
        GameController.instance.CurrentGameState.IsDoingRoulette = false;
        SpawnController.instance.StartNextWave();
        GameController.instance.UpdateGameState();
        RoulettePanel.SetActive(false);
    }
}
