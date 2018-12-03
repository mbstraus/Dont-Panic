using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWhaleRoulette : IRouletteItem {

    public override void ApplyResult() {
        GameController.instance.CurrentGameState.IsPlayerAWhale = true;
        GameController.instance.CurrentGameState.PlayerMoveRate /= 2;
        GameController.instance.CurrentGameState.ScoreMultiplerFromSlots += 3;
    }

    public override string GetDescription() {
        return "Player turns into a Sperm Whale\nMovement Speed 1/2\nScore Multiplier +3";
    }

    public override string GetTitle() {
        return "What's that round thing?";
    }

    public override bool IsOneTime() {
        return true;
    }

}
