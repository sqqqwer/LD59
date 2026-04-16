using UnityEngine;

public class G : MonoBehaviour
{
    public static bool isGameGoing = false;
    public static UI Ui;
    [SerializeField] private UI _Ui;

    private void Awake()
    {
        Ui = _Ui;
    }
    private void Start()
    {
        MetaGame.Instance.StartGame();
    }
}
