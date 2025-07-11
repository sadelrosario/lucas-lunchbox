using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragDropable : MonoBehaviour 
{
	[SerializeField] private InputAction press, screenPos;

	private Vector3 curScreenPos;

	Camera camera;
	private bool isDragging;
	public RaycastHit hit;
	public LayerMask hitLayer;
	public float gridSize = 0.88f;

	private Vector3 WorldPos
	{
		get
		{
			float z = camera.WorldToScreenPoint(transform.position).z;
			return camera.ScreenToWorldPoint(curScreenPos + new Vector3(0, 0, z));
		}
	}
	private bool isClickedOn
	{
		get
		{
			Ray ray = camera.ScreenPointToRay(curScreenPos);

			Debug.Log("HIT POINT : " + hit.point);
			Debug.Log("RAY ORIGIN : " + ray.origin);
			if (Physics.Raycast(ray, out hit, 100, hitLayer))
			{
				Debug.Log("YPPPPP" + hit.point);
				return hit.transform == transform;
			}
			return false;
		}
	}
	private void Awake() 
	{
		camera = Camera.main;
		screenPos.Enable();
		press.Enable();
		screenPos.performed += context => { curScreenPos = context.ReadValue<Vector2>(); };
		press.performed += _ => { Debug.Log("HI"); StartCoroutine(Drag()); };
		press.canceled += _ => { isDragging = false; };

	}

	private IEnumerator Drag()
	{
		isDragging = true;
		Vector3 offset = transform.position - WorldPos;
		// grab
		// GetComponent<Rigidbody>().useGravity = false;
		while(isDragging)
		{
			// dragging
			Debug.Log("DRAG");
			transform.position = WorldPos + offset;
			// transform.position = new Vector3(Mathf.RoundToInt(WorldPos.x / gridSize) * gridSize, Mathf.RoundToInt(WorldPos.y / gridSize) * gridSize, WorldPos.z);

			yield return null;
		}
		// drop
		// GetComponent<Rigidbody>().useGravity = true;
	}
}
