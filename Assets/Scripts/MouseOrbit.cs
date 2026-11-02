using UnityEngine;
using UnityEngine.InputSystem;

public class MouseOrbit : MonoBehaviour
{
    public Transform player;
    public float distance = 5f;
    public float MouseSensitivity = 0.2f;
    public Vector3 offset = new Vector3(0f, 1.5f, 0f);
    private float rotX;
    private float rotY;
    public float minY = -30f;
    public float maxY = 60f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (Time.timeScale == 0f) return;

        if (player != null)
        {
            Vector2 delta = Mouse.current.delta.ReadValue() * MouseSensitivity;
            rotX += delta.x;
            rotY = Mathf.Clamp(rotY - delta.y, minY, maxY);

            Quaternion rotation = Quaternion.Euler(rotY, rotX, 0);
            transform.position = player.position + offset - (rotation * Vector3.forward * distance);
            transform.rotation = rotation;
        }
    }
}