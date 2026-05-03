using UnityEngine;
using DG.Tweening;	//DOTweenを使うときはこのusingを入れる

public class ShotShaker : MonoBehaviour
{
    Quaternion originalRotation; // 元の回転を保存する変数

    bool isShaking = false; // 振動中かどうかのフラグ

    void Start()
    {
        originalRotation = transform.localRotation; // 元の回転を保存
    }
    public void Shake()
    {
        // カメラを上方向に向かせる
        // カメラを前方向に揺らす
        // DoRotateを使用して現在のx軸から5度だけ回転させる
        isShaking = true;
        
    }

    void Update()
    {
        if(isShaking)
        {
            // 現在のxの角度-5fの角度まで変更、0.01秒かけて回転させる
            // 回転が完了したら、元の位置に戻す
            transform.DOLocalRotate(new Vector3(-3f, 0f, 0f), 0.05f).OnComplete(() =>
            {
                transform.localRotation = originalRotation; // 元の回転に戻す
                isShaking = false;
            });
        }
    } 
}
