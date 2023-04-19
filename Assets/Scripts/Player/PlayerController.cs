using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private float moveSpeed = 5f; // The speed at which the player moves
    [SerializeField] private float maxVelocity = 10f; // The maximum velocity the player can have

    [SerializeField] private AudioClip playerDeathSound;

    private Vector2 currentVelocity;
    private Vector2 currentMovementDirection;
    private Rigidbody2D rb; // The player's rigidbody

    public static event Action OnPlayerKilled;
    public static event Action OnCurrentHealthChanged;
    public static event Action OnMaxHealthChanged;
    public static event Action OnPlayerCreated;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        MountCollector.OnWeaponMounted += MountCollector_OnWeaponMounted;
        currentHealth = maxHealth;
        OnMaxHealthChanged?.Invoke();
        OnMaxHealthChanged?.Invoke();
        OnPlayerCreated?.Invoke();
    }

    private void MountCollector_OnWeaponMounted() {
        IncreaseMaxHealth(5);
    }

    private void IncreaseMaxHealth(int increment) {
        maxHealth += increment;
        currentHealth += maxHealth/10;
        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        OnMaxHealthChanged?.Invoke();
        OnCurrentHealthChanged?.Invoke();
    }
    private void FixedUpdate() {
        Move();
        if (Input.GetKeyDown(KeyCode.L)) {
            TakeDamage(1000000);
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            PlayerPrefs.DeleteAll();
        }
    }

    private void Move() {
        // Get input from the player
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction and apply it to the player's velocity
        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput).normalized;
        currentMovementDirection = movementDirection;
        rb.AddForce(movementDirection * moveSpeed, ForceMode2D.Force);

        currentVelocity = rb.velocity;
        // Limit the player's velocity if it exceeds the maximum velocity
        if (rb.velocity.magnitude > maxVelocity) {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            currentHealth = 0;
            SoundManager.instance.PlayPlayerDeath(playerDeathSound);
            OnPlayerKilled?.Invoke();
        }
        OnCurrentHealthChanged?.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if(bullet.Source == Bullet.BulletSource.Enemy){
                this.TakeDamage(bullet.BulletDamage);
                Destroy(collision.gameObject);
            }
        }

        if(collision.gameObject.tag == "Border") {
            Debug.Log("Hit a border!");
            this.rb.AddForce(-collision.gameObject.transform.right * 1000f, ForceMode2D.Force);
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Border") {
            Debug.Log("Hit a border!");
            this.rb.AddForce(-collision.gameObject.transform.right * 1000f, ForceMode2D.Force);
        }
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }

    public int GetMaxHealth() {
        return maxHealth;
    }
}