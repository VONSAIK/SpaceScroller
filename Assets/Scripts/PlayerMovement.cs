using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedMovement;

    [SerializeField] private float mixX = -2.5f;
    [SerializeField] private float maxX = 2.5f;

    private Vector2 moveInput;


    private void Update()
    {
        Vector3 newPosition = transform.position + Vector3.right * moveInput.x * _speedMovement * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, mixX, maxX);

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
