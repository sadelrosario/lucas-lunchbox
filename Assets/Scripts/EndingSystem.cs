using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingSystem : MonoBehaviour
{
    // please do not delete the door, or else this scene will break.

    public Animator lucaAnim;
    public GameObject ui;
    public Text score;
    public Text endingName;

    private LogicScript logic;
    private int totalScore, GGG;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    void Start()
    {
        logic = GameObject.Find("LogicManager").GetComponent<LogicScript>();
        if (DontDestroy.Instance != null)
        {
            totalScore = DontDestroy.Instance.DD_Score;
            GGG = DontDestroy.Instance.DD_GGG;
        }

        score.text = totalScore.ToString();

        if (totalScore < 50)
        {
            //starved ending
            lucaAnim.Play("luca_bad");
            endingName.text = "BAD ENDING";
            audioManager.PlaySFX(audioManager.fail);
        }
        else
        {
            audioManager.PlaySFX(audioManager.win);
            if (GGG == 1)
            {
                //go ending
                lucaAnim.Play("luca_go");
                endingName.text = "GO ENDING";
            }
            else if (GGG == 2)
            {
                //grow ending
                lucaAnim.Play("luca_grow");
                endingName.text = "GROW ENDING";
            }
            else if (GGG == 3)
            {
                //glow ending
                lucaAnim.Play("luca_glow");
                endingName.text = "GLOW ENDING";
            }
            else if (GGG == 4)
            {
                //healthy ending
                lucaAnim.Play("luca_good");
                endingName.text = "BEST ENDING";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUI()
    {
        ui.SetActive(true);
        //Debug.Log("EVENT IS BEING TOUCHED");
    }

    public void RetryGame()
    {
        Debug.Log("You should be retrying game NOW");
        SceneManager.LoadScene("Game");
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Title");
    }
}
