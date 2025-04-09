using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedMovement;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    private Vector2 moveInput;


    private void Update()
    {
        Vector3 newPosition = transform.position + Vector3.right * moveInput.x * _speedMovement * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        transform.position = newPosition;

    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else
        {
            moveInput = Vector2.zero;
        }
    }
}
