using NUnit.Framework.Interfaces;
using UnityEngine;

public class DoorVendor : VendorBehavior
{
    public GameObject door1;
    public GameObject door2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!(door1 && door2))
        {
            gameObject.SetActive(false); //disable if the doors weren't set
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    override public void buyUpgrade()
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
