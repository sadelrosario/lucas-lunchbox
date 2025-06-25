using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class shapeSquare : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image occupiedImage;
    void Start()
    {
        occupiedImage.gameObject.SetActive(false);
    }
}
