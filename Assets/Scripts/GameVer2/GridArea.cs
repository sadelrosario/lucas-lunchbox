using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridArea : MonoBehaviour
{
    private int totalScore;
    private int goNum, growNum, glowNum;
    public Vector2 gridSize;
    public List<Vector3> occupiedPositions;
    public List<Food> storedFood;
    // create a list that stores food

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set the tag of this GameObject to Food
        gameObject.tag = "Grid";
        totalScore = 0;
        goNum = 0;
        growNum = 0;
        glowNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var items = FindObjectsOfType<Food>();

        storedFood.Clear();

        foreach (Food f in items)
        {
            // get its position and update the occupied position list

            // add food object into the list of stored food
            storedFood.Add(f);
        }
    }

    public int getTotalScore()
    {
        var items = FindObjectsOfType<Food>();
        foreach (Food f in items)
        {
            totalScore += f.getFoodScore();
            Debug.Log(totalScore);
        }
        return totalScore;
    }

    public int getEnding()
    {
        var items = FindObjectsOfType<Food>();

        foreach (Food f in items)
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
        || (goNum > glowNum && growNum > glowNum)) //go/grow >> glow
        {
            return 1;
        }
        if ((growNum > goNum && growNum > glowNum) //grow >> go/glow
        || (growNum > goNum && glowNum > goNum)) //grow/glow >> go
        {
            return 2;
        }
        if ((glowNum > growNum && glowNum > goNum) //glow >> go/grow
        || (glowNum > growNum && goNum > growNum)) //glow/go >> grow
        {
            return 3;
        }
        if (goNum == growNum && growNum == glowNum) //all equal
        {
            return 4;
        }
        return 0;
    }
}
