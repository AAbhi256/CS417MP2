using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GemVendor : VendorBehavior
{
    public int gem_gen_amt = 1; // Change in inspector
    public int vendor_num = 0; // Tier from 0 to 2. Change in inspector

    static GameObject[] vendors = new GameObject[4];
    
    public override void Start()
    {
        base.Start();

        vendors[vendor_num] = gameObject;
        if (vendor_num != 0)
        {
            gameObject.SetActive(false);
        }
    }

    override public void buyUpgrade(SelectEnterEventArgs arg)
    {
        if (PlayerBehavior.goldAmount >= price)
        {
            PlayerBehavior.goldAmount -= price;
            PlayerBehavior.gemGenerationRate += gem_gen_amt;


            if (vendor_num < vendors.Length - 1)
            {
                vendors[vendor_num + 1].SetActive(true);
            }
            if (vendor_num == 0)
            {
                MoneyVendor.money_vendor.SetActive(true);
                JewelVendor.jewel_vendor.SetActive(true);
            }
            StartCoroutine(destroyVendor());
        }
    }
}
