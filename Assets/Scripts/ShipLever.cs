using UnityEngine;
using DG.Tweening;

public class ShipLever : Interactable
{
    [SerializeField] private bool isGrabbed = false;
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] public float minY = 0f;
    [SerializeField] private float maxY = 5f;
    [SerializeField] public AudioSource audioSource;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
        startPosition.y = minY;
    }

    override public void Interact()
    {
        isGrabbed = true;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isGrabbed = false;
        }
        if (Input.GetMouseButton(0) && isGrabbed)
        {
            float mouseDeltaY = Input.GetAxis("Mouse Y") * moveSpeed;
                
            Vector3 newPos = transform.localPosition;
            newPos.y += mouseDeltaY;
            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

            transform.localPosition = newPos;
            


            MetaGame.Instance.shipLeverLevel = (newPos.y - minY) / (maxY - minY);
            if (MetaGame.Instance.fuelEngine > 0 & MetaGame.Instance.waterEngine > 0 & MetaGame.Instance.oilEngine > 0)
            {
                audioSource.DOPitch(Mathf.Lerp(0.02f, 1.4f, MetaGame.Instance.shipLeverLevel), 0.1f);
                //audioSource.pitch = Mathf.Lerp(0.02f, 1.4f, MetaGame.Instance.shipLeverLevel);
            }
            else
            {
                //audioSource.DOPitch(Mathf.Lerp(0.02f, 1.4f, MetaGame.Instance.shipLeverLevel), 0.1f);
            }
        }
        else
        {
            if (isGrabbed)
            {

                isGrabbed = false;
            }
        }
        if (MetaGame.Instance.fuelEngine > 0 & MetaGame.Instance.waterEngine > 0 & MetaGame.Instance.oilEngine > 0)
        {
            audioSource.DOPitch(Mathf.Lerp(0.02f, 1.4f, MetaGame.Instance.shipLeverLevel), 0.1f);
            //audioSource.pitch = Mathf.Lerp(0.02f, 1.4f, MetaGame.Instance.shipLeverLevel);
        }
    }
}

