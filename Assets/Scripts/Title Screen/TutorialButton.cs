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

    public void TutorialOpened()
    {
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
