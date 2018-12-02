using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {

    public GameState(IRouletteItem[] rouletteItems) {
        AvailableRouletteItems = new List<IRouletteItem>(rouletteItems);
    }

    /*************************************** ROULETTE STATE **************************************/
    public List<IRouletteItem> AvailableRouletteItems;

    /***************************************** SCORE *********************************************/
    public int Score = 0;
    public bool IsGameOver = false;
    public int ScoreMultiplier = 1;
    public int ScoreMultiplerFromSlots = 1;
    public int CurrentScoreChain = 1;
    public int ScorePerEnemy = 42;
    public int EnemiesKilledWithoutHit = 1;
    public int EnemiesKilledToIncreaseMultiplier = 5;

    /** MODIFIERS */
    // Multiplies the player's shield by this number
    public float PlayerShieldsMultiplier = 1;
    // Adds to the player's shield by this number
    public int PlayerShieldsModifier = 0;
    // Multiplies the enemy's shield by this number
    public int EnemyShieldsMultiplier = 1;
    // Adds to the enemy's shield by this number
    public int EnemyShieldsModifier = 0;
    // Multiples the enemy's move rate by this number
    public float EnemyMoveRateMultiplier = 1;
    // Adds the enemy's move rate by this number
    public float EnemyMoveRateModifier = 0;
    // Multiples the enemy's move rate by this number
    public float EnemyFireRateMultiplier = 1;
    // Adds the enemy's move rate by this number
    public float EnemyFireRateModifier = 0;
    public int NumberOfEnemiesModifier = 0;
    public int NumberOfEnemiesMultiplier = 1;
    // +/- this value on the effects gravity has on the bowl of petunias.
    public float BowlOfPetuniasDropSpeedRange = 1f;
    public float BowlOfPetuniasDropSpeed = 10f;
    // Frequency in which a bowl of petunias spawns (when the modifier has been applied)
    public float BowlOfPetuniasSpawnFrequencySec = 3f;
    // Variability in the spawn frequency of the bowl of petunias
    public float BowlOfPetuniasSpawnVariabilitySec = 1f;

    /** PLAYER STATE */
    public int CurrentHullStrength = 1;
    public int BaseShieldStrength = 10;
    public int CurrentShieldStrength = 10;
    public int CurrentBullet = 0;
    public int MaxShieldStrength {
        get { return (int) (((float) BaseShieldStrength + PlayerShieldsModifier) * PlayerShieldsMultiplier); }
    }
    public float PlayerMoveRate = 10f;
    public float PlayerFireDelay = 0f;
    public float PlayerGunJamFrequencySec = 15f;
    public float PlayerGunJamVariationSec = 5f;
    public float PlayerGunJamDurationSec = 5f;
    public bool IsPlayerGunJammed = false;

    /** PLAYER BULLET */
    public float PlayerBulletMoveRate = 25f;

    /** SPAWN STATE */
    public int NumberOfEnemiesToSpawn {
        get { return (BaseNumberOfEnemiesToSpawn + NumberOfEnemiesModifier) * NumberOfEnemiesMultiplier;  }
    }
    public int BaseNumberOfEnemiesToSpawn = 50;
    public float DelayBetweenSpawnsSec = 0.5f;
    public float SpawnVariationSec = 0.2f;
    public int KilledEnemies = 0;

    /** ENEMY BULLET */
    public float EnemyBulletMoveRate = 10f;
    public float EnemyBulletMoveVariation = 2f;

    /** ENEMY */

    /** ROULETTE STATE */
    public bool IsDoingRoulette = false;
    public bool IsDroppingPetunias = false;
    public bool IsPlayerAWhale = false;
    public bool IsPlayerGunJamming = false;

    public void RecalculateStats() {
        KilledEnemies = 0;
        CurrentShieldStrength = MaxShieldStrength;
    }

    public void AddScore() {
        CurrentScoreChain = ScorePerEnemy * ScoreMultiplier * ScoreMultiplerFromSlots;
        Score += CurrentScoreChain;

        EnemiesKilledWithoutHit += 1;
        if (EnemiesKilledWithoutHit % EnemiesKilledToIncreaseMultiplier == 0) {
            ScoreMultiplier += 1;
        }
    }

    public void ResetScoreMultiplier() {
        ScoreMultiplier = 1;
        CurrentScoreChain = 1;
        EnemiesKilledWithoutHit = 1;
    }
}
