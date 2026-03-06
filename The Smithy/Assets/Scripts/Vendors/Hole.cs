using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hole : VendorBehavior
{
    public GameObject hole;

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void buyUpgrade(SelectEnterEventArgs arg)
    {
        if (PlayerBehavior.goldAmount >= price)
        {
            PlayerBehavior.goldAmount -= price;
            price *= 2F;
            hole.transform.localScale += new Vector3(0.5F,0,0.5F);
            SetText();
        }
    }
}
