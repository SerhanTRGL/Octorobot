using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private const string HIGHEST_WAVES_SURVIVED_KEY = "wavesSurvived";
    private const string HIGHEST_WEAPONS_COLLECTED_KEY = "weaponsCollected";
    private const string HIGHEST_ENEMIES_KILLED = "enemiesKilled";
    private const string HIGHEST_ORCAS_KILLED = "orcasKilled";

    [SerializeField] private TextMeshProUGUI wavesSurvivedThisRun;
    [SerializeField] private TextMeshProUGUI wavesSurvivedHighest;
    [SerializeField] private TextMeshProUGUI weaponsCollectedThisRun;
    [SerializeField] private TextMeshProUGUI weaponsCollectedHighest;
    [SerializeField] private TextMeshProUGUI enemiesKilledThisRun;
    [SerializeField] private TextMeshProUGUI enemiesKilledHighest;
    [SerializeField] private TextMeshProUGUI orcasKilledThisRun;
    [SerializeField] private TextMeshProUGUI orcasKilledHighest;

    private void Start() {
        UpdatePlayerPrefs();
        UpdateUI();
    }
    
    private void UpdatePlayerPrefs() {
        int wavesSurvived = GameManager.gameManager.waveCounter;
        int weaponsCollected = GameManager.gameManager.weaponCounter;
        int enemiesKilled = GameManager.gameManager.enemyKillCounter;
        int bossesKilled = GameManager.gameManager.bossKillCounter;

        int high;
        if (PlayerPrefs.HasKey(HIGHEST_WAVES_SURVIVED_KEY)) {
            high = Mathf.Max(PlayerPrefs.GetInt(HIGHEST_WAVES_SURVIVED_KEY), wavesSurvived);
            PlayerPrefs.SetInt(HIGHEST_WAVES_SURVIVED_KEY, high);
        } else {
            PlayerPrefs.SetInt(HIGHEST_WAVES_SURVIVED_KEY, wavesSurvived);
        }

        if (PlayerPrefs.HasKey(HIGHEST_WEAPONS_COLLECTED_KEY)) {
            high = Mathf.Max(PlayerPrefs.GetInt(HIGHEST_WEAPONS_COLLECTED_KEY), weaponsCollected);
            PlayerPrefs.SetInt(HIGHEST_WEAPONS_COLLECTED_KEY, high);
        } else {
            PlayerPrefs.SetInt(HIGHEST_WEAPONS_COLLECTED_KEY, weaponsCollected);
        }

        if (PlayerPrefs.HasKey(HIGHEST_ENEMIES_KILLED)) {
            high = Mathf.Max(PlayerPrefs.GetInt(HIGHEST_ENEMIES_KILLED), enemiesKilled);
            PlayerPrefs.SetInt(HIGHEST_ENEMIES_KILLED, high);
        } else {
            PlayerPrefs.SetInt(HIGHEST_ENEMIES_KILLED, enemiesKilled);
        }

        if (PlayerPrefs.HasKey(HIGHEST_ORCAS_KILLED)) {
            high = Mathf.Max(PlayerPrefs.GetInt(HIGHEST_ORCAS_KILLED), bossesKilled);
            PlayerPrefs.SetInt(HIGHEST_ORCAS_KILLED, high);
        } else {
            PlayerPrefs.SetInt(HIGHEST_ORCAS_KILLED, bossesKilled);
        }
    }

    private void UpdateUI() {
        int wavesSurvived = GameManager.gameManager.waveCounter;
        int weaponsCollected = GameManager.gameManager.weaponCounter;
        int enemiesKilled = GameManager.gameManager.enemyKillCounter;
        int bossesKilled = GameManager.gameManager.bossKillCounter;

        wavesSurvivedThisRun.text = wavesSurvived.ToString();
        wavesSurvivedHighest.text = PlayerPrefs.GetInt(HIGHEST_WAVES_SURVIVED_KEY).ToString();

        weaponsCollectedThisRun.text = weaponsCollected.ToString();
        weaponsCollectedHighest.text = PlayerPrefs.GetInt(HIGHEST_WEAPONS_COLLECTED_KEY).ToString();

        enemiesKilledThisRun.text = enemiesKilled.ToString();
        enemiesKilledHighest.text = PlayerPrefs.GetInt(HIGHEST_ENEMIES_KILLED).ToString();

        orcasKilledThisRun.text = bossesKilled.ToString();
        orcasKilledHighest.text = PlayerPrefs.GetInt(HIGHEST_ORCAS_KILLED).ToString();
    }

}
