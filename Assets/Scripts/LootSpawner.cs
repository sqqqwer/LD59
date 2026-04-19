using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] lootPrefabs;
    
    [SerializeField] private Transform spawnMin;
    [SerializeField] private Transform spawnMax;
    
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float baseLootChance = 30f;
    [SerializeField] private float luckMultiplier = 2f;
    
    private float timer;
    
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= spawnInterval)
        {
            TrySpawnLoot();
            timer = 0;
        }
    }
    
    void TrySpawnLoot()
    {
        if (lootPrefabs.Length == 0 || spawnMin == null || spawnMax == null) return;
        
        float chance = baseLootChance + (MetaGame.Instance.luckLevel * luckMultiplier);
        
        if (Random.Range(0, 100) >= chance) return;
        
        Vector3 pos = new Vector3(
            Random.Range(spawnMin.position.x, spawnMax.position.x),
            Random.Range(spawnMin.position.y, spawnMax.position.y),
            Random.Range(spawnMin.position.z, spawnMax.position.z)
        );
        
        GameObject prefab = ChooseLootByNeeds(MetaGame.Instance.luckLevel);
        Instantiate(prefab, pos, Quaternion.identity);
    }
    
    GameObject ChooseLootByNeeds(float luck)
    {
        float luckNormalized = Mathf.Clamp01(luck / 100f);
        
        if (Random.Range(0, 100) < luckNormalized)
        {
            return GetNeededLoot();
        }
        else
        {
            return lootPrefabs[Random.Range(0, lootPrefabs.Length)];
        }
    }
    
    GameObject GetNeededLoot()
    {
        float fuelFill = MetaGame.Instance.fuelEngine / MetaGame.Instance.fuelMax;
        float waterFill = MetaGame.Instance.waterEngine / MetaGame.Instance.waterMax;
        float oilFill = MetaGame.Instance.oilEngine / MetaGame.Instance.oilMax;
        
        float fuelNeed = 1f - fuelFill;
        float waterNeed = 1f - waterFill;
        float oilNeed = 1f - oilFill;
        
        float totalNeed = fuelNeed + waterNeed + oilNeed;
        
        if (totalNeed <= 0) return lootPrefabs[Random.Range(0, lootPrefabs.Length)];
        
        float rand = Random.Range(0, totalNeed);
        
        if (rand < fuelNeed) return GetLootByType("Fuel");
        if (rand < fuelNeed + waterNeed) return GetLootByType("Water");
        return GetLootByType("Oil");
    }
    
    GameObject GetLootByType(string type)
    {
        foreach (GameObject loot in lootPrefabs)
        {
            Loot l = loot.GetComponent<Loot>();
            if (l != null)
            {
                if (type == "Fuel" && l.isFuel) return loot;
                if (type == "Water" && l.isWater) return loot;
                if (type == "Oil" && l.isOil) return loot;
            }
        }
        return lootPrefabs[0];
    }
}