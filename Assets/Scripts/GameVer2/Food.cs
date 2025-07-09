using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Food : MonoBehaviour
{
    SpriteRenderer sprite;
    public enum GGGType { Go, Grow, Glow } // type of food
    public GGGType type;
    public int scoreValue;
    private bool drag = false;
    private bool collide;
    public float gridSize = 1.5f;
    private Vector3 offset;
    public Vector3 currentPos;
    public Vector2 anchorPoint; // used for locating its spawning point

    void Start()
    {
        //Set the tag of this GameObject to Food
        gameObject.tag = "Food";

        //Fetch the SpriteRenderer from the GameObject
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (drag) // movement snaps to grid size
        {
            currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(Mathf.RoundToInt(currentPos.x / gridSize) * gridSize, Mathf.RoundToInt(currentPos.y / gridSize) * gridSize, currentPos.z);
        }

        if (!collide)
        {
            sprite.color = new Color(1, 1, 1, 1);
        }

        // 

    }

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        drag = true;
    }

    void OnMouseUp()
    {
        drag = false;
    }

    void OnCollisionExit2D(Collision2D collision) // if collision of food exits grid area collision
    {

        if (collision.gameObject.tag == "Grid") // get collision of grid area by
        {
            Destroy(gameObject, 0.5f); // food gets destroyed after one second
            Debug.Log("GRID");
        }

        if (collision.gameObject.tag == "Food")
        {
            sprite.color = new Color(1, 1, 1, 1);
            collide = false;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Food") // if object collides with another food
        {
            collide = true;
            // insert error that food should not be colliding 
            // make it glow red or smth
            sprite.color = new Color(0.93f, 0.34f, 0.22f, 0.5f);
            Debug.Log("COLLIDING");
        }
    }

    public int getFoodScore()
    {
        return scoreValue;
    }

    public GGGType getGGG()
    {
        return type;
    }
}
