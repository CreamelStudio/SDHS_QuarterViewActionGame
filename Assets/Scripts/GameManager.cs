using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform playerTrans;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }
}
