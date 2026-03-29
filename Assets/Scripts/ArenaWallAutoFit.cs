using UnityEngine;

public class ArenaWallAutoFit : MonoBehaviour
{
    public BoxCollider2D wallTop;
    public BoxCollider2D wallBottom;
    public BoxCollider2D wallLeft;
    public BoxCollider2D wallRight;

    public float thickness = 1f;
    public float extraPadding = 0.5f;

    private void Start()
    {
        FitWalls();
    }

    [ContextMenu("Fit Walls")]
    public void FitWalls()
    {
        Camera cam = Camera.main;
        if (cam == null || !cam.orthographic) return;

        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        float halfWidth = camWidth / 2f;
        float halfHeight = camHeight / 2f;

        if (wallTop != null)
        {
            wallTop.transform.position = new Vector3(0f, halfHeight + extraPadding, 0f);
            wallTop.size = new Vector2(camWidth + thickness, thickness);
        }

        if (wallBottom != null)
        {
            wallBottom.transform.position = new Vector3(0f, -halfHeight - extraPadding, 0f);
            wallBottom.size = new Vector2(camWidth + thickness, thickness);
        }

        if (wallLeft != null)
        {
            wallLeft.transform.position = new Vector3(-halfWidth - extraPadding, 0f, 0f);
            wallLeft.size = new Vector2(thickness, camHeight + thickness);
        }

        if (wallRight != null)
        {
            wallRight.transform.position = new Vector3(halfWidth + extraPadding, 0f, 0f);
            wallRight.size = new Vector2(thickness, camHeight + thickness);
        }
    }
}