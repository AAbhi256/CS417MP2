using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

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
        text.text = 
            "Gold: " + math.round(PlayerBehavior.goldAmount) + 
            "\nSell Price: " + PlayerBehavior.sellPrice +
            "\nGold Income: " + PlayerBehavior.goldGenerationRate +
            "\nGold Income Multiplier: " + PlayerBehavior.goldGenerationRateMult; 
        if (PlayerBehavior.prestigeMult != 1)
        {
            text.text += 
            "\nPrestige Multiplier: " + PlayerBehavior.prestigeMult;
        }

        if (PlayerBehavior.gemAmount != 0 || PlayerBehavior.gemGenerationRate != 0)
        {
            text.text += 
            "\nGems: " + math.round(PlayerBehavior.gemAmount)+
            "\nGem Income: " + PlayerBehavior.gemGenerationRate;;
        }
        if (PlayerBehavior.soulAmount != 0)
        {
            text.text += 
            "\nSouls: " + PlayerBehavior.soulAmount;
        }
    }
}