using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartDuel : MonoBehaviour, IMenuSelection {
    [SerializeField] private float fadeOutDuration = 3.0f;
    
    private AudioSource soundEffect;
    public static event Action <GameObject> OnStartDuel;

    public void Select(GameObject controller) {
        if (OnStartDuel == null) return;
        soundEffect = gameObject.GetComponent<AudioSource>();
        OnStartDuel.Invoke(controller);
    }

    private void OnEnable() {
        OnStartDuel += StartDuelLogic;
    }

    private void OnDisable() {
        OnStartDuel -= StartDuelLogic;
    }

    private void StartDuelLogic(GameObject controller) {
        // // Attach wand to hand
        gameObject.AddComponent<FixedJoint>();
        FixedJoint joint = gameObject.GetComponent<FixedJoint>();
        joint.connectedBody = controller.GetComponent<Rigidbody>();
        joint.breakForce = Mathf.Infinity;

        // Position wand to fit in hand
        // wandObject.transform.position = transform.position;
        // wandObject.transform.Rotate(70f, 0, 0);

        if (controller.name.Contains("left")) {
            Player1.DominantSide = "left";
        } else if (controller.name.Contains("right")) {
            Player1.DominantSide = "right";
        } else {
            Debug.LogError("Failed to initialize DominantSide variable from PlayerData.");
        }

        // Play an equip wand sound effect
        if (soundEffect.isPlaying) soundEffect.Stop();
        soundEffect.Play();
        
        // Load "DuelingScene" while fading user screen
        GameObject cameraRig = GameObject.Find("[CameraRig]");
        TransistionScene transition = FindObjectOfType<TransistionScene>();
        transition.fadeOutToScene(fadeOutDuration, "DuelingScene");
    }
}
