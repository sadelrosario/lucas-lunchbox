using UnityEngine;

public class snap : MonoBehaviour
{
    private bool drag = false;
    public int gridSize = 5;
    private Vector3 offset;
    public Vector3 pos;


    // Update is called once per frame
    void Update()
    {
        if (drag)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(Mathf.RoundToInt(pos.x/gridSize)*gridSize, Mathf.RoundToInt(pos.y/gridSize)*gridSize, pos.z);
        }
    }

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        drag = true;
        Debug.Log("Down");
    }

    void OnMouseUp()
    {
        drag = false;
        Debug.Log("Up");
    }
}
