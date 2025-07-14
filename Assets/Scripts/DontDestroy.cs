using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//from: https://docs.unity3d.com/6000.1/Documentation/ScriptReference/Object.DontDestroyOnLoad.html
//from: https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#634f8281edbc2a65c86270ca

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;
    public int DD_Score;
    public int DD_GGG;

    void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}