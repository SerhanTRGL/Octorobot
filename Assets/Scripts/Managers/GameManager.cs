using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    public static GameManager gameManager { get; private set; }

    public int enemyKillCounter { get; private set; }
    public int bossKillCounter { get; private set; }
    public int weaponCounter { get; private set; }
    public int waveCounter { get; private set; }
    public int currentHealth { get; private set; }
    public int maxHealth { get; private set; }

    private void Awake() {
        enemyKillCounter = 0;
        bossKillCounter = 0;
        weaponCounter = 0;
        waveCounter = 0;
        if(gameManager != null && gameManager != this) {
            Destroy(this);
            return;
        }
        gameManager = this;
        DontDestroyOnLoad(this);
    }

    private void Start() {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        Enemy.OnBossKilled += Enemy_OnBossKilled;
        Enemy.OnEnemyKilled += Enemy_OnEnemyKilled;
        EnemySpawner.OnNewWave += EnemySpawner_OnNewWave;
        MountCollector.OnWeaponMounted += MountCollector_OnWeaponMounted;
        PlayerController.OnCurrentHealthChanged += PlayerController_OnCurrentHealthChanged;
        PlayerController.OnMaxHealthChanged += PlayerController_OnMaxHealthChanged;
        PlayerController.OnPlayerKilled += PlayerController_OnPlayerKilled;
        PlayerController.OnPlayerCreated += PlayerController_OnPlayerCreated;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1) {
        if(SceneManager.GetActiveScene().buildIndex == 1) {
            enemyKillCounter = 0;
            bossKillCounter = 0;
            weaponCounter = 0;
            waveCounter = 0;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            currentHealth = player.GetCurrentHealth();
            maxHealth = player.GetMaxHealth();
        }
    }

    private void PlayerController_OnPlayerCreated() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentHealth = player.GetCurrentHealth();
        maxHealth = player.GetMaxHealth();
    }

    private void PlayerController_OnPlayerKilled() {
        player.gameObject.SetActive(false);
        StartCoroutine(GoToStatsScene());
    }
    
    IEnumerator GoToStatsScene() {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }

    private void PlayerController_OnMaxHealthChanged() {
        maxHealth = player.GetMaxHealth();
    }

    private void PlayerController_OnCurrentHealthChanged() {
        currentHealth = player.GetCurrentHealth();
    }

    private void MountCollector_OnWeaponMounted() {
        weaponCounter++;
    }

    private void EnemySpawner_OnNewWave() {
        waveCounter++;
    }

    private void Enemy_OnEnemyKilled() {
        enemyKillCounter++;
    }

    private void Enemy_OnBossKilled() {
        bossKillCounter++;
    }

}
