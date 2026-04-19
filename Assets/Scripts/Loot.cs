using DG.Tweening;
using UnityEngine;

public class Loot : Interactable
{
    [SerializeField] public bool isFuel;
    [SerializeField] public bool isWater;
    [SerializeField] public bool isOil;

    [SerializeField] private GameObject visual;
    [SerializeField] private GameObject visualRotato;
    [SerializeField] private float bobSpeed = 1f;        // Скорость покачивания
    [SerializeField] private float bobHeight = 0.2f;     // Высота покачивания
    [SerializeField] private float bobTilt = 15f;        // Угол наклона при покачивании
    
    [SerializeField] private float rotationSpeed = 10f;  // Скорость вращения (градусов в секунду)
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // Ось вращения


    [SerializeField] private float maxGrabDistance = 3f;
    [SerializeField] private float takeSpeed = 50f;
    [SerializeField] private float returnSpeed = 100f;
    [SerializeField] private float progressToTake = 100f;
    [SerializeField] private float currentProgressToTake = 0f;

    [SerializeField] private float moveSpeed = 60f;
    
    private bool isTaking = false;
    private Camera mainCamera;
    private Vector3 startPosition;
    private float timeOffset;
    
    private void Start()
    {
        isTaking = false;
        currentProgressToTake = 0f;
        mainCamera = Camera.main;
        startPosition = visual.transform.localPosition;
        timeOffset = Random.Range(0f, 100f);
    }
    
    override public void Interact()
    {
        isTaking = true;
    }
    
    public void Update()
    {   
        if (transform.position.z > 0)
        {
            Destroy(gameObject);
        }      
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        float bob = Mathf.Sin((Time.time + timeOffset) * bobSpeed) * bobHeight;
        visual.transform.localPosition = startPosition + Vector3.up * bob;
        float tilt = Mathf.Sin((Time.time + timeOffset) * bobSpeed) * bobTilt;
        visualRotato.transform.rotation = Quaternion.Euler(tilt, 0, tilt * 0.5f);
        

        visual.transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);


        if (Input.GetMouseButtonDown(1))
        {
            isTaking = false;
            currentProgressToTake = 0f;
            G.Ui.imageProgressBarTakeLoot.fillAmount = 0;
        }
        
        if (Input.GetMouseButton(0) && isTaking)
        {

            float distance = GetDistanceFromMouseToObject();
            
            if (distance <= maxGrabDistance)
            {
                currentProgressToTake += takeSpeed * MetaGame.Instance.takeLookFaster * Time.deltaTime;
                currentProgressToTake = Mathf.Clamp(currentProgressToTake, 0f, progressToTake);
                
                G.Ui.imageProgressBarTakeLoot.fillAmount = GetProgress();

                if (currentProgressToTake >= progressToTake)
                {
                    OnLootTaken();
                }
            }
            else
            {
                G.Ui.imageProgressBarTakeLoot.fillAmount = 0;
                currentProgressToTake -= returnSpeed * Time.deltaTime;
                currentProgressToTake = Mathf.Clamp(currentProgressToTake, 0f, progressToTake);
                
                if (currentProgressToTake <= 0f)
                {
                    isTaking = false;
                }
            }
        }
        else
        {
            if (isTaking)
            {
                G.Ui.imageProgressBarTakeLoot.fillAmount = 0;
                isTaking = false;
                currentProgressToTake = 0f;
            }
        }
    }
    
    private float GetDistanceFromMouseToObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 closestPoint = ray.origin + ray.direction * Vector3.Dot(transform.position - ray.origin, ray.direction);
        
        return Vector3.Distance(transform.position, closestPoint);
    }
    
    private bool isTaken = false;
    private void OnLootTaken()
    {
        G.Ui.imageProgressBarTakeLoot.fillAmount = 0;
        if (isTaken == false)
        {
            G.Ui.imageProgressBarTakeLoot.fillAmount = 0;
            if (isFuel)
            {
                MetaGame.Instance.fuelCan += 1;
                G.Ui.UpdateResText();
                G.Ui.ShakeFuelImage();
            }
            if (isWater)
            { 
                MetaGame.Instance.waterCan += 1;
                G.Ui.UpdateResText();
                G.Ui.ShakeWaterImage();
            }
            if (isOil)
            { 
                MetaGame.Instance.oilCan += 1;
                G.Ui.UpdateResText();
                G.Ui.ShakeOilImage();
            }
            
            transform.DOScale(Vector3.zero, 0.2f).OnComplete(
                () => Destroy(gameObject)
            );
        }
        isTaken = true;
    }
    
    public float GetProgress()
    {
        G.Ui.imageProgressBarTakeLoot.fillAmount = 0;
        return currentProgressToTake / progressToTake;
    }
}