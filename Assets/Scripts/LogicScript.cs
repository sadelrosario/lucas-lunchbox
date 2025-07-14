using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int totalScore, GGG;
    GridArea gridAreaScript;

    public void gameEnd()
    {
        gridAreaScript = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridArea>();
        totalScore = gridAreaScript.getTotalScore();
        GGG = gridAreaScript.getEnding();
        DontDestroy.Instance.DD_Score = totalScore;
        DontDestroy.Instance.DD_GGG = GGG;
        Debug.Log("Total: " + totalScore);
        Debug.Log("End: " + GGG);
        SceneManager.LoadScene("Endings");
    }

    //1 = go, 2 = grow, 3 = glow, 4 = even

    //public int GetScore()
    //{
    //    return totalScore;
    //}

    //public int GetGGG()
    //{
    //    return GGG;
    //}
    
}
