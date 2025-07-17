using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialButton : MonoBehaviour
{
    public Button openTutorial;
    public Button closeTutorial;
    public Button settings;
    public TextMeshProUGUI tutorialText;
    public Image tutorialBG;
    public Image tutorial;

    AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void TutorialOpened()
    {
        audioManager.PlaySFX(audioManager.gen_click);

        closeTutorial.gameObject.SetActive(true);
        closeTutorial.interactable = true;

        tutorialText.gameObject.SetActive(true);
        tutorialBG.gameObject.SetActive(true);
        tutorial.gameObject.SetActive(true);

        openTutorial.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
    }
    public void TutorialClosed()
    {
        audioManager.PlaySFX(audioManager.gen_click);

        openTutorial.gameObject.SetActive(true);
        openTutorial.interactable = true;

        tutorialText.gameObject.SetActive(false);
        tutorialBG.gameObject.SetActive(false);
        tutorial.gameObject.SetActive(false);

        closeTutorial.gameObject.SetActive(false);

        settings.gameObject.SetActive(true);
        settings.interactable = true;
    }
}
