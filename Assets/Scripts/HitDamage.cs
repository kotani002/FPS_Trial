using UnityEngine;
using TMPro;
using System.Collections;

public class HitDamage : MonoBehaviour
{
    [SerializeField]
    private GameObject hitDamagePrefab;
    [SerializeField]
    private float duration = 1.0f; // フェードアウトの時間
    [SerializeField]
    private float speed = 1.0f; // 移動速度
    private Transform cameraTransform;

    public void ViewHitDamage(int damage)
    {
        // ダメージ演出 表示時にコルーチンを起動。1秒かけて上に移動しながらフェードアウトさせる
        StartCoroutine(FadeOutDamage(damage));
        // メインカメラのTransformを取得
        cameraTransform = Camera.main.transform;
    }

    private IEnumerator FadeOutDamage(int damage)
    {
        // Hit時にTMPのテキストを表示させる
        GameObject hitDamage = Instantiate(hitDamagePrefab, this.transform.position, Quaternion.identity);
        //hitDamage.transform.LookAt(cameraTransform, Vector3.up);
        hitDamage.transform.SetParent(this.transform);

        // 1秒かけて上に移動しながらフェードアウトさせる
        // 初期位置をランダムに設定
        Vector3 randomOffset = new Vector3(Random.Range(-0.25f, 0.25f), 0 + 1f, 0);
        hitDamage.transform.localPosition = randomOffset; // 初期位置をリセット
        float elapsed = 0f; // 経過時間
        TextMeshPro textMesh = hitDamage.GetComponent<TextMeshPro>();
        textMesh.text = damage.ToString(); // ダメージ値をテキストに設定
        Color originalColor = textMesh.color; // 元の色を保存

        while (elapsed < duration)
        {
            // 上に移動
            hitDamage.transform.localPosition += Vector3.up * Time.deltaTime; // 上に移動
            hitDamage.transform.LookAt(cameraTransform);
            hitDamage.transform.Rotate(0, 180, 0); // Y軸を基準に反転
            elapsed += Time.deltaTime * speed; // 経過時間を更新

            // サイズを小さくしていく
            float scale = Mathf.Lerp(1f, 0.1f, elapsed / duration); // サイズを計算
            hitDamage.transform.localScale = new Vector3(scale, scale, scale); // サイズを更新

            // フェードアウト
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration); // アルファ値を計算
            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // 色を更新
            yield return null; // 次のフレームまで待機
        }
        Destroy(hitDamage); // ダメージ表示オブジェクトを削除

        //ここを追加したよ
    }
}
