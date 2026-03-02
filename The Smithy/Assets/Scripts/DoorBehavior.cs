using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public DoorBehavior linkedDoor;
    public PlayerBehavior player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WarpPlayer()
    {
        player.transform.position = linkedDoor.transform.position;
    }
}
