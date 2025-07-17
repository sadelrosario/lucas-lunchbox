using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingButtons : MonoBehaviour
{
    public Button openSettings;
    public Button closeSettings;
    public Button tutorial;
    public TextMeshProUGUI settingsText;
    public Image settingsBG;
    public Slider musicSlide;
    public Slider SFXSlide;
    public 

    public void SettingsOpened()
    {
        closeSettings.gameObject.SetActive(true);
        closeSettings.interactable = true;

        settingsText.gameObject.SetActive(true);
        settingsBG.gameObject.SetActive(true);

        musicSlide.gameObject.SetActive(true);
        SFXSlide.gameObject.SetActive(true);

        openSettings.gameObject.SetActive(false);
        tutorial.gameObject.SetActive(false);
    }
    public void SettingsClosed()
    {
        openSettings.gameObject.SetActive(true);
        openSettings.interactable = true;

        settingsText.gameObject.SetActive(false);
        settingsBG.gameObject.SetActive(false);

        musicSlide.gameObject.SetActive(false);
        SFXSlide.gameObject.SetActive(false);

        closeSettings.gameObject.SetActive(false);

        tutorial.gameObject.SetActive(true);
        tutorial.interactable = true;
    }
}
