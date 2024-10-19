using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class TriggerExit : MonoBehaviour
{
    public float delay = 5f;

    public delegate void ExitAction();
    public static event ExitAction OnChunkExited;

    private bool exited = false;

    private GameObject Chunk;

    private void Awake()
    {
        Chunk = transform.parent.gameObject; // Set Chunk to the parent LevelBlock object
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Chunk exit triggered");
        CarTag carTag = other.GetComponent<CarTag>();
        if (carTag != null)
        {
            if (!exited)
            {
                exited = true;
                OnChunkExited?.Invoke();
                StartCoroutine(WaitAndDestroy());
            }
        }
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy(Chunk); // Destroy the parent LevelBlock object
        Debug.Log("Chunk destroyed");
    }
}
