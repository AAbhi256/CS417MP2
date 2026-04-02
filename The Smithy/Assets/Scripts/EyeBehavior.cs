using UnityEngine;

public class EyeBehavior : MonoBehaviour
{
    public PlayerBehavior player;
    public GameObject sclera;
    public GameObject closedEyeModel;
    public GameObject mouth;
    public bool isSmiling = true;
    private float blinkTime = 3;
    private bool blinkMode = true;
    private float surpriseTime = 3;
    private bool surpriseMode = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (juice detected && surpriseMode == false) {
        //     surpriseMode = true;
        //     ShowSurprise();
        // }
        //if good event detected, smile and set issmiling true. if bad event detected, frown and set issmiling false
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
            closedEyeModel.SetActive(!blinkMode);
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
        while (surpriseTime > 0)
        {
            surpriseTime -= Time.deltaTime;
        }
        surpriseTime = 3;
        surpriseMode = false;
        this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y / 2, this.transform.localScale.z);

    }

    public void Frown()
    {
        if (isSmiling)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y * -1, this.transform.localScale.z);
        }
    }

    public void Smile()
    {
        if (!isSmiling)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y * -1, this.transform.localScale.z);
        }
    }
}
