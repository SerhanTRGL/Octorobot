using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/BulletStats")]
public class BulletStatsSO : ScriptableObject
{
    
    public int bulletDamage;
    public Vector3 bulletScale;
    public Sprite bulletSprite;
    public float bulletVelocity;
    public Bullet.BulletSource bulletSource;
    public string bulletSourceName;
}
