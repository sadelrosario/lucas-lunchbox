using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridArea : MonoBehaviour
{
    public int totalScore, gggEnd;
    private int goNum, growNum, glowNum;
    public Vector2 gridSize;
    public List<Vector3> occupiedPositions;
    public List<Food> storedFood;
    // create a list that stores food

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set the tag of this GameObject to Food
        Time.timeScale = 1;
        gameObject.tag = "Grid";
        storedFood.Clear();
        totalScore = 0;
        goNum = 0;
        growNum = 0;
        glowNum = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void getFood()
    {
        storedFood.Clear();
        var items = FindObjectsOfType<Food>();
        foreach (Food f in items)
        {
            if (f != null)
            {
                storedFood.Add(f);
            }
        }
    }

    public int getTotalScore()
    {
        getFood();
        foreach (Food f in storedFood)
        {
            totalScore += f.getFoodScore();
            Debug.Log(totalScore);
        }
        return totalScore;
    }

    public int getEnding()
    {
        getFood();
        foreach (Food f in storedFood)
        {
            if (f.getGGG().ToString() == "Go")
            {
                goNum += 1;
            }
            if (f.getGGG().ToString() == "Grow")
            {
                growNum += 1;
            }
            if (f.getGGG().ToString() == "Glow")
            {
                glowNum += 1;
            }
        }

        //1 = go, 2 = grow, 3 = glow, 4 = even
        if ((goNum > growNum && goNum > glowNum) //go >> grow/glow
        || (goNum == growNum && growNum > glowNum)) //go/grow >> glow
        {
            gggEnd = 1;
        }
        if ((growNum > goNum && growNum > glowNum) //grow >> go/glow
        || (growNum == glowNum && glowNum > goNum)) //grow/glow >> go
        {
            gggEnd = 2;
        }
        if ((glowNum > growNum && glowNum > goNum) //glow >> go/grow
        || (glowNum == goNum && goNum > growNum)) //glow/go >> grow
        {
            gggEnd = 3;
        }
        if (goNum == growNum && growNum == glowNum) //all equal
        {
            gggEnd = 4;
        }
        Debug.Log("Go: " + goNum);
        Debug.Log("Grow: " + growNum);
        Debug.Log("Glow: " + glowNum);
        return gggEnd;
    }
}
