using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component moves its object when the player clicks the arrow keys or WASD keys.
 */
public class InputMover : MonoBehaviour
{
    [Tooltip("Speed of movement, in meters per second")]
    [SerializeField] private float speed = 10f;

    [SerializeField] private InputAction move = new InputAction(
        type: InputActionType.Value,
        binding: "<Keyboard>/w,<Keyboard>/a,<Keyboard>/s,<Keyboard>/d,<Keyboard>/upArrow,<Keyboard>/downArrow,<Keyboard>/leftArrow,<Keyboard>/rightArrow",
        expectedControlType: nameof(Vector2));

    void OnEnable()
    {
        move.Enable();
    }

    void OnDisable()
    {
        move.Disable();
    }

    void Update()
    {
        // Read movement input from the player
        Vector2 moveDirection = move.ReadValue<Vector2>();
        
        // Translate the object based on movement input and speed
        Vector3 movementVector = new Vector3(moveDirection.x, moveDirection.y, 0) * speed * Time.deltaTime;
        transform.position += movementVector;
    }
}
