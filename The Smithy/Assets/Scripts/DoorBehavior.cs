using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public DoorBehavior linkedDoor;
    public PlayerBehavior player;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void WarpPlayer()
    {
        player.transform.position = linkedDoor.transform.position;
    }
}
