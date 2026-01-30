using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    private InputAction move;
    private InputAction look;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float gravity = -10f;
    private Vector3 velocity;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float mouseSensY = 5f;
    [SerializeField, Child] private Camera cam;

    private void OnValidate()
    {
        this.ValidateRefs();
    }

    void Start()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        look = InputSystem.actions.FindAction("Player/Look");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 readMove = move.ReadValue<Vector2>();
        Vector2 readLook = look.ReadValue<Vector2>();
        //Player movement
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;
        velocity.y += gravity * Time.deltaTime;
        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;
        controller.Move(velocity * Time.deltaTime);

        //Player rotation
        transform.Rotate(Vector3.up * readLook.x * rotationSpeed * Time.deltaTime);

        //Camera rotation
        mouseSensY = readLook.y * mouseSensY;
        mouseSensY = Mathf.Clamp(mouseSensY, -90f, 90f);
        cam.gameObject.transform.localRotation = Quaternion.Euler(mouseSensY, 0, 0);
    }
}
