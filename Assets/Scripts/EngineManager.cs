using UnityEngine;

public class EngineManager : MonoBehaviour
{
    [SerializeField] private bool isLoosed = false;
    void Update()
    {
        if (isLoosed == false)
        {
            if (MetaGame.Instance.drunkLevel >= MetaGame.Instance.drunkLevelMax)
            {
                MetaGame.Instance.GameLoseDrunk();
                isLoosed = true;
            }
            if (MetaGame.Instance.armHealth <= 0)
            {
                MetaGame.Instance.GameLoseArmLoseShock();
                isLoosed = true;
            }
            if (MetaGame.Instance.engineHealth <= 0)
            {
                MetaGame.Instance.GameLoseEngineDead();
                isLoosed = true;
            }
        }

        if (MetaGame.Instance.fuelEngine > 0)
        {
            MetaGame.Instance.fuelEngine -= 
                MetaGame.Instance.fuelConsumption * 
                MetaGame.Instance.speedOfGame *
                MetaGame.Instance.shipLeverLevel *
                Time.deltaTime;
        }
        else
        {
            MetaGame.Instance.engineHealth -=
                MetaGame.Instance.engineHealthConsumptionPer * 
                Time.deltaTime;
        }

        if (MetaGame.Instance.waterEngine > 0)
        {
            MetaGame.Instance.waterEngine -= 
                MetaGame.Instance.waterConsumption * 
                MetaGame.Instance.speedOfGame *
                MetaGame.Instance.shipLeverLevel *
                Time.deltaTime;
        }
        else
        {
            MetaGame.Instance.engineHealth -=
                MetaGame.Instance.engineHealthConsumptionPer * 
                Time.deltaTime;
        }

        if (MetaGame.Instance.oilEngine > 0)
        {
            MetaGame.Instance.oilEngine -= 
                MetaGame.Instance.oilConsumption * 
                MetaGame.Instance.speedOfGame *
                MetaGame.Instance.shipLeverLevel *
                Time.deltaTime;
        }
        else
        {
            MetaGame.Instance.engineHealth -=
                MetaGame.Instance.engineHealthConsumptionPer * 
                Time.deltaTime;
        }
    }
}
