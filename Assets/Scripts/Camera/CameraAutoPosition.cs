using UnityEngine;

public class CameraAutoPosition : MonoBehaviour
{
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private float padding;

    private void Awake()
    {
        PositionCamera();
    }

    public void PositionCamera()
    {
        Camera mainCamera = Camera.main;

        Vector3 cameraPos = new Vector3((width - 1) / 2, (height - 1) / 2, -10);
        mainCamera.transform.position = cameraPos;

        if (width > height)
        {
            float aspectRatio = mainCamera.aspect;
            mainCamera.orthographicSize = (width / 2 + padding) / aspectRatio;
        }

        else
        {
            mainCamera.orthographicSize = height / 2 + padding;
        }
    }
}
