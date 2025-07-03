using UnityEngine;
using System.Collections.Generic; // use for collections

[RequireComponent(typeof(MeshRenderer))]
// automatically adds required components as dependencies
public class GridBox : MonoBehaviour
{
    public Vector2 gridSize = new Vector2(1, 1);

    Vector2 gridOffset;
    Vector3 lastGridSize;
    Vector3 lastPosition;

    public List<Vector4> occupiedPositions;

    // used to initialize any variable before game starts
    void Awake()
    {
        UpdateScale(); // set the textures and scale of grid
    }

    // Update is called once per frame
    void Update()
    {
        // check if transformation has changed
        if (transform.hasChanged)
        {
            // update the grid texture size and fix positions of the draggable game objects
            transform.hasChanged = false;
            occupiedPositions.Clear(); // clear the list of occupied grids
            GetComponent<MeshRenderer>().material.mainTextureScale = gridSize; // rescale the texture according to gridSize
            FixPositions();

        }

        // if grid size has changed
        // insert code, tho i don't think we will be needing it
    }


    // Update transform and texture size of grids
    void UpdateScale()
    {
        // scale the grid and its texture according to gridSize
        transform.localScale = new Vector3(gridSize.x, gridSize.y, 1);
        GetComponent<MeshRenderer>().material.mainTextureScale = gridSize;
    }

    void FixPositions()
    {
        var objs = FindObjectsOfType<FoodBlock>(); // get all food block objects
        var diff = transform.localPosition - lastPosition;
        foreach (FoodBlock i in objs)
        {
            i.FixPosition(diff); // fixes food block into the grid
            i.UpdateAll(); // update the positions and add it to list of occupiedPosition
        }
        lastPosition = transform.localPosition;
    }

    // use to get grid offset which is the position of the transform relative to the parent transform
    public Vector2 GetGridOffset()
    {
        gridOffset.x = transform.localPosition.x;
        gridOffset.y = transform.localPosition.y;
        return gridOffset;
    }

}
