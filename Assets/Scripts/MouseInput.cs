using UnityEngine;
using System;
using Unity.VisualScripting;

public class MouseInput : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private ParticleSystem particle;
    [SerializeField]
    private float clickInterval = 0.5f; // クリック間隔
    private float elapsed = 0f; // 経過時間
    [SerializeField]
    private float maxDistance = 10.0f; // レイの最大距離
    [SerializeField]
    private AudioClip audioClip; // クリック音
    [SerializeField]
    private AudioSource audioSource; // AudioSourceコンポーネント

    // Update is called once per frame
    void Update()
    {
        // 左クリックされた瞬間
        if (Input.GetMouseButton(0))
        {
            elapsed += Time.deltaTime; // 経過時間を更新
            if (elapsed < clickInterval) // 指定された間隔以上経過している場合に処理を実行
            {
                return; // まだクリック間隔が経過していない場合は処理をスキップ
            }

            elapsed -= clickInterval; // 経過時間をリセット

            // AudioSourceコンポーネントを追加してクリック音を再生する
            audioSource.clip = audioClip;
            audioSource.time = 0.1f; // オフセット分の時間を設定
            audioSource.Play();

            // パーティクルシステムのインスタンスを生成する。
            ParticleSystem newParticle = Instantiate(particle);

            newParticle.gameObject.transform.position = target.transform.position;
            // カメラが見ている方向にパーティクルを回転させる。
            newParticle.gameObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            
            // パーティクルを発生させる。
            newParticle.Play();
            // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
            // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
            Destroy(newParticle.gameObject, 1.0f);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.gameObject.tag == "Target")
                {
                    hit.collider.gameObject.GetComponent<HitDamage>().ViewHitDamage(10);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            elapsed = clickInterval; // 経過時間をリセット
        }
    }
}
