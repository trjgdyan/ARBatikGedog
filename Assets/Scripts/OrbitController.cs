using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public Transform target;           // Objek model 3D yang diam di tengah
    public float distance = 3.0f;      // Jarak kamera ke target
    public float xSpeed = 120.0f;      // Kecepatan rotasi horizontal
    public float ySpeed = 120.0f;      // Kecepatan rotasi vertikal
    public float yMinLimit = -20f;     // Batas rotasi vertikal bawah
    public float yMaxLimit = 80f;      // Batas rotasi vertikal atas
    public float zoomSpeed = 2f;       // Kecepatan zoom scroll
    public float minDistance = 1.5f;   // Zoom minimum
    public float maxDistance = 6f;     // Zoom maksimum

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("OrbitController: Target belum di-assign.");
            return;
        }

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (Input.GetMouseButton(0)) // Klik kiri tahan + drag
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);
        }

        // Zoom pakai scroll wheel
        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}