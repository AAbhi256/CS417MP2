using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class JewelVendor : VendorBehavior
{
    public static GameObject jewel_vendor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        price_item_type = 1;
        base.Start();
        jewel_vendor = gameObject;
        gameObject.SetActive(false);
    }

    override public void buyUpgrade(SelectEnterEventArgs arg)
    {
        if (PlayerBehavior.gemAmount >= price)
        {
            PlayerBehavior.gemAmount -= price;
            PlayerBehavior.goldGenerationRateMult = 10F;
            gameObject.SetActive(false);
        }
    }
}
