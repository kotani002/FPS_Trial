using UnityEngine;

public class BulletHit : MonoBehaviour
{
    public float _damage = 10f; // ダメージ量を設定

    // 弾が当たった時に停止する
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("BulletHit: " + collision.gameObject.name);
        // 弾を停止させる
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // 物理挙動を停止する
        }
        // 弾が当たったオブジェクトのタグが "Target" の場合
        if (collision.gameObject.CompareTag("Target"))
        {
            // HitDamageスクリプトを取得してダメージを表示する
            HitDamage hitDamage = collision.gameObject.GetComponent<HitDamage>();
            if (hitDamage != null)
            {
                hitDamage.ViewHitDamage((int)_damage); // 設定されたダメージ量を表示
            }
        }
    }
}
