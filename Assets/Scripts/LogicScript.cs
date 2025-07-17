using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int totalScore, GGG;
    private bool isColliding;
    GridArea gridAreaScript;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void gameEnd()
    {
        gridAreaScript = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridArea>();
        isColliding = gridAreaScript.foodColliding();
        if (!isColliding)
        {
            totalScore = gridAreaScript.getTotalScore();
            GGG = gridAreaScript.getEnding();
            DontDestroy.Instance.DD_Score = totalScore;
            DontDestroy.Instance.DD_GGG = GGG;
            Debug.Log("Total: " + totalScore);
            Debug.Log("End: " + GGG);
            SceneManager.LoadScene("Endings");
        }
        // add pop out warning if food items are colliding
    }

    public void reloadGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    
}
