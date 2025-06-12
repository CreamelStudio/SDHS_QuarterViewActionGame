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

    public float delayTime;
    public float attackTime;

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
        float dist;
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
                    dist = Vector3.Distance(GameManager.instance.playerTrans.position, transform.position);
                    
                    if (dist <= searchRange)
                    {
                        if (dist <= attackRange)
                        {
                            agent.isStopped = true;
                            ChangeState(EnemyState.Attack);
                        }
                    }
                    else
                    {
                        agent.isStopped = true;
                        ChangeState(EnemyState.Attack);
                        attackState = AttackState.Attack;
                    }
                }
                break;
            case EnemyState.Attack:
                dist = Vector3.Distance(transform.position, GameManager.instance.playerTrans.position);
                if (dist > attackRange)
                {
                    agent.isStopped = false;
                    attackTime = 0;
                    delayTime = 0;
                    attackState = AttackState.None;
                    ChangeState(EnemyState.Move);
                }
                
                
                
	switch (attackState)
        {

            case AttackState.Attack:
                delayTime += Time.deltaTime;
                if (delayTime >= (attackClip.length * 0.4f))
                {
                    delayTime = 0;
                    attackTime = 0;
                    attackState = AttackState.Delay;
                    AnimSet(0);
                    attackState = AttackState.Delay;

                }
                break;

            case AttackState.Delay:
                delayTime += Time.deltaTime;
                if(delayTime >= (attackClip.length * 0.6f) * 2.0f)
                {
                    delayTime = 0;
                    attackTime = 0;
                    AnimSet(0);
                    attackState = AttackState.Attack;
                }
                break;
        }


                break;
        }

        
    }

    void ChangeState(EnemyState state)
    {
        this.state = state;
        AnimInit();
    }

    void AnimSet(int stateInt)
    {
        anim.SetInteger("EnemyState", stateInt);
    }

    //에니메이션 실행
    void AnimInit()
    {
        anim.SetInteger("EnemyState", (int)state);
    }
}
