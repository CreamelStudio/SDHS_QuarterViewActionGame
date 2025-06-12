using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private NavMeshAgent navAgent;
    public Animator anim;

    private float targetDistance;

    [SerializeField]private Transform movePoint;

    public enum PlayerState
    {
        Idle,
        Move,
        Attack
    }
    public PlayerState playerState = PlayerState.Idle;
    public float attackRange;
    public float attackDist;
    public Transform target;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

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
                    if (hit.transform.CompareTag("Enemy")){
                        target = hit.transform;
                    }
                    AnimOn(PlayerState.Move);
                    movePoint.position = hit.point;
                    movePoint.gameObject.SetActive(true);
                    playerState = PlayerState.Move;

                    if (target)
                    {
                        attackDist = Vector3.Distance(transform.position, target.position);

                        if(attackDist < attackRange)
                        {
                            playerState = PlayerState.Attack;
                            AnimOn(playerState);
                        }
                    }
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
