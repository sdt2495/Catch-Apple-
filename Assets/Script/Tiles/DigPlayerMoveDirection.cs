using UnityEngine;

public class DigPlayerMoveDirection : MonoBehaviour
{
    [Header("移動")]
    public float moveSpeed = 5f;

    [Header("掘る")]
    public DigController digController;
    public float digDistance = 1f;

    [Header("レティクル")]
    public Transform reticle; // プレイヤーの子に矢印やクロスヘアを用意

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDirection = Vector2.right; // デフォルト右向き

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (reticle == null)
            Debug.LogWarning("Reticleがセットされていません");
    }

    void Update()
    {
        // 1. 移動入力
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        // 2. 移動方向がある場合、レティクル向き更新
        if (movement.sqrMagnitude > 0.01f)
        {
            lastDirection = movement;
        }

        // 3. レティクル回転
        if (reticle != null)
        {
            float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
            reticle.rotation = Quaternion.Euler(0, 0, angle);
        }

        // 4. スペースで掘る
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 digPos = (Vector2)transform.position + lastDirection.normalized * digDistance;
            digController.Dig(digPos);
            digController.DigTreasure(digPos);
        }
    }

    void FixedUpdate()
    {
        // Rigidbody2D で移動
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
