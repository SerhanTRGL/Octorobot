using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI currentWave;
    [SerializeField] private TextMeshProUGUI enemiesKilled;
    [SerializeField] private TextMeshProUGUI bossesKilled;
    [SerializeField] private TextMeshProUGUI maxHealth;
    [SerializeField] private TextMeshProUGUI currentHealth;
    [SerializeField] private TextMeshProUGUI weaponsCollected;
    void Start()
    {
        Enemy.OnBossKilled += Enemy_OnBossKilled;
        Enemy.OnEnemyKilled += Enemy_OnEnemyKilled;
        EnemySpawner.OnNewWave += EnemySpawner_OnNewSpawnWave;
        MountCollector.OnWeaponMounted += MountCollector_OnWeaponMounted;
        PlayerController.OnCurrentHealthChanged += PlayerController_OnCurrentHealthChanged;
        PlayerController.OnMaxHealthChanged += PlayerController_OnMaxHealthChanged;

        PlayerController_OnMaxHealthChanged();
        PlayerController_OnCurrentHealthChanged();
        MountCollector_OnWeaponMounted();
        EnemySpawner_OnNewSpawnWave();
        Enemy_OnEnemyKilled();
        Enemy_OnBossKilled();
    }

    private void PlayerController_OnMaxHealthChanged() {
        maxHealth.text = GameManager.gameManager.maxHealth.ToString();
    }

    private void PlayerController_OnCurrentHealthChanged() {
       currentHealth.text = GameManager.gameManager.currentHealth.ToString();
    }

    private void MountCollector_OnWeaponMounted() {
        weaponsCollected.text = GameManager.gameManager.weaponCounter.ToString();
    }

    private void EnemySpawner_OnNewSpawnWave() {
        currentWave.text = GameManager.gameManager.waveCounter.ToString();
    }

    private void Enemy_OnEnemyKilled() {
        enemiesKilled.text = GameManager.gameManager.enemyKillCounter.ToString();
    }

    private void Enemy_OnBossKilled() {
        bossesKilled.text = GameManager.gameManager.bossKillCounter.ToString();
    }
}
