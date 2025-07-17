using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int totalScore, GGG;
    private bool isColliding;
    GridArea gridAreaScript;
    public bool isDone = false;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void gameEnd()
    {
        Debug.Log(isDone);
        gridAreaScript = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridArea>();
        isColliding = gridAreaScript.foodColliding();
        if (!isColliding)
        {
            isDone = true;
            totalScore = gridAreaScript.getTotalScore();
            GGG = gridAreaScript.getEnding();
            DontDestroy.Instance.DD_Score = totalScore;
            DontDestroy.Instance.DD_GGG = GGG;
            Debug.Log("Total: " + totalScore);
            Debug.Log("End: " + GGG);
            SceneManager.LoadScene("Endings");
        }
        Debug.Log("ITS DONE");
        // add pop out warning if food items are colliding
    }

    public bool GetDone
    {
        get {
            return isDone;
        } 
    }

    public void reloadGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene("Title");
    }
}
