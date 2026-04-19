using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private Door door;
    
    private Camera mainCamera;
    private GameObject lastHitObject;
    
    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        if (Screen.width <= 0 || Screen.height <= 0) return;
        
        if (Input.GetMouseButtonDown(1))
        {
            
            if (G.lvlOfInteraction == -2)// в рубке
            {
                G.lvlOfInteraction = 0;
                G.cameraMoveToPoint.moveTime = 0.17f;
                door.ToggleDoor();
                G.cameraMoveToPoint.GoToPoint(4);
            }
            else if (G.lvlOfInteraction == -1)// у двери
            {
                G.lvlOfInteraction = 0;
                G.cameraMoveToPoint.moveTime = 0.17f;
                door.ToggleDoor();
                G.cameraMoveToPoint.GoToPoint(0);
            }
            else if (G.lvlOfInteraction == 0) // в движке
            {
                G.lvlOfInteraction = -1;
                G.cameraMoveToPoint.moveTime = 0.17f;
                door.ToggleDoor();
                G.cameraMoveToPoint.GoToPoint(4);
            }
            else if (G.lvlOfInteraction == 1)
            {
                G.cameraMoveToPoint.moveTime = 0.22f;
                G.lvlOfInteraction = 0;

                G.miniGameManager.ActivateMiniGamesChoiserColliders();
                G.miniGameManager.DeactivateGroupMiniGameInteractable();

                Debug.Log("GoToBasePosition");
                G.cameraMoveToPoint.GoToPoint(0);
            }

        }

        if (Screen.width <= 0 || Screen.height <= 0) return;
        if (float.IsInfinity(Input.mousePosition.x) || float.IsInfinity(Input.mousePosition.y) || float.IsNaN(Input.mousePosition.x) || float.IsNaN(Input.mousePosition.y))
        {
            return;
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                GameObject hitObject = hit.collider.gameObject;
                
                if (hitObject != lastHitObject)
                {
                    if (lastHitObject != null)
                        OnMouseExitObject(lastHitObject);



                    OnMouseEnterObject(hitObject);
                    lastHitObject = hitObject;
                }
                
                if (Input.GetMouseButtonDown(0))
                    OnMouseClickObject(hitObject, hit.point);
            }
            else
            {
                if (lastHitObject != null)
                {
                    OnMouseExitObject(lastHitObject);
                    lastHitObject = null;
                }
            }
        }
        else
        {
            if (lastHitObject != null)
            {
                OnMouseExitObject(lastHitObject);
                lastHitObject = null;
            }
        }
    }
    
    void OnMouseEnterObject(GameObject obj)
    {
        GoToMiniGameInteract objInteractable = obj.GetComponent<GoToMiniGameInteract>();
        if (objInteractable)
        {
            objInteractable.HiLightShow();
        }

        FuelCheck noseInteractable = obj.GetComponent<FuelCheck>();
        WaterCheck waterInteractable = obj.GetComponent<WaterCheck>();
        OilCheck oilInteractable = obj.GetComponent<OilCheck>();
        Loot LootInteractable = obj.GetComponent<Loot>();
        if (noseInteractable)
        {
            G.Ui.cursor.SetNose();
        }
        else if (waterInteractable || oilInteractable || LootInteractable)
        {
            G.Ui.cursor.SetGrab();
        }
        else
        {
            G.Ui.cursor.SetInteractble();
        }
    }
    
    void OnMouseExitObject(GameObject obj)
    {
        GoToMiniGameInteract objInteractable = obj.GetComponent<GoToMiniGameInteract>();
        if (objInteractable)
        {
            objInteractable.HiLightHide();
        }

        G.Ui.cursor.SetNormal();
    }
    
    void OnMouseClickObject(GameObject obj, Vector3 hitPoint)
    {
        FuelCheck noseInteractable = obj.GetComponent<FuelCheck>();
        WaterCheck waterInteractable = obj.GetComponent<WaterCheck>();
        OilCheck oilInteractable = obj.GetComponent<OilCheck>();
        Loot LootInteractable = obj.GetComponent<Loot>();
        if (noseInteractable)
        {
            G.Ui.cursor.SetNose();
        }
        else if (waterInteractable || oilInteractable || LootInteractable)
        {
            G.Ui.cursor.SetGrab();
        }
        else
        {
            G.Ui.cursor.SetInteractble();
        }
        Interactable objInteractable = obj.GetComponent<Interactable>();
        if (objInteractable)
        {
            objInteractable.Interact();
        }
    }
}
