using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GridScaler : MonoBehaviour
{
    private Vector2 canvasScale = GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution;
    public float gridScale = 0.88f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //Assembly assembly = Assembly.Load("UnityEditor.dll");
        //Type gridSettings = assembly.GetType("UnityEditor.GridSettings");
        //PropertyInfo gridSize = gridSettings.GetProperty("size");
        //gridSize.SetValue("size", new Vector3(canvasScale.x, canvasScale.y, 0) * gridScale);
        //EditorSnapSettings.gridSize(gridScale * new Vector3(canvasScale.x, canvasScale.y, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
