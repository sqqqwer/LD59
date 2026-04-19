using DG.Tweening;
using UnityEngine;

public class OilCheck : Interactable
{
    [SerializeField] private bool isGrabbed = false;
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 5f;
    [SerializeField] private float returnSpeed = 3f;

    [SerializeField] private float positionYOnOilCheck = 3f;
    [SerializeField] private MeshRenderer armVisual1;
    [SerializeField] private MeshRenderer armVisual2;

    private Vector3 startPosition;

    void Start()
    {
        armVisual1.enabled = true;
        armVisual2.enabled = false;

        startPosition = transform.localPosition;
        startPosition.y = maxY;
    }

    override public void Interact()
    {
        isGrabbed = true;
    }

    private bool CheckEngineTouch()
    {
        float fingerY = transform.localPosition.y;
        
        return fingerY <= positionYOnOilCheck;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            G.Ui.HideAllButtons();
            if (MetaGame.Instance.isControllShown == false)
            {
                G.Ui.tutorOil.gameObject.SetActive(false);
                G.Ui.tutorControll.gameObject.SetActive(true);
            }

            isGrabbed = false;
            transform.parent.DOLocalMove(G.armOilCheckHide, 0.35f);

            armVisual1.enabled = true;
            armVisual2.enabled = false;
        }
        if (Input.GetMouseButton(0) && isGrabbed)
        {
            float mouseDeltaY = Input.GetAxis("Mouse X") * moveSpeed;
                
            Vector3 newPos = transform.localPosition;
            newPos.y += mouseDeltaY;
            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

            transform.localPosition = newPos;
            
            if (CheckEngineTouch())
            {
                armVisual1.enabled = false;
                armVisual2.enabled = true;
                if (MetaGame.Instance.oilEngine / MetaGame.Instance.oilMax <= 0.35)
                {
                    G.Ui.damageScreen.DOFade(0.7f, 0.2f);
                    MetaGame.Instance.armHealth -= MetaGame.Instance.armHealthOilDamage * Time.deltaTime;
                }
                else
                {
                    G.Ui.damageScreen.DOFade(0.05f, 0.2f);
                }
            }
            else
            {
                armVisual1.enabled = true;
                armVisual2.enabled = false;
            }
        }
        else
        {
            if (isGrabbed)
            {
                G.Ui.oilButton.gameObject.SetActive(true);

                armVisual1.enabled = true;
                armVisual2.enabled = false;

                isGrabbed = false;
                G.Ui.damageScreen.DOFade(0f, 0.2f);
            }
            
            if (!isGrabbed)
            {
                Vector3 targetPos = transform.localPosition;
                targetPos.y = maxY;
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, returnSpeed * Time.deltaTime);
            }
        }
    }
}
