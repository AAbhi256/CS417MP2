using System.Collections;
using UnityEngine;

public class JuicyTrophy : MonoBehaviour
{
    private const float SecondChildVisibleSeconds = 5f;

    [SerializeField] private int soulsNeeded = 1;

    private bool _unlocked;
    public ParticleSystem ps;

    private void Start()
    {

    }

    private void Update()
    {
        if (_unlocked)
            return;

        if (Mathf.FloorToInt(PlayerBehavior.soulAmount) < soulsNeeded)
            return;

        if (transform.childCount < 1)
        {
            Debug.LogWarning($"[TrophyRevealOnSouls] '{name}': Need at least 1 child.", this);
            return;
        }

        _unlocked = true;
        ParticleSystem.EmissionModule emission = ps.emission;
        emission.rateOverTime = 3;


        Debug.Log($"[TrophyRevealOnSouls] '{name}': Child 0 '{transform.GetChild(0).name}' shown (permanent).", this);

        if (transform.childCount >= 2)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            Debug.Log($"[TrophyRevealOnSouls] '{name}': Child 1 '{transform.GetChild(1).name}' shown for {SecondChildVisibleSeconds}s.", this);
            StartCoroutine(HideSecondChildAfterDelay());
        }
    }

    private IEnumerator HideSecondChildAfterDelay()
    {
        yield return new WaitForSeconds(SecondChildVisibleSeconds);
        if (transform.childCount > 1)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            Debug.Log($"[TrophyRevealOnSouls] '{name}': Child 1 hidden after {SecondChildVisibleSeconds}s.", this);
        }
    }
}