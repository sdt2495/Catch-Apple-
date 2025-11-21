using UnityEngine;

public class GroundMiss : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            GameManager.instance.Miss(); // Å© É~ÉX
        }
    }
}
