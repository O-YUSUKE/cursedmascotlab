using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraEffectController : MonoBehaviour
{
    public Volume postProcessVolume;

    private Vignette vignette;
    private Bloom bloom;
    
    // エフェクトのON/OFF状態を記憶するフラグ
    private bool isVignetteEffectActive = false;
    private bool isBloomEffectActive = false;
    private bool isFlipped = false;

    // ★★★ Inspectorで設定した初期値を記憶するための変数を追加 ★★★
    private float initialVignetteIntensity;
    private float initialBloomIntensity;

    void Start()
    {
        // 各エフェクトの情報を取得
        postProcessVolume.profile.TryGet(out vignette);
        postProcessVolume.profile.TryGet(out bloom);
        
        // ★★★ ゲーム開始時の値を記憶する処理 ★★★
        if (vignette != null)
        {
            // 現在のVignetteのIntensity値を記憶
            initialVignetteIntensity = vignette.intensity.value;
        }
        if (bloom != null)
        {
            // 現在のBloomのIntensity値を記憶
            initialBloomIntensity = bloom.intensity.value;
        }
    }

    void Update()
    {
        // --- Vignetteのトグル ---
        if (Input.GetKeyDown(KeyCode.V))
        {
            isVignetteEffectActive = !isVignetteEffectActive;

            if (isVignetteEffectActive)
            {
                // ONにする時は、指定の強い値（例: 0.8f）にする
                vignette.intensity.value = 10f;
                Debug.Log("Vignette効果を強くしました");
            }
            else
            {
                // ★★★ OFFにする時は、記憶しておいた「元の値」に戻す ★★★
                vignette.intensity.value = initialVignetteIntensity;
                Debug.Log("Vignette効果を元に戻しました");
            }
        }

        // --- Bloomのトグル ---
        if (Input.GetKeyDown(KeyCode.B))
        {
            isBloomEffectActive = !isBloomEffectActive;

            if (isBloomEffectActive)
            {
                // ONにする時は、指定の強い値にする
                bloom.intensity.value = 100f;
                Debug.Log("Bloom効果を強くしました");
            }
            else
            {
                // ★★★ OFFにする時は、記憶しておいた「元の値」に戻す ★★★
                bloom.intensity.value = initialBloomIntensity;
                Debug.Log("Bloom効果を元に戻しました");
            }
        }
        
        // --- 画面の左右反転 ---
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlipped = !isFlipped;
            FlipCamera(isFlipped);
            Debug.Log("画面を左右反転: " + isFlipped);
        }
    }

    private void FlipCamera(bool flip)
    {
        Camera cam = Camera.main;
        Matrix4x4 mat = cam.projectionMatrix;
        mat.m00 = flip ? -1f : 1f;
        cam.projectionMatrix = mat;
    }
}