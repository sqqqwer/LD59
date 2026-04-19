using UnityEngine;
using DG.Tweening;

public class FuelCheck : Interactable
{
    [SerializeField] private bool isSniffing = false;
    [SerializeField] private GameObject eye;
    [SerializeField] private Vector3 eyeApearScale;

    [SerializeField] private GameObject eyePupil;
    [SerializeField] private Vector3 eyePupilMinScale;
    [SerializeField] private Vector3 eyePupilMaxScale;
    [SerializeField] private float sniffToCheck = 5;
    private float sniffed = 0;
    override public void Interact()
    {
        sniffed = 0;
        eye.transform.DOScale(eyeApearScale, 0.2f);
        eyePupil.transform.DOScale(eyePupilMinScale, 0.1f);

        isSniffing = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            G.Ui.HideAllButtons();
            if (MetaGame.Instance.isControllShown == false)
            {
                G.Ui.tutorFuel.gameObject.SetActive(false);
                G.Ui.tutorControll.gameObject.SetActive(true);
            }

            if (isSniffing == true)
            {
                eye.transform.DOScale(Vector3.zero, 0.2f);
            }
            isSniffing = false;
        }


        if (Input.GetMouseButton(0) & isSniffing == true)
        {
            G.Ui.tutorFuel.gameObject.SetActive(false);
            if (MetaGame.Instance.fuelEngine / MetaGame.Instance.fuelMax >= 0.20f)
            {
                MetaGame.Instance.drunkLevel += MetaGame.Instance.drunkLevelSpeed * MetaGame.Instance.fuelEngine / MetaGame.Instance.fuelMax * Time.deltaTime;
                sniffed += MetaGame.Instance.drunkLevelSpeed * MetaGame.Instance.fuelEngine / MetaGame.Instance.fuelMax * Time.deltaTime;
                
                float t = sniffed / sniffToCheck;
                eyePupil.transform.localScale = Vector3.Lerp(eyePupilMinScale, eyePupilMaxScale, t);
            }
            else
            {
                eyePupil.transform.DOScale(eyePupilMinScale, 0.1f);
            }
        }
        else
        {
            if (isSniffing == true)
            {
                G.Ui.fuelButton.gameObject.SetActive(true);
                eye.transform.DOScale(Vector3.zero, 0.2f);
            }
            isSniffing = false;
        }
    }
}
