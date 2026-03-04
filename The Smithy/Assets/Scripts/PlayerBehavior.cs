using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    static public float goldAmount;
    static public float soulAmount;
    static public float sellPrice;

    public float goldGenerationRate;
    public float reputationGenerationRate;



    void Start()
    {
        goldAmount = 0;
        soulAmount = 0;
        sellPrice = 1;
        goldGenerationRate = 0;
        reputationGenerationRate = 0;
    }


    void Update()
    {
        EarnGold();
        EarnReputation();
    }

    public void EarnGold()
    {
        goldAmount += 0.1f * goldGenerationRate * Time.deltaTime;
    }

    public void EarnReputation()
    {
        soulAmount += 0.1f * reputationGenerationRate * Time.deltaTime;   
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Player Collision Detected!");

        GameObject target = hit.gameObject;
        DoorBehavior door = target.GetComponent<DoorBehavior>();
        if (door != null) {
            door.WarpPlayer();
            return;
        }
        
        VendorBehavior vendor = target.GetComponent<VendorBehavior>();
        if (vendor != null) {
            vendor.buyUpgrade();
            return;
        }

    }
}
