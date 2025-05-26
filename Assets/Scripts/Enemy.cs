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

    public enum AttackState
    {
        None, Attack, Delay
    }
    public AttackState attackState = AttackState.None;
    public AnimationClip attackClip;

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
        switch (state)
        {
            case EnemyState.Idle:
                if (Vector3.Distance(GameManager.instance.playerTrans.position, transform.position) <= searchRange)
                {
                    ChangeState(EnemyState.Move);
                }
                break;
            case EnemyState.Move:
                if(GameManager.instance.playerTrans != null)
                {
                    agent.SetDestination(GameManager.instance.playerTrans.position);

                    if (Vector3.Distance(GameManager.instance.playerTrans.position, transform.position) <= attackRange)
                    {
                        agent.isStopped = true;
                        ChangeState(EnemyState.Attack);
                    }
                }
                
                break;
        }
    }

    void ChangeState(EnemyState state)
    {
        this.state = state;
        AnimInit();
    }

    //에니메이션 실행
    void AnimInit()
    {
        anim.SetInteger("EnemyState", (int)state);
    }
}
