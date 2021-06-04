﻿/**
 * Don't Panic, a shoot-em-up game with jokes from Hitchiker's Guide to the Galaxy
 * Copyright (C) 2021 Mathew Strauss
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software Foundation,
 * Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
 */
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    private float PlayerBulletLifeSpan = 2f;

    public float BulletMoveRate {
        get; set;
    }

    private void Update () {
        Vector3 movement = new Vector3(Time.deltaTime * BulletMoveRate, 0, 0);
        transform.Translate(movement);
        PlayerBulletLifeSpan -= Time.deltaTime;
        if (PlayerBulletLifeSpan <= 0) {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Collided - " + collision.gameObject.name);

        GameObject go = collision.gameObject;
        Enemy enemy = go.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
