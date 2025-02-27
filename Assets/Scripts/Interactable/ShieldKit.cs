using UnityEngine;

public class ShieldKit : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Take shield");
    }

    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        base.Move();
    }
}
