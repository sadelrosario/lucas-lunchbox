using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Analytics;

public class SettingButtons : MonoBehaviour
{
    public Button openSettings;
    public Button closeSettings;
    public Button tutorial;
    public TextMeshProUGUI settingsText;
    public Image settingsBG;
    public Slider musicSlide;
    public Slider SFXSlide;

    AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void SettingsOpened()
    {
        audioManager.PlaySFX(audioManager.gen_click);

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
        audioManager.PlaySFX(audioManager.gen_click);

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
