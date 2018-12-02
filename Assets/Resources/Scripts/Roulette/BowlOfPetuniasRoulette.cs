using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlOfPetuniasRoulette : IRouletteItem {

    public override void ApplyResult() {
        GameController.instance.CurrentGameState.IsDroppingPetunias = true;
        GameController.instance.CurrentGameState.ScoreMultiplerFromSlots += 1;
    }

    public override string GetDescription() {
        return "Bowls of petunias randomly fall\nScore Multiplier +1";
    }

    public override string GetTitle() {
        return "Oh no, not again.";
    }

    public override bool IsOneTime() {
        return true;
    }
}
