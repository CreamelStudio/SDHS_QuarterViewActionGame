using UnityEngine;

public class SeeCamera : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform); //파티클이 카메라 보게
    }
}
