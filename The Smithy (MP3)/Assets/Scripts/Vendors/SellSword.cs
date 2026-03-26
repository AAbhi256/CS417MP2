using Unity.Mathematics;
using UnityEngine;

public class SellSword : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        FinalSwordBehavior sword = collision.gameObject.GetComponent<FinalSwordBehavior>();
        if (sword != null)
        {
            PlayerBehavior.goldAmount += math.round(PlayerBehavior.sellPrice * PlayerBehavior.prestigeMult);
            Destroy(sword.gameObject);
            ItemManager.numItemsLeftToSpawn += 1;
        }
    }
}
