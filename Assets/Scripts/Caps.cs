using DG.Tweening;
using UnityEngine;

public class Caps : Interactable
{
    [SerializeField] private Door cap;
    [SerializeField] private Collider selfCollider;
    [SerializeField] private Transform posOpen;
    [SerializeField] private Transform posClose;
    [SerializeField] private Collider nextActionCollider;
    [SerializeField] private GameObject fuelGraph;
    [SerializeField] private Vector3 fuelGraphScaler;

    [SerializeField] private GameObject armWater;
    [SerializeField] private float armWaterHide;
    [SerializeField] private float armWaterShow;

    [SerializeField] private bool isFuel;
    [SerializeField] private bool isWater;

    void Start()
    {
        if (armWater != null)
        {
            G.armWaterCheckShow = armWaterShow;
            G.armWaterCheckHide = armWaterHide;
        }
    }
    override public void Interact()
    {
        if (cap.isOpen == true)
        {
            cap.CloseDoor();
            if (fuelGraph != null)
            {
                fuelGraph.transform.DOScale(Vector3.zero, 0.3f);
            }
            if (armWater != null)
            {
                armWater.transform.DOLocalMoveX(G.armWaterCheckShow, 0.35f);
            }
            if (isFuel == true)
            {
                MetaGame.Instance.fuelConsumption -= MetaGame.Instance.fuelConsumptionOpenCup;
            }
            if (isWater == true)
            {
                MetaGame.Instance.fuelConsumption -= MetaGame.Instance.waterConsumptionOpenCup;
            }
            selfCollider.transform.position = posOpen.position;
            nextActionCollider.enabled = false;
        }
        else if (cap.isOpen == false)
        {
            cap.OpenDoor();
            if (fuelGraph != null)
            {
                fuelGraph.transform.DOScale(fuelGraphScaler, 0.3f);
            }
            if (armWater != null)
            {
                armWater.transform.DOLocalMoveX(armWaterHide, 0.35f);
            }
            if (isFuel == true)
            {
                MetaGame.Instance.fuelConsumption += MetaGame.Instance.fuelConsumptionOpenCup;
            }
            if (isWater == true)
            {
                MetaGame.Instance.fuelConsumption += MetaGame.Instance.waterConsumptionOpenCup;
            }
            selfCollider.transform.position = posClose.position;
            nextActionCollider.enabled = true;
        }
    }
}
