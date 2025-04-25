using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private NavMeshAgent navAgent;
    public Animator anim;

    public float targetDistance;

    [SerializeField]private Transform movePoint;

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
                    movePoint.position = hit.point;
                    movePoint.gameObject.SetActive(true);
                    playerState = PlayerState.Move;
                }

                break;
            case PlayerState.Move:

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && Input.GetMouseButton(0))
                {
                    movePoint.position = hit.point;
                    movePoint.gameObject.SetActive(true);
                }
                targetDistance = Vector3.Distance(movePoint.position, transform.position);
                if(targetDistance <= 0.1f)
                {
                    AnimOn(0);
                    playerState = PlayerState.Idle;
                    movePoint.gameObject.SetActive(false);
                }
                
                navAgent.SetDestination(movePoint.position);

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
