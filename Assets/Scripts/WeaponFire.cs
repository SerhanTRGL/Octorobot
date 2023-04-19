using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform underProjectileSpawnPoint;
    public Transform aboveProjectileSpawnPoint;
    public float projectileSpeed = 10f;
    public float projectileLifetime = 2.5f;
    public float shootRate = 2f;
    public int damage;
    Bullet.BulletSource source;
    private void Start() {
        if(transform.parent.CompareTag("Enemy")){
            source = Bullet.BulletSource.Enemy;
        }
        else{
            source = Bullet.BulletSource.Player;
        }

        InvokeRepeating(nameof(ShootProjectiles), 1f, shootRate);
    }

    private void ShootProjectiles() {
        Debug.Log("Shooting");
        GameObject underProjectile = Instantiate(projectilePrefab, underProjectileSpawnPoint.position, Quaternion.identity);
        GameObject aboveProjectile = Instantiate(projectilePrefab, aboveProjectileSpawnPoint.position, Quaternion.identity);

        SetProjectileSource(underProjectile, aboveProjectile);
        RotateProjectileVisual(underProjectile, aboveProjectile);
        SetProjectileVelocity(underProjectile, aboveProjectile);
        DestroyProjectiles(underProjectile, aboveProjectile);

    }
    private void SetProjectileDamage(GameObject underProjectile, GameObject aboveProjectile){
        underProjectile.GetComponentInChildren<Bullet>().BulletDamage = damage;
        aboveProjectile.GetComponentInChildren<Bullet>().BulletDamage = damage;
    }
    private void SetProjectileSource(GameObject underProjectile, GameObject aboveProjectile){
        underProjectile.GetComponentInChildren<Bullet>().Source = source;
        aboveProjectile.GetComponentInChildren<Bullet>().Source = source;
    }

    private void RotateProjectileVisual(GameObject underProjectile, GameObject aboveProjectile){
        underProjectile.GetComponentInChildren<SpriteRenderer>().transform.eulerAngles = transform.eulerAngles + (new Vector3(0,0,90));
        aboveProjectile.GetComponentInChildren<SpriteRenderer>().transform.eulerAngles = transform.eulerAngles + (new Vector3(0, 0, 90));
    }

    private void SetProjectileVelocity(GameObject underProjectile, GameObject aboveProjectile){
        Rigidbody2D underProjectileRb = underProjectile.GetComponent<Rigidbody2D>();
        Rigidbody2D aboveProjectileRb = aboveProjectile.GetComponent<Rigidbody2D>();

        underProjectileRb.velocity = projectileSpeed * Time.deltaTime * transform.right;
        aboveProjectileRb.velocity = projectileSpeed * Time.deltaTime * transform.right;
    }

    private void DestroyProjectiles(GameObject underProjectile, GameObject aboveProjectile){
        Destroy(underProjectile, projectileLifetime);
        Destroy(aboveProjectile, projectileLifetime);
    }
}



