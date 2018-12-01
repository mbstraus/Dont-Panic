using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {

    public int Score = 0;
    public bool IsGameOver = false;
    public int ScoreMultiplier = 1;
    public int ScoreMultiplerFromSlots = 1;
    public int CurrentScoreChain = 1;
    public int ScorePerEnemy = 1;
    public int EnemiesKilledWithoutHit = 1;
    public int EnemiesKilledToIncreaseMultiplier = 5;

    /** MODIFIERS */
    // Multiplies the player's shield by this number
    public int PlayerShieldsMultiplier = 1;
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

    /** PLAYER STATE */
    public int CurrentHullStrength = 1;
    public int BaseShieldStrength = 10;
    public int CurrentShieldStrength = 3;
    public int MaxShieldStrength {
        get { return (BaseShieldStrength + PlayerShieldsModifier) * PlayerShieldsMultiplier; }
    }
    public float PlayerMoveRate = 5f;
    public float PlayerFireDelay = 0f;

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
