using TMPro;
using UnityEngine;

[System.Serializable]
public class UpgradeSaveData
{
    public int EngineEfficiencyLvl;
    public int CollectionSpeedLvl;
    public int EngineDurabilityLvl;
    public int ShipAccelerationLvl;
    public int ShipMaxSpeedLvl;
    public int EResourceSpawnRateLvl;
}

public class BuyUpgrade : Interactable
{
    [SerializeField] private string statName = "max Speed";
    [SerializeField] private int level = 0;
    [SerializeField] private int maxLevel = 3;
    [SerializeField] private float[] values = { 5f, 6f, 7f};
    [SerializeField] private int[] costs = { 200, 400, 600};
    
    [SerializeField] private string text;
    [SerializeField] private TextMeshPro info;

    void Start()
    {
        if (statName == "Engine Efficiency") MetaGame.Instance.speedOfGame = values[level];
        if (statName == "Collection Speed") MetaGame.Instance.takeLookFaster = values[level];
        if (statName == "Engine Durability") MetaGame.Instance.engineHealth = values[level];
        if (statName == "Ship Acceleration") MetaGame.Instance.shipSpeedAcsel = values[level];
        if (statName == "Ship Max Speed") MetaGame.Instance.shipMaxSpeed = values[level];
        if (statName == "Resource Spawn Rate") MetaGame.Instance.luckLevel = values[level];
        SetInfo();
    }


    private void SetInfo()
    {
        info.text = "";
        info.text += $"{statName} {level} lvl\n";
        if (level != maxLevel)
        {
            info.text += $"cost:{costs[level]}";
        }
        else
        {
            info.text += $"MAX lvl.";
        }
    }
    override public void Interact()
    {
        Buy();
    }
     
    public void Buy()
    {
        if (level >= maxLevel) return;
        if (MetaGame.Instance.kmCompleted < costs[level])
        {
            G.Ui.ShowNotEnough("Not enough!");
            return;
        }
        
        MetaGame.Instance.kmCompleted -= costs[level];
        level++;
        
        if (statName == "Engine Efficiency") MetaGame.Instance.speedOfGame = values[level];
        if (statName == "Collection Speed") MetaGame.Instance.takeLookFaster = values[level];
        if (statName == "Engine Durability") MetaGame.Instance.engineHealth = values[level];
        if (statName == "Ship Acceleration") MetaGame.Instance.shipSpeedAcsel = values[level];
        if (statName == "Ship Max Speed") MetaGame.Instance.shipMaxSpeed = values[level];
        if (statName == "Resource Spawn Rate") MetaGame.Instance.luckLevel = values[level];

        SetInfo();
    }
}
