using UnityEngine;
using UnityEngine.InputSystem;
using Avatar = Alteruna.Avatar;
using TrackedPoseDriver = UnityEngine.InputSystem.XR.TrackedPoseDriver;

public class PlayerBehavior : MonoBehaviour
{
    static public float goldAmount;
    static public float gemAmount;
    static public float soulAmount;
    static public float sellPrice;

    static public float goldGenerationRateMult;
    static public float goldGenerationRate;
    static public float gemGenerationRate;
    static public float prestigeMult = 1; 

    public Canvas StatsCanvas;
    public InputActionReference statsAction;

    private Canvas temp = null;




    private Avatar avatar;
    [SerializeField]
    private Transform head;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private int playerSelfLayer;
    [SerializeField]
    private TrackedPoseDriver leftHandDriver, rightHandDriver;
    private bool initialized = false;
    [SerializeField]
    private Camera fallbackCamera;

    void Awake()
    {
        avatar = GetComponent<Avatar>();
    }
    void Start()
    {
        if (avatar != null && avatar.IsMe)
        {
            goldAmount = 1000000;
            gemAmount = 1000000;
            soulAmount = 0;
            sellPrice = 1;
            goldGenerationRateMult = 1;
            goldGenerationRate = 0;
            gemGenerationRate = 0;
        }

        if (statsAction != null)
        {
            statsAction.action.Enable();
            statsAction.action.performed += (ctx) =>
            {
                if (temp == null)
                {
                    ViewStats();
                }
                else
                {
                    Destroy(temp);
                    temp = null;
                }
            };
        }
    }


    void Update()
    {
        if (avatar == null) return;

        if (!initialized && avatar.IsMe)
        {
            initialized = true;
            head.gameObject.layer = playerSelfLayer;
            foreach (Transform child in head)
            {
                child.gameObject.layer = playerSelfLayer;
            }
            fallbackCamera.gameObject.SetActive(false);

        }
        else if (!initialized && !avatar.IsMe && avatar.IsPossessed)
        {
            initialized = true;
            if (leftHandDriver != null) leftHandDriver.enabled = false;
            if (rightHandDriver != null) rightHandDriver.enabled = false;
            if (camera != null) camera.gameObject.SetActive(false);
        }

        if (avatar.IsMe)
        {
            head.localPosition = camera.transform.localPosition;
            head.rotation = camera.transform.rotation;

            EarnGold();
            EarnGem();
        }
    }

    public void EarnGold()
    {
        goldAmount += 0.5F * prestigeMult * goldGenerationRate * goldGenerationRateMult * Time.deltaTime;
    }

    public void EarnGem()
    {
        gemAmount += 0.1F * prestigeMult * gemGenerationRate * Time.deltaTime;   
    }

    public void ViewStats()
    {
        if (StatsCanvas == null) return;
        temp = Instantiate(StatsCanvas, this.transform.position + new Vector3(1,2,0), this.transform.rotation, null);
        temp.transform.Rotate(Vector3.up * 180);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Debug.Log("Player Collision Detected!");

        GameObject target = hit.gameObject;
        DoorBehavior door = target.GetComponent<DoorBehavior>();
        if (door != null) {
            door.WarpPlayer();
            return;
        }
    }
}
