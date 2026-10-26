using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField, Range(1f, 30f)] float lifeTime = 10f;
    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(gameObject); // Destroys itself after the set lifeTime
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    /*
    private void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, 5, layerMask))
        {
            Debug.Log("Did Hit");
            Destroy(gameObject);
        }
    }
    */
}
