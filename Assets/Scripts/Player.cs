using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    public enum PlayerState
    {
        Idle,
        Move,
        Attack
    }
    public PlayerState playerState = PlayerState.Idle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        
    }

    void PlayerMove()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Move:
                break;
            case PlayerState.Attack:
                break;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && Input.GetMouseButton(0))
        {
            Debug.Log($"Ray : {hit.point}");
        }
    }
}
