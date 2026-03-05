using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public GameObject linkedDoor;
    public PlayerBehavior player;
    public float dist = 1;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void WarpPlayer()
    {
        player.transform.position = linkedDoor.GetComponent<DoorBehavior>().transform.position + new Vector3(dist,0,0);
    }
}
