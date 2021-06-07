using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Toggle MusicToggle;
    public Dropdown textureQualityDropdown;
    public Dropdown resolutinonDropdown;
    public Dropdown languageDropdown;
    public Slider VolumeMasterSlider;

    public Button applyButton;
    public Button cancelButton;

    public AudioSource audioSource;
    public GameSettings gameSettings;
    public Resolution[] resolutions;

    public LocalizationManager localization_settings;
    
    private void OnEnable()
    {

       
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullScrenToggle(); Debug.Log("FullScreen settings is changed"); });
        MusicToggle.onValueChanged.AddListener(delegate { OnMusicToggle(); Debug.Log("MusicToggle settings is changed"); });
        resolutinonDropdown.onValueChanged.AddListener(delegate { OnResolutionScreenChange(); Debug.Log("Resolution settings is changed"); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); Debug.Log("Texture Quality settings is changed"); });
        VolumeMasterSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); Debug.Log("Volume master settings is changed"); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); Debug.Log("Apply Button settings is clicked"); });
        cancelButton.onClick.AddListener(delegate {OnCancelButtonClick(); Debug.Log("Cancel Button settings is clicked"); });
        languageDropdown.onValueChanged.AddListener(delegate { OnLanguageChange(); Debug.Log("Dropdown value is  " + languageDropdown.value); });
        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutinonDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        LoadSettings();
    }

    public void OnFullScrenToggle()
    {
      gameSettings.fullscreen =  Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnMusicToggle()
    {
        audioSource.mute = gameSettings.music = MusicToggle.isOn;
    }

    public void OnResolutionScreenChange()
    {
        Screen.SetResolution(resolutions[resolutinonDropdown.value].width, resolutions[resolutinonDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutinonDropdown.value;
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;
       
    }

    public void OnLanguageChange()
    {
        
        localization_settings.SetLanguage(languageDropdown.value);
        gameSettings.language = languageDropdown.value;
    }

    public void OnMusicVolumeChange()
    {
        audioSource.volume = gameSettings.musicVolume =  VolumeMasterSlider.value;
    }

    public void OnApplyButtonClick()
    {
        SaveSettings();
    }

    public void OnCancelButtonClick()
    {
        LoadSettings();
    }

    public void SaveSettings()
    {
        Debug.Log("Save Settings");
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings()
    {
        Debug.Log("Load Settings");
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

        VolumeMasterSlider.value = gameSettings.musicVolume;
        textureQualityDropdown.value = gameSettings.textureQuality;
        resolutinonDropdown.value = gameSettings.resolutionIndex;
        fullscreenToggle.isOn = gameSettings.fullscreen;
        MusicToggle.isOn = gameSettings.music;
        languageDropdown.value = gameSettings.language;
        resolutinonDropdown.RefreshShownValue();
    }
}
