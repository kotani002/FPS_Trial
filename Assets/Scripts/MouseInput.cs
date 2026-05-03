using UnityEngine;
using System;
using Unity.VisualScripting;
using Cinemachine;

public class MouseInput : MonoBehaviour
{
    [Header("弾の速度")]
    [SerializeField, Range(10, 100)]
    private float bulletSpeed = 10f; // 弾の速度
    [Header("長押しした際のクリック感覚")]
    [SerializeField, Range(0, 10)]
    private float clickInterval = 0.15f; // クリック間隔
    private float elapsed = 0f; // 経過時間
    [Header("弾が当たる最大距離")]
    [SerializeField, Range(0, 10)]
    private float maxDistance = 10.0f; // レイの最大距離
    [Header("弾が出る時の音")]
    [SerializeField]
    private AudioClip audioClip; // クリック音
    [SerializeField]
    private AudioSource audioSource; // AudioSourceコンポーネント

    [SerializeField]
    private GameObject bullet = null;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private ParticleSystem particle;
    [SerializeField]
    private CinemachineImpulseSource cinemachineImpulseSource;
    [SerializeField]
    private ShotShaker shotShaker;

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

            //bulletのインスタンスを生成する。
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = target.transform.position;
            newBullet.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);

            // AudioSourceコンポーネントを追加してクリック音を再生する
            //SEPlay();
            
            // 補正
            //newBullet.GetComponent<Rigidbody>().AddForce((newBullet.transform.forward + new Vector3(-0.01f, 0, 0)) * bulletSpeed, ForceMode.Impulse);
            
            // カメラを振動させる
            //cinemachineImpulseSource.GenerateImpulse();

            // オブジェクトを振動させる
            //shotShaker.Shake();
        }

        if (Input.GetMouseButtonUp(0))
        {
            elapsed = clickInterval; // 経過時間をリセット
        }
    }

    private void SEPlay()
    {
        // AudioSourceコンポーネントを追加してクリック音を再生する
        audioSource.clip = audioClip;
        audioSource.time = 0.1f; // オフセット分の時間を設定
        audioSource.Play();
    }
}
