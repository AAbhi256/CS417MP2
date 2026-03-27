using UnityEngine;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(Camera))]
public class FOVRestriction : MonoBehaviour
{
    [Header("FOV Settings")]
    public float movingFOV = 80f;
    public float fovTransitionSpeed = 5f;

    private Camera cam;
    private XROrigin xrOrigin;
    private Vector3 lastPosition;
    private float defaultFOV;

    void Start()
    {
        cam = GetComponent<Camera>();
        defaultFOV = cam.fieldOfView; // captures whatever XR sets by default
        xrOrigin = FindObjectOfType<XROrigin>();
        lastPosition = xrOrigin.transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = xrOrigin.transform.position;
        Vector3 delta = currentPosition - lastPosition;
        delta.y = 0f;

        bool isMoving = delta.magnitude / Time.deltaTime > 0.1f;
        lastPosition = currentPosition;

        float targetFOV = isMoving ? movingFOV : defaultFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * fovTransitionSpeed);
    }
}