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
}
