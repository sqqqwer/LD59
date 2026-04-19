using UnityEngine;

public class G : MonoBehaviour
{
    public static bool isGameGoing = false;
    public static int lvlOfInteraction = 0;
    public static float armWaterCheckShow = 0f;
    public static float armWaterCheckHide = 0f;
    public static Vector3 armOilCheckShow;
    public static Vector3 armOilCheckHide;

    public static UI Ui;
    [SerializeField] private UI _Ui;

    public static CameraMoveToPoint cameraMoveToPoint;
    [SerializeField] private CameraMoveToPoint _cameraMoveToPoint;

    public static MiniGameManager miniGameManager;
    [SerializeField] private MiniGameManager _miniGameManager;


    private void Awake()
    {
        Ui = _Ui;
        cameraMoveToPoint = _cameraMoveToPoint;
        miniGameManager = _miniGameManager;
    }
    private void Start()
    {
        MetaGame.Instance.StartGame();
    }
}
