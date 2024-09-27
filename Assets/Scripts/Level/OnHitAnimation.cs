using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitAnimation : MonoBehaviour
{
    private Animator animator;

    public string onHitTriggerName = "OnHitTrigger";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger(onHitTriggerName);
        }
    }
}
