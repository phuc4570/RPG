using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingDialog : UIView
{
#region Static Method
	public static SettingDialog Show(params object[] @params) => DialogController.Ins.Show(DialogDefine.Setting, @params) as SettingDialog;
	#endregion

	[SerializeField] private List<Button> closeButtons;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private Resolution[] availableResolutions;
    private List<Resolution> filteredResolutions;

    protected override void OnShow()
    {
        base.OnShow();
    }

	protected override void OnHide()
    {
        base.OnHide();
	}

    private void Awake()
    {
        foreach (var button in closeButtons)
        {
            button.onClick.AddListener(OnCloseButtonClick);
        }

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        Init();
    }

    private void OnDestroy()
    {
        foreach (var button in closeButtons)
        {
            button.onClick.RemoveListener(OnCloseButtonClick);
        }

        masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);

        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    private void Init()
    {
        //Volume
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);

        //Resolution
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        availableResolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        double currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            if (Mathf.Approximately((float)availableResolutions[i].refreshRateRatio.value, (float)currentRefreshRate))
            {
                filteredResolutions.Add(availableResolutions[i]);

                string option = availableResolutions[i].width + " x " + availableResolutions[i].height;
                options.Add(option);

                if (availableResolutions[i].width == Screen.currentResolution.width &&
                    availableResolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = filteredResolutions.Count - 1;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();

        SetFullscreen(fullscreenToggle.isOn);
        SetResolution(resolutionDropdown.value);
    }

    private void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    private void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    private void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreenMode = isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution selectedRes = filteredResolutions[resolutionIndex];
        Screen.SetResolution(selectedRes.width, selectedRes.height, Screen.fullScreenMode);
    }

    private void OnCloseButtonClick()
    {
        Hide();
    }
}