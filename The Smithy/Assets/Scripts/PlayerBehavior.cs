using System;
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

    private const string keyHasSave = "HasSave";
    private const string keyQuitTimeUtcUnix = "QuitTimeUtcUnix";
    private const string keyGoldAmount = "GoldAmount";
    private const string keyGemAmount = "GemAmount";
    private const string keySoulAmount = "SoulAmount";
    private const string keySellPrice = "SellPrice";
    private const string keyGoldGenerationRateMult = "GoldGenerationRateMult";
    private const string keyGoldGenerationRate = "GoldGenerationRate";
    private const string keyGemGenerationRate = "GemGenerationRate";
    private const string keyPrestigeMult = "PrestigeMult";
    private const string keyItemNumLeftToSpawn = "ItemNumLeftToSpawn";
    private const string keyMoneyVendorActive = "MoneyVendorActive";
    private const string keyMoneyVendorPrice = "MoneyVendorPrice";
    private const string keyJewelVendorActive = "JewelVendorActive";
    private const string keyJewelVendorPrice = "JewelVendorPrice";
    private const string keyGeneratorVendorPrice = "GeneratorVendorPrice";
    private const string keySoulVendorPrice = "SoulVendorPrice";
    private const string keyGemVendorCount = "GemVendorCount";
    private const string keyDoorVendorCount = "DoorVendorCount";
    private const string keyHoleCount = "HoleCount";
    private const string keyMP2ManagerCount = "MP2ManagerCount";


    void Start()
    {
        if (PlayerPrefs.GetFloat(keyHasSave, 0f) > 0.5f)
        {
            Load();
        }
        else
        {
            SetDefaultStats();
        }

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
        temp = Instantiate(StatsCanvas, this.transform.position + new Vector3(1,2,0), this.transform.rotation, null);
        temp.transform.Rotate(Vector3.up * 180);
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(keyHasSave, 1f);
        PlayerPrefs.SetFloat(keyQuitTimeUtcUnix, (float)DateTimeOffset.UtcNow.ToUnixTimeSeconds());

        PlayerPrefs.SetFloat(keyGoldAmount, goldAmount);
        PlayerPrefs.SetFloat(keyGemAmount, gemAmount);
        PlayerPrefs.SetFloat(keySoulAmount, soulAmount);
        PlayerPrefs.SetFloat(keySellPrice, sellPrice);
        PlayerPrefs.SetFloat(keyGoldGenerationRateMult, goldGenerationRateMult);
        PlayerPrefs.SetFloat(keyGoldGenerationRate, goldGenerationRate);
        PlayerPrefs.SetFloat(keyGemGenerationRate, gemGenerationRate);
        PlayerPrefs.SetFloat(keyPrestigeMult, prestigeMult);
        PlayerPrefs.SetFloat(keyItemNumLeftToSpawn, ItemManager.numItemsLeftToSpawn);

        SaveGemVendors();
        SaveMoneyAndJewelVendors();
        SaveSingleVendorPrices();
        SaveDoorVendors();
        SaveHoles();
        SavePowerups();

        PlayerPrefs.Save();
    }

    public void Load()
    {
        SetDefaultStats();

        goldAmount = PlayerPrefs.GetFloat(keyGoldAmount, goldAmount);
        gemAmount = PlayerPrefs.GetFloat(keyGemAmount, gemAmount);
        soulAmount = PlayerPrefs.GetFloat(keySoulAmount, soulAmount);
        sellPrice = PlayerPrefs.GetFloat(keySellPrice, sellPrice);
        goldGenerationRateMult = PlayerPrefs.GetFloat(keyGoldGenerationRateMult, goldGenerationRateMult);
        goldGenerationRate = PlayerPrefs.GetFloat(keyGoldGenerationRate, goldGenerationRate);
        gemGenerationRate = PlayerPrefs.GetFloat(keyGemGenerationRate, gemGenerationRate);
        prestigeMult = PlayerPrefs.GetFloat(keyPrestigeMult, prestigeMult);
        ItemManager.numItemsLeftToSpawn = Mathf.RoundToInt(PlayerPrefs.GetFloat(keyItemNumLeftToSpawn, ItemManager.numItemsLeftToSpawn));

        float elapsedSeconds = 0f;
        if (PlayerPrefs.HasKey(keyQuitTimeUtcUnix))
        {
            float quitTime = PlayerPrefs.GetFloat(keyQuitTimeUtcUnix, (float)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            elapsedSeconds = Mathf.Max(0f, (float)DateTimeOffset.UtcNow.ToUnixTimeSeconds() - quitTime);
        }

        goldAmount += 0.5f * prestigeMult * goldGenerationRate * goldGenerationRateMult * elapsedSeconds;
        gemAmount += 0.1f * prestigeMult * gemGenerationRate * elapsedSeconds;

        ApplySceneStateAfterVendors(elapsedSeconds);
    }

    private void SaveGemVendors()
    {
        GemVendor[] gemVendors = FindObjectsByType<GemVendor>(FindObjectsSortMode.None);
        Array.Sort(gemVendors, (a, b) => a.vendor_num.CompareTo(b.vendor_num));

        PlayerPrefs.SetFloat(keyGemVendorCount, gemVendors.Length);
        for (int i = 0; i < gemVendors.Length; i++)
        {
            PlayerPrefs.SetFloat("GemVendorActive_" + i, gemVendors[i].gameObject.activeSelf ? 1f : 0f);
            PlayerPrefs.SetFloat("GemVendorPrice_" + i, gemVendors[i].price);
        }
    }

    private void SaveMoneyAndJewelVendors()
    {
        if (MoneyVendor.money_vendor != null)
        {
            PlayerPrefs.SetFloat(keyMoneyVendorActive, MoneyVendor.money_vendor.activeSelf ? 1f : 0f);
            MoneyVendor money = MoneyVendor.money_vendor.GetComponent<MoneyVendor>();
            if (money != null)
            {
                PlayerPrefs.SetFloat(keyMoneyVendorPrice, money.price);
            }
        }

        if (JewelVendor.jewel_vendor != null)
        {
            PlayerPrefs.SetFloat(keyJewelVendorActive, JewelVendor.jewel_vendor.activeSelf ? 1f : 0f);
            JewelVendor jewel = JewelVendor.jewel_vendor.GetComponent<JewelVendor>();
            if (jewel != null)
            {
                PlayerPrefs.SetFloat(keyJewelVendorPrice, jewel.price);
            }
        }
    }

    private void SaveSingleVendorPrices()
    {
        GeneratorVendor generator = FindFirstObjectByType<GeneratorVendor>();
        if (generator != null)
        {
            PlayerPrefs.SetFloat(keyGeneratorVendorPrice, generator.price);
        }

        SoulVendor soulVendor = FindFirstObjectByType<SoulVendor>();
        if (soulVendor != null)
        {
            PlayerPrefs.SetFloat(keySoulVendorPrice, soulVendor.price);
        }
    }

    private void SaveDoorVendors()
    {
        DoorVendor[] doors = FindObjectsByType<DoorVendor>(FindObjectsSortMode.None);
        PlayerPrefs.SetFloat(keyDoorVendorCount, doors.Length);

        for (int i = 0; i < doors.Length; i++)
        {
            PlayerPrefs.SetFloat("DoorVendorActive_" + i, doors[i].gameObject.activeSelf ? 1f : 0f);
            PlayerPrefs.SetFloat("DoorVendorPrice_" + i, doors[i].price);
            PlayerPrefs.SetFloat("Door1Active_" + i, doors[i].door1 != null && doors[i].door1.activeSelf ? 1f : 0f);
            PlayerPrefs.SetFloat("Door2Active_" + i, doors[i].door2 != null && doors[i].door2.activeSelf ? 1f : 0f);
        }
    }

    private void SaveHoles()
    {
        Hole[] holes = FindObjectsByType<Hole>(FindObjectsSortMode.None);
        PlayerPrefs.SetFloat(keyHoleCount, holes.Length);

        for (int i = 0; i < holes.Length; i++)
        {
            PlayerPrefs.SetFloat("HolePrice_" + i, holes[i].price);
            if (holes[i].hole != null)
            {
                Vector3 scale = holes[i].hole.transform.localScale;
                PlayerPrefs.SetFloat("HoleScaleX_" + i, scale.x);
                PlayerPrefs.SetFloat("HoleScaleY_" + i, scale.y);
                PlayerPrefs.SetFloat("HoleScaleZ_" + i, scale.z);
            }
        }
    }

    private void SavePowerups()
    {
        MP2Manager[] managers = FindObjectsByType<MP2Manager>(FindObjectsSortMode.None);
        PlayerPrefs.SetFloat(keyMP2ManagerCount, managers.Length);

        for (int i = 0; i < managers.Length; i++)
        {
            PlayerPrefs.SetFloat("MP2BaseGrowthRate_" + i, managers[i].baseGrowthRate);
            PlayerPrefs.SetFloat("MP2BoostedGrowthRate_" + i, managers[i].boostedGrowthRate);
            PlayerPrefs.SetFloat("MP2MedicineAmount_" + i, managers[i].medicineAmount);
        }
    }

    private void ApplySceneStateAfterVendors(float elapsedSeconds)
    {
        GemVendor[] gemVendors = FindObjectsByType<GemVendor>(FindObjectsSortMode.None);
        Array.Sort(gemVendors, (a, b) => a.vendor_num.CompareTo(b.vendor_num));
        int gemVendorCount = Mathf.Min(gemVendors.Length, Mathf.RoundToInt(PlayerPrefs.GetFloat(keyGemVendorCount, gemVendors.Length)));
        for (int i = 0; i < gemVendorCount; i++)
        {
            gemVendors[i].price = PlayerPrefs.GetFloat("GemVendorPrice_" + i, gemVendors[i].price);
            gemVendors[i].SetText();
            gemVendors[i].gameObject.SetActive(PlayerPrefs.GetFloat("GemVendorActive_" + i, gemVendors[i].gameObject.activeSelf ? 1f : 0f) > 0.5f);
        }

        if (MoneyVendor.money_vendor != null)
        {
            MoneyVendor money = MoneyVendor.money_vendor.GetComponent<MoneyVendor>();
            if (money != null)
            {
                money.price = PlayerPrefs.GetFloat(keyMoneyVendorPrice, money.price);
                money.SetText();
            }
            MoneyVendor.money_vendor.SetActive(PlayerPrefs.GetFloat(keyMoneyVendorActive, MoneyVendor.money_vendor.activeSelf ? 1f : 0f) > 0.5f);
        }

        if (JewelVendor.jewel_vendor != null)
        {
            JewelVendor jewel = JewelVendor.jewel_vendor.GetComponent<JewelVendor>();
            if (jewel != null)
            {
                jewel.price = PlayerPrefs.GetFloat(keyJewelVendorPrice, jewel.price);
                jewel.SetText();
            }
            JewelVendor.jewel_vendor.SetActive(PlayerPrefs.GetFloat(keyJewelVendorActive, JewelVendor.jewel_vendor.activeSelf ? 1f : 0f) > 0.5f);
        }

        GeneratorVendor generator = FindFirstObjectByType<GeneratorVendor>();
        if (generator != null)
        {
            generator.price = PlayerPrefs.GetFloat(keyGeneratorVendorPrice, generator.price);
            generator.SetText();
        }

        SoulVendor soulVendor = FindFirstObjectByType<SoulVendor>();
        if (soulVendor != null)
        {
            soulVendor.price = PlayerPrefs.GetFloat(keySoulVendorPrice, soulVendor.price);
            soulVendor.SetText();
        }

        DoorVendor[] doors = FindObjectsByType<DoorVendor>(FindObjectsSortMode.None);
        int doorCount = Mathf.Min(doors.Length, Mathf.RoundToInt(PlayerPrefs.GetFloat(keyDoorVendorCount, doors.Length)));
        for (int i = 0; i < doorCount; i++)
        {
            doors[i].price = PlayerPrefs.GetFloat("DoorVendorPrice_" + i, doors[i].price);
            doors[i].SetText();
            if (doors[i].door1 != null)
            {
                doors[i].door1.SetActive(PlayerPrefs.GetFloat("Door1Active_" + i, doors[i].door1.activeSelf ? 1f : 0f) > 0.5f);
            }
            if (doors[i].door2 != null)
            {
                doors[i].door2.SetActive(PlayerPrefs.GetFloat("Door2Active_" + i, doors[i].door2.activeSelf ? 1f : 0f) > 0.5f);
            }
            doors[i].gameObject.SetActive(PlayerPrefs.GetFloat("DoorVendorActive_" + i, doors[i].gameObject.activeSelf ? 1f : 0f) > 0.5f);
        }

        Hole[] holes = FindObjectsByType<Hole>(FindObjectsSortMode.None);
        int holeCount = Mathf.Min(holes.Length, Mathf.RoundToInt(PlayerPrefs.GetFloat(keyHoleCount, holes.Length)));
        for (int i = 0; i < holeCount; i++)
        {
            holes[i].price = PlayerPrefs.GetFloat("HolePrice_" + i, holes[i].price);
            holes[i].SetText();
            if (holes[i].hole != null)
            {
                holes[i].hole.transform.localScale = new Vector3(
                    PlayerPrefs.GetFloat("HoleScaleX_" + i, holes[i].hole.transform.localScale.x),
                    PlayerPrefs.GetFloat("HoleScaleY_" + i, holes[i].hole.transform.localScale.y),
                    PlayerPrefs.GetFloat("HoleScaleZ_" + i, holes[i].hole.transform.localScale.z));
            }
        }

        MP2Manager[] managers = FindObjectsByType<MP2Manager>(FindObjectsSortMode.None);
        int managerCount = Mathf.Min(managers.Length, Mathf.RoundToInt(PlayerPrefs.GetFloat(keyMP2ManagerCount, managers.Length)));
        for (int i = 0; i < managerCount; i++)
        {
            managers[i].baseGrowthRate = PlayerPrefs.GetFloat("MP2BaseGrowthRate_" + i, managers[i].baseGrowthRate);
            managers[i].boostedGrowthRate = PlayerPrefs.GetFloat("MP2BoostedGrowthRate_" + i, managers[i].boostedGrowthRate);
            managers[i].medicineAmount = PlayerPrefs.GetFloat("MP2MedicineAmount_" + i, managers[i].medicineAmount);
            managers[i].medicineAmount += managers[i].baseGrowthRate * elapsedSeconds;
        }
    }

    void OnApplicationQuit()
    {
        Save();
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
