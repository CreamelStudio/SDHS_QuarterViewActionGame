using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle, Move, Attack, Dead
    }
    public EnemyState state = EnemyState.Idle;
    public float searchRange;

    //씬 뷰에 기즈모를 그리는 함수
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, searchRange);
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
                    state = EnemyState.Move;
                }
                break;
            case EnemyState.Move:
                break;
        }
    }

    //에니메이션 실행
    void AnimOn(int n)
    {
        switch (n)
        {
            
        }
    }
}
