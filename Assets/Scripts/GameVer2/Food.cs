using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Food : MonoBehaviour
{
    [SerializeField] private InputAction press, screenPosition;
    SpriteRenderer sprite;
    public enum GGGType { Go, Grow, Glow } // type of food
    public GGGType type;
    public int scoreValue;
    private bool dragging = false;
    private bool collide;
    private Vector3 offset;
    public Vector3 currentPos;
    public LayerMask hitLayer;
    public RaycastHit hit;
    public Ray ray;
    Camera camera;

    // grid math
    private float baseGridSize = 0.88f; // base/original grid size
    private float scaleValue = 0.00625f; // base/original scale of the canvas on our original screen size (2560 x 1600)
    private float gridSize;

    private Vector3 WorldPos // read the world position
    {
        get
        {
            float z = camera.WorldToScreenPoint(transform.position).z;
            return camera.ScreenToWorldPoint(currentPos + new Vector3(0, 0, z));
        }
    }
    
    private bool isClickedOn
    {
        get
        {
            RaycastHit2D hit = Physics2D.Raycast(WorldPos, Vector2.up, Mathf.Infinity, hitLayer);

            if (hit)
            {
                // Debug.Log(hit.point);
                return hit.transform == transform;
            }
            return false;
        }
    }

    private void Awake()
    {
        // set camera
        camera = Camera.main;

        // enable the input actions
        press.Enable();
        screenPosition.Enable();

        // get the callback context of the screen position event, update current screen position with the vec2 position of the screen position
        screenPosition.performed += context => { currentPos = context.ReadValue<Vector2>(); };

        // if press action is performed and canceled do the following
        press.performed += _ => { if(isClickedOn) StartCoroutine(Drag()); };
        // press.performed += Drag;
        press.canceled += _ => { dragging = false; };
        // press.Disable(); 
        
    }

    private IEnumerator Drag()
    {
        dragging = true;
        // offset = transform.position - new Vector3(Mathf.RoundToInt(WorldPos.x), Mathf.RoundToInt(WorldPos.y),  WorldPos.z);
        offset = transform.position - WorldPos;
        while (dragging)
        {
            Debug.Log("PRESSSS");

            transform.position = new Vector3(Mathf.RoundToInt(WorldPos.x + offset.x / gridSize) * gridSize, Mathf.RoundToInt(WorldPos.y + offset.y / gridSize) * gridSize, WorldPos.z);
            //transform.position = WorldPos + offset;
            yield return null;
        }
    }


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
        RectTransform canvasTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
        gridSize = baseGridSize * (canvasTransform.transform.localScale.x / scaleValue);

        if (!collide)
        {
            sprite.color = new Color(1, 1, 1, 1);
        }

        Debug.DrawLine(Vector2.zero, camera.ScreenToWorldPoint(currentPos), Color.yellow);
        if (isClickedOn) { Debug.Log("HITTTINGG");  }
    }

    void OnCollisionExit2D(Collision2D collision) // if collision of food exits grid area collision
    {

        if (collision.gameObject.tag == "Grid") // get collision of grid area by
        {
            Destroy(gameObject, 0.5f); // food gets destroyed after one second
            // Debug.Log("GRID");
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
            // make it glow red
            sprite.color = new Color(0.93f, 0.34f, 0.22f, 0.5f);
            // Debug.Log("COLLIDING");
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
