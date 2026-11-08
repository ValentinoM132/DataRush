using UnityEngine;

public class ObstacleAnimationControl : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField, Range(0f, 5f)] float animationSpeed = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator != null)
        {
            animator.speed = animationSpeed;
        }
    }
}
