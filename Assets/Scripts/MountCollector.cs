using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MountCollector : MonoBehaviour {
    private BasicWeapon weapon;

    public static event Action OnWeaponMounted;
    [SerializeField] private AudioClip mountSound;

    private void Start() {
        weapon = GetComponentInParent<BasicWeapon>();
        if (!weapon.CanMountOnThis()) {
            Destroy(this.gameObject);
        }
    }
    private void Update() {
        /*For gameplay*/
        if (!weapon.CanMountOnThis()) {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        BasicWeapon weaponToMount = collision.GetComponentInParent<BasicWeapon>();
        if (collision.CompareTag("Collectible") && !weaponToMount.IsMounted() && !weapon.IsCollectible()) {
            ArmController arm = weapon.GetConnectedArm();
            arm.Mount(weaponToMount);
            SoundManager.instance.PlayWeaponMount(mountSound);
            OnWeaponMounted?.Invoke();
        }
    }
}
