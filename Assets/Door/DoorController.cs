using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string requiredKeyID = "RedKey";

    private bool isPlayerInRange = false;
    private bool isDoorOpen = false;

    // プレイヤーがトリガー範囲に入った時に呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        // ★デバッグログ1：何かが範囲に入ったことを知らせる
        Debug.Log(gameObject.name + " のトリガーに " + other.gameObject.name + " が入りました。");

        // それがプレイヤーかどうかタグで確認
        if (other.CompareTag("Player"))
        {
            // ★デバッグログ2：プレイヤーだったことを知らせる
            Debug.Log("侵入したのはプレイヤーです。isPlayerInRange を true にします。");
            isPlayerInRange = true;
        }
    }

    // プレイヤーがトリガー範囲から出た時に呼ばれる
    private void OnTriggerExit(Collider other)
    {
        // ★デバッグログ3：何かが出ていったことを知らせる
        Debug.Log(gameObject.name + " のトリガーから " + other.gameObject.name + " が出ました。");

        if (other.CompareTag("Player"))
        {
            // ★デバッグログ4：プレイヤーが出ていったことを知らせる
            Debug.Log("出ていったのはプレイヤーです。isPlayerInRange を false にします。");
            isPlayerInRange = false;
        }
    }

    // 毎フレーム呼ばれる
    private void Update()
    {
        // Eキーが押された瞬間かチェック
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ★デバッグログ5：Eキーが押されたことを知らせる
            Debug.Log("Eキーが押されました。現在の isPlayerInRange は " + isPlayerInRange + " です。");

            // プレイヤーが範囲内にいる場合のみ、先の処理に進む
            if (isPlayerInRange)
            {
                // ★デバッグログ6：範囲内だったので、鍵のチェックに進む
                Debug.Log("プレイヤーは範囲内にいます。鍵のチェックを開始します。");
                
                PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
                
                if (playerInventory != null && playerInventory.HasKey(requiredKeyID))
                {
                    isDoorOpen = !isDoorOpen; 
                    if (isDoorOpen) { doorAnimator.SetTrigger("Open"); }
                    else { doorAnimator.SetTrigger("Close"); }
                }
                else
                {
                    Debug.Log("鍵のチェックに失敗しました。");
                }
            }
        }
    }
}