using DG.Tweening;
using UnityEngine;

public class WaterCheck : Interactable
{
    [SerializeField] private bool isGrabbed = false;
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 5f;
    [SerializeField] private float returnSpeed = 3f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        startPosition.y = maxY;
    }

    override public void Interact()
    {
        isGrabbed = true;
    }

    private bool CheckWaterTouch()
    {
        float waterLevel = MetaGame.Instance.waterEngine / MetaGame.Instance.waterMax;
        float waterSurfaceY;

        if (waterLevel <= 0.2f)
        {
            waterSurfaceY = 0;
        }
        else
        {
            waterSurfaceY = Mathf.Lerp(1.426f, 1.477014f, waterLevel);
        }
        
        float fingerY = transform.position.y;
        
        return fingerY <= waterSurfaceY;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            G.Ui.HideAllButtons();
            if (MetaGame.Instance.isControllShown == false)
            {
                G.Ui.tutorWater.gameObject.SetActive(false);
                G.Ui.tutorControll.gameObject.SetActive(true);
            }

            isGrabbed = false;
            transform.DOLocalMoveX(G.armWaterCheckShow, 0.35f);
        }
        if (Input.GetMouseButton(0) && isGrabbed)
        {
            float mouseDeltaY = Input.GetAxis("Mouse Y") * moveSpeed;
                
            Vector3 newPos = transform.position;
            newPos.y += mouseDeltaY;
            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

            transform.position = newPos;
            
            if (CheckWaterTouch())
            {
                G.Ui.damageScreen.DOFade(0.7f, 0.2f);
                MetaGame.Instance.armHealth -= MetaGame.Instance.armHealthWaterDamage * Time.deltaTime;
            }
            else
            {
                G.Ui.damageScreen.DOFade(0.0f, 0.2f);
            }

        }
        else
        {
            if (isGrabbed)
            {
                isGrabbed = false;
                G.Ui.waterButton.gameObject.SetActive(true);
                G.Ui.damageScreen.DOFade(0.0f, 0.2f);
            }
            
            if (!isGrabbed)
            {
                Vector3 targetPos = transform.position;
                targetPos.y = maxY;
                transform.position = Vector3.Lerp(transform.position, targetPos, returnSpeed * Time.deltaTime);
            }
        }
    }
}
