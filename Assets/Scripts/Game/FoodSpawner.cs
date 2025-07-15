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
        //Instantiate(foodPrefab, spawnPos.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        Instantiate(foodPrefab, spawnPos.position, Quaternion.identity, spawnPos);
        //GameObject food = Instantiate(foodPrefab, new Vector3(0,0,0), Quaternion.identity);
        //food.transform.SetParent(GameObject.Find("Canvas").transform, worldPositionStays: false);
        //Instantiate(foodPrefab, transform.position, Quaternion.identity);
    } 
}
