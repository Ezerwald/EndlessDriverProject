using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTag : MonoBehaviour
{
    private HoverMotor hoverMotor;
    [SerializeField] private CanvasManager canvasManager;

    private void Start()
    {
        // Get the HoverMotor component attached to the car
        hoverMotor = GetComponent<HoverMotor>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the car collides with an obstacle
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Walls"))
        {
            // Stop the car
            hoverMotor.StopCar();

            // Play particle effect
            hoverMotor.PlayCollisionParticles();

            // Show endgame screen
            canvasManager.EndGame();

            // Optionally, you can add other logic here for what happens after the collision
        }
        else if (collision.gameObject.CompareTag("TeamTrees"))
        {
            // Handle other collisions if needed
        }
        else if (collision.gameObject.CompareTag("LevelBlock"))
        {
            // Handle other collisions if needed
        }
        else if (collision.gameObject.CompareTag("Untagged"))
        {
            Debug.LogWarning("Undefined Object hit - please set the Tag");
        }
    }
}
