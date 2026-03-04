using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public GameObject linkedDoor;
    public PlayerBehavior player;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void WarpPlayer()
    {
        player.transform.position = linkedDoor.GetComponent<DoorBehavior>().transform.position + new Vector3(1,0,0);
    }
}
