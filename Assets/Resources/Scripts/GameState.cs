using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {

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
    public int NumberOfEnemiesToSpawn = 5;
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
}
