using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoShieldsRoulette : IRouletteItem {

    public override void ApplyResult() {
        GameController.instance.CurrentGameState.PlayerShieldsMultiplier = 0;
        GameController.instance.CurrentGameState.ScoreMultiplerFromSlots += 2;
    }

    public override string Description() {
        return "Player Permanently loses all Shields.\nScore Multiplier +2";
    }

    public override bool IsOneTime() {
        return true;
    }
}
