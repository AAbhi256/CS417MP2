using UnityEngine;
using Alteruna;

public class AutoJoinRoom : MonoBehaviour
{
    void Start()
    {
        var multiplayer = GetComponent<Multiplayer>();
        if (multiplayer != null)
            multiplayer.OnConnected.AddListener((mp, ep) => mp.JoinOnDemandRoom());
    }
}
