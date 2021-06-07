using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuLoaded : MonoBehaviour
{
    public Dropdown DropDown;
    public GameObject menuUI_PopUpBlock;
    public GameObject menuUI_Quit;
    public GameObject menuUI_Settings;
    public GameObject menuUI_Play;

    public static bool Quit_Window = false;
    public static bool PopUp_Block = false;
    public static bool Settings_Window = false;
    public static bool Play_Window = false;

    public Button button_level_1;
    public Button button_level_2;
    public Button button_level_3;
    public Button button_play;

    public string lelN = "Scenes/MainMenu";

    public LevelManager level;

    public TextMeshProUGUI Level1_Text;
    public TextMeshProUGUI Level2_Text;
    public TextMeshProUGUI Level3_Text;
    public TextMeshProUGUI Level_Clear;

    //void OnEnable()
    //{
    //    Debug.Log("OnEnable called");
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //// called second
    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    Debug.Log("OnSceneLoaded: " + scene.name);
    //    Debug.Log(mode);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}


    private void Start()
    {
    
        //    Screen.SetResolution(1920, 1080, true, 60);
        menuUI_PopUpBlock.SetActive(false);
        menuUI_Play.SetActive(false);
        menuUI_Quit.SetActive(false);
        menuUI_Settings.SetActive(false);
        button_play.interactable = false;
        button_level_1.onClick.AddListener(delegate { Play_Button("Scenes/Level_1"); Debug.Log("Level 1 textplay.info has changed"); /*Level1_Text.text = "level1_text";*/ });
        button_level_2.onClick.AddListener(delegate { Play_Button("Scenes/Level_2"); Debug.Log("Level 2 textplay.info has changed"); /*Level2_Text.text = "level2_text";*/ });
        button_level_3.onClick.AddListener(delegate { Play_Button("Scenes/Level_3"); Debug.Log("Level 3 textplay.info has changed"); /*Level3_Text.text = "level3_text"; */});
        button_play.onClick.AddListener(delegate { levelLoad_button(lelN);});
    }

    // Update is called once per frame  

    
    public void SettingsOn()
    {
        Debug.Log("Open PopUp Window Setiings");
        Debug.Log("Open PopUp Block Layer ");
        menuUI_PopUpBlock.SetActive(true);
        menuUI_Settings.SetActive(true);
        Settings_Window = true;
    }

 

    public void QuitGame()
    {
        Debug.Log("Open PopUp Window Quit");
        Debug.Log("Open PopUp Block Layer");
        menuUI_PopUpBlock.SetActive(true);
        menuUI_Quit.SetActive(true);
        Quit_Window = true;
        
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
        Quit_Window = false;
    }

    public void PlayMenu()
    {
        Debug.Log("Open PopUp Window Play Menu");
        Debug.Log("Open PopUp Block Layer");
        menuUI_PopUpBlock.SetActive(true);
        menuUI_Play.SetActive(true);
        Play_Window = true;
        Debug.Log("textplay.info has cleared");
        Level_Clear.text = "";

    }

    public void Back_Play_Button()
    {
        Debug.Log("Back Button was pressed");
        Debug.Log("Play Window was closed");
        menuUI_PopUpBlock.SetActive(false);
        menuUI_Play.SetActive(false);
    }

    public void Back_Settings_Button()
    {
        Debug.Log("Back Button was pressed");
        Debug.Log("Play Window was closed");
        menuUI_PopUpBlock.SetActive(false);
        menuUI_Play.SetActive(false);
        menuUI_Settings.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }




    public string Play_Button(string Level_Name)
    {
        button_play.interactable = true;
        Debug.Log("Level " + Level_Name + " is selected");
        return lelN = Level_Name;
    }
   
    public void levelLoad_button(string lelNew)
    {
        string levelthatloaded = lelNew;
        Debug.Log("Level " + levelthatloaded + " is Loaded");
        //SceneManager.LoadScene(levelthatloaded);
        level.LoadLevel(levelthatloaded);
    }

    public void LoadPlayerCurrency()
    {
       // SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.save");
    }
}
