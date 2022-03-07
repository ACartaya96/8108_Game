using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [Header ("Pause Menu")]
    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    CursorLockMode desiredMode;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        
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
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        Cursor.visible = true;
        Cursor .lockState= CursorLockMode.None;
        SceneManager.LoadScene("Win Screen");
   
    }

    public void YouLose()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Game Over Screen");
  
    }
    #endregion
}
