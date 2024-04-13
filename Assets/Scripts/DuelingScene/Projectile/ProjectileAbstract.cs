using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ProjectileAbstract : MonoBehaviour, IDamage {
    public static event Action <GameObject> OnCollision;

    private void Update() {
        fireProjectile();
    }

    public void Hit(GameObject Player) {
        if (OnCollision == null) return;
        OnCollision.Invoke(Player);
    }

    private void OnEnable() {
        OnCollision += ProjectileHit;
    }

    private void OnDisable() {
        OnCollision -= ProjectileHit;
    }

    public abstract void fireProjectile();

    public abstract void ProjectileHit (GameObject Player);

    public virtual void DestroyProjectile() {
        Destroy(gameObject);
    }
}
