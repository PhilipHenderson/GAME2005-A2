using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBody : Body
{
    public Vector3 launchPosition;
    public float launchSpeed;
    public float launchPitch = 0.0f;
    public float launchYaw = 0.0f;
    public float launchTime;
    public float landTime;
    public bool testerAlreadyInScene;

    public GameObject tester;
    public float timeSinceLaunch;

    private void Update()
    {
        testerAlreadyInScene = FindAnyObjectByType<ExperimentTester>();
    }

    public void Launch()
    {
        launched = true;
        // Decompose pitch and yaw into velocity components
        float pitchRadians = Mathf.Deg2Rad * launchPitch;
        float yawRadians = Mathf.Deg2Rad * launchYaw;

        // Calculate initial velocity components
        float initialSpeed = launchSpeed;
        float initialVelocityX = initialSpeed * Mathf.Cos(pitchRadians) * Mathf.Cos(yawRadians);
        float initialVelocityY = initialSpeed * Mathf.Sin(pitchRadians);
        float initialVelocityZ = initialSpeed * Mathf.Cos(pitchRadians) * Mathf.Sin(yawRadians);

        // Set initial velocity components
        vel = new Vector3(initialVelocityX, initialVelocityY, initialVelocityZ);

        // Record the launch time
        launchTime = Time.time;

        //float dragFactor = 0.0f; // Adjust as needed
        //vel *= dragFactor;

        float terminalVelocity = 500.0f;
        if (vel.magnitude > terminalVelocity)
        {
            vel = vel.normalized * terminalVelocity;
        }

        transform.position = launchPosition;
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.Space) && !launched)
        {
            landed = false;
            Launch();
        }
        if(Input.GetKey(KeyCode.R))
        {
            ResetProjectile();
        }
        if (!testerAlreadyInScene)
        {
            if (Input.GetKey(KeyCode.T))
            {
                launchTime = Time.time;
                landTime = 0.0f;
                TestCreator();
            }
        }
        Simulate(new Vector3(0.0f, -1.6f, 0.0f), dt);

        if (launched && !landed)
        {
            // Check if the projectile has landed
            if (transform.position.y <= 0.0f)
            {
                landed = true;
                landTime = Time.time;
                landTime -= launchTime;
            }
        }
    }

    public void ResetProjectile()
    {
        launched = false;
        landed = false;
        vel = Vector3.zero; // Reset velocity to zero
        transform.position = Vector3.zero; // Reset position to zero
        launchTime = 0.0f;
        landTime = 0.0f;
    }

    public void TestCreator()
    {
        Instantiate(tester);
    }

}
