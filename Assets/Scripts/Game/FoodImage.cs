using UnityEngine;

public class FoodImage : MonoBehaviour
{
    public Transform shapePos;
    void Start()
    {
        transform.position = shapePos.position;
    }
}
