using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoneyVendor : VendorBehavior
{

    public static GameObject money_vendor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        price_item_type = 1;
        base.Start();
        money_vendor = gameObject;
        gameObject.SetActive(false);
    }

    override public void buyUpgrade(SelectEnterEventArgs arg)
    {
        if (PlayerBehavior.gemAmount >= price)
        {
            PlayerBehavior.gemAmount -= price;
            PlayerBehavior.goldGenerationRate += 20;
            price *= 2F;
            SetText();
        }
    }
}
