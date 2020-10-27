using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameSettingManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Slider musicVolumeSlider;
    public Slider SFXVolumeSlider;

    public AudioSource musicSource;
    public AudioSource SFXSource;
    public Button applyButton;

    private Resolution[] resolutions;
    private GameSettings gameSettings;
    private Text musicPercentText;
    private Text SFXPercentText;

    private void OnEnable()
    {
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChanged(); });
        qualityDropdown.onValueChanged.AddListener(delegate { OnQualityChanged(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChanged(); });
        SFXVolumeSlider.onValueChanged.AddListener(delegate { OnSFXVolumeChanged(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

        resolutions = Screen.resolutions;
        foreach (var resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
            if (Screen.currentResolution.width == resolution.width && Screen.currentResolution.height == resolution.height)
            {
                resolutionDropdown.value = resolutionDropdown.options.Count - 1;
            }
        }

        fullscreenToggle.isOn = Screen.fullScreen;
        musicPercentText = musicVolumeSlider.GetComponentInChildren<Text>();
        musicPercentText.text = $"{Mathf.Round(musicVolumeSlider.value * 100)}%";
        SFXPercentText = SFXVolumeSlider.GetComponentInChildren<Text>();
        SFXPercentText.text = $"{Mathf.Round(SFXVolumeSlider.value * 100)}%";

        if (File.Exists(Application.persistentDataPath + "/GameSetting.json"))
            LoadSettings();

    }

    public void OnFullscreenToggle()
    {
        applyButton.interactable = true;
        gameSettings.Fullscreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChanged()
    {
        applyButton.interactable = true;
        gameSettings.ResolutionIndex = resolutionDropdown.value;
    }
    public void OnQualityChanged()
    {
        applyButton.interactable = true;
        gameSettings.QualityIndex = qualityDropdown.value;
    }
    public void OnMusicVolumeChanged()
    {
        applyButton.interactable = true;
        gameSettings.MusicVolume = musicSource.volume = musicVolumeSlider.value;
        musicPercentText.text = $"{Mathf.Round(musicVolumeSlider.value * 100)}%";
    }
    public void OnSFXVolumeChanged()
    {
        applyButton.interactable = true;
        gameSettings.SFXVolume = SFXVolumeSlider.value;
        //gameSettings.SFXVolume = SFXSource.volume = SFXVolumeSlider.value;
        SFXPercentText.text = $"{Mathf.Round(SFXVolumeSlider.value * 100)}%";
    }

    public void OnApplyButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        Screen.fullScreen = gameSettings.Fullscreen;
        Screen.SetResolution(resolutions[gameSettings.ResolutionIndex].width, resolutions[gameSettings.ResolutionIndex].height, gameSettings.Fullscreen);
        QualitySettings.masterTextureLimit = gameSettings.QualityIndex;
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/GameSetting.json", jsonData);
        applyButton.interactable = false;
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/GameSetting.json"));
        musicVolumeSlider.value = gameSettings.MusicVolume;
        SFXVolumeSlider.value = gameSettings.SFXVolume;
        musicPercentText.text = $"{Mathf.Round(musicVolumeSlider.value * 100)}%";
        SFXPercentText.text = $"{Mathf.Round(SFXVolumeSlider.value * 100)}%";
        qualityDropdown.value = gameSettings.QualityIndex;
        resolutionDropdown.value = gameSettings.ResolutionIndex;
        fullscreenToggle.isOn = gameSettings.Fullscreen;
    }
}
