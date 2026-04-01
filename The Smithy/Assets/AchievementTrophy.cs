using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Unlocks trophies when PlayerBehavior.soulAmount reaches each threshold (once per play session).
/// Assign dropTarget to your XR Origin / camera rig so drops appear above the player.
/// </summary>
public class SoulTrophyAchievements : MonoBehaviour
{
    [Serializable]
    public class SoulTrophyTier
    {
        [Tooltip("Minimum souls required (inclusive). Souls are +1 per SoulVendor purchase.")]
        public int soulThreshold = 1;

        [Tooltip("Unique id for this tier (debug / future use).")]
        public string tierId = "soul_01";

        [Tooltip("Prefab spawned high above the player. Add Rigidbody if it should fall.")]
        public GameObject dropPrefab;

        [TextArea]
        public string message = "Achievement unlocked!";

        public UnityEvent onUnlocked;
    }

    [SerializeField] private SoulTrophyTier[] tiers = new SoulTrophyTier[]
    {
        new SoulTrophyTier { soulThreshold = 1,  tierId = "soul_trophy_1", message = "First soul — the forge remembers." },
        new SoulTrophyTier { soulThreshold = 3,  tierId = "soul_trophy_3", message = "Three souls — the sky answers." },
        new SoulTrophyTier { soulThreshold = 5, tierId = "soul_trophy_5", message = "Five souls — blade from the heavens." },
    };

    [SerializeField] private Transform dropTarget;

    [SerializeField] private float dropHeight = 8f;

    [SerializeField] private float randomRadius = 0.75f;

    private bool[] tierClaimedThisSession;

    private void Reset()
    {
        if (dropTarget == null)
            dropTarget = transform;
    }

    private void Awake()
    {
        if (dropTarget == null)
            dropTarget = transform;
        tierClaimedThisSession = new bool[tiers.Length];
    }

    private void Update()
    {
        int souls = Mathf.FloorToInt(PlayerBehavior.soulAmount);

        for (int i = 0; i < tiers.Length; i++)
        {
            SoulTrophyTier tier = tiers[i];
            if (tier == null || tier.dropPrefab == null)
                continue;

            if (souls < tier.soulThreshold)
                continue;

            if (tierClaimedThisSession[i])
                continue;

            tierClaimedThisSession[i] = true;

            UnlockTier(tier);
        }
    }

    private void UnlockTier(SoulTrophyTier tier)
    {
        Vector3 origin = dropTarget.position;
        Vector2 r = UnityEngine.Random.insideUnitCircle * randomRadius;
        Vector3 spawnPos = origin + new Vector3(r.x, dropHeight, r.y);
        Quaternion rot = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

        Instantiate(tier.dropPrefab, spawnPos, rot);
        Debug.Log($"[Trophy] {tier.message}");
        tier.onUnlocked?.Invoke();
    }
}