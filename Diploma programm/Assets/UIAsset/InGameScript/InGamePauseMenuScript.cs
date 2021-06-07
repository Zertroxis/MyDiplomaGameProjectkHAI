using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePauseMenuScript : MonoBehaviour
{

    public GameObject menuUI_PopUpBlock;
    public GameObject menuUI_Quit;
    public GameObject menuUI_Settings;
    public GameObject menuUI_MainMenu;

    public GameObject menuUI_Pause;

    public static bool GameIsPaused = false;

    private void Start()
    {
        menuUI_Pause.SetActive(false);
        menuUI_MainMenu.SetActive(false);
        menuUI_PopUpBlock.SetActive(false);
        menuUI_Quit.SetActive(false);
        menuUI_Settings.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

   public void Resume()
    {
        menuUI_Pause.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

   public void Pause()
    {
        menuUI_Pause.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void SettingsOn()
    {
        Debug.Log("Open PopUp Window Setiings");
        Debug.Log("Open PopUp Block Layer ");
        menuUI_PopUpBlock.SetActive(true);
        menuUI_Settings.SetActive(true);

    }

    public void Back_Settings_Button()
    {
        Debug.Log("Back Button was pressed");
        Debug.Log("Play Window was closed");
        menuUI_PopUpBlock.SetActive(false);
        menuUI_Settings.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Open PopUp Window Quit");
        Debug.Log("Open PopUp Block Layer");
        menuUI_PopUpBlock.SetActive(true);
        menuUI_Quit.SetActive(true);
    }
    
    public void MainMenu_PopUp()
    {
        Debug.Log("Open PopUp Main Menu");
        menuUI_MainMenu.SetActive(true);
        menuUI_PopUpBlock.SetActive(true);
    }

    public void Yes_QuitGame()
    {
        Debug.Log("Quit the game");
        Application.Quit();
    }

    public void No_QuitGame()
    {
        Debug.Log("Cancel Quit the game");
        menuUI_PopUpBlock.SetActive(false);
        menuUI_Quit.SetActive(false);
    }

    public void No_BackToMainMenu()
    {
        Debug.Log("Cancel go to Main Menu");
        menuUI_PopUpBlock.SetActive(false);
        menuUI_MainMenu.SetActive(false);
    }

    public void Yes_BackToMainMenu()
    {
        Debug.Log("Main Menu scene loaded");
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
