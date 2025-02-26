using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketPackageCollider : MonoBehaviour
{
    [Header("Package Detection")]
    [SerializeField] public GameObject[] Package;
    [SerializeField] public float DetectionRadius = 2f;
    [SerializeField] public Transform rocketTransform;
    [SerializeField] public LayerMask packageLayer;       // Layer for touch detection

    [Header("Glow Effect")]
    [SerializeField] public float glowAlphaValue = 0.7f;  // Max alpha value for the glow

    [Header("Spring Joint Settings")]
    [SerializeField] public float springDistance = 1.0f;  // Distance of the "string"
    [SerializeField] public float springDampingRatio = 0.5f; // How bouncy the connection is
    [SerializeField] public float springFrequency = 1.0f; // How stiff the connection is
    [SerializeField] public float springBreakForce = 1000.0f; // How much force is needed to break the connection

    [Header("Package Physics")]
    [SerializeField] public float packageGravityScale = 0.5f;
    [SerializeField] public float packageDrag = 0.5f;

    [Header("Game State References")]
    [SerializeField] private GameWonManager gameWonManager;

    private Dictionary<GameObject, SpriteRenderer> glowRenderers = new Dictionary<GameObject, SpriteRenderer>();
    private GameObject selectedPackage = null;
    private SpringJoint2D springJoint = null;
    private int totalInitialPackages = 0;
    private int packagesDelivered = 0;

    void Start()
    {
        // Find all glow children and cache their renderers
        foreach (GameObject package in Package)
        {
            Transform glowTransform = package.transform.Find("Glow");
            if (glowTransform != null)
            {
                SpriteRenderer glowRenderer = glowTransform.GetComponent<SpriteRenderer>();
                if (glowRenderer != null)
                {
                    glowRenderers.Add(package, glowRenderer);
                    // Initialize all glows to be invisible
                    Color color = glowRenderer.color;
                    color.a = 0;
                    glowRenderer.color = color;
                }
            }
        }

        // Store the initial count of packages for tracking
        totalInitialPackages = Package.Length;

        // Find the GameWonManager if not assigned in inspector
        if (gameWonManager == null)
        {
            gameWonManager = FindObjectOfType<GameWonManager>();
            if (gameWonManager == null)
            {
                Debug.LogWarning("GameWonManager not found in scene, creating one...");
                GameObject managerObj = new GameObject("GameWonManager");
                gameWonManager = managerObj.AddComponent<GameWonManager>();
            }
        }
    }

    void Update()
    {
        UpdateGlowVisibility();
        HandleTouchInput();
    }

    void UpdateGlowVisibility()
    {
        foreach (GameObject package in Package)
        {
            float distance = Vector3.Distance(rocketTransform.position, package.transform.position);

            if (distance <= DetectionRadius && package != selectedPackage)
            {
                // Package is in range, make glow visible
                if (glowRenderers.TryGetValue(package, out SpriteRenderer glowRenderer))
                {
                    Color color = glowRenderer.color;
                    // Optionally make the alpha proportional to distance
                    float alphaRatio = 1.0f - (distance / DetectionRadius);
                    color.a = glowAlphaValue * alphaRatio;
                    glowRenderer.color = color;
                }
            }
            else
            {
                // Package is out of range, make glow invisible
                if (glowRenderers.TryGetValue(package, out SpriteRenderer glowRenderer))
                {
                    Color color = glowRenderer.color;
                    color.a = 0;
                    glowRenderer.color = color;
                }
            }
        }
    }

    void HandleTouchInput()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Convert touch position to world position
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0; // Ensure we're in 2D space

                // Cast a ray to see if we hit a package
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero, 0.1f, packageLayer);

                if (hit.collider != null)
                {
                    GameObject hitPackage = hit.collider.gameObject;

                    // Check if the hit package is in range
                    float distance = Vector3.Distance(rocketTransform.position, hitPackage.transform.position);
                    if (distance <= DetectionRadius)
                    {
                        AttachPackageToRocket(hitPackage);
                    }
                }
            }
        }
    }

    void AttachPackageToRocket(GameObject package)
    {
        // If we already have a package attached, detach it first
        if (selectedPackage != null)
        {
            Destroy(springJoint);
            selectedPackage = null;
        }

        // Set the new selected package
        selectedPackage = package;

        // Add a spring joint to the package if it doesn't have one
        springJoint = selectedPackage.GetComponent<SpringJoint2D>();
        if (springJoint == null)
        {
            springJoint = selectedPackage.AddComponent<SpringJoint2D>();
        }

        // Configure the spring joint using inspector values
        springJoint.connectedBody = rocketTransform.GetComponent<Rigidbody2D>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = Vector2.zero; // Attach to center of rocket
        springJoint.distance = springDistance;
        springJoint.dampingRatio = springDampingRatio;
        springJoint.frequency = springFrequency;
        springJoint.breakForce = springBreakForce;

        // Make sure the package has a Rigidbody2D with specified parameters
        Rigidbody2D packageRigidbody = selectedPackage.GetComponent<Rigidbody2D>();
        if (packageRigidbody == null)
        {
            packageRigidbody = selectedPackage.AddComponent<Rigidbody2D>();
        }

        // Apply the physics settings from inspector
        packageRigidbody.gravityScale = packageGravityScale;
        packageRigidbody.drag = packageDrag;
    }

    // Optional: Add method to visualize the detection radius in the editor
    void OnDrawGizmosSelected()
    {
        if (rocketTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(rocketTransform.position, DetectionRadius);
        }
    }

    public void PackageDelivered(GameObject package)
    {
        // Check if this package is actually in our array before processing
        if (!System.Array.Exists(Package, p => p == package))
        {
            Debug.LogWarning("Attempting to deliver a package that's not in our tracking array!");
            return;
        }

        // If this is the currently selected package, detach it
        if (package == selectedPackage)
        {
            if (springJoint != null)
            {
                Destroy(springJoint);
                springJoint = null;
            }
            selectedPackage = null;
        }

        // Increment the counter of delivered packages
        packagesDelivered++;

        // Show notification if there are multiple packages
        if (totalInitialPackages > 1 && gameWonManager != null)
        {
            gameWonManager.ShowPackageCollected(packagesDelivered, totalInitialPackages);
        }

        // Remove the package from our array
        List<GameObject> packageList = new List<GameObject>(Package);
        packageList.Remove(package);
        Package = packageList.ToArray();

        // Remove from the glow renderers dictionary
        if (glowRenderers.ContainsKey(package))
        {
            glowRenderers.Remove(package);
        }

        // Add debugging to verify package count and delivery
        Debug.Log($"Package delivered. Packages remaining: {Package.Length}, Packages delivered: {packagesDelivered}/{totalInitialPackages}");

        // Check if all packages are delivered
        AllPackagesDelivered();
    }

    void AllPackagesDelivered()
    {
        // Double-check that our counts match
        if (Package.Length == 0 && packagesDelivered == totalInitialPackages)
        {
            // Ensure we have the GameWonManager reference
            if (gameWonManager == null)
            {
                gameWonManager = FindObjectOfType<GameWonManager>();
                if (gameWonManager == null)
                {
                    Debug.LogError("GameWonManager not found, cannot trigger game won state!");
                    return;
                }
            }

            Debug.Log("All packages delivered successfully! Triggering game won state.");

            // Small delay before showing the game won screen
            StartCoroutine(DelayedGameWonTrigger());
        }
        else if (Package.Length == 0 && packagesDelivered != totalInitialPackages)
        {
            Debug.LogError($"Package count mismatch! Array empty but only {packagesDelivered}/{totalInitialPackages} packages delivered.");
        }
    }

    private IEnumerator DelayedGameWonTrigger()
    {
        // Wait a short time to let any animations or effects finish
        yield return new WaitForSeconds(1.0f);

        // Trigger the game won state
        gameWonManager.TriggerGameWon();

        // Disable player controls or other gameplay elements if needed
        // This could be done through a GameManager reference or other methods
    }

    // Check if a GameObject is one of our packages
    public bool IsPackage(GameObject obj)
    {
        return System.Array.Exists(Package, p => p == obj);
    }
}