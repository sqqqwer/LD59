using UnityEngine;

public class GoToBack : Interactable
{
    override public void Interact()
    {
        MetaGame.Instance.StartNextRun();
    }
}
