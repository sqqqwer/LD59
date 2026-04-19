using DG.Tweening;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField] private ShipLever lever;
    void Update()
    {
        

        if (MetaGame.Instance.fuelEngine <= 0 || MetaGame.Instance.waterEngine <= 0 || MetaGame.Instance.oilEngine <= 0)
        {
            if (lever.transform.localPosition.y != lever.minY)
            {
                lever.audioSource.pitch = -1.8f;
                lever.transform.DOLocalMoveY(lever.minY, 0.2f);
                MetaGame.Instance.shipLeverLevel = 0f;
            }
        }
        if (MetaGame.Instance.shipLeverLevel > 0)
        {
            float currentAcsel = MetaGame.Instance.shipSpeedAcsel * MetaGame.Instance.shipLeverLevel;
            MetaGame.Instance.shipSpeed += currentAcsel * Time.deltaTime;
        }
        else
        {
            MetaGame.Instance.shipSpeed -= MetaGame.Instance.shipSpeedDecel * Time.deltaTime;
        }

        MetaGame.Instance.shipSpeed = Mathf.Clamp(MetaGame.Instance.shipSpeed, 0f, MetaGame.Instance.shipMaxSpeed);

        G.Ui.textSpeed.text = $"{MetaGame.Instance.shipSpeed:F0} km/h";

        MetaGame.Instance.kmCompleted += (MetaGame.Instance.shipSpeed / 10) * Time.deltaTime;
        G.Ui.textKmCompleted.text = $"{MetaGame.Instance.kmCompleted:F1} km";
    }
}
