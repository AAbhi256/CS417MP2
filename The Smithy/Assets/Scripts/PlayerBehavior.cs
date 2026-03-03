using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float goldAmount;
    public float reputationAmount;
    public float goldGenerationRate;
    public float reputationGenerationRate;



    void Start()
    {
        goldAmount = 100;
        reputationAmount = 0;
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
        reputationAmount += 0.1f * reputationGenerationRate * Time.deltaTime;   
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Player Collision Detected!");
        GameObject target = hit.gameObject;
        DoorBehavior door = target.GetComponent<DoorBehavior>();
        if (door != null) {
            door.WarpPlayer();
        }
        
    }
}
