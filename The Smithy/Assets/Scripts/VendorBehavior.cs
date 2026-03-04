using UnityEngine;

public class VendorBehavior : MonoBehaviour
{
    public float price;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void buyUpgrade()
    {
        // Override this in child class
        Debug.Log("DEFAULT VENDOR BEHAVIOR USED. PLEASE OVERRIDE THIS FUNCTION");
    }
}
