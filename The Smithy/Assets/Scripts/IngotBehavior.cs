using UnityEngine;

public class IngotBehavior : MonoBehaviour
{
    public IngotType ingotType;
    public bool isHeated;
    public Material heatedMaterial;
    public HotSwordBehavior hotSword;


    void Start()
    {
        isHeated = false;
    }


    void Update()
    {
        
    }

    public void BecomeMolten()
    {
        isHeated = true;
        GetComponent<MeshRenderer>().material = heatedMaterial;
    }

    public void BecomeHotSword()
    {
        Instantiate(hotSword, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}

public enum IngotType
{
    None,
    Bronze,
    Copper,
    Gold,
    Platinum,
    Silver,
    Steel
}