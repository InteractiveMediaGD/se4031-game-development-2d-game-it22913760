using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform body;

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        if (body == null) return;

        if (moveX < 0)
            body.localScale = new Vector3(-1f, 1f, 1f);
        else if (moveX > 0)
            body.localScale = new Vector3(1f, 1f, 1f);
    }
}