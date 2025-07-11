using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingSystem : MonoBehaviour
{
    public Animator lucaAnim;
    public GameObject ui;
    public Text score;
    public Text endingName;

    private LogicScript logic;
    private int totalScore, GGG;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.Find("LogicManager").GetComponent<LogicScript>();
        if (DontDestroy.Instance != null)
        {
            totalScore = DontDestroy.Instance.DD_Score;
            GGG = DontDestroy.Instance.DD_GGG;
        }
    }

    // Update is called once per frame
    void Update()
    {
        score.text = totalScore.ToString();

        if (totalScore < 50)
        {
            //starved ending
            lucaAnim.Play("luca_bad");
            endingName.text = "BAD ENDING";
        }
        else
        {
            if (GGG == 1)
            {
                //go ending
                lucaAnim.Play("luca_good"); // temporary until we get the go asset
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
}
