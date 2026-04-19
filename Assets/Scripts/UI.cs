using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class UI : MonoBehaviour
{
    public CursorManager cursor;
    public TextMeshProUGUI text;

    public TextMeshProUGUI textSpeed;
    public TextMeshProUGUI textKmCompleted;


    public TextMeshProUGUI tutorFuel;
    public TextMeshProUGUI tutorOil;
    public TextMeshProUGUI tutorWater;
    public TextMeshProUGUI tutorControll;


    public TextMeshProUGUI textKmCompletedInShop;

    [SerializeField] private GameObject textPrefab;


    public TextMeshProUGUI textFuel;
    public Image imageFuel;
    private Vector3 imageFuelinitScale;
    public TextMeshProUGUI textOil;
    public Image imageOil;
    private Vector3 imageOilinitScale;

    public TextMeshProUGUI textWater;
    public Image imageWater;
    private Vector3 imageWaterinitScale;


    public Image imageProgressBarTakeLoot;

    public Image blackScreen;
    public Image damageScreen;

    public Button fuelButton;
    public Button oilButton;
    public Button waterButton;



    public TextMeshProUGUI textGameLoseDrunk;
    public TextMeshProUGUI textLoseArmLoseShock;
    public TextMeshProUGUI textLoseEngineDead;
    public GameObject UpgradesShop;


    public bool isGameLoose = false;

    void Start()
    {
        if (MetaGame.Instance.isControllShown == false & tutorControll != null)
        {
            tutorControll.gameObject.SetActive(true);
        }
        if (isGameLoose == false)
        {
            G.Ui.blackScreen.DOFade(0f, 0.2f);
        }
        else
        {
            StartCoroutine(CutsceneGameLoose());
        }
        if (imageOil != null)
        {
            UpdateResText();

            imageFuelinitScale = imageFuel.transform.localScale;
            imageOilinitScale = imageOil.transform.localScale;
            imageWaterinitScale = imageWater.transform.localScale;
        }
    }
    private IEnumerator CutsceneGameLoose()
    {
        UpgradesShop.SetActive(false);
        if (MetaGame.Instance.loseNumber == 1)
        {
            textGameLoseDrunk.DOFade(1f, 0.5f);
        }
        else if (MetaGame.Instance.loseNumber == 2)
        {
            textLoseArmLoseShock.DOFade(1f, 0.5f);
        }
        else if (MetaGame.Instance.loseNumber == 3)
        {
            textLoseEngineDead.DOFade(1f, 0.5f);
        }
        yield return new WaitForSeconds(3f);
        textGameLoseDrunk.DOFade(0f, 0.5f);
        textLoseArmLoseShock.DOFade(0f, 0.5f);
        textLoseEngineDead.DOFade(0f, 0.5f);
        yield return new WaitForSeconds(0.5f);

        UpgradesShop.SetActive(true);
        G.Ui.blackScreen.DOFade(0f, 0.4f);
    }
    public void Update()
    {
        if (textKmCompletedInShop != null)
        {
            textKmCompletedInShop.text = $"TOTAL:{MetaGame.Instance.kmCompletedTOTAL:F1} km";
        }

    }
    public void UpdateResText()
    {
        textFuel.text = $"{MetaGame.Instance.fuelCan}";
        textOil.text = $"{MetaGame.Instance.oilCan}";
        textWater.text = $"{MetaGame.Instance.waterCan}";
    }
    public void ShakeFuelImage()
    {
        imageFuel.transform.DOKill();
        imageFuel.transform.localScale = imageFuelinitScale;
        imageFuel.transform.DOPunchScale(new Vector3(
            imageFuelinitScale.x - (imageFuelinitScale.x * 0.2f),
            imageFuelinitScale.y - (imageFuelinitScale.y * 0.2f),
            imageFuelinitScale.z - (imageFuelinitScale.z * 0.2f)), 0.3f, 10, 0.5f);
    }
    public void ShakeOilImage()
    {
        imageOil.transform.DOKill();
        imageOil.transform.localScale = imageOilinitScale;
        imageOil.transform.DOPunchScale(new Vector3(
            imageOilinitScale.x - (imageOilinitScale.x * 0.2f),
            imageOilinitScale.y - (imageOilinitScale.y * 0.2f),
            imageOilinitScale.z - (imageOilinitScale.z * 0.2f)), 0.3f, 10, 0.5f);
    }
    public void ShakeWaterImage()
    {
        imageWater.transform.DOKill();
        imageWater.transform.localScale = imageWaterinitScale;
        imageWater.transform.DOPunchScale(new Vector3(
            imageWaterinitScale.x - (imageWaterinitScale.x * 0.2f),
            imageWaterinitScale.y - (imageWaterinitScale.y * 0.2f),
            imageWaterinitScale.z - (imageWaterinitScale.z * 0.2f)), 0.3f, 10, 0.5f);
    }

    public void HideAllButtons()
    {
        fuelButton.gameObject.SetActive(false);
        oilButton.gameObject.SetActive(false);
        waterButton.gameObject.SetActive(false);
    }
    public void OnFuelButtonPress()
    {
        if (MetaGame.Instance.fuelCan >= 1)
        {
            MetaGame.Instance.fuelCan -= 1;

            if (MetaGame.Instance.fuelEngine / MetaGame.Instance.fuelMax >= 0.9)
            {
                ShowNotEnough("Overfilled, canister wasted!");
                MetaGame.Instance.fuelEngine = MetaGame.Instance.fuelMax;
            }
            else
            {
                MetaGame.Instance.fuelEngine += MetaGame.Instance.fuelCanCapacity;
                if (MetaGame.Instance.fuelEngine / MetaGame.Instance.fuelMax >= 1)
                {
                    MetaGame.Instance.fuelEngine = MetaGame.Instance.fuelMax;
                }
            }
            ShakeFuelImage();
            UpdateResText();
        }
        else
        {
            ShowNotEnough("No canister!");
        }
    }
    
    public void OnOilButtonPress()
    {
        if (MetaGame.Instance.oilCan >= 1)
        {
            MetaGame.Instance.oilCan -= 1;

            if (MetaGame.Instance.oilEngine / MetaGame.Instance.oilMax >= 0.9)
            {
                ShowNotEnough("Overfilled, canister wasted!");
                MetaGame.Instance.oilEngine = MetaGame.Instance.oilMax;
            }
            else
            {
                MetaGame.Instance.oilEngine += MetaGame.Instance.oilCanCapacity;
                if (MetaGame.Instance.oilEngine / MetaGame.Instance.oilMax >= 1)
                {
                    MetaGame.Instance.oilEngine = MetaGame.Instance.oilMax;
                }
            }
            ShakeOilImage();
            UpdateResText();
        }
        else
        {
            ShowNotEnough("No canister!");
        }

    }
    public void OnWaterButtonPress()
    {
        if (MetaGame.Instance.waterCan >= 1)
        {
            MetaGame.Instance.waterCan -= 1;

            if (MetaGame.Instance.waterEngine / MetaGame.Instance.waterMax >= 0.9)
            {
                ShowNotEnough("Overfilled, canister wasted!");
                MetaGame.Instance.waterEngine = MetaGame.Instance.waterMax;
            }
            else
            {
                MetaGame.Instance.waterEngine += MetaGame.Instance.waterCanCapacity;
                if (MetaGame.Instance.waterEngine / MetaGame.Instance.waterMax >= 1)
                {
                    MetaGame.Instance.waterEngine = MetaGame.Instance.waterMax;
                }
            }
            ShakeWaterImage();
            UpdateResText();
        }
        else
        {
            ShowNotEnough("No canister!");
        }
    }
    public void ShowNotEnough(string textMessage)
    {
        GameObject textObj = Instantiate(textPrefab, transform);
        TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
        
        tmp.text = textMessage;
        
        textObj.transform.position = Input.mousePosition + Vector3.up * 30f;
        
        tmp.DOFade(0, 1f).SetEase(Ease.OutQuad);
        textObj.transform.DOMoveY(textObj.transform.position.y + 20f, 1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => Destroy(textObj));
    }
}
