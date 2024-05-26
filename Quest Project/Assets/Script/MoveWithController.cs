using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRig : MonoBehaviour
{
    public InputActionReference leftJoystickAction;
    public InputActionReference rightJoystickAction;
    public float moveSpeed = 2.0f;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        leftJoystickAction.action.Enable();
        rightJoystickAction.action.Enable();
    }

    void Update()
    {
        Vector2 leftInput = leftJoystickAction.action.ReadValue<Vector2>();
        Vector2 rightInput = rightJoystickAction.action.ReadValue<Vector2>();

        // 왼쪽 조이스틱으로 이동
        Vector3 move = new Vector3(leftInput.x, 0, leftInput.y) * moveSpeed * Time.deltaTime;
        move = Camera.main.transform.TransformDirection(move);
        move.y = 0;
        characterController.Move(move);

        // 오른쪽 조이스틱으로 y좌표 변경
        float verticalInput = rightInput.y;
        Vector3 position = transform.position;
        position.y += verticalInput * moveSpeed * Time.deltaTime;
        transform.position = position;
    }

    void OnDestroy()
    {
        leftJoystickAction.action.Disable();
        rightJoystickAction.action.Disable();
    }
}
