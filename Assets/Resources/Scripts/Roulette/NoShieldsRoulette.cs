using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoShieldsRoulette : MonoBehaviour, IRouletteItem {

    public void ApplyResult() {
        GameController.instance.CurrentGameState.PlayerShieldsMultiplier = 0;
    }
}
