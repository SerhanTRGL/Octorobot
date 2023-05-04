using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Bullet : MonoBehaviour {
    private IObjectPool<GameObject> bulletPool;
    
    public enum BulletSource{
        Player,
        Enemy
    }

    [SerializeField] public int BulletDamage{get; set;}
    [SerializeField] public BulletSource Source{get; set;}

    public void SetPool(IObjectPool<GameObject> pool){
        bulletPool = pool;
    }

    public IObjectPool<GameObject> GetPool(){
        return bulletPool;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        HandleCollisionWithPlayer(other);
        HandleCollisionWithEnemy(other);
    }

    private void HandleCollisionWithPlayer(Collider2D other){
        if(other.gameObject.tag == "Player" && this.Source != Bullet.BulletSource.Player){
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(BulletDamage);
            if(this.gameObject.activeSelf){
                bulletPool.Release(this.gameObject);
            }
        }
    }

    private void HandleCollisionWithEnemy(Collider2D other){
        if(other.gameObject.tag == "Enemy" && this.Source != Bullet.BulletSource.Enemy){
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(BulletDamage);
            if(this.gameObject.activeSelf){
                bulletPool.Release(this.gameObject);
            }
        }
    }
}
