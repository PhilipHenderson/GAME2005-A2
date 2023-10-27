using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ExperimentTester : MonoBehaviour
{
    public GameObject projectilePrefab; // The prefab of the projectile

    private ProjectileBody projectile;

    void Start()
    {
        // Instantiate the projectile from the prefab
        GameObject projectileObject = Instantiate(projectilePrefab);
        projectile = projectileObject.GetComponent<ProjectileBody>();

        // Start the testing process
        StartCoroutine(RunTests());
    }

    IEnumerator RunTests()
    {
        Time.timeScale = 5;
        // Define test cases with elevation angles (in degrees)
        float[] angles = { 3.19f, 6.42f, 9.736f, 13.194f, 16.874f, 20.905f, 25.529f, 31.367f, 
                           45.0f, 58.633f, 64.471f, 69.095f, 73.126f, 76.806f, 80.264f, 83.58f, 86.81f, 90.0f };

        foreach (float angle in angles)
        {
            // Set the launch parameters for the current test case
            projectile.launchPitch = angle;
            projectile.launchSpeed = 120.0f; // Adjust as needed

            // Launch the projectile
            projectile.Launch();

            // Wait for the projectile to land (you can use your logic here)
            while (!projectile.landed)
            {
                yield return null;
            }

            // Record data (position, time, angle, etc.)
            Vector3 landingPosition = projectile.transform.position;
            float landingTime = projectile.landTime;
            float tolerance = 0.0001f; // Define a small tolerance level
            if (Mathf.Abs(landingPosition.x) < tolerance)
            {
                landingPosition.x = 0.0f;
            }

            float roundedLandingTime = (float)Math.Round(landingTime, 2);
            Debug.Log("Angle: " + angle + " degrees, " + "Landing Position: " + landingPosition.x + ", " + "Landing Time: " + roundedLandingTime);

            // Reset the projectile for the next test case
            projectile.ResetProjectile();
        }
    }
}
