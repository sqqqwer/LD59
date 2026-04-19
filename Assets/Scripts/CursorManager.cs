using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite interactbleSprite;
    [SerializeField] private Sprite interactbleClickSprite;
    [SerializeField] private Sprite noseSprite;
    [SerializeField] private Sprite noseClickSprite;
    [SerializeField] private Sprite grabSprite;
    [SerializeField] private Sprite grabClickSprite;

    [SerializeField] private int cursorState = 0;// 0=normal 1=interact 2=nose 3=grab


    public bool isNormal = true;
    
    private Image cursorImage;
    
    void Start()
    {
        cursorImage = GetComponent<Image>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        SetNormal();
    }
    
    void Update()
    {
        if (Screen.width <= 0 || Screen.height <= 0) return;
        if (float.IsInfinity(Input.mousePosition.x) || float.IsInfinity(Input.mousePosition.y) || float.IsNaN(Input.mousePosition.x) || float.IsNaN(Input.mousePosition.y))
        {
            return;
        }

        transform.position = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            if (cursorState == 0)
            {
                cursorImage.sprite = interactbleClickSprite;
            }
            else if (cursorState == 1)
            {
                cursorImage.sprite = interactbleClickSprite;
            }
            else if (cursorState == 2)
            {
                cursorImage.sprite = noseClickSprite;
            }
            else if (cursorState == 3)
            {
                cursorImage.sprite = grabClickSprite;
            }
        }
        else
        {
            if (cursorState == 0)
            {
                cursorImage.sprite = normalSprite;
            }
            else if (cursorState == 1)
            {
                cursorImage.sprite = interactbleSprite;
            }
            else if (cursorState == 2)
            {
                cursorImage.sprite = noseSprite;
            }
            else if (cursorState == 3)
            {
                cursorImage.sprite = grabSprite;
            }
        }
    }

    public void SetNormal()
    {
        cursorState = 0;
    }
    public void SetInteractble()
    {
        cursorState = 1;
    }
    public void SetNose()
    {
        cursorState = 2;
    }
    public void SetGrab()
    {
        cursorState = 3;
    }
}