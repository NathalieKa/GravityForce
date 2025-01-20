using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Toggle motionControlsToggle; // Assign in the inspector

    private void Start()
    {
        // Initialize the toggle state from the singleton
        if (GameSettings.Instance != null)
        {
            motionControlsToggle.isOn = GameSettings.Instance.motionControl;
        }

        // Add a listener to handle toggle changes
        motionControlsToggle.onValueChanged.AddListener(OnMotionControlToggleChanged);
    }

    private void OnMotionControlToggleChanged(bool isOn)
    {
        // Update the singleton state
        if (GameSettings.Instance != null)
        {
            GameSettings.Instance.motionControl = isOn;
        }
    }
}