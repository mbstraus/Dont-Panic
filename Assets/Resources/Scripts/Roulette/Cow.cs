using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : IRouletteItem
{

    public override void ApplyResult()
    {
        GameController.instance.CurrentGameState.PlayerFireDelay += 0.25f;
        GameController.instance.CurrentGameState.PlayerBulletMoveRate /= 2;
        GameController.instance.CurrentGameState.CurrentBullet += 1;
        GameController.instance.CurrentGameState.ScoreMultiplerFromSlots += 3;
    }

    public override string GetTitle()
    {
        return "Have A Cow!";
    }

    public override string GetDescription()
    {
        return "Don't be a Mooron, press the button!\nScore Multiplier +3";
    }

    public override bool IsOneTime()
    {
        return true;
    }
}
