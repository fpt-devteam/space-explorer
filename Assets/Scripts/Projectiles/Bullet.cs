using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private float lifetime = 20f;
    [SerializeField] private float shootCooldown = 5;
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
