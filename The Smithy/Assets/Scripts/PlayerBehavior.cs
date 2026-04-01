using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(100)]
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
    private const float defaultGoldAmount = 1000000f;
    private const float defaultGemAmount = 1000000f;
    private const float defaultSoulAmount = 0f;
    private const float defaultSellPrice = 1f;
    private const float defaultGoldGenerationRateMult = 1f;
    private const float defaultGoldGenerationRate = 0f;
    private const float defaultGemGenerationRate = 0f;
    private const float defaultPrestigeMult = 1f;

    /// <summary>
    /// Static economy survives scene reloads (e.g. prestige). Defaults apply once per Play Mode session only.
    /// </summary>
    private static bool sessionEconomyInitialized;

    /// <summary>
    /// Run in Awake so static economy fields are set before other scripts' first Update.
    /// </summary>
    private void Awake()
    {
        if (!sessionEconomyInitialized)
        {
            SetDefaultStats();
            sessionEconomyInitialized = true;
        }
    }

    void Start()
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

    private void SetDefaultStats()
    {
        goldAmount = defaultGoldAmount;
        gemAmount = defaultGemAmount;
        soulAmount = defaultSoulAmount;
        sellPrice = defaultSellPrice;
        goldGenerationRateMult = defaultGoldGenerationRateMult;
        goldGenerationRate = defaultGoldGenerationRate;
        gemGenerationRate = defaultGemGenerationRate;
        prestigeMult = defaultPrestigeMult;
    }

    void Update()
    {
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
        temp = Instantiate(StatsCanvas, this.transform.position + new Vector3(1, 2, 0), this.transform.rotation, null);
        temp.transform.Rotate(Vector3.up * 180);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject target = hit.gameObject;
        DoorBehavior door = target.GetComponent<DoorBehavior>();
        if (door != null)
        {
            door.WarpPlayer();
            return;
        }
    }
}
