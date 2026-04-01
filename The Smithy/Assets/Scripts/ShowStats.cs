using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

public class ShowStats : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject controller;

    AudioSource audio_source;
    bool[] is_new_unlock = {false, false}; //idx 0 is gems and 1 is souls
    //Storing stats in here statically
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        audio_source = controller.GetComponent<AudioSource>();
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
            if (!is_new_unlock[0])
            {
                is_new_unlock[0] = true;
                audio_source.Play();
            }
            text.text += 
            "\nGems: " + math.round(PlayerBehavior.gemAmount)+
            "\nGem Income: " + PlayerBehavior.gemGenerationRate;;
        }
        if (PlayerBehavior.soulAmount != 0)
        {
            if (!is_new_unlock[1])
            {
                is_new_unlock[1] = true;
                audio_source.Play();
            }
            text.text += 
            "\nSouls: " + PlayerBehavior.soulAmount;
        }
    }
}