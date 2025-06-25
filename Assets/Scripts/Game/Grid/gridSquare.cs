using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gridSquare : MonoBehaviour
{
    public Image normalImage;
    public List<Sprite> normalImages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void SetImage(bool setFirstImage)
    {
        normalImage.GetComponent<Image>().sprite = setFirstImage ? normalImages[1] : normalImages[0];
    }
}
