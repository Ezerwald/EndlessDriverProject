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

    [SerializeField] private GameObject Chunk;

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Chunk exit triggered");
        CarTag carTag = other.GetComponent<CarTag>();
        if (carTag != null)
        {
            if (!exited)
            {
                exited = true;
                OnChunkExited();
                StartCoroutine(WaitAndDestroy());
            }
        }
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds (delay);
        Destroy(Chunk);
        Debug.Log("Chunk destroyed");
        // Destroy the LevelBlock
    }
}
