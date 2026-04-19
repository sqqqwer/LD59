using DG.Tweening;
using UnityEngine;

public class GoToMiniGameInteract : Interactable
{
    [SerializeField] private int cameraPosId;

    [SerializeField] private GameObject hiLight;
    [SerializeField] private GameObject armWaterCheck;
    [SerializeField] private Door armWaterCheckDoor;

    [SerializeField] private GameObject armOilCheck;
    [SerializeField] private Vector3 armOilHidePos;
    [SerializeField] private Vector3 armOilShowPos;
    [SerializeField] private GameObject textTutor;

    void Start()
    {
        if (armOilCheck != null)
        {
            G.armOilCheckHide = armOilHidePos;
            G.armOilCheckShow = armOilShowPos;
        }
        HiLightHide();
    }
    override public void Interact()
    {
        G.lvlOfInteraction = 1;

        G.miniGameManager.ActivateGroupMiniGameInteractable(cameraPosId);
        G.miniGameManager.DeactivateMiniGamesChoiserColliders();

        G.cameraMoveToPoint.moveTime = 0.22f;

        if (armWaterCheck != null)
        {
            if (armWaterCheckDoor.isOpen)
            {
                armWaterCheck.transform.DOLocalMoveX(G.armWaterCheckHide, 0.35f);
            }
        }
        if (armOilCheck != null)
        {
            armOilCheck.transform.DOLocalMove(G.armOilCheckShow, 0.35f);
        }
        
        G.cameraMoveToPoint.GoToPoint(cameraPosId);
        
        if (MetaGame.Instance.isControllShown == false)
        {
            textTutor.SetActive(true);
            G.Ui.tutorControll.gameObject.SetActive(false);
        }
    }
    public void HiLightShow()
    {
        hiLight.SetActive(true);
    }
    public void HiLightHide()
    {
        hiLight.SetActive(false);
    }
}
