using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [Header("Game Over Text Settings")]
    [SerializeField] private string gameOverMessage = "GAME OVER";
    [SerializeField] private float fontSize = 72f;
    [SerializeField] private TMP_FontAsset customFont;

    // Flash settings for the text
    [SerializeField] private float flashSpeed = 2f;
    [SerializeField] private Color flashColor1 = Color.red;
    [SerializeField] private Color flashColor2 = new Color(0.8f, 0, 0);

    [Header("Game Over Text Outline Settings")]
    [SerializeField] private bool addTextOutline = false;
    [SerializeField] private Color textOutlineColor = Color.black;
    [SerializeField] private float textOutlineWidth = 0.2f;

    [Header("Button Settings")]
    [SerializeField] private Vector2 buttonSize = new Vector2(200f, 50f);
    [SerializeField] private float buttonSpacing = 20f;
    [SerializeField] private float buttonYOffset = 50f;
    [SerializeField] private Color buttonColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    [SerializeField] private Color buttonTextColor = Color.white;

    [Header("Button Transition Settings")]
    [SerializeField] private Color buttonHighlightedColor = new Color(0.3f, 0.3f, 0.3f, 1f);
    [SerializeField] private Color buttonPressedColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    [SerializeField] private Color buttonSelectedColor = new Color(0.25f, 0.25f, 0.25f, 1f);
    [SerializeField] private Color buttonDisabledColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    [SerializeField] private float buttonFadeDuration = 0.1f;

    [Header("Button Text Settings")]
    [SerializeField] private float buttonTextFontSize = 24f;
    [SerializeField] private TextAlignmentOptions buttonTextAlignment = TextAlignmentOptions.Center;
    [SerializeField] private TMP_FontAsset buttonCustomFont;

    private Canvas mainCanvas;
    private GameObject gameOverContainer;
    private TextMeshProUGUI gameOverText;

    // Singleton pattern to ensure only one instance of the GameOverManager exists
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

        CreateGameOverUI();
    }

    private void CreateGameOverUI()
    {
        // Create container
        gameOverContainer = new GameObject("GameOver Container");
        gameOverContainer.transform.SetParent(mainCanvas.transform, false);

        RectTransform containerRect = gameOverContainer.AddComponent<RectTransform>();
        containerRect.anchorMin = new Vector2(0.5f, 0.5f);
        containerRect.anchorMax = new Vector2(0.5f, 0.5f);
        containerRect.pivot = new Vector2(0.5f, 0.5f);
        containerRect.sizeDelta = new Vector2(800f, 400f); // Increased height for buttons

        // Create Game Over text
        GameObject textObj = new GameObject("GameOver Text");
        textObj.transform.SetParent(containerRect, false);
        gameOverText = textObj.AddComponent<TextMeshProUGUI>();
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0, 0.5f);
        textRect.anchorMax = new Vector2(1, 1f);
        textRect.sizeDelta = Vector2.zero;

        // Set text properties
        gameOverText.text = gameOverMessage;
        gameOverText.fontSize = fontSize;
        gameOverText.alignment = TextAlignmentOptions.Center;
        if (customFont != null)
        {
            gameOverText.font = customFont;
        }
        // Apply outline settings if enabled
        if (addTextOutline)
        {
            gameOverText.outlineColor = textOutlineColor;
            gameOverText.outlineWidth = textOutlineWidth;
        }

        // Calculate horizontal offset based on button width and spacing
        float horizontalOffset = (buttonSize.x + buttonSpacing) / 2f;
        CreateButton("MainMenuButton", "Main Menu", -horizontalOffset, () => SceneManager.LoadScene("MainMenu"));
        CreateButton("RestartButton", "Restart", horizontalOffset, () => SceneManager.LoadScene("LoadingScreen"));

        // Initially hide the container
        gameOverContainer.SetActive(false);
    }

    private void CreateButton(string name, string text, float xOffset, UnityEngine.Events.UnityAction onClick)
    {
        GameObject buttonObj = new GameObject(name);
        buttonObj.transform.SetParent(gameOverContainer.transform, false);

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

    public void TriggerGameOver()
    {
        // Disable all other UI elements except the game over container
        foreach (Transform child in mainCanvas.transform)
        {
            if (child.gameObject != gameOverContainer)
            {
                child.gameObject.SetActive(false);
            }
        }

        gameOverContainer.SetActive(true);
        StartCoroutine(FlashText());
    }

    // Coroutine to flash the game over text between two colors
    private IEnumerator FlashText()
    {
        while (true)
        {
            float t = Mathf.PingPong(Time.time * flashSpeed, 1f);
            gameOverText.color = Color.Lerp(flashColor1, flashColor2, t);
            yield return null;
        }
    }
}
