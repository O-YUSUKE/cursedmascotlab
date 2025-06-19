using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [SerializeField] private string keyID = "DefaultKey";

    // 他のColliderがこのオブジェクトのトリガーに侵入したときに呼び出される関数
    private void OnTriggerEnter(Collider other)
    {
        // ★デバッグログ1：まずは、このイベントがそもそも発生しているか確認
        Debug.Log(gameObject.name + " の OnTriggerEnter が " + other.gameObject.name + " によって呼び出されました。");

        // 侵入してきたオブジェクトが "Player" タグを持っているか確認
        if (other.CompareTag("Player"))
        {
            // ★デバッグログ2：タグの確認が成功したか
            Debug.Log("衝突したオブジェクトのタグは 'Player' です。");

            // プレイヤーのPlayerInventoryコンポーネントを取得
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            
            // ★デバッグログ3：コンポーネントの取得を試みた
            Debug.Log(other.gameObject.name + " から PlayerInventory コンポーネントの取得を試みます。");

            // PlayerInventoryが見つかった場合
            if (playerInventory != null)
            {
                // ★デバッグログ4：コンポーネントが見つかったか
                Debug.Log("PlayerInventory コンポーネントを発見しました。鍵を追加します。");

                // プレイヤーのインベントリに、この鍵のID(文字列)を追加する
                playerInventory.AddKey(keyID);

                // プレイヤーに取得されたので、この鍵オブジェクトをシーンから破壊（消滅）させる
                Destroy(gameObject);
            }
            else
            {
                // ★デバッグログ5：コンポーネントが見つからなかった場合のエラー表示
                Debug.LogError(other.gameObject.name + " に PlayerInventory コンポーネントが見つかりません！");
            }
        }
        else
        {
            // ★デバッグログ6：タグが違った場合
            Debug.LogWarning("衝突したオブジェクトのタグが 'Player' ではありませんでした。タグ: " + other.tag);
        }
    }
}