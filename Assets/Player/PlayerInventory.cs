using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public HashSet<string> collectedKeyIDs = new HashSet<string>();

    public void AddKey(string keyID)
    {
        // ★デバッグログ
        Debug.Log("PlayerInventory の AddKey が ID: '" + keyID + "' で呼び出されました。");

        if (!collectedKeyIDs.Contains(keyID))
        {
            collectedKeyIDs.Add(keyID);
            Debug.Log("鍵「" + keyID + "」をインベントリに追加しました。");
        }
    }

    public bool HasKey(string keyID)
    {
        bool result = collectedKeyIDs.Contains(keyID);
        // ★デバッグログ
        Debug.Log("PlayerInventory の HasKey で ID: '" + keyID + "' の所持チェック。結果: " + result);
        return result;
    }
}