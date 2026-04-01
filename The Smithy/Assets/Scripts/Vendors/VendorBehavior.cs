using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VendorBehavior : MonoBehaviour
{
    public float price;
    public GameObject text_gameobj;
    public AudioParticle ap;
    
    protected string init_text;

    protected int price_item_type = 0; // 0 for gold, 1 for gem, 2 for souls

    protected Vector3 local_scale_save;
    protected int upgrade = 0; // 0 for nothing, 2 for slight grow animation, 3 for slight shrink
    protected int destroy = 0; // 0 for alive, 2 for slight grow animation, 3 for shrink till gone

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        init_text = text_gameobj.GetComponent<TextMeshProUGUI>().text;
        SetText();
        local_scale_save = transform.localScale;
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

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && destroy == 0)
        {
            StartCoroutine(destroyVendor());
        }

        if (destroy == 1)
        {
            Vector3 new_scale = transform.localScale;
            new_scale.y = new_scale.y +  (2F * new_scale.y *  Time.deltaTime);
            transform.localScale = new_scale;
        }
        else if (destroy == 2)
        {
            Vector3 new_scale = transform.localScale;
            new_scale.y = new_scale.y -  (7F * new_scale.y *  Time.deltaTime);
            transform.localScale = new_scale;

            if (transform.localScale.y < 0.05F)
            {
                gameObject.SetActive(false);
            }
        }


        if (upgrade == 1)
        {
            Vector3 new_scale = transform.localScale;
            new_scale.y = new_scale.y +  (2F * new_scale.y *  Time.deltaTime);
            transform.localScale = new_scale;
        }
        else if (upgrade == 2)
        {
            Vector3 new_scale = transform.localScale;
            new_scale.y = new_scale.y -  (2F * new_scale.y *  Time.deltaTime);
            transform.localScale = new_scale;

            if (transform.localScale.y < local_scale_save.y)
            {
                upgrade = 0;
                transform.localScale = local_scale_save;
            }
        }
    }

    public virtual IEnumerator destroyVendor()
    {
        ap.Proc();
        destroy = 1;
        yield return new WaitForSeconds(0.1F);
        destroy = 2;
    }

    public virtual IEnumerator upgradeVendor()
    {
        ap.Proc();
        upgrade = 1;
        yield return new WaitForSeconds(0.1F);
        upgrade = 2;
    }

    public virtual void buyUpgrade(SelectEnterEventArgs arg)
    {
        // Override this in child class
        Debug.Log("DEFAULT VENDOR BEHAVIOR USED. PLEASE OVERRIDE THIS FUNCTION");
    }
}
