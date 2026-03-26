using UnityEngine;

public class HammerBehavior : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnCollisionEnter (Collision collision)
    {
        IngotBehavior ingot = collision.gameObject.GetComponent<IngotBehavior>();
        if (ingot != null)
        {
            ingot.BecomeHotSword();
        }
    }
}
