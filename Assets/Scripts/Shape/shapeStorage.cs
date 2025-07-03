using System.Collections.Generic;
using UnityEngine;

public class shapeStorage : MonoBehaviour
{
    public List<shapeData> shapeDataList; // List of possible shapes 
    public List<shape> shapeList; // List of actual shapes

    int i = 0;
    void Start()
    {
        foreach (var shape in shapeList)
        {
            //var shapeIndex = UnityEngine.Random.Range(0, shapeDataList.Count);
            shape.CreateShape(shapeDataList[i]);
            i++;
        }
    }


}
