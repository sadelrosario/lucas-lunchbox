using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class shapeSquare : MonoBehaviour
{
    // Individual square for the Shape
    public Image occupiedImage;
    void Start()
    {
        occupiedImage.gameObject.SetActive(false);
    }
}
