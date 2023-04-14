using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource playerDeathSource;
    public AudioSource enemyDeathSource;
    public AudioSource weaponMountSource;

    public static SoundManager instance;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        else if(instance != this) {
            Destroy(gameObject);
        }
    }

    public void PlayEnemyDeath(AudioClip clip) {
        enemyDeathSource.clip = clip;
        enemyDeathSource.Play();
    } 
    public void PlayPlayerDeath(AudioClip clip) {
        playerDeathSource.clip = clip;
        playerDeathSource.Play();
    }
    public void PlayWeaponMount(AudioClip clip) {
        weaponMountSource.clip = clip;
        weaponMountSource.Play();
    }
}
