using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VendorBehavior : MonoBehaviour
{
    public float price;
    public GameObject text_gameobj;
    
    protected string init_text;

    protected int price_item_type = 0; // 0 for gold, 1 for gem, 2 for souls

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        init_text = text_gameobj.GetComponent<TextMeshProUGUI>().text;
        SetText();
    }

    public void SetText()
    {
        TextMeshProUGUI text = text_gameobj.GetComponent<TextMeshProUGUI>();
        text.text = init_text + price;
        switch (price_item_type)
        {
            case 0:
                text.text += " Gold";
                break;
            case 1:
                text.text += " Gems";
                break;
            case 2:
                text.text += " Souls";
                break;
        }
    }

    public virtual void buyUpgrade(SelectEnterEventArgs arg)
    {
        // Override this in child class
        Debug.Log("DEFAULT VENDOR BEHAVIOR USED. PLEASE OVERRIDE THIS FUNCTION");
    }
}
