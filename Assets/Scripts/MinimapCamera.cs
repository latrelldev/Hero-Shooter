using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform player;
    public float altura = 40f;

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = new Vector3(player.position.x, altura, player.position.z);
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}