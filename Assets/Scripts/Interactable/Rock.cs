using UnityEngine;

public class Rock : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Take damage");
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
