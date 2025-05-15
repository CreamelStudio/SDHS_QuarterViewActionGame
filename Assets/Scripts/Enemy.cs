using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle, Move, Attack, Dead
    }
    public EnemyState state = EnemyState.Idle;
    public float searchRange;
    public float attackRange;
    public NavMeshAgent agent;
    public Animator anim;

    //씬 뷰에 기즈모를 그리는 함수
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, searchRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        EnemyAct();
    }

    void EnemyAct()
    {
        AnimInit();
        switch (state)
        {
            case EnemyState.Idle:
                if (Vector3.Distance(GameManager.instance.playerTrans.position, transform.position) <= searchRange)
                {
                    state = EnemyState.Move;
                }
                break;
            case EnemyState.Move:
                agent.SetDestination(GameManager.instance.playerTrans.position);
                
                if (Vector3.Distance(GameManager.instance.playerTrans.position, transform.position) <= attackRange)
                {
                    state = EnemyState.Attack;
                }
                break;
        }
    }

    //에니메이션 실행
    void AnimInit()
    {
        anim.SetInteger("EnemyState", (int)state);
    }
}
