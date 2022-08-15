using UnityEngine;
using Zenject;

public class LockAtCam : MonoBehaviour
{
    [Inject] private Camera _cam;
    [SerializeField] private float clampX;

    void Update()
    {
        var dir = _cam.transform.position - transform.position;
        Quaternion LookAtRotation = Quaternion.LookRotation(-dir);

        Quaternion LookAtRotationOnly_Y =
            Quaternion.Euler(Mathf.Clamp(LookAtRotation.eulerAngles.x, -clampX, clampX), transform.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = LookAtRotationOnly_Y;
    }
}