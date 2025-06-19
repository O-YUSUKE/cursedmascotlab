using UnityEngine;
using UnityEngine.AI; // NavMeshAgentを使うために必要

public class EnemyAIController : MonoBehaviour
{
    // 状態の定義
    public enum AIState
    {
        Wandering, // 徘徊
        Chasing,   // 追跡
        Attacking  // 攻撃（今回は使いませんが拡張用に）
    }

    [Header("AIの状態")]
    public AIState currentState = AIState.Wandering; // 現在の状態

    [Header("参照するコンポーネント")]
    public Transform player; // プレイヤーのTransform
    private NavMeshAgent agent; // NavMeshAgent

    [Header("徘徊の設定")]
    public float wanderRadius = 10f; // 徘徊する範囲の半径
    public float wanderTimer = 5f; // 次の徘徊先を決めるまでの時間
    private float timer; // 時間計測用

    [Header("追跡の設定")]
    public float sightRadius = 15f; // プレイヤーを認識する距離

    void Start()
    {
        // 必要なコンポーネントを取得
        agent = GetComponent<NavMeshAgent>();
        // "Player"タグのついたオブジェクトを探してplayer変数に格納
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        timer = wanderTimer;
        currentState = AIState.Wandering; // 初期状態を徘徊に設定
    }

    void Update()
    {
        // 状態に応じて、それぞれの処理を呼び出す
        switch (currentState)
        {
            case AIState.Wandering:
                Wandering();
                break;
            case AIState.Chasing:
                Chasing();
                break;
        }
    }

    // --- 状態ごとの処理 ---

    // 徘徊処理
    void Wandering()
    {
        timer += Time.deltaTime;

        // 設定した時間が経過したら、新しい徘徊先を探す
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    // 追跡処理
    void Chasing()
    {
        // プレイヤーの位置を目的地に設定し、追いかけ続ける
        agent.SetDestination(player.position);
    }

    // --- 状態遷移のきっかけ (Trigger) ---

    // SphereColliderのトリガー範囲内に他のColliderが入った時に呼ばれる
    void OnTriggerEnter(Collider other)
    {
        // 入ってきたのがプレイヤーなら、状態を「追跡」に変更
        if (other.transform == player)
        {
            currentState = AIState.Chasing;
            Debug.Log("プレイヤー発見！追跡開始！");
        }
    }

    // SphereColliderのトリガー範囲内から他のColliderが出た時に呼ばれる
    void OnTriggerExit(Collider other)
    {
        // 出ていったのがプレイヤーなら、状態を「徘徊」に変更
        if (other.transform == player)
        {
            currentState = AIState.Wandering;
            Debug.Log("プレイヤーを見失った。徘徊を再開。");
        }
    }

    // --- ヘルパー関数 ---

    // 指定された中心点から、指定半径内のNavMesh上のランダムな位置を返す
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    // 索敵範囲をギズモで視覚化するためのコード
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}