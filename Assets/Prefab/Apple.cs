using UnityEngine;

public class Apple : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        float speed = GameManager.instance.appleSpeed;
        transform.Translate(0, -speed * Time.deltaTime, 0);

        if (transform.position.y < -6f)
            Destroy(gameObject);
    }
}
