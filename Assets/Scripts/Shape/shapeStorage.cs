using System.Collections.Generic;
using UnityEngine;

public class shapeStorage : MonoBehaviour
{
    public List<shapeData> shapeDataList;
    public List<shape> shapeList;
    void Start()
    {
        foreach (var shape in shapeList)
        {
            var shapeIndex = UnityEngine.Random.Range(0, shapeDataList.Count);
            shape.CreateShape(shapeDataList[shapeIndex]);
        }
    }


}
