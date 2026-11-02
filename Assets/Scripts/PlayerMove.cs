using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float sprintMultiplier = 2f;
    public Rigidbody rb;
    public PlayerStamina stamina;

    private Vector3 direction;
    private float currentSpeed;
    private bool querCorrer;

    private Vector3 dashVelocity;
    private float dashTime;

    void Update()
    {
        if (Time.timeScale == 0f) return;

        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

        direction = Vector3.zero;

        if (Keyboard.current[Key.W].isPressed)
        {
            direction += transform.forward;
        }
        if (Keyboard.current[Key.S].isPressed)
        {
            direction -= transform.forward;
        }
        if (Keyboard.current[Key.D].isPressed)
        {
            direction += transform.right;
        }
        if (Keyboard.current[Key.A].isPressed)
        {
            direction -= transform.right;
        }

        direction = Vector3.ClampMagnitude(direction, 1f);

        Correr();
        dashTime -= Time.deltaTime;
    }

    void Correr()
    {
        if (Keyboard.current[Key.LeftShift].wasPressedThisFrame) querCorrer = true;
        if (!Keyboard.current[Key.LeftShift].isPressed) querCorrer = false;

        currentSpeed = speed;
        if (querCorrer && direction != Vector3.zero)
        {
            if (stamina.Correr())
            {
                currentSpeed = speed * sprintMultiplier;
            }
            else
            {
                querCorrer = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (dashTime > 0f)
        {
            rb.linearVelocity = new Vector3(dashVelocity.x, rb.linearVelocity.y, dashVelocity.z);
            return;
        }

        rb.linearVelocity = new Vector3(direction.x * currentSpeed,
        rb.linearVelocity.y, direction.z * currentSpeed);
    }

    public void Dash(float forca, float duracao)
    {
        Vector3 dir = direction != Vector3.zero ? direction.normalized : transform.forward;
        dashVelocity = dir * forca;
        dashTime = duracao;
    }
}