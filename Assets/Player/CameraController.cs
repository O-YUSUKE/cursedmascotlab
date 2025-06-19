using UnityEngine;

public class CameraController : MonoBehaviour
{
    // --- Public Variables (Inspectorから設定) ---
    [SerializeField] private Transform target; // 追従する対象（プレイヤー）
    [SerializeField] private float distance = 10.0f; // プレイヤーからの距離
    [SerializeField] private float rotationSpeed = 120.0f; // カメラの回転速度
    [SerializeField] private Vector2 minMaxPitch = new Vector2(-45, 80); // カメラの上下の角度制限（Xが最小, Yが最大）

    // --- Private Variables ---
    private float yaw = 0.0f;   // 水平方向の回転
    private float pitch = 20.0f;  // 垂直方向の回転

    void Start()
    {
        // ゲーム開始時にカーソルを非表示にして、画面中央にロックする（お好みで）
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }


    // LateUpdateは、すべてのUpdate処理が終わった後に呼ばれる
    // キャラクターが動いた後にカメラを動かすのに適している
    void LateUpdate()
    {
        // ターゲットが設定されていなければ何もしない
        if (target == null) return;

        // --- キー入力で回転角度を更新 ---
        if (Input.GetKey(KeyCode.L)) // Lキーで右回転
        {
            yaw += rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.J)) // Jキーで左回転
        {
            yaw -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.I)) // Iキーで上を見る
        {
            pitch -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.K)) // Kキーで下を見る
        {
            pitch += rotationSpeed * Time.deltaTime;
        }

        // --- 角度の制限 ---
        // pitch（上下の角度）が、設定した最小値と最大値の範囲に収まるように制限する
        pitch = Mathf.Clamp(pitch, minMaxPitch.x, minMaxPitch.y);

        // --- 回転と位置の計算 ---
        // 計算した角度から、カメラの最終的な回転(Quaternion)を決定
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // カメラの最終的な位置を計算
        // (プレイヤーの位置) から (計算した回転の向きに、設定した距離だけ離れた場所) へ配置
        Vector3 position = target.position - (rotation * Vector3.forward * distance);

        // --- カメラへ反映 ---
        transform.position = position;
        transform.rotation = rotation;
    }
}