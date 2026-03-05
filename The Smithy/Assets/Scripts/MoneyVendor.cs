using Unity.Mathematics;
using UnityEngine;

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

    override public void buyUpgrade(GameObject player)
    {
        if (PlayerBehavior.gemAmount >= price)
        {
            PlayerBehavior.gemAmount -= price;
            PlayerBehavior.goldGenerationRate += 20;
            player.transform.position += new Vector3(2, 0, 2);
            price *= 2F;
            SetText();
        }
    }
}
