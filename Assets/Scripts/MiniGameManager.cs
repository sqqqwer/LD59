using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] public List<Collider> allMiniGamesCollisions;
    [SerializeField] public GameObject groupMiniGameInteractableWater;
    [SerializeField] public GameObject groupMiniGameInteractableOil;
    [SerializeField] public GameObject groupMiniGameInteractableFuel;

    public void ActivateGroupMiniGameInteractable(int idCameraPos)
    {
        DeactivateGroupMiniGameInteractable();

        if (idCameraPos == 1)
        {
            groupMiniGameInteractableWater.SetActive(true);
        }
        else if (idCameraPos == 2)
        {
            groupMiniGameInteractableOil.SetActive(true);
        }
        else if (idCameraPos == 3)
        {
            groupMiniGameInteractableFuel.SetActive(true);
        }
    }
    public void DeactivateGroupMiniGameInteractable()
    {
        groupMiniGameInteractableWater.SetActive(false);
        groupMiniGameInteractableOil.SetActive(false);
        groupMiniGameInteractableFuel.SetActive(false);
    }

    public void DeactivateMiniGamesChoiserColliders()
    {
        foreach (Collider collision in allMiniGamesCollisions)
        {
            collision.enabled = false;
        }
    }
    public void ActivateMiniGamesChoiserColliders()
    {
        foreach (Collider collision in allMiniGamesCollisions)
        {
            collision.enabled = true;
        }
    }

}
