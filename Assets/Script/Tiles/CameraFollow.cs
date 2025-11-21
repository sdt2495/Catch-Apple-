using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // プレイヤー
    public Vector3 offset = new Vector3(0, 0, -10); // カメラの高さ

    public float smoothSpeed = 5f;  // 追従のスムーズさ

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
    }
}
