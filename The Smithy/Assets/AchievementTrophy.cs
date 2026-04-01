using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Multi-trophy manager: one component, many tiers, each with its own scene <see cref="SoulTrophyTier.sceneTrophy"/>.
/// For a single sword, prefer <see cref="SoulSwordTrophy"/> on that object instead.
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

        [Tooltip("Pre-placed GameObject in the scene (sword + trophy hierarchy). Hidden until souls reach the threshold.")]
        public GameObject sceneTrophy;

        [TextArea]
        public string message = "Achievement unlocked!";

        public UnityEvent onUnlocked;
    }

    [SerializeField] private SoulTrophyTier[] tiers = new SoulTrophyTier[]
    {
        new SoulTrophyTier { soulThreshold = 1, tierId = "soul_trophy_1", message = "First soul — the forge remembers." },
        new SoulTrophyTier { soulThreshold = 3, tierId = "soul_trophy_3", message = "Three souls — the sky answers." },
        new SoulTrophyTier { soulThreshold = 5, tierId = "soul_trophy_5", message = "Five souls — blade from the heavens." },
    };

    private bool[] tierUnlockedThisSession;

    private void Awake()
    {
        tierUnlockedThisSession = new bool[tiers.Length];
        HideAllSceneTrophies();
    }

    /// <summary>
    /// Ensures trophies start invisible even if left active in the scene.
    /// </summary>
    private void HideAllSceneTrophies()
    {
        for (int i = 0; i < tiers.Length; i++)
        {
            if (tiers[i] != null && tiers[i].sceneTrophy != null)
                tiers[i].sceneTrophy.SetActive(false);
        }
    }

    private void Update()
    {
        int souls = Mathf.FloorToInt(PlayerBehavior.soulAmount);

        for (int i = 0; i < tiers.Length; i++)
        {
            SoulTrophyTier tier = tiers[i];
            if (tier == null || tier.sceneTrophy == null)
                continue;

            if (souls < tier.soulThreshold)
                continue;

            if (tierUnlockedThisSession[i])
                continue;

            tierUnlockedThisSession[i] = true;
            UnlockTier(tier);
        }
    }

    private void UnlockTier(SoulTrophyTier tier)
    {
        tier.sceneTrophy.SetActive(true);
        Debug.Log($"[Trophy] {tier.message}");
        tier.onUnlocked?.Invoke();
    }
}
