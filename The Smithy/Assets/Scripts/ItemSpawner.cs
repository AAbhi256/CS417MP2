using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Total 5 items allowed to exist at once
    public static int numItemsLeftToSpawn = 4; 

    // The steel on table. If it leaves a certain distance, spawn another (If we have curr remaining)
    public GameObject CurrentSpawnedItem; 

    public GameObject ItemPrefab; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (numItemsLeftToSpawn > 0 && other.gameObject == CurrentSpawnedItem)
        {
            CurrentSpawnedItem = Instantiate(ItemPrefab, this.transform);
            numItemsLeftToSpawn -= 1;
        }
    }
}
