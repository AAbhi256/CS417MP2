using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorVendor : VendorBehavior
{
    public GameObject door1;
    public GameObject door2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        if (!(door1 && door2))
        {
            //if the dev doesn't actually assign the doors that the purchase should unlock, then hide the vendor
            gameObject.SetActive(false);
        }
                
        // ------ TESTING ------
        // door1.SetActive(true);
        // door2.SetActive(true);
        // PlayerBehavior.goldAmount -= price;
        // gameObject.SetActive(false);
        // ---------------------
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    override public void buyUpgrade(SelectEnterEventArgs arg)
    {
        if (PlayerBehavior.goldAmount >= price)
        {
            door1.SetActive(true);
            door2.SetActive(true);
            PlayerBehavior.goldAmount -= price;
            gameObject.SetActive(false);
        }
    }

}
