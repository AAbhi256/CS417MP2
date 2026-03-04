using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowStats : MonoBehaviour
{
    public TextMeshProUGUI text;

    //Storing stats in here statically
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Gold: " + PlayerBehavior.goldAmount.ToString() + 
                    "\nSouls: " + PlayerBehavior.soulAmount.ToString();
    }
}