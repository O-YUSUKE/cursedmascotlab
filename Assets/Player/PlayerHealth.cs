using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // public GameManager gameManager; ← この行はもう不要なので削除します！

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突した相手のタグが "Enemy" だったら
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("敵と衝突した！");
            
            // GameManagerの「唯一のインスタンス」に直接アクセスして、関数を呼び出す
            GameManager.instance.ShowGameOverScreen();
        }
    }
}