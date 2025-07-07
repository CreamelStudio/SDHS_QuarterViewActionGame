using UnityEngine;

public class SeeCamera : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
