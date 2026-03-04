using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowStats : MonoBehaviour
{
    public TextMeshProUGUI text;
    public PlayerBehavior player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Gold: " + player.goldAmount.ToString() + "\n Reputation: " + player.reputationAmount.ToString();
        
    }
}