using UnityEngine;

public class Coin : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Take coin");
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
