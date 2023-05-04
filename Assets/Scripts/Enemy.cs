using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private int currentHealth = 20;
    [SerializeField] private int contactDamage = 3;

    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D enemyRigidBody;
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private BasicWeapon dropWeapon;

    [SerializeField] private float contactDamageCooldown = 1f;

    [SerializeField] private AudioClip enemyDeathSound;

    private bool isDead = false;
    private float contactDamageTimer = 0f;
    private bool isDropVersion;

    [SerializeField] public Vector3 lookDirection;

    public static event Action OnEnemyKilled;
    public static event Action OnBossKilled;
    private void Start() {
        isDropVersion = dropWeapon != null;
        currentHealth = maxHealth;
    }

    private void FixedUpdate() {
        MoveEnemy();
    }

    private void MoveEnemy() {
        if (playerRigidBody != null) {
            Vector2 movementDirection = playerRigidBody.position - enemyRigidBody.position;
            movementDirection.Normalize();
            lookDirection = movementDirection;
            enemyRigidBody.velocity = movementDirection * moveSpeed * Time.deltaTime;

            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    public void SetPlayerRigidBody(Rigidbody2D playerRigidBody) {
        this.playerRigidBody = playerRigidBody;
    }

    public void IncreaseMaxHealth(int maxHealth) {
        this.maxHealth += maxHealth;
        currentHealth = maxHealth;
    }

    public void IncreaseContactDamage(int increase) {
        this.contactDamage += increase;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if(currentHealth < 0) {
            if (isDropVersion) {
                dropWeapon.transform.parent = null;
                dropWeapon.gameObject.SetActive(true);
            }

            if (GetComponentInChildren<SpriteRenderer>().tag == "Boss") {
                OnBossKilled?.Invoke();
            }
            if (!isDead) {
                OnEnemyKilled?.Invoke();
                isDead = true;
            }
            SoundManager.instance.PlayEnemyDeath(enemyDeathSound);
            Destroy(this.gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player"){
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(contactDamage);
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            contactDamageTimer += Time.deltaTime;
            if(contactDamageTimer > contactDamageCooldown) {
                contactDamageTimer = 0;
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                player.TakeDamage(contactDamage);
            }
        }
    }
}
