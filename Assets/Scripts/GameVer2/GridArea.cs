using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridArea : MonoBehaviour
{
    public Vector2 gridSize;
    public List<Vector3> occupiedPositions;
    public List<Food> storedFood;
    // create a list that stores food

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set the tag of this GameObject to Food
        gameObject.tag = "Grid";
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
}
