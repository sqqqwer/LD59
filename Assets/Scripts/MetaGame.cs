using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaGame : MonoBehaviour
{
    public static MetaGame Instance;

    public float fuelEngine = 70;
    public float fuelMax = 100;
    public float fuelCan = 1;
    public float fuelCanCapacity = 20;

    public float waterEngine = 60;
    public float waterMax = 100;
    public float waterCan = 1;
    public float waterCanCapacity = 20;

    public float oilEngine = 60;
    public float oilMax = 100;
    public float oilCan = 1;
    public float oilCanCapacity = 20;

    public float luckLevel = 30;
    public float drunkLevel = 20;
    public float drunkLevelSpeed = 2;
    public float drunkRestorUndrunk = 4;
    public float drunkLevelMax = 100;
    public float armHealth = 80;
    public float armHealthMax = 100;
    public float armHealthWaterDamage = 5f;
    public float armHealthOilDamage = 10f;
    public float armHealthRestor = 2;

    public float fuelConsumption = 5f;
    public float fuelConsumptionOpenCup = 2f;
    public float waterConsumption = 5f;
    public float waterConsumptionOpenCup = 5f;
    public float oilConsumption = 2f;


    public float engineHealth = 60;
    public float engineHealthConsumptionPer = 2;


    public float shipSpeed = 0f;
    public float shipMaxSpeed = 25f;
    public float shipLeverLevel = 0f;// 0-1
    public float shipSpeedAcsel = 2f;
    public float shipSpeedDecel = 1f; 

    public float speedOfGame = 0.5f;// 0.5-1

    public float kmCompleted = 0f;
    public float kmCompletedTOTAL = 0f;

    public float takeLookFaster = 1f;


    public bool isControllShown = false;






    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void StartGame()
    {
        Debug.Log("Game Start");
    }
    
    public void StartNextRun()
    {
        StartCoroutine(StartNextRunCoroutine());
    }
    private IEnumerator StartNextRunCoroutine()
    {
        G.Ui.blackScreen.DOFade(1f, 0.2f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    [SerializeField] public int loseNumber = 1;
    public void GameLoseDrunk()
    {
        loseNumber = 1;
        StartCoroutine(GameLoseCoroutine());
    }
    public void GameLoseArmLoseShock()
    {
        loseNumber = 2;
        StartCoroutine(GameLoseCoroutine());
    }
    public void GameLoseEngineDead()
    {
        loseNumber = 3;
        StartCoroutine(GameLoseCoroutine());
    }
    private IEnumerator GameLoseCoroutine()
    {
        isControllShown = true;
        kmCompletedTOTAL += kmCompleted;
        Debug.Log("ПРИБАВЛЯЮ");
        yield return G.Ui.blackScreen.DOFade(1f, 0.2f);
        ResetRunStats();
        SceneManager.LoadScene(1);
    }

    public void ResetRunStats()
    {
        kmCompleted = 0;
        fuelEngine = 70;
        fuelCan = 1;
        waterEngine = 60;
        waterCan = 1;
        oilEngine = 60;
        oilCan = 1;

        drunkLevel = 20;
        armHealth = 80;
        fuelConsumption = 5f;
        waterConsumption = 5f;
        oilConsumption = 4f;

        shipSpeed = 0f;
        shipLeverLevel = 0f;
    }
}
