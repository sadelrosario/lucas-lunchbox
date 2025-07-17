using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    AudioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void SpawnFood()
    {
        Transform spawnPos = GameObject.Find("SpawnPoint").transform;
        Instantiate(foodPrefab, spawnPos.position, Quaternion.identity, spawnPos);
        audioManager.PlaySFX(audioManager.food_click);
    } 
}
