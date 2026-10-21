using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField, Range(1f, 30f)] public float lifeTime = 10f;

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(gameObject); // Destroys itself after the set lifeTime
    }
}
