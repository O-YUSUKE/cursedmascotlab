using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // --- Public Variables (Inspectorから設定) ---
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float characterRotationSpeed = 10f; // プレイヤーが向きを変える速さ
    [SerializeField] private Transform cameraTransform; // カメラのTransform

    // --- Private Variables ---
    private Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // --- 入力の取得 ---
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D または ←/→
        float verticalInput = Input.GetAxis("Vertical");     // W/S または ↑/↓

        // --- カメラの向きを基準にした移動方向の計算 ---
        // カメラの前方向ベクトルを取得し、Y軸（高さ）を0にして水平にする
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        // カメラの右方向ベクトルを取得
        Vector3 camRight = cameraTransform.right;

        // 入力とカメラの向きから、最終的な移動方向を決定
        moveInput = (camForward * verticalInput + camRight * horizontalInput).normalized;

        // --- キャラクターの回転 ---
        // 移動方向のベクトルがゼロより大きい場合（=何かしらの入力がある場合）
        if (moveInput.magnitude > 0.1f)
        {
            // キャラクターが移動方向をスムーズに向くように回転させる
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, characterRotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        // 物理演算を使ってキャラクターを移動させる
        // Y軸の速度(rb.velocity.y)はそのままにすることで、ジャンプや重力に影響を与えない
        rb.linearVelocity = new Vector3(moveInput.x * moveSpeed, rb.linearVelocity.y, moveInput.z * moveSpeed);
    }
}