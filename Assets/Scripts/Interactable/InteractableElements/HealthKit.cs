using UnityEngine;

public class HealthKit : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Take health");
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
