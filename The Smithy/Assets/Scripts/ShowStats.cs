using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

public class ShowStats : MonoBehaviour
{
    public TextMeshProUGUI text;

    public GameObject right_controller;
    bool[] new_unlock = {false,false};

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
            "\nSword Market Value: " + PlayerBehavior.sellPrice +
            "\nGold Income: " + PlayerBehavior.goldGenerationRate +
            "\nGold Income Multiplier: " + PlayerBehavior.goldGenerationRateMult; 
        if (PlayerBehavior.prestigeMult != 1)
        {
            text.text += 
            "\nPrestige Multiplier: " + PlayerBehavior.prestigeMult;
        }

        if (PlayerBehavior.gemAmount != 0 || PlayerBehavior.gemGenerationRate != 0)
        {
            if (!new_unlock[0])
            {
                new_unlock[0] = true;
                right_controller.GetComponent<AudioSource>().Play();
            }
            text.text += 
            "\nGems: " + math.round(PlayerBehavior.gemAmount)+
            "\nGem Income: " + PlayerBehavior.gemGenerationRate;;
        }
        if (PlayerBehavior.soulAmount != 0)
        {            
            if (!new_unlock[1])
            {
                new_unlock[1] = true;
                right_controller.GetComponent<AudioSource>().Play();
            }
                text.text += 
            "\nSouls: " + PlayerBehavior.soulAmount;
        }
    }
}