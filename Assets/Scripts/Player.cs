using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private NavMeshAgent navAgent;
    public Animator anim;

    private Vector3 movePoint;

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
        navAgent = GetComponent<NavMeshAgent>();
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

                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && Input.GetMouseButton(0))
                {
                    AnimOn(PlayerState.Move);
                    movePoint = hit.point;
                    playerState = PlayerState.Move;
                }

                break;
            case PlayerState.Move:

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && Input.GetMouseButton(0))
                {
                    movePoint = hit.point;
                }

                navAgent.SetDestination(movePoint);
                //playerState = PlayerState.Idle;
                break;
            case PlayerState.Attack:
                break;
        }
    }

    void AnimOn(PlayerState n)
    {
        anim.SetInteger("PlayerState", (int)n);
    }
}
