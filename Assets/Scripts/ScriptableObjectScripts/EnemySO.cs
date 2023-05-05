using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/BulletStats")]
public class EnemySO : ScriptableObject{
    int startingMaxHealth;
    int contactDamage;
    int moveSpeed;
    BasicWeapon dropWeapon;
    bool isBoss;
    Sprite enemySprite;
    BulletStatsSO enemyBulletStats;
    string enemyName;
}
