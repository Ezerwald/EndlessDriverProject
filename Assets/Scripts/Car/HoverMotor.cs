using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class HoverMotor : MonoBehaviour
{
    [Header("Hover Car Settings")]
    // Move speed of a car
    public float speed;
    // Turning speed of a car
    public float turnSpeed;
    // Turn smoothing speed of a car
    public float smoothing;
    // Hover force of a car
    public float hoverForce;
    // Hover height of a car
    public float hoverHeight;
    // Burning exhaust of a car
    public ParticleSystem burnerParticles;
    public ParticleSystem collisionParticles;
    // Should the car accelerate
    public bool accelerating = true;

    private Rigidbody carMain;

    // Vehicle lean
    [Header("Leaning Settings")]
    public AnimationCurve vehicleLeanBackRate;
    public AnimationCurve vehicleLeanRate;

    public Transform carBody;

    public float maxLeanAngle = 20f;
    public float leanTime = 0.8f;

    private LeaningDirection leaningDir = LeaningDirection.None;
    private float leanTimeElapsed;

    private float powerInput;
    private float turnInput;
    private bool isStopped = false;

    private float TOLERANCE = 0.001f;

    void Awake()
    {
        carMain = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isStopped)
        {
            UpdateTurnInput();
        }
    }

    void FixedUpdate()
    {
        UpdateFloating();
        UpdateTurning();
        UpdateLeaning();
    }

    private void UpdateTurnInput()
    {
        turnInput = Input.GetAxis("Horizontal");
    }

    private void UpdateFloating()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * (proportionalHeight * hoverForce * Random.Range(1.0f, 1.05f));
            carMain.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }
    }

    private void UpdateTurning()
    {
        float tempInput = 0f;
    
        float smoothedTurn = Mathf.Lerp(turnInput, tempInput, smoothing);

        if (accelerating)
        {
            burnerParticles.Play();
            carMain.AddForce(transform.forward * speed, ForceMode.Acceleration);
            speed += Time.deltaTime;
        }
        else
        {
            burnerParticles.Stop();
        }

        carMain.transform.Rotate(new Vector3(0f, smoothedTurn * turnSpeed, 0f));
    }

    private void UpdateLeaning()
    {
        float zRot = carBody.localEulerAngles.z;

        if (zRot > 180)
            zRot = zRot - 360;

        if (Math.Abs(turnInput) > TOLERANCE)
        {
            LeanIntoAngle(zRot);
        }
        else
        {
            if (leaningDir != LeaningDirection.None)
            {
                leaningDir = LeaningDirection.None;
                leanTimeElapsed = 0;
            }

            LeanBack(zRot);
        }

        leanTimeElapsed += Time.deltaTime;
        Camera.main.transform.localEulerAngles = new Vector3(0f, 0f, zRot / 2f);
    }

    private void LeanIntoAngle(float zRot)
    {
        if (turnInput > 0)
        {
            if (leaningDir != LeaningDirection.Right)
            {
                leaningDir = LeaningDirection.Right;
                leanTimeElapsed = 0;
            }

            if (-maxLeanAngle > zRot)
                return;

            float compensatedAngle = vehicleLeanRate.Evaluate(leanTimeElapsed / leanTime);
            Vector3 leanAngle = Vector3.Lerp(new Vector3(0, 0, zRot), new Vector3(0, 0, -maxLeanAngle), compensatedAngle);
            carBody.localEulerAngles = leanAngle;
        }
        else if (turnInput < 0)
        {
            if (leaningDir != LeaningDirection.Left)
            {
                leaningDir = LeaningDirection.Left;
                leanTimeElapsed = 0;
            }

            if (zRot > maxLeanAngle)
                return;

            float compensatedAngle = vehicleLeanRate.Evaluate(leanTimeElapsed / leanTime);
            Vector3 leanAngle = Vector3.Lerp(new Vector3(0, 0, zRot), new Vector3(0, 0, maxLeanAngle), compensatedAngle);
            carBody.localEulerAngles = leanAngle;
        }
    }

    private void LeanBack(float zRot)
    {
        if (Math.Abs(zRot) > TOLERANCE)
        {
            float compensatedAngle = vehicleLeanBackRate.Evaluate(leanTimeElapsed / leanTime);
            Vector3 leanAngle = Vector3.Lerp(new Vector3(0, 0, zRot), Vector3.zero, compensatedAngle);
            carBody.localEulerAngles = leanAngle;
        }
    }

    enum LeaningDirection
    {
        None,
        Left,
        Right,
    }

    public void StopCar()
    {
        speed = 0;
        isStopped = true;
        carMain.velocity = Vector3.zero;
    }

    public void PlayCollisionParticles()
    {
        if (collisionParticles != null)
        {
            collisionParticles.Play();
        }
    }

}
