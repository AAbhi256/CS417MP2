using UnityEngine;

public class HotSwordBehavior : MonoBehaviour
{
    public FinalSwordBehavior finalSword;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void BecomeFinalSword()
    {
        Instantiate(finalSword, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
