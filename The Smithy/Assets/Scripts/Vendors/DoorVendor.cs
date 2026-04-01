using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorVendor : VendorBehavior
{
    public GameObject door1;
    public GameObject door2;

    bool destroy = false;
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
        if (Input.GetKeyDown(KeyCode.P)) //Testing
        {
            destroy = true;
            door1.SetActive(true);
            door2.SetActive(true);
            PlayerBehavior.goldAmount -= price;
        }
        
        if (destroy)
        {
            Vector3 new_scale = transform.localScale;
            new_scale.y = new_scale.y -  (7F * new_scale.y *  Time.deltaTime);
            transform.localScale = new_scale;

            if (transform.localScale.y < 0.05F)
            {
                gameObject.SetActive(false);
            }
        }
    }
    override public void buyUpgrade(SelectEnterEventArgs arg)
    {
        if (PlayerBehavior.goldAmount >= price)
        {
            door1.SetActive(true);
            door2.SetActive(true);
            PlayerBehavior.goldAmount -= price;
            destroy = true;
        }
    }

}
