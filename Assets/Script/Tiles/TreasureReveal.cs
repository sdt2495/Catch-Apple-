using UnityEngine;

public class TreasureReveal : MonoBehaviour
{
    public float checkDistance = 1.1f;  // 四方向の距離
    public float revealAlpha = 0.4f;    // 透ける透明度
    private float normalAlpha = 1.0f;

    private Renderer rend;
    private Color originalColor;
    private Transform player;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;

        // プレイヤーを自動で取得（任意）
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        // プレイヤーが4方向にいるかチェック
        if (IsPlayerIn4Directions())
        {
            SetAlpha(revealAlpha);  // 透ける
        }
        else
        {
            SetAlpha(normalAlpha);  // 元に戻す
        }
    }

    bool IsPlayerIn4Directions()
    {
        Vector3 pos = transform.position;
        Vector3 p = player.position;

        // X方向 + 隣接
        if (Mathf.Abs(pos.x - p.x) < checkDistance &&
            Mathf.Abs(pos.y - p.y) < 0.5f)
            return true;

        // Y方向 + 隣接
        if (Mathf.Abs(pos.y - p.y) < checkDistance &&
            Mathf.Abs(pos.x - p.x) < 0.5f)
            return true;

        return false;
    }

    void SetAlpha(float a)
    {
        Color c = originalColor;
        c.a = a;
        rend.material.color = c;
    }
}
