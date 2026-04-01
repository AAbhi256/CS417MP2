using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class PrestigeVendor : VendorBehavior
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        // Does nothing. Overrides tho
    }

    override public void buyUpgrade(SelectEnterEventArgs arg)
    {
        PlayerBehavior.prestigeMult = 1 + (0.5F * PlayerBehavior.soulAmount);
        GameStateSaver saver = FindFirstObjectByType<GameStateSaver>();
        if (saver != null)
        {
            saver.SaveData();
        }
        // Restart from start w/ prestiege 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
