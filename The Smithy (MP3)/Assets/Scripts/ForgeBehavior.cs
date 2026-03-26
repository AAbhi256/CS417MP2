using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ForgeBehavior : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor sock;


    void Start()
    {
        sock = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
        sock.selectEntered.AddListener(OnInSocket);
        sock.selectExited.AddListener(OnOutSocket);
        
    }


    void Update()
    {
        
    }

    void OnInSocket(SelectEnterEventArgs args)
    {
        GameObject item = args.interactableObject.transform.gameObject;
        IngotBehavior ingot = item.GetComponent<IngotBehavior>();
        if (ingot != null)
        {
            ingot.BecomeMolten();
        }
    }
    
    void OnOutSocket(SelectExitEventArgs args)
    {
        
    }


}
