using UnityEngine;
using UnityEngine.SceneManagement; // SceneManagerに必要

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // この変数はInspectorから設定せず、コードで探すようにするためprivateに変更します
    private GameObject gameOverPanel; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            // シーンがロードされた時に呼ばれる関数を登録する
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // シーンがロードされた時に実行される関数
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 新しいシーンで "GameOverUI" というタグが付いたオブジェクトを探す
        // FindGameObjectWithTagは、非アクティブなオブジェクトも探してくれる
        Transform canvas = FindObjectOfType<Canvas>().transform;
        gameOverPanel = canvas.Find("GameOverPanel").gameObject; // Panelの名前が違う場合は修正してください

        // 見つけたUIを、念のため非表示にしておく
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void ShowGameOverScreen()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // 1. この関数が呼ばれたことを知らせる
        Debug.Log("--- RestartGame関数が呼び出されました ---");

        // 2. Time.timeScaleを変更する直前のログ
        Debug.Log("Time.timeScaleを1fに設定します。");
        Time.timeScale = 1f;
        
        // 3. シーン名をログに出す
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("現在のシーン名「" + sceneName + "」の読み込みを開始します。");

        // 4. シーンを読み込む
        SceneManager.LoadScene(sceneName);
    }
    
    // オブジェクトが破棄される時に、登録した関数を解除する（お作法）
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}