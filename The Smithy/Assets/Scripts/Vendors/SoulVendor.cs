using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SoulVendor : VendorBehavior
{
    override public void buyUpgrade(SelectEnterEventArgs arg)
    {
        if (PlayerBehavior.goldAmount >= price)
        {
            PlayerBehavior.goldAmount -= price;
            PlayerBehavior.soulAmount += 1;
            price *= 3F;
            SetText();
        }
    }
}
