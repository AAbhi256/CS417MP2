using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Attach to each trophy sword in the scene. Hides <see cref="rootToShow"/> until
/// <see cref="PlayerBehavior.soulAmount"/> reaches <see cref="soulThreshold"/> (once per play session).
/// If <see cref="rootToShow"/> is unassigned, uses this GameObject.
/// </summary>
public class SoulSwordTrophy : MonoBehaviour
{
    [Tooltip("Minimum souls required (inclusive).")]
    [SerializeField] private int soulThreshold = 1;

    [SerializeField] private string tierId = "";

    [Tooltip("Optional: assign a parent if the script lives on a child. If empty, this GameObject is shown/hidden.")]
    [SerializeField] private GameObject rootToShow;

    [TextArea]
    [SerializeField] private string message = "Achievement unlocked!";

    [SerializeField] private UnityEvent onUnlocked;

    private GameObject Root => rootToShow != null ? rootToShow : gameObject;

    private bool unlockedThisSession;

    private void Awake()
    {
        string label = string.IsNullOrEmpty(tierId) ? Root.name : tierId;
        Debug.Log($"[SoulSwordTrophy] Hidden '{label}' until souls >= {soulThreshold} (on '{gameObject.name}').", this);
        Root.SetActive(false);
    }

    private void Update()
    {
        if (unlockedThisSession)
            return;

        int souls = Mathf.FloorToInt(PlayerBehavior.soulAmount);
        if (souls < soulThreshold)
            return;

        unlockedThisSession = true;
        Root.SetActive(true);
        Debug.Log(
            $"[SoulSwordTrophy] Unlocked '{(string.IsNullOrEmpty(tierId) ? Root.name : tierId)}': souls={souls} >= threshold={soulThreshold}. Showing '{Root.name}'.",
            this);
        if (!string.IsNullOrEmpty(message))
            Debug.Log($"[SoulSwordTrophy] {message}", this);
        onUnlocked?.Invoke();
    }
}
