using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameWonManager : MonoBehaviour
{
    public static GameWonManager Instance { get; private set; }

    [SerializeField] private GameManager gameManager;

    [Header("Game Won Text Settings")]
    [SerializeField] private string gameWonMessage = "LEVEL COMPLETE";
    [SerializeField] private float fontSize = 72f;
    [SerializeField] private TMP_FontAsset customFont;

    // Flash settings for the text
    [SerializeField] private float flashSpeed = 2f;
    [SerializeField] private Color flashColor1 = Color.green;
    [SerializeField] private Color flashColor2 = new Color(0, 0.8f, 0);

    [Header("Game Won Text Outline Settings")]
    [SerializeField] private bool addTextOutline = false;
    [SerializeField] private Color textOutlineColor = Color.black;
    [SerializeField] private float textOutlineWidth = 0.2f;

    [Header("Button Settings")]
    [SerializeField] private Vector2 buttonSize = new Vector2(200f, 50f);
    [SerializeField] private float buttonSpacing = 20f;
    [SerializeField] private float buttonYOffset = 50f;
    [SerializeField] private Color buttonColor = new Color(0.2f, 0.5f, 0.2f, 1f);
    [SerializeField] private Color buttonTextColor = Color.white;

    [Header("Button Transition Settings")]
    [SerializeField] private Color buttonHighlightedColor = new Color(0.3f, 0.6f, 0.3f, 1f);
    [SerializeField] private Color buttonPressedColor = new Color(0.1f, 0.4f, 0.1f, 1f);
    [SerializeField] private Color buttonSelectedColor = new Color(0.25f, 0.5f, 0.25f, 1f);
    [SerializeField] private Color buttonDisabledColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    [SerializeField] private float buttonFadeDuration = 0.1f;

    [Header("Button Text Settings")]
    [SerializeField] private float buttonTextFontSize = 24f;
    [SerializeField] private TextAlignmentOptions buttonTextAlignment = TextAlignmentOptions.Center;
    [SerializeField] private TMP_FontAsset buttonCustomFont;

    [Header("Package Collection Notification")]
    [SerializeField] private float packageNotificationDuration = 2f;
    [SerializeField] private float packageNotificationFontSize = 36f;
    [SerializeField] private Color packageNotificationColor = Color.white;

    private Canvas mainCanvas;
    private GameObject gameWonContainer;
    private TextMeshProUGUI gameWonText;
    private GameObject packageNotificationObj;
    private TextMeshProUGUI packageNotificationText;

    // Singleton pattern to ensure only one instance of the GameWonManager exists
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        mainCanvas = FindObjectOfType<Canvas>();
        if (mainCanvas == null)
        {
            GameObject canvasObj = new GameObject("MainCanvas");
            mainCanvas = canvasObj.AddComponent<Canvas>();
            mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }

        CreateGameWonUI();
        CreatePackageNotification();
    }

    private void CreateGameWonUI()
    {
        // Create container
        gameWonContainer = new GameObject("GameWon Container");
        gameWonContainer.transform.SetParent(mainCanvas.transform, false);

        RectTransform containerRect = gameWonContainer.AddComponent<RectTransform>();
        containerRect.anchorMin = new Vector2(0.5f, 0.5f);
        containerRect.anchorMax = new Vector2(0.5f, 0.5f);
        containerRect.pivot = new Vector2(0.5f, 0.5f);
        containerRect.sizeDelta = new Vector2(800f, 400f); // Increased height for buttons

        // Create Game Won text
        GameObject textObj = new GameObject("GameWon Text");
        textObj.transform.SetParent(containerRect, false);
        gameWonText = textObj.AddComponent<TextMeshProUGUI>();
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0, 0.5f);
        textRect.anchorMax = new Vector2(1, 1f);
        textRect.sizeDelta = Vector2.zero;

        // Set text properties
        gameWonText.text = gameWonMessage;
        gameWonText.fontSize = fontSize;
        gameWonText.alignment = TextAlignmentOptions.Center;
        if (customFont != null)
        {
            gameWonText.font = customFont;
        }
        // Apply outline settings if enabled
        if (addTextOutline)
        {
            gameWonText.outlineColor = textOutlineColor;
            gameWonText.outlineWidth = textOutlineWidth;
        }

        // Calculate horizontal offset based on button width and spacing
        float horizontalOffset = (buttonSize.x + buttonSpacing) / 2f;
        CreateButton("MainMenuButton", "Main Menu", -horizontalOffset, () => SceneManager.LoadScene("MainMenu"));
        CreateButton("NextLevelButton", "Continue", horizontalOffset, LoadNextLevel);

        // Initially hide the container
        gameWonContainer.SetActive(false);
    }

    private void CreatePackageNotification()
    {
        packageNotificationObj = new GameObject("Package Notification");
        packageNotificationObj.transform.SetParent(mainCanvas.transform, false);

        RectTransform notifRect = packageNotificationObj.AddComponent<RectTransform>();
        notifRect.anchorMin = new Vector2(0.5f, 0.7f);
        notifRect.anchorMax = new Vector2(0.5f, 0.7f);
        notifRect.pivot = new Vector2(0.5f, 0.5f);
        notifRect.sizeDelta = new Vector2(600f, 100f);

        packageNotificationText = packageNotificationObj.AddComponent<TextMeshProUGUI>();
        packageNotificationText.alignment = TextAlignmentOptions.Center;
        packageNotificationText.fontSize = packageNotificationFontSize;
        packageNotificationText.color = packageNotificationColor;
        if (customFont != null)
        {
            packageNotificationText.font = customFont;
        }

        // Apply outline for better visibility
        packageNotificationText.outlineWidth = 0.2f;
        packageNotificationText.outlineColor = Color.black;

        // Initially hide the notification
        packageNotificationObj.SetActive(false);
    }

    private void CreateButton(string name, string text, float xOffset, UnityEngine.Events.UnityAction onClick)
    {
        GameObject buttonObj = new GameObject(name);
        buttonObj.transform.SetParent(gameWonContainer.transform, false);

        // Button setup
        RectTransform buttonRect = buttonObj.AddComponent<RectTransform>();
        buttonRect.anchorMin = new Vector2(0.5f, 0.5f);  // Center anchor
        buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
        buttonRect.pivot = new Vector2(0.5f, 0.5f);
        buttonRect.sizeDelta = buttonSize;
        buttonRect.anchoredPosition = new Vector2(xOffset, -buttonYOffset);  // Position from bottom

        Button button = buttonObj.AddComponent<Button>();
        Image buttonImage = buttonObj.AddComponent<Image>();
        buttonImage.color = buttonColor;
        button.targetGraphic = buttonImage;

        // Configure button transitions
        ColorBlock cb = button.colors;
        cb.normalColor = buttonColor;
        cb.highlightedColor = buttonHighlightedColor;
        cb.pressedColor = buttonPressedColor;
        cb.selectedColor = buttonSelectedColor;
        cb.disabledColor = buttonDisabledColor;
        cb.fadeDuration = buttonFadeDuration;
        button.colors = cb;

        // Button text setup
        GameObject buttonTextObj = new GameObject("Text");
        buttonTextObj.transform.SetParent(buttonObj.transform, false);
        TextMeshProUGUI buttonText = buttonTextObj.AddComponent<TextMeshProUGUI>();

        RectTransform buttonTextRect = buttonTextObj.GetComponent<RectTransform>();
        buttonTextRect.anchorMin = Vector2.zero;
        buttonTextRect.anchorMax = Vector2.one;
        buttonTextRect.sizeDelta = Vector2.zero;

        buttonText.text = text;
        buttonText.alignment = buttonTextAlignment;
        buttonText.color = buttonTextColor;
        buttonText.fontSize = buttonTextFontSize;
        if (buttonCustomFont != null)
        {
            buttonText.font = buttonCustomFont;
        }
        else if (customFont != null)
        {
            buttonText.font = customFont;
        }

        button.onClick.AddListener(onClick);
    }

    public void ShowPackageCollected(int current, int total)
    {
        if (total > 1)
        {
            packageNotificationText.text = $"Package {current} of {total} Collected!";
            packageNotificationObj.SetActive(true);
            packageNotificationText.enableWordWrapping = false;
            StartCoroutine(HidePackageNotification());
        }
    }

    private IEnumerator HidePackageNotification()
    {
        yield return new WaitForSeconds(packageNotificationDuration);
        packageNotificationObj.SetActive(false);
    }

    public void TriggerGameWon()
    {
        // Disable all other UI elements except the game won container
        foreach (Transform child in mainCanvas.transform)
        {
            if (child.gameObject != gameWonContainer && child.gameObject.name != "background")
            {
                child.gameObject.SetActive(false);
            }
        }

        gameWonContainer.SetActive(true);
        StartCoroutine(FlashText());

        // Update the Continue button text based on current scene
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Game2")
        {
            Transform nextLevelButton = gameWonContainer.transform.Find("NextLevelButton");
            if (nextLevelButton != null)
            {
                TextMeshProUGUI buttonText = nextLevelButton.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = "Finish";
                }
            }
        }

    }

    private void LoadNextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Game0":
                SceneManager.LoadScene("Game1");
                break;
            case "Game1":
                SceneManager.LoadScene("Game2");
                break;
            case "Game2":
            default:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    // Coroutine to flash the game won text between two colors
    private IEnumerator FlashText()
    {
        while (true)
        {
            float t = Mathf.PingPong(Time.time * flashSpeed, 1f);
            gameWonText.color = Color.Lerp(flashColor1, flashColor2, t);
            yield return null;
        }
    }
}