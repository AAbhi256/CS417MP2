using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FOVRestriction : MonoBehaviour
{
    [Header("FOV Settings")]
    public float movingFOV = 80F;
    public float fovTransitionSpeed = 5f;

    private Camera cam;
    private Vector3 lastPosition;
    private float defaultFOV;

    void Start()
    {
        cam = GetComponent<Camera>();
        defaultFOV = cam.fieldOfView;
        lastPosition = cam.transform.position;
    }

    void Update()
    {
        // cam.fieldOfView = 40;
        // Vector3 currentPosition = cam.transform.position;
        // Vector3 delta = currentPosition - lastPosition;
        // delta.y = 0f;

        // bool isMoving = delta.magnitude / Time.deltaTime > 0.1f;
        // lastPosition = currentPosition;

        // if (isMoving)
        // {
        //     cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, movingFOV, Time.deltaTime * fovTransitionSpeed);
        //     // cam.fieldOfView = movingFOV; // snap instantly to narrow FOV
        // }
        // else
        // {
        //     cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultFOV, Time.deltaTime * fovTransitionSpeed); // lerp back smoothly
        // }

        // Debug.Log($"Moving: {isMoving} | Actual FOV: {cam.fieldOfView}");
    }
}