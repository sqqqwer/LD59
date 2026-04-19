using UnityEngine;


public class CameraMoveToPoint : MonoBehaviour
{

    [SerializeField] private Transform[] points;
    [SerializeField] public float moveTime = 1.5f;
    

    [SerializeField] [Range(0f, 0.5f)] private float edgeThresholdPercent = 0.15f;


    [SerializeField] private float[] fovValues;
    [SerializeField] private float defaultFov = 25.1f;
    

    [SerializeField] private float maxTiltAngle = 3.0f;
    [SerializeField] private float smoothSpeed = 5.0f;

    [SerializeField] private Camera cam;
    [SerializeField] private Door door;
    
    private int currentPoint = 0;
    private Transform targetPoint;
    private float targetFov;
    
    private Vector3 velocity = Vector3.zero;
    private float rotationVelocity = 0f;
    private float fovVelocity = 0f;
    
    private Quaternion baseRotation;
    private float targetPitch;
    private float targetYaw;
    private float currentPitch;
    private float currentYaw;

    void Start()
    {
        baseRotation = transform.localRotation;
        currentPitch = 0f;
        currentYaw = 0f;
        
        if (points.Length > 0)
            GoToPoint(0);
    }

    void Update()
    {

        // if (Input.GetKeyDown(KeyCode.Space) && points.Length > 0)
        // {
        //     currentPoint = (currentPoint + 1) % points.Length;
        //     targetPoint = points[currentPoint];
        // }
        
        if (targetPoint != null)
        {
            transform.position = Vector3.SmoothDamp(
                transform.position, 
                targetPoint.position, 
                ref velocity, 
                moveTime
            );
            
            baseRotation = Quaternion.Slerp(baseRotation, targetPoint.rotation, Time.deltaTime / moveTime);

            if (doorAnim == true)
            {
                if (Quaternion.Angle(baseRotation, targetPoint.rotation) < 0.5f & Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
                {
                    if (G.lvlOfInteraction == -1)
                    {
                        GoToPoint(5);
                    }
                    else if (G.lvlOfInteraction == 0)
                    {
                        GoToPoint(0);
                    }
                    doorAnim = false;
                }
            }
        }
        
        cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, targetFov, ref fovVelocity, moveTime);

        EvaluateMousePosition();
        ApplySmoothRotation();
    }
    [SerializeField] private bool doorAnim = false;
    public void GoToPoint(int idPoint)
    {
        targetPoint = points[idPoint];
        if (idPoint == 4)
        {
            doorAnim = true;
        }
        else if (idPoint == 5)
        {
            G.lvlOfInteraction = -2;
            // doorAnim = true;
        }
        targetFov = GetFovForPoint(idPoint);
    }
    float GetFovForPoint(int index)
    {
        if (fovValues != null && index < fovValues.Length)
            return fovValues[index];
        return defaultFov;
    }
    void EvaluateMousePosition()
    {
        Vector2 normalizedMousePos = new Vector2(
            Input.mousePosition.x / Screen.width,
            Input.mousePosition.y / Screen.height
        );

        float pitchFactor = 0f;
        if (normalizedMousePos.y > 1f - edgeThresholdPercent)
        {
            float t = (normalizedMousePos.y - (1f - edgeThresholdPercent)) / edgeThresholdPercent;
            pitchFactor = -t;
        }
        else if (normalizedMousePos.y < edgeThresholdPercent)
        {
            float t = 1f - (normalizedMousePos.y / edgeThresholdPercent);
            pitchFactor = t;
        }
        targetPitch = pitchFactor * maxTiltAngle;

        float yawFactor = 0f;
        if (normalizedMousePos.x > 1f - edgeThresholdPercent)
        {
            float t = (normalizedMousePos.x - (1f - edgeThresholdPercent)) / edgeThresholdPercent;
            yawFactor = t;
        }
        else if (normalizedMousePos.x < edgeThresholdPercent)
        {
            float t = 1f - (normalizedMousePos.x / edgeThresholdPercent);
            yawFactor = -t;
        }
        targetYaw = yawFactor * maxTiltAngle;

        float combinedMagnitude = Mathf.Sqrt(targetPitch * targetPitch + targetYaw * targetYaw);
        if (combinedMagnitude > maxTiltAngle)
        {
            targetPitch = targetPitch * maxTiltAngle / combinedMagnitude;
            targetYaw = targetYaw * maxTiltAngle / combinedMagnitude;
        }
    }

    void ApplySmoothRotation()
    {
        if (float.IsNaN(currentPitch)) currentPitch = 0f;
        if (float.IsNaN(currentYaw)) currentYaw = 0f;
        if (float.IsNaN(targetPitch)) targetPitch = 0f;
        if (float.IsNaN(targetYaw)) targetYaw = 0f;

        currentPitch = Mathf.Lerp(currentPitch, targetPitch, Time.deltaTime * smoothSpeed);
        currentYaw = Mathf.Lerp(currentYaw, targetYaw, Time.deltaTime * smoothSpeed);

        Quaternion tiltRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, baseRotation * tiltRotation, Time.deltaTime * smoothSpeed);
    }
}