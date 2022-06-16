using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    private Resolution[] resolutions;

    public Dropdown resolutionsDropdown;

    private void Awake() 
    {
        LoadSettings();

        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

        }

        resolutionsDropdown.AddOptions(options)

        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }

    public SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
        PlayerPrefs.Save();
    }

    public SetFullscreen (bool isFullscreen)
    {
        Screen.fullscreen = isFullscreen;

        PlayerPrefs.SetInt("Fullscreen", (isFullscreen ? 1 : 0));
        PlayerPrefs.Save();
    }

    public SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullscreen);

        PlayerPrefs.SetInt("ResolutionWidth", resolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", resolution.height);
        PlayerPrefs.Save();
    }

    private LoadSettings()
    {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel", 1));
        Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth", Screen.width),
                             PlayerPrefs.GetInt("ResolutionHeight", Screen.height), 
                             ((PlayerPrefs.GetInt("Fullscreen", 1) == 0) ? false : true));
    }

    private SaveSettings()
    {
        PlayerPrefs.SetInt("QualityLevel", QualitySettings.GetQualityLevel);
        PlayerPrefs.SetInt("ResolutionWidth", Screen.currentResolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", Screen.currentResolution.height);
        PlayerPrefs.SetInt("Fullscreen", (Screen.fullscreen ? 1 : 0));

        PlayerPrefs.Save();
    }
}
