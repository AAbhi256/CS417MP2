using UnityEngine;

public class HammerBehavior : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnColliderEnter (Collider other)
    {
        IngotBehavior ingot = other.GetComponent<IngotBehavior>();
        if (ingot != null)
        {
            ingot.BecomeHotSword();
        }
    }
}
