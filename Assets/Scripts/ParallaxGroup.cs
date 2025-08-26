using UnityEngine;

public class ParallaxGroup : MonoBehaviour
{
    [SerializeField] private float parallaxEffect = 0.5f;
    [SerializeField] private bool lockYPosition = true;
    [SerializeField] private float yOffset = 0.1f;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        if (cameraTransform == null) return;

        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        if (float.IsNaN(deltaMovement.x)) deltaMovement.x = 0;

        // Двигаем всю группу
        transform.position += new Vector3(deltaMovement.x * parallaxEffect, 0, 0);

        // Фиксируем позицию по Y
        if (lockYPosition && Camera.main != null)
        {
            Vector3 viewportPoint = new Vector3(0, yOffset, Camera.main.nearClipPlane);
            float cameraBottom = Camera.main.ViewportToWorldPoint(viewportPoint).y;

            if (!float.IsNaN(cameraBottom))
            {
                transform.position = new Vector3(transform.position.x, cameraBottom, transform.position.z);
            }
        }

        lastCameraPosition = cameraTransform.position;
    }
}