using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoolingStationBehavior : MonoBehaviour
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
        HotSwordBehavior hotSword = item.GetComponent<HotSwordBehavior>();
        if (hotSword != null)
        {
            hotSword.BecomeFinalSword();
        }
    }
    
    void OnOutSocket(SelectExitEventArgs args)
    {
        
    }


}
