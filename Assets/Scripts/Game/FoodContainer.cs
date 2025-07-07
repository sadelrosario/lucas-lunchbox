using UnityEngine;

public class FoodContainer : MonoBehaviour
{
    public Transform foodShapePos; // get position of shape
    void Start()
    {
        transform.position = foodShapePos.position + new Vector3(1.2f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
