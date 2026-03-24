using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIController : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool paused = false;

    void Start()
    {
        HidePauseMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                HidePauseMenu();
            else
                ShowPauseMenu();
        }
    }

    private void ShowPauseMenu()
    {
        paused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        // Pausa movimentações com Rigidbody2D
        foreach (Rigidbody2D rb in Object.FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None))
        {
            rb.simulated = false;
        }
    }

    private void HidePauseMenu()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        // Retoma movimentações com Rigidbody2D
        foreach (Rigidbody2D rb in Object.FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None))
        {
            rb.simulated = true;
        }
    }

    public void BTN_Resume()
    {
        HidePauseMenu();
    }

    public void BTN_Quit()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
