using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorVendor : VendorBehavior
{
    public GameObject door1;
    public GameObject door2;

    public GameObject message;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        if (message != null) message.SetActive(false);

    	// 개발자가 인스펙터에서 문을 연결하지 않은 경우, 상점 자체를 비활성화
    	if (door1 == null || door2 == null)
    	{
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
            message.SetActive(true);
            PlayerBehavior.goldAmount -= price;
            gameObject.SetActive(false);
        }
    }

}
