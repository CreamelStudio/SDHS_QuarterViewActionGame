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
    public float searchRange; //확인 범위
    public float attackRange; //공격 범위

    public enum AttackState //공격 스테이트
    {
        None, Attack, Delay
    }
    public AttackState attackState = AttackState.None;
    public AnimationClip attackClip;

    public float delayTime; //공격 딜레이
    public float attackTime; //공격 시간

    public NavMeshAgent agent;
    public Animator anim;

    public ParticleSystem myParticleSystem; //파티클

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
                    attackState = AttackState.None;
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
                            attackState = AttackState.Attack;
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
                attackTime += Time.deltaTime;
                if (attackTime >= (attackClip.length * 0.4f))
                {
		            myParticleSystem.Stop();
		            myParticleSystem.Play();
                    GameManager.instance.playerTrans.GetComponent<Player>().Hit();
                    delayTime = 0;
                    attackTime = 0;
                    attackState = AttackState.Delay;
                    AnimSet(0);
		    

                }
                break;

            case AttackState.Delay:
                delayTime += Time.deltaTime;
                if(delayTime >= (attackClip.length * 0.6f) * 2.0f)
                {
                    delayTime = 0;
                    attackTime = 0;
                    AnimSet(2);
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
