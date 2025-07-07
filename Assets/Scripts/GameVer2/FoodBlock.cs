using UnityEngine;

public class FoodBlock : MonoBehaviour
{
    // Restrictions
    public bool considerScale = true;
    public bool considerOtherObjects = true;
    public Vector2 foodScale = new Vector2(1, 1);
    public Vector4 currentPosition = new Vector4(1, 1, 1, 1);
    private Vector3 offset;
    private bool drag = false;

    Vector2 gridOffset = Vector2.zero; // zero offsets
    Vector3 gridSize = Vector2.one; // 1x1 grid size
    Vector3 screenPoint;
    Vector4 lastPos;
    Vector3 lastParentPos;
    Vector4 targetPos;

    // used to initialize any variable before game starts
    void Awake()
    {
        // Fix the position based on the scale of this object
        var newPos = transform.localPosition;
        newPos.x = (transform.localScale.x / 2f) - 0.5f; // will figure out this formula...
        newPos.y = -((transform.localScale.y / 2f) - 0.5f);

        transform.localPosition = newPos;

        // Update Data
        UpdateGridData();
        UpdatePosition();

        // Save actual position as the last position
        lastParentPos = transform.position;
        lastPos = currentPosition;

        // Add position 
        AddPosition(lastPos);
    }

    void Update()
    {
        if (drag)
        {
            Debug.Log("Update");
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) + offset;
        }

        UpdatePosition();
        UpdateAll();
    }

    // Get recent values of the Grid
    void UpdateGridData()
    {
        gridSize = FindFirstObjectByType<GridBox>().gridSize;
        gridOffset = FindFirstObjectByType<GridBox>().GetGridOffset();
    }

    void OnMouseDown()
    {
        drag = true;
        Debug.Log("Down");

        // Remove the last position
        RemovePosition(lastPos);

        // Update data
        UpdateGridData();

        // Correct the position according to the scale of the object
        var newPos = transform.localPosition;
        newPos.x = (transform.localScale.x / 2f) - 0.5f; // will figure out this formula...
        newPos.y = -((transform.localScale.y / 2f) - 0.5f);

        transform.localPosition = newPos;

        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        UpdatePosition();
    }

    void OnDrag()
    {
        // Get World Point using the mouse position
        screenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        screenPoint.z = 0;

        // Fix the request position if the number of the cells is even
        // to avoid being stuck in between cells
        if (gridSize.x % 2 == 0)
        {
            screenPoint.x -= 0.5f;
        }
        if (gridSize.y % 2 == 0)
        {
            screenPoint.y -= 0.5f;
        }

        // Change GameObject position according to adjustment
        transform.localPosition = SnapToGrid(screenPoint);
    }

    void OnMouseUp()
    {
        drag = false;

        UpdateGridData();

        // Save target position 
        // i dont get how this is being calculated
        targetPos.x = transform.parent.position.x + (gridSize.x * 0.5f) + 0.5f;
        targetPos.y = transform.parent.position.x + (gridSize.x * 0.5f) + 0.5f + transform.localScale.x - 1;

        targetPos.z = -(transform.parent.position.y + (gridSize.y * 0.5f) + 0.5f);
        targetPos.w = -(transform.parent.position.y + (gridSize.y * 0.5f) + 0.5f) + transform.localScale.y - 1;

        // Check if that position is occupied by another object
        if (!IsOccupied())
        {
            // if position is not occupied, we remove the last saved position and update
            RemovePosition(lastPos);
            UpdatePosition();
            AddPosition(targetPos); // new position targetPos is added
        }
        else
        {
            AddPosition(lastPos); // last position is still the current latest position
        }
    }

    public void UpdateAll()
    {
        UpdatePosition();
        AddPosition(lastPos);
    }

    void AddPosition(Vector4 pos)
    {
        var grid = FindFirstObjectByType<GridBox>();
        // check if position is in the list of occupied positions of grid (if not)
        if (!grid.occupiedPositions.Contains(pos))
        {
            grid.occupiedPositions.Add(pos);
            // add position data into the grid's list of occupied positions
        }
    }

    void RemovePosition(Vector4 pos)
    {
        var grid = FindFirstObjectByType<GridBox>();
        // check if position is in the list of occupited positions of grid (if yes)
        if (grid.occupiedPositions.Contains(pos))
        {
            grid.occupiedPositions.Remove(pos);
            // remove the position in the grid's list of occupied positions
        }
    }

    // Check if target position is occupied
    bool IsOccupied()
    {
        var occupied = FindFirstObjectByType<GridBox>().occupiedPositions;
        foreach (Vector4 pos in occupied)
        {
            // figure this out but basically checks if positions "collide" with each other 
            if (
                ((targetPos.x >= pos.x && targetPos.y <= pos.y) ||
                 (targetPos.y >= pos.x && targetPos.y <= pos.y) ||
                 (pos.y >= targetPos.x && pos.y <= targetPos.y))
                &&
                ((targetPos.z >= pos.z && targetPos.z <= pos.w) ||
                 (targetPos.w >= pos.z && targetPos.w <= pos.w) ||
                 (pos.w >= targetPos.z && pos.w <= targetPos.w))
            )
            {
                transform.parent.position = lastParentPos;
                currentPosition = lastPos;
                return true;
            }
        }
        return false;
    }

    // Update object position variable
    void UpdatePosition()
    {
        targetPos.x = transform.position.x + (gridSize.x * 0.5f) + 0.5f;
        targetPos.y = transform.position.x + (gridSize.x * 0.5f) + 0.5f + transform.localScale.x - 1;

        targetPos.z = -(transform.position.y + (gridSize.y * 0.5f) + 0.5f);
        targetPos.w = -(transform.position.y + (gridSize.y * 0.5f) + 0.5f) + transform.localScale.y - 1;

        // Save actual position
        lastParentPos = transform.position;
        lastPos = currentPosition;

    }

    // Fix the GameObject position if the Grid Transformr has changed
    public void FixPosition(Vector3 newPos)
    {
        newPos.z = 0;
        transform.position = transform.position + newPos;

        UpdateGridData();
        UpdatePosition();
    }

    // Function that allows snapping to grid
    Vector3 SnapToGrid(Vector3 dragPos)
    {
        // if x is even, fix the target position
        if (gridSize.x % 2 == 0)
        {
            dragPos.x = (Mathf.Round(dragPos.x / foodScale.x) * foodScale.x) + 0.5f;
        }
        else
        {
            dragPos.x = Mathf.Round(dragPos.x / foodScale.x) * foodScale.x;
        }

        // if y is even, fix the target position
        if (gridSize.y % 2 == 0)
        {
            dragPos.y = (Mathf.Round(dragPos.y / foodScale.y) * foodScale.y) + 0.5f;
        }
        else
        {
            dragPos.y = Mathf.Round(dragPos.y / foodScale.y) * foodScale.y;
        }

        // Restrict exit from grid (might not need)
        var maxXPos = ((gridSize.x - 1) * 0.5f) + gridOffset.x;
        var maxYPos = ((gridSize.y - 1) * 0.5f) + gridOffset.y;

        // Considering GameObject Scale
        if (dragPos.x > maxXPos - transform.localScale.x + 1)
        {
            dragPos.x = maxXPos - transform.localScale.x + 1;
        }

        if (dragPos.x < -maxXPos + gridOffset.x + gridOffset.x)
        {
            dragPos.x = -maxXPos + gridOffset.x + gridOffset.x;
        }

        if (dragPos.y > maxYPos)
        {
            dragPos.y = maxYPos;
        }

        if (dragPos.y < -maxYPos + gridOffset.y + gridOffset.y + transform.localScale.y - 1)
        {
            dragPos.y = -maxYPos + gridOffset.y + gridOffset.y + transform.localScale.y - 1;
        }

        return dragPos;
    }
}
