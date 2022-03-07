using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [Header ("Pause Menu")]
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    CursorLockMode desiredMode;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    #region Main Menu Script
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Level Prototype");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }
    #endregion

    #region Pause Menu Script

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        desiredMode = CursorLockMode.Confined;

    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        desiredMode = CursorLockMode.None;
        {
            Cursor.lockState = desiredMode;
        }
    }

    public void LoadMenu()
    {
        Debug.Log("loading menu...");
        SceneManager.LoadScene("8108 Main Menu");
    }
    #endregion

    #region Win & Lose Conditions
    public void YouWin()
    {
        SceneManager.LoadScene("Win Screen");
    }

    public void YouLose()
    {
        SceneManager.LoadScene("Game Over Screen");
    }
    #endregion
}