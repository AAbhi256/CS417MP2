using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GeneratorVendor : VendorBehavior
{
    override public void buyUpgrade(SelectEnterEventArgs arg)
    {
        if (PlayerBehavior.goldAmount >= price)
        {
            PlayerBehavior.goldAmount -= price;
            PlayerBehavior.sellPrice += 5;

            price = math.round(price * 1.2F);
            SetText();
            StartCoroutine(upgradeVendor());

        }
    }
}
