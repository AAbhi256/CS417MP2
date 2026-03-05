using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    static public float goldAmount;
    static public float gemAmount;
    static public float soulAmount;
    static public float sellPrice;

    static public float goldGenerationRateMult;
    static public float goldGenerationRate;
    static public float gemGenerationRate;
    static public float prestigeMult = 1; 


    void Start()
    {
        goldAmount = 1000000;
        gemAmount = 1000000;
        soulAmount = 10;
        sellPrice = 1;
        goldGenerationRateMult = 1;
        goldGenerationRate = 0;
        gemGenerationRate = 0;
    }


    void Update()
    {
        EarnGold();
        EarnGem();
    }

    public void EarnGold()
    {

        goldAmount += 0.5F * prestigeMult * goldGenerationRate * goldGenerationRateMult * Time.deltaTime;
    }

    public void EarnGem()
    {
        gemAmount += 0.1F * prestigeMult * gemGenerationRate * Time.deltaTime;   
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Debug.Log("Player Collision Detected!");

        GameObject target = hit.gameObject;
        DoorBehavior door = target.GetComponent<DoorBehavior>();
        if (door != null) {
            door.WarpPlayer();
            return;
        }
    }
}
