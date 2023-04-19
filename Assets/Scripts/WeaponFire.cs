using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform underProjectileSpawnPoint;
    public Transform aboveProjectileSpawnPoint;
    public float projectileSpeed;
    public float projectileLifetime;
    public float shootRate;
    [SerializeField] private int damage;
    Bullet.BulletSource source;
    private void Start() {
        if(transform.parent.CompareTag("Enemy")){
            source = Bullet.BulletSource.Enemy;
        }
        else{
            source = Bullet.BulletSource.Player;
        }
    }

private float shootTimer = 0f;
private void FixedUpdate() {
    shootTimer += Time.fixedDeltaTime;
    if(shootTimer >= shootRate){
        ShootProjectiles();
        shootTimer = 0f;
    }
}
    private void ShootProjectiles() {
        GameObject underProjectile = Instantiate(projectilePrefab, underProjectileSpawnPoint.position, Quaternion.identity);
        GameObject aboveProjectile = Instantiate(projectilePrefab, aboveProjectileSpawnPoint.position, Quaternion.identity);

        SetProjectileDamage(underProjectile, aboveProjectile);
        SetProjectileSource(underProjectile, aboveProjectile);
        RotateProjectiles(underProjectile, aboveProjectile);
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

    private void RotateProjectiles(GameObject underProjectile, GameObject aboveProjectile){
        underProjectile.transform.eulerAngles = underProjectileSpawnPoint.transform.eulerAngles;
        aboveProjectile.transform.eulerAngles = aboveProjectileSpawnPoint.transform.eulerAngles;
    }

    private void SetProjectileVelocity(GameObject underProjectile, GameObject aboveProjectile){
        Rigidbody2D underProjectileRb = underProjectile.GetComponent<Rigidbody2D>();
        Rigidbody2D aboveProjectileRb = aboveProjectile.GetComponent<Rigidbody2D>();

        underProjectileRb.velocity = projectileSpeed * Time.deltaTime * underProjectileSpawnPoint.transform.right;
        aboveProjectileRb.velocity = projectileSpeed * Time.deltaTime * aboveProjectileSpawnPoint.transform.right;
    }

    private void DestroyProjectiles(GameObject underProjectile, GameObject aboveProjectile){
        Destroy(underProjectile, projectileLifetime);
        Destroy(aboveProjectile, projectileLifetime);
    }
}



