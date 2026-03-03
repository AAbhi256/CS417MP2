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
        Instantiate(finalSword, transform);
        Destroy(this.gameObject);
    }
}
