using UnityEngine;

public class MetaGame : MonoBehaviour
{
    public static MetaGame Instance;
    
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
}
