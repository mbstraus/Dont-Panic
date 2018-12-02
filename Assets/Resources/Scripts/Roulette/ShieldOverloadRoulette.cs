using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldOverloadRoulette : IRouletteItem {

    public override void ApplyResult() {
        GameController.instance.CurrentGameState.PlayerShieldsMultiplier = GameController.instance.CurrentGameState.PlayerShieldsMultiplier / 2;
        GameController.instance.CurrentGameState.ScoreMultiplerFromSlots += 2;
    }

    public override string GetTitle() {
        return "Shield Overload!";
    }

    public override string GetDescription() {
        return "Shield Strength halved (Rounded Down).\nScore Multiplier +2";
    }

    public override bool IsOneTime() {
        return false;
    }
}
