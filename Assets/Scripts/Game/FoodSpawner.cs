using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFood()
    {
        Transform spawnPos = GameObject.Find("SpawnPoint").transform;
        Instantiate(foodPrefab, spawnPos.position, Quaternion.identity, spawnPos);
    } 
}
