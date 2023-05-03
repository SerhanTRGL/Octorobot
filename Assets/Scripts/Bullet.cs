using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public enum BulletSource{
        Player,
        Enemy
    }

    [SerializeField] public int BulletDamage{get; set;}
    [SerializeField] public BulletSource Source{get; set;}
}
