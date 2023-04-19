using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public enum BulletSource{
        Player,
        Enemy
    }

    [SerializeField] int BulletDamage{get; set;}
    [SerializeField] BulletSource Source{get; private set;}
}
