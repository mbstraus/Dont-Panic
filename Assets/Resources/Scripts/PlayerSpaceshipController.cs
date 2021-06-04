/**
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
using UnityEngine.InputSystem;

namespace mbstraus.DontPanic.Player
{
    public class PlayerSpaceshipController : MonoBehaviour
    {
        #region Private locally controlled variables
        private Camera mainCamera;
        private Quaternion defaultRotation;
        private Vector2 movement;
        private bool isRotated;
        private GameStateManager gameStateManager;
        private float shotCooldown;
        #endregion

        #region Inspector exposed variables
        [SerializeField]
        private float PlayerMoveRate;
        [SerializeField]
        private float PlayerMoveRotation;
        [SerializeField]
        private Transform PlayerGraphics;
        [SerializeField]
        private Transform BulletOrigination;
        [SerializeField]
        private float ShotSpeed;
        [SerializeField]
        private float TimeBetweenShots;
        [SerializeField]
        private PlayerBullet PlayerBulletPrefab;
        [SerializeField]
        private Transform BulletContainer;
        #endregion

        #region Input Events
        public void MoveCharacter(InputAction.CallbackContext context) {
            movement = context.ReadValue<Vector2>();
        }
        #endregion

        #region Unity Events
        void Start() {
            mainCamera = Camera.main;
            defaultRotation = PlayerGraphics.rotation;
            movement = Vector2.zero;
            gameStateManager = FindObjectOfType<GameStateManager>();

            if (BulletContainer == null) {
                Debug.LogWarning("No bullet container defined for the player, bullets will be spawned at the root of the hierarchy.");
            }
        }

        void Update() {
            if (gameStateManager.IsInPlayMode()) {
                DoMoveCharacter();
                DoShootBullet();
            }
        }
        #endregion

        #region Update handlers
        private void DoMoveCharacter() {
            Vector3 translate = new Vector2(movement.x * Time.deltaTime * PlayerMoveRate, movement.y * Time.deltaTime * PlayerMoveRate);
            Vector3 newPosition = gameObject.transform.position + translate;
            var direction = (newPosition - transform.position).normalized;
            var distance = Vector3.Distance(transform.position, newPosition);
            Ray ray = new Ray(transform.position, direction);
            Debug.DrawRay(transform.position, direction);
            bool raycastHit = Physics.Raycast(ray, distance, LayerMask.GetMask("Border"));
            if (raycastHit) {
                return;
            }
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

            if (movement.y != 0 && !isRotated) {
                float xRotation = PlayerMoveRotation;
                if (movement.y > 0) {
                    xRotation = -PlayerMoveRotation;
                }
                PlayerGraphics.Rotate(new Vector3(xRotation, 0f, 0f));
                isRotated = true;
            } else if (movement.y == 0) {
                isRotated = false;
                PlayerGraphics.rotation = defaultRotation;
            }
        }

        private void DoShootBullet() {
            if (shotCooldown <= 0) {
                PlayerBullet bullet = Instantiate(PlayerBulletPrefab, BulletOrigination.position, BulletOrigination.rotation, BulletContainer);
                bullet.BulletMoveRate = ShotSpeed;
                shotCooldown = TimeBetweenShots;
            } else {
                shotCooldown -= Time.deltaTime;
            }
        }
        #endregion
    }
}