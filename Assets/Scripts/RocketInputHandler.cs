using UnityEngine;
using UnityEngine.UIElements;

public class RocketInputHandler : MonoBehaviour
{
    RocketController rocketController;

    [Header("Tilt Control Settings")]
    [SerializeField] private bool useTiltRotation = true;
    [SerializeField] private float tiltSensitivity = 3f;
    [SerializeField] private float smoothing = 0.1f;
    [SerializeField] private float deadzone = 0.1f; // Ignore small tilts

    private float verticalValue = 0;   // For thrust
    private float horizontalValue = 0;  // For rotation
    private float currentTiltAngle = 0f;
    private float targetTiltAngle = 0f;

    private void Awake()
    {
        rocketController = GetComponent<RocketController>();
        // Initialize accelerometer if available
        if (SystemInfo.supportsAccelerometer)
        {
            Input.gyro.enabled = true;
        }
    }
    private void Start()
    {
       //Set UseTiltRotation to the value stored in PlayerPrefs
         if(PlayerPrefs.GetInt("motionControls") == 1)
         {
             useTiltRotation = true;
         }
         else if (PlayerPrefs.GetInt("motionControls") == 0)
         {
             useTiltRotation = false;
         }
         else
         {
             useTiltRotation = true;
         }
    }


    // Thrust button methods (unchanged)
    public void SetVerticalValueToOne()
    {
        verticalValue = 1;
    }

    public void SetVerticalValueToZero()
    {
        verticalValue = 0;
    }

    // Legacy rotation methods - only used when tilt is disabled
    public void SetHorizobtalValueToMinusOne()
    {
        if (!useTiltRotation) horizontalValue = -1;
    }

    public void SetHorizobtalValueToZero()
    {
        if (!useTiltRotation) horizontalValue = 0;
    }

    public void SetHorizobtalValueToOne()
    {
        if (!useTiltRotation) horizontalValue = 1;
    }



    private void Update()
    {
        Vector2 inputVector = Vector2.zero;

        if (useTiltRotation && SystemInfo.supportsAccelerometer)
        {
            float tiltX = Input.acceleration.x;

            if (Mathf.Abs(tiltX) > deadzone)
            {
                targetTiltAngle = tiltX * tiltSensitivity;
            }
            else
            {
                targetTiltAngle = 0f;
            }

            currentTiltAngle = Mathf.Lerp(currentTiltAngle, targetTiltAngle, smoothing);
            horizontalValue = Mathf.Clamp(currentTiltAngle, -1f, 1f);
        }

        inputVector.x = horizontalValue;
        inputVector.y = verticalValue;

        rocketController.SetInputVector(inputVector);
    }



    public void CalibrateNeutralPosition()
    {
        if (SystemInfo.supportsAccelerometer)
        {
            // Reset current values
            currentTiltAngle = 0f;
            targetTiltAngle = 0f;
            horizontalValue = 0f;
        }
    }
}