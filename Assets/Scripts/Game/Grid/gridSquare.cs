using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class gridSquare : MonoBehaviour
{
    public Image activeImage;
    public Image hoverImage;
    public Image normalImage;
    public List<Sprite> normalImages;

    public bool Selected { get; set; }
    public int SquareIndex { get; set; }
    public bool SquareOccupied { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Selected = false;
        SquareOccupied = false;
    }

    public bool CheckUsable()
    {
        return hoverImage.gameObject.activeSelf;
    }

    public void ActivateSquare()
    {
        hoverImage.gameObject.SetActive(false);
        activeImage.gameObject.SetActive(true);
        Selected = true;
        SquareOccupied = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hoverImage.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hoverImage.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        hoverImage.gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void SetImage(bool setFirstImage)
    {
        normalImage.GetComponent<Image>().sprite = setFirstImage ? normalImages[1] : normalImages[0];
    }
}
