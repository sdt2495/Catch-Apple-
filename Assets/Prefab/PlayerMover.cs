using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal") * speed;
        transform.Translate(x * Time.deltaTime, 0, 0);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            ScoreManager.instance.AddScore(1); // Å© í«â¡
        }
    }


}
