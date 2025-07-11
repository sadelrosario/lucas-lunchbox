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
    public enum Type { Go, Grow, Glow }; // type of food
    private bool dragging = false;
    private bool collide;
    public float gridSize = 0.88f;
    private Vector3 offset;
    public Vector3 currentPos;
    public LayerMask hitLayer;
    public RaycastHit hit;
    public Ray ray;
    Camera camera;

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
                // Debug.Log("HITTTT");
                return hit.transform == transform;
            }
            return false;
        }
    }

    // private bool isClickedOn // read if raycast is hitting on something
    // {
    //     get
    //     {
    //         currentPos += new Vector3(0, 0, 10);
    //         ray = camera.ScreenPointToRay(currentPos);
    //         // ray = new Ray(transform.position, transform.forward);

    //         if (Physics.Raycast(ray, out hit))
    //         {
    //             Debug.Log("TEST");
    //             return hit.transform == transform; // if raycast hits something
    //         }
    //         // if (Physics.Raycast(ray, out hit, 10, hitLayer))
    //         // {
    //         //     if (hit.collider != null)
    //         //     {
    //         //         Debug.Log("HIT SMTH");
    //         //     }

    //         //     Debug.Log("TEST");
    //         //     return hit.transform == transform; // if raycast hits something
    //         // }
    //         Debug.Log("NOTHING");
    //         return false;
    //     }
    // }

    // private void OnEnable()
    // {
    //     press.Enable();
    //     screenPosition.Enable();

    //     press.performed += Drag;
    // }

    // private void OnDisable()
    // {
    //     press.performed -= Drag;
    //     press.Disable();
    //     // screenPosition.Disable();
    // }

    // private void Drag(InputAction.CallbackContext context)
    // {
    //     Debug.Log("PRESSSS");
    //     ray = camera.ScreenPointToRay(currentPos);
    //     if (Physics.Raycast(ray, out hit))
    //     {
    //         Debug.Log("AMEN");
    //     }
    // }
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

        if (!collide) // check if is colliding with other food, if not then the colors are normal
        {
            sprite.color = new Color(1, 1, 1, 1);
        }

        if (isClickedOn)
        {
            Debug.Log("CLICK");
        }
        // press.performed += _ => { if (isClickedOn) StartCoroutine(Drag()); };

        // Debug.DrawLine(new Vector3(5,5,0), hit.point);
        // Debug.Log("RAY ORIGIN: " + ray.origin);
        // Debug.Log("HIT POSITION: " + hit.point);
        // Debug.Log("RAY DIRECTION: " + ray.direction);

        // Debug.DrawLine(ray.origin, ray.direction, Color.yellow);
        Debug.DrawLine(Vector2.zero, camera.ScreenToWorldPoint(currentPos), Color.yellow);
        // Debug.Log("CURRENT POS: " + camera.ScreenToWorldPoint(currentPos));
        // Debug.Log("mouse POS " + WorldPos);
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

}
