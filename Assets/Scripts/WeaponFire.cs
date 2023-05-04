using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class WeaponFire : MonoBehaviour
{
    [SerializeField] private BasicWeapon weapon;
    private IObjectPool<GameObject> bulletPool; //ObjectPooling
    [SerializeField] private BulletStatsSO bulletStats;
    public GameObject projectilePrefab;
    public Transform underProjectileSpawnPoint;
    public Transform aboveProjectileSpawnPoint;
    public float projectileLifetime;
    public float shootRate;

    private void Awake() {
        bulletPool = new ObjectPool<GameObject>(
            CreateBullet,
            OnGet,
            OnRelease
        );
    }

    private void Start(){
        InvokeRepeating(nameof(ShootProjectiles), 1, shootRate);
    }

    private GameObject CreateBullet(){
        GameObject bullet = Instantiate(projectilePrefab);
        bullet.GetComponent<Bullet>().SetPool(bulletPool);
        return bullet;
    } 

     private void OnGet(GameObject bullet){
        bullet.SetActive(true);
    }

    private void OnRelease(GameObject bullet){
        bullet.SetActive(false);
    }

    private void ShootProjectiles() {
        if(weapon == null || (weapon != null && weapon.IsMounted())){
            GameObject underProjectile = bulletPool.Get();
            GameObject aboveProjectile = bulletPool.Get();

            SetProjectilePosition(underProjectile, aboveProjectile);
            SetProjectileDamage(underProjectile, aboveProjectile);
            SetProjectileSource(underProjectile, aboveProjectile);
            SetProjectileSprite(underProjectile, aboveProjectile);
            SetProjectileScale(underProjectile, aboveProjectile);
            RotateProjectiles(underProjectile, aboveProjectile);
            SetProjectileVelocity(underProjectile, aboveProjectile);
        
            if(gameObject.activeInHierarchy){
                IEnumerator releaseProjectileCoroutine = ReleaseProjectiles(underProjectile, aboveProjectile);
                StartCoroutine(releaseProjectileCoroutine);
            }
        }
    }

    private void SetProjectilePosition(GameObject underProjectile, GameObject aboveProjectile){
        underProjectile.transform.position = underProjectileSpawnPoint.position;
        aboveProjectile.transform.position = aboveProjectileSpawnPoint.position;
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

    private IEnumerator ReleaseProjectiles(GameObject underProjectile, GameObject aboveProjectile){
        yield return new WaitForSeconds(projectileLifetime);
        if(underProjectile.activeSelf){
            bulletPool.Release(underProjectile);
        }
        if(aboveProjectile.activeSelf){
            bulletPool.Release(aboveProjectile);
        }
    }
}



