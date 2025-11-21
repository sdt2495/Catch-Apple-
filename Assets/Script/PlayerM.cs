using UnityEngine;

public class PlayerM : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // 外部から変更できる速度
    private Rigidbody2D rb;
    private Vector2 moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDir = Vector2.zero;

        // WASD操作（現状：上下左右移動）
        if (Input.GetKey(KeyCode.W)) moveDir.y = 1;
        if (Input.GetKey(KeyCode.S)) moveDir.y = -1;
        if (Input.GetKey(KeyCode.D)) moveDir.x = 1;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1;

        moveDir.Normalize();
    }

    void FixedUpdate()
    {
        Vector2 targetPos = rb.position + moveDir * moveSpeed * Time.fixedDeltaTime;

        // 壁判定（Raycastで方向ごとにチェック）
        if (moveDir.x != 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.right * moveDir.x, 0.5f, LayerMask.GetMask("Wall"));
            if (hit.collider != null)
                targetPos.x = rb.position.x;
        }
        if (moveDir.y != 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.up * moveDir.y, 0.5f, LayerMask.GetMask("Wall"));
            if (hit.collider != null)
                targetPos.y = rb.position.y;
        }

        rb.MovePosition(targetPos);
    }
}
