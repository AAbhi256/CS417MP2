using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Inter-session persistence via PlayerPrefs (flat keys, no JSON).
/// Load runs one frame after Start so vendor static refs (MoneyVendor, etc.) are assigned.
/// </summary>
public class GameStateSaver : MonoBehaviour
{
    private const string keyHasSave = "HasSave";
    private const string keyGoldAmount = "GoldAmount";
    private const string keyGemAmount = "GemAmount";
    private const string keySoulAmount = "SoulAmount";
    private const string keySellPrice = "SellPrice";
    private const string keyGoldGenerationRateMult = "GoldGenerationRateMult";
    private const string keyGoldGenerationRate = "GoldGenerationRate";
    private const string keyGemGenerationRate = "GemGenerationRate";
    private const string keyPrestigeMult = "PrestigeMult";
    private const string keyItemNumLeftToSpawn = "ItemNumLeftToSpawn";

    private const string keyGemVendorCount = "GemVendorCount";
    private const string keyMoneyVendorActive = "MoneyVendorActive";
    private const string keyMoneyVendorPrice = "MoneyVendorPrice";
    private const string keyJewelVendorActive = "JewelVendorActive";
    private const string keyJewelVendorPrice = "JewelVendorPrice";
    private const string keyGeneratorVendorPrice = "GeneratorVendorPrice";
    private const string keySoulVendorPrice = "SoulVendorPrice";
    private const string keyDoorVendorCount = "DoorVendorCount";
    private const string keyHoleCount = "HoleCount";
    private const string keyMP2ManagerCount = "MP2ManagerCount";

    private void Start()
    {
        StartCoroutine(LoadWhenReady());
    }

    private IEnumerator LoadWhenReady()
    {
        yield return null;
        LoadFromPrefs();
    }

    private void LoadFromPrefs()
    {
        if (PlayerPrefs.GetFloat(keyHasSave, 0f) <= 0.5f)
            return;

        PlayerBehavior.goldAmount = PlayerPrefs.GetFloat(keyGoldAmount, PlayerBehavior.goldAmount);
        PlayerBehavior.gemAmount = PlayerPrefs.GetFloat(keyGemAmount, PlayerBehavior.gemAmount);
        PlayerBehavior.soulAmount = PlayerPrefs.GetFloat(keySoulAmount, PlayerBehavior.soulAmount);
        PlayerBehavior.sellPrice = PlayerPrefs.GetFloat(keySellPrice, PlayerBehavior.sellPrice);
        PlayerBehavior.goldGenerationRateMult = PlayerPrefs.GetFloat(keyGoldGenerationRateMult, PlayerBehavior.goldGenerationRateMult);
        PlayerBehavior.goldGenerationRate = PlayerPrefs.GetFloat(keyGoldGenerationRate, PlayerBehavior.goldGenerationRate);
        PlayerBehavior.gemGenerationRate = PlayerPrefs.GetFloat(keyGemGenerationRate, PlayerBehavior.gemGenerationRate);
        PlayerBehavior.prestigeMult = PlayerPrefs.GetFloat(keyPrestigeMult, PlayerBehavior.prestigeMult);

        ItemManager.numItemsLeftToSpawn = PlayerPrefs.GetInt(keyItemNumLeftToSpawn, ItemManager.numItemsLeftToSpawn);

        ApplyGemVendorsFromPrefs();
        ApplyMoneyAndJewelFromPrefs();
        ApplyGeneratorAndSoulFromPrefs();
        ApplyDoorVendorsFromPrefs();
        ApplyHolesFromPrefs();
        ApplyMp2FromPrefs();
    }

    private static void ApplyGemVendorsFromPrefs()
    {
        GemVendor[] gemVendors = FindObjectsByType<GemVendor>(FindObjectsSortMode.None);
        Array.Sort(gemVendors, (a, b) => a.vendor_num.CompareTo(b.vendor_num));
        int count = Mathf.Min(gemVendors.Length, PlayerPrefs.GetInt(keyGemVendorCount, gemVendors.Length));
        for (int i = 0; i < count; i++)
        {
            gemVendors[i].price = PlayerPrefs.GetFloat(GemPriceKey(i), gemVendors[i].price);
            gemVendors[i].SetText();
            gemVendors[i].gameObject.SetActive(GetBoolPref(GemActiveKey(i), gemVendors[i].gameObject.activeSelf));
        }
    }

    private static void ApplyMoneyAndJewelFromPrefs()
    {
        if (MoneyVendor.money_vendor != null)
        {
            MoneyVendor money = MoneyVendor.money_vendor.GetComponent<MoneyVendor>();
            if (money != null)
            {
                money.price = PlayerPrefs.GetFloat(keyMoneyVendorPrice, money.price);
                money.SetText();
            }
            MoneyVendor.money_vendor.SetActive(GetBoolPref(keyMoneyVendorActive, MoneyVendor.money_vendor.activeSelf));
        }

        if (JewelVendor.jewel_vendor != null)
        {
            JewelVendor jewel = JewelVendor.jewel_vendor.GetComponent<JewelVendor>();
            if (jewel != null)
            {
                jewel.price = PlayerPrefs.GetFloat(keyJewelVendorPrice, jewel.price);
                jewel.SetText();
            }
            JewelVendor.jewel_vendor.SetActive(GetBoolPref(keyJewelVendorActive, JewelVendor.jewel_vendor.activeSelf));
        }
    }

    private static void ApplyGeneratorAndSoulFromPrefs()
    {
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
    }

    private static void ApplyDoorVendorsFromPrefs()
    {
        DoorVendor[] doors = FindObjectsByType<DoorVendor>(FindObjectsSortMode.None);
        Array.Sort(doors, (a, b) => string.CompareOrdinal(a.gameObject.name, b.gameObject.name));
        int count = Mathf.Min(doors.Length, PlayerPrefs.GetInt(keyDoorVendorCount, doors.Length));
        for (int i = 0; i < count; i++)
        {
            doors[i].price = PlayerPrefs.GetFloat(DoorPriceKey(i), doors[i].price);
            doors[i].SetText();
            if (doors[i].door1 != null)
                doors[i].door1.SetActive(GetBoolPref(Door1Key(i), doors[i].door1.activeSelf));
            if (doors[i].door2 != null)
                doors[i].door2.SetActive(GetBoolPref(Door2Key(i), doors[i].door2.activeSelf));
            doors[i].gameObject.SetActive(GetBoolPref(DoorVendorActiveKey(i), doors[i].gameObject.activeSelf));
        }
    }

    private static void ApplyHolesFromPrefs()
    {
        Hole[] holes = FindObjectsByType<Hole>(FindObjectsSortMode.None);
        Array.Sort(holes, (a, b) => string.CompareOrdinal(a.gameObject.name, b.gameObject.name));
        int count = Mathf.Min(holes.Length, PlayerPrefs.GetInt(keyHoleCount, holes.Length));
        for (int i = 0; i < count; i++)
        {
            holes[i].price = PlayerPrefs.GetFloat(HolePriceKey(i), holes[i].price);
            holes[i].SetText();
            if (holes[i].hole != null)
            {
                holes[i].hole.transform.localScale = new Vector3(
                    PlayerPrefs.GetFloat(HoleScaleXKey(i), holes[i].hole.transform.localScale.x),
                    PlayerPrefs.GetFloat(HoleScaleYKey(i), holes[i].hole.transform.localScale.y),
                    PlayerPrefs.GetFloat(HoleScaleZKey(i), holes[i].hole.transform.localScale.z));
            }
        }
    }

    private static void ApplyMp2FromPrefs()
    {
        MP2Manager[] managers = FindObjectsByType<MP2Manager>(FindObjectsSortMode.None);
        Array.Sort(managers, (a, b) => string.CompareOrdinal(a.gameObject.name, b.gameObject.name));
        int count = Mathf.Min(managers.Length, PlayerPrefs.GetInt(keyMP2ManagerCount, managers.Length));
        for (int i = 0; i < count; i++)
        {
            managers[i].baseGrowthRate = PlayerPrefs.GetFloat(Mp2BaseKey(i), managers[i].baseGrowthRate);
            managers[i].boostedGrowthRate = PlayerPrefs.GetFloat(Mp2BoostKey(i), managers[i].boostedGrowthRate);
            managers[i].medicineAmount = PlayerPrefs.GetFloat(Mp2MedicineKey(i), managers[i].medicineAmount);
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat(keyHasSave, 1f);

        PlayerPrefs.SetFloat(keyGoldAmount, PlayerBehavior.goldAmount);
        PlayerPrefs.SetFloat(keyGemAmount, PlayerBehavior.gemAmount);
        PlayerPrefs.SetFloat(keySoulAmount, PlayerBehavior.soulAmount);
        PlayerPrefs.SetFloat(keySellPrice, PlayerBehavior.sellPrice);
        PlayerPrefs.SetFloat(keyGoldGenerationRateMult, PlayerBehavior.goldGenerationRateMult);
        PlayerPrefs.SetFloat(keyGoldGenerationRate, PlayerBehavior.goldGenerationRate);
        PlayerPrefs.SetFloat(keyGemGenerationRate, PlayerBehavior.gemGenerationRate);
        PlayerPrefs.SetFloat(keyPrestigeMult, PlayerBehavior.prestigeMult);

        PlayerPrefs.SetInt(keyItemNumLeftToSpawn, ItemManager.numItemsLeftToSpawn);

        SaveGemVendors();
        SaveMoneyAndJewel();
        SaveGeneratorAndSoul();
        SaveDoorVendors();
        SaveHoles();
        SaveMp2();

        PlayerPrefs.Save();
    }

    private static void SaveGemVendors()
    {
        GemVendor[] gemVendors = FindObjectsByType<GemVendor>(FindObjectsSortMode.None);
        Array.Sort(gemVendors, (a, b) => a.vendor_num.CompareTo(b.vendor_num));
        PlayerPrefs.SetInt(keyGemVendorCount, gemVendors.Length);
        for (int i = 0; i < gemVendors.Length; i++)
        {
            SetBoolPref(GemActiveKey(i), gemVendors[i].gameObject.activeSelf);
            PlayerPrefs.SetFloat(GemPriceKey(i), gemVendors[i].price);
        }
    }

    private static void SaveMoneyAndJewel()
    {
        if (MoneyVendor.money_vendor != null)
        {
            SetBoolPref(keyMoneyVendorActive, MoneyVendor.money_vendor.activeSelf);
            MoneyVendor money = MoneyVendor.money_vendor.GetComponent<MoneyVendor>();
            if (money != null)
                PlayerPrefs.SetFloat(keyMoneyVendorPrice, money.price);
        }

        if (JewelVendor.jewel_vendor != null)
        {
            SetBoolPref(keyJewelVendorActive, JewelVendor.jewel_vendor.activeSelf);
            JewelVendor jewel = JewelVendor.jewel_vendor.GetComponent<JewelVendor>();
            if (jewel != null)
                PlayerPrefs.SetFloat(keyJewelVendorPrice, jewel.price);
        }
    }

    private static void SaveGeneratorAndSoul()
    {
        GeneratorVendor generator = FindFirstObjectByType<GeneratorVendor>();
        if (generator != null)
            PlayerPrefs.SetFloat(keyGeneratorVendorPrice, generator.price);

        SoulVendor soulVendor = FindFirstObjectByType<SoulVendor>();
        if (soulVendor != null)
            PlayerPrefs.SetFloat(keySoulVendorPrice, soulVendor.price);
    }

    private static void SaveDoorVendors()
    {
        DoorVendor[] doors = FindObjectsByType<DoorVendor>(FindObjectsSortMode.None);
        Array.Sort(doors, (a, b) => string.CompareOrdinal(a.gameObject.name, b.gameObject.name));
        PlayerPrefs.SetInt(keyDoorVendorCount, doors.Length);
        for (int i = 0; i < doors.Length; i++)
        {
            SetBoolPref(DoorVendorActiveKey(i), doors[i].gameObject.activeSelf);
            PlayerPrefs.SetFloat(DoorPriceKey(i), doors[i].price);
            if (doors[i].door1 != null)
                SetBoolPref(Door1Key(i), doors[i].door1.activeSelf);
            if (doors[i].door2 != null)
                SetBoolPref(Door2Key(i), doors[i].door2.activeSelf);
        }
    }

    private static void SaveHoles()
    {
        Hole[] holes = FindObjectsByType<Hole>(FindObjectsSortMode.None);
        Array.Sort(holes, (a, b) => string.CompareOrdinal(a.gameObject.name, b.gameObject.name));
        PlayerPrefs.SetInt(keyHoleCount, holes.Length);
        for (int i = 0; i < holes.Length; i++)
        {
            PlayerPrefs.SetFloat(HolePriceKey(i), holes[i].price);
            if (holes[i].hole != null)
            {
                Vector3 scale = holes[i].hole.transform.localScale;
                PlayerPrefs.SetFloat(HoleScaleXKey(i), scale.x);
                PlayerPrefs.SetFloat(HoleScaleYKey(i), scale.y);
                PlayerPrefs.SetFloat(HoleScaleZKey(i), scale.z);
            }
        }
    }

    private static void SaveMp2()
    {
        MP2Manager[] managers = FindObjectsByType<MP2Manager>(FindObjectsSortMode.None);
        Array.Sort(managers, (a, b) => string.CompareOrdinal(a.gameObject.name, b.gameObject.name));
        PlayerPrefs.SetInt(keyMP2ManagerCount, managers.Length);
        for (int i = 0; i < managers.Length; i++)
        {
            PlayerPrefs.SetFloat(Mp2BaseKey(i), managers[i].baseGrowthRate);
            PlayerPrefs.SetFloat(Mp2BoostKey(i), managers[i].boostedGrowthRate);
            PlayerPrefs.SetFloat(Mp2MedicineKey(i), managers[i].medicineAmount);
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private static string GemActiveKey(int i) => "GemVendorActive_" + i;
    private static string GemPriceKey(int i) => "GemVendorPrice_" + i;
    private static string DoorVendorActiveKey(int i) => "DoorVendorActive_" + i;
    private static string DoorPriceKey(int i) => "DoorVendorPrice_" + i;
    private static string Door1Key(int i) => "Door1Active_" + i;
    private static string Door2Key(int i) => "Door2Active_" + i;
    private static string HolePriceKey(int i) => "HolePrice_" + i;
    private static string HoleScaleXKey(int i) => "HoleScaleX_" + i;
    private static string HoleScaleYKey(int i) => "HoleScaleY_" + i;
    private static string HoleScaleZKey(int i) => "HoleScaleZ_" + i;
    private static string Mp2BaseKey(int i) => "MP2BaseGrowthRate_" + i;
    private static string Mp2BoostKey(int i) => "MP2BoostedGrowthRate_" + i;
    private static string Mp2MedicineKey(int i) => "MP2MedicineAmount_" + i;

    private static void SetBoolPref(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    private static bool GetBoolPref(string key, bool defaultValue)
    {
        if (!PlayerPrefs.HasKey(key))
            return defaultValue;
        return PlayerPrefs.GetInt(key, 0) != 0;
    }
}
