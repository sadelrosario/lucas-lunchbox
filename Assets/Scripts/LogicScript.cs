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
        Debug.Log("Total: " + totalScore);
        Debug.Log("End: " + GGG);
        SceneManager.LoadScene("GameEnd");
    }

    //1 = go, 2 = grow, 3 = glow, 4 = even
    public void chooseEnding()
    {
        if (totalScore < 50)
        {
            //starved ending
        }
        else
        {
            if (GGG == 1)
            {
                //go ending
            }
            else if (GGG == 2)
            {
                //grow ending
            }
            else if (GGG == 3)
            {
                //glow ending
            }
            else if (GGG == 4)
            {
                //healthy ending
            }
        }
    }
}
