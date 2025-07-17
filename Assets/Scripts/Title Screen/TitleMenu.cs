using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    AudioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    public void StartGame()
    {
        audioManager.PlaySFX(audioManager.gen_click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
