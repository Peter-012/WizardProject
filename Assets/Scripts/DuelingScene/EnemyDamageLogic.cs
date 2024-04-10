using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage {
    public void Type();
}

public class EnemyDamageLogic : MonoBehaviour {
    private Rigidbody enemyRigid;
    private BoxCollider collisionBox;

    private IDamage Damage;

    private void Start() {
        addComponents();
    }

    private void OnTriggerEnter(Collider collision) {
        Damage = collision.GetComponent<IDamage>();
    }

    private void addComponents() {
        // Create hands as rigidbody
        gameObject.AddComponent<Rigidbody>();
        Rigidbody enemyRigid = FindObjectOfType<Rigidbody>();
        enemyRigid.useGravity = false;
        enemyRigid.isKinematic = true;

        // Add collision box to hands
        gameObject.AddComponent<BoxCollider>();
        BoxCollider collisionBox = FindObjectOfType<BoxCollider>();
        collisionBox.isTrigger = true;
        collisionBox.center = new Vector3(0f, 0f, 0.0015f);
        collisionBox.size = new Vector3(0.02f, 0.005f, 0.02f);
    }
}
