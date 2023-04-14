using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponFire : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform underProjectileSpawnPoint;
    public Transform aboveProjectileSpawnPoint;
    public float projectileSpeed = 10f;
    public float projectileLifetime = 2.5f;
    public float shootRate = 2f;

    private void Start() {
        InvokeRepeating(nameof(ShootProjectiles), 1f, shootRate);
    }

    private void ShootProjectiles() {
        GameObject underProjectile = Instantiate(projectilePrefab, underProjectileSpawnPoint.position, Quaternion.identity);
        GameObject aboveProjectile = Instantiate(projectilePrefab, aboveProjectileSpawnPoint.position, Quaternion.identity);

        underProjectile.GetComponentInChildren<SpriteRenderer>().transform.eulerAngles = transform.eulerAngles + (new Vector3(0,0,90));
        aboveProjectile.GetComponentInChildren<SpriteRenderer>().transform.eulerAngles = transform.eulerAngles + (new Vector3(0, 0, 90));

        Rigidbody2D underProjectileRb = underProjectile.GetComponent<Rigidbody2D>();
        Rigidbody2D aboveProjectileRb = aboveProjectile.GetComponent<Rigidbody2D>();

        underProjectileRb.velocity = projectileSpeed * Time.deltaTime * transform.right;
        aboveProjectileRb.velocity = projectileSpeed * Time.deltaTime * transform.right;

        Destroy(underProjectile, projectileLifetime);
        Destroy(aboveProjectile, projectileLifetime);
    }
}

