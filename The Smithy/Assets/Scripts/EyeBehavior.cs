using UnityEngine;

public class EyeBehavior : MonoBehaviour
{
    public PlayerBehavior player;
    public GameObject sclera;
    private float blinkTime = 3;
    private bool blinkMode = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.ShowSurprise();
    }

    // Update is called once per frame
    void Update()
    {
        this.FollowPlayer();
        this.Blink();
    }

    public void FollowPlayer()
    {
        this.transform.LookAt(player.transform);
    }

    public void Blink()
    {
        if (blinkTime > 0)
        {
            sclera.SetActive(blinkMode);
            blinkTime -= Time.deltaTime;
        }
        else
        {
            blinkMode = !blinkMode;
            blinkTime = 3;
        }

    }

    public void ShowSurprise()
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y * 2, this.transform.localScale.z);
    }
}
