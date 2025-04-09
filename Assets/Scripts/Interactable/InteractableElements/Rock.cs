using UnityEngine;

public class Rock : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Take damage");
    }
}
