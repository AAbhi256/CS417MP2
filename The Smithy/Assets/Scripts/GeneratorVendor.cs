using Unity.Mathematics;
using UnityEngine;

public class GeneratorVendor : VendorBehavior
{
    // Update is called once per frame
    void Update()
    {
        
    }

    override public void buyUpgrade(GameObject player)
    {
        if (PlayerBehavior.goldAmount >= price)
        {
            PlayerBehavior.goldAmount -= price;
            player.transform.position -= new Vector3(2, 0, 1);
            PlayerBehavior.sellPrice += 5;

            price = math.round(price * 1.2F);
            SetText();
        }
    }
}
