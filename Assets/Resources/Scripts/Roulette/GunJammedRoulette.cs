using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunJammedRoulette : IRouletteItem {

    public override void ApplyResult() {
        GameController.instance.CurrentGameState.IsPlayerGunJamming = true;
        GameController.instance.CurrentGameState.ScoreMultiplerFromSlots += 2;
    }

    public override string GetDescription() {
        return "Gun will periodically jam\nScore Multiplier +2";
    }

    public override string GetTitle() {
        return "Uhh, sir, I think our gun may be jammed...";
    }

    public override bool IsOneTime() {
        return true;
    }
}
