using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float speed = 90f;
    [SerializeField] private Vector3 axis = Vector3.forward;
    
    private Quaternion startRotation;
    private Quaternion targetRotation;
    public bool isOpen = false;
    private bool isMoving = false;
    
    void Start()
    {
        startRotation = transform.localRotation;
        targetRotation = startRotation;
    }
    
    void Update()
    {
        if (!isMoving) return;
        
        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation,
            targetRotation,
            speed * Time.deltaTime
        );
        
        if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.5f)
        {
            transform.localRotation = targetRotation;
            isMoving = false;
        }
    }
    
    public void ToggleDoor()
    {
        if (isOpen)
            CloseDoor();
        else
            OpenDoor();
    }
    
    public void OpenDoor()
    {
        targetRotation = startRotation * Quaternion.Euler(axis * openAngle);
        isOpen = true;
        isMoving = true;
    }
    
    public void CloseDoor()
    {
        targetRotation = startRotation;
        isOpen = false;
        isMoving = true;
    }
    
}