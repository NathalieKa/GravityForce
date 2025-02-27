using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{

    //Autor: Korte

    [SerializeField] private int scoreValue = 100;  //Score to add per Package
    [SerializeField] private float destroyDelay = 1.0f; // Time before package disappears after delivery
    [SerializeField] private RocketPackageCollider rocketController;
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        // Ensure the delivery zone is a trigger collider
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }

        // If rocketController is not set in inspector, try to find it in scene
        // Should no longer be necessary but well keep it as a failsafe
        if (rocketController == null)
        {
            rocketController = FindObjectOfType<RocketPackageCollider>();
            Debug.Log("Auto-found RocketPackageCollider: " + (rocketController != null));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called with: " + other.gameObject.name);

        // Check if the colliding object is one of our tracked packages
        if (rocketController != null && rocketController.IsPackage(other.gameObject))
        {
            Debug.Log("Package recognized: " + other.gameObject.name);

            // Notify the package it's been delivered
            rocketController.PackageDelivered(other.gameObject);
            Debug.Log("PackageDelivered called on: " + other.gameObject.name);

            // Destroy the package after a delay
            Destroy(other.gameObject, destroyDelay);
            Debug.Log("Destroy scheduled for: " + other.gameObject.name + " after delay: " + destroyDelay);
            gameManager.AddScore(scoreValue);
        }
        else
        {
            Debug.Log("Not a recognized package or rocketController is null");
        }
    }
}