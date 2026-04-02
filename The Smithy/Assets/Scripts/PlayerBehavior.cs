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

    void Awake()
    {
        avatar = GetComponent<Avatar>();
    }
    void Start()
    {

        if (avatar.IsMe)
        {
            head.gameObject.layer = playerSelfLayer;
            foreach (Transform child in head)
            {
                child.gameObject.layer = playerSelfLayer;
            }
        }
        else
        {
            leftHandDriver.enabled = false;
            rightHandDriver.enabled = false;
            camera.gameObject.SetActive(false);
        }


        goldAmount = 1000000;
        gemAmount = 1000000;
        soulAmount = 0;
        sellPrice = 1;
        goldGenerationRateMult = 1;
        goldGenerationRate = 0;
        gemGenerationRate = 0;

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


    void Update()
    {
        if (avatar.IsMe)
        {
            head.localPosition = camera.transform.localPosition;
            head.rotation = camera.transform.rotation;
        }


        EarnGold();
        EarnGem();
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
