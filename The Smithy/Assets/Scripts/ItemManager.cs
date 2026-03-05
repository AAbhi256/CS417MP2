using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Total 5 items allowed to exist at once (ie: steel, molten, sword)
    public static int curr_remaining = 4; 

    // The steel on table. If it leaves a certain distance, spawn another (If we have curr remaining)
    [SerializeField] GameObject curr_steel; 

    [SerializeField] GameObject steel_prefab; 

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
        if (curr_remaining > 0 && other.gameObject == curr_steel)
        {
            curr_steel = Instantiate(steel_prefab, transform);
            curr_remaining -= 1;
        }
    }
}
