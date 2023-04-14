using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Ammo : MonoBehaviour {
    [SerializeField] private int damage = 3;
    public void SetDamage(int damage) {
        this.damage = damage;
    }

    public int Damage() {
        return damage;
    }
}
