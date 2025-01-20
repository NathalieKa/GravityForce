using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private bool allPackagesCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rocket"))
        {
            if (allPackagesCollected)
            {
                Debug.Log("Alle Pakete eingesammelt! Du hast das Spiel geschafft.");
                ShowEndGameOptions();
            }
            else
            {
                Debug.Log("Nicht alle Pakete eingesammelt! Sammle zuerst alle Pakete ein.");
            }
        }
    }

    public void SetAllPackagesCollected()
    {
        allPackagesCollected = true;
    }

    void ShowEndGameOptions()
    {
        // Zeigt Optionen für das Spielende an
        // Option 1: Hauptmenü laden
        // Option 2: Aktuelle Szene neu starten
        Invoke("LoadMainMenu", 3f);  // Nach 3 Sekunden zum Hauptmenü
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
