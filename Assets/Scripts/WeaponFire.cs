using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    [SerializeField] private BulletStatsSO bulletStats;
    public GameObject projectilePrefab;
    public Transform underProjectileSpawnPoint;
    public Transform aboveProjectileSpawnPoint;
    public float projectileLifetime;
    public float shootRate;
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
        SetProjectileSprite(underProjectile, aboveProjectile);
        SetProjectileScale(underProjectile, aboveProjectile);
        RotateProjectiles(underProjectile, aboveProjectile);
        SetProjectileVelocity(underProjectile, aboveProjectile);
        DestroyProjectiles(underProjectile, aboveProjectile);

    }
    private void SetProjectileDamage(GameObject underProjectile, GameObject aboveProjectile){
        underProjectile.GetComponentInChildren<Bullet>().BulletDamage = bulletStats.bulletDamage;;
        aboveProjectile.GetComponentInChildren<Bullet>().BulletDamage = bulletStats.bulletDamage;;
    }
    private void SetProjectileSource(GameObject underProjectile, GameObject aboveProjectile){
        underProjectile.GetComponentInChildren<Bullet>().Source = bulletStats.bulletSource;
        aboveProjectile.GetComponentInChildren<Bullet>().Source = bulletStats.bulletSource;
    }
    private void SetProjectileSprite(GameObject underProjectile, GameObject aboveProjectile){
        underProjectile.GetComponentInChildren<SpriteRenderer>().sprite = bulletStats.bulletSprite;
        aboveProjectile.GetComponentInChildren<SpriteRenderer>().sprite = bulletStats.bulletSprite;
    }
    private void SetProjectileScale(GameObject underProjectile, GameObject aboveProjectile){
        underProjectile.transform.localScale = bulletStats.bulletScale;
        aboveProjectile.transform.localScale = bulletStats.bulletScale;
    }
    private void RotateProjectiles(GameObject underProjectile, GameObject aboveProjectile){
        underProjectile.transform.eulerAngles = underProjectileSpawnPoint.transform.eulerAngles;
        aboveProjectile.transform.eulerAngles = aboveProjectileSpawnPoint.transform.eulerAngles;
    }

    private void SetProjectileVelocity(GameObject underProjectile, GameObject aboveProjectile){
        Rigidbody2D underProjectileRb = underProjectile.GetComponent<Rigidbody2D>();
        Rigidbody2D aboveProjectileRb = aboveProjectile.GetComponent<Rigidbody2D>();

        underProjectileRb.velocity = bulletStats.bulletVelocity * Time.deltaTime * underProjectileSpawnPoint.transform.right;
        aboveProjectileRb.velocity = bulletStats.bulletVelocity * Time.deltaTime * aboveProjectileSpawnPoint.transform.right;
    }

    private void DestroyProjectiles(GameObject underProjectile, GameObject aboveProjectile){
        Destroy(underProjectile, projectileLifetime);
        Destroy(aboveProjectile, projectileLifetime);
    }
}



