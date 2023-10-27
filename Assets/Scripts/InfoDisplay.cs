using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    public Transform targetObject;
    public Text positionText;
    public Text velocityText;
    public Text launchSpeedText;
    public Text pitchText;
    public Text yawText;
    public Text timeInAirText;  // Add this variable

    private ProjectileBody projectileBody;  // Add this variable

    void Start()
    {
        // Attempt to get the ProjectileBody component from the targetObject
        projectileBody = targetObject.GetComponent<ProjectileBody>();
    }

    void Update()
    {
        if (targetObject != null)
        {
            // Display position
            positionText.text = "Position: " + targetObject.position.ToString("F2");

            if (projectileBody != null)
            {
                // Display velocity, launch speed, pitch, and yaw
                velocityText.text = "Velocity: " + projectileBody.vel.ToString("F2");
                launchSpeedText.text = "Launch Speed: " + projectileBody.launchSpeed.ToString("F2");
                pitchText.text = "Pitch: " + projectileBody.launchPitch.ToString("F2");
                yawText.text = "Yaw: " + projectileBody.launchYaw.ToString("F2");

                if (projectileBody.launched)
                {
                    // Calculate time in air if the projectile is launched
                    float timeInAir;
                    if (projectileBody.landed)
                    {
                        // If the projectile has landed, calculate the time from launch to landing
                        timeInAir = projectileBody.landTime - projectileBody.launchTime;
                    }
                    else
                    {
                        // If the projectile is still in the air, calculate the time from launch to the current time
                        timeInAir = Time.time - projectileBody.launchTime;
                    }

                    // Display time in air
                    timeInAirText.text = "Time in Air: " + timeInAir.ToString("F2") + " seconds";
                }
                else
                {
                    // Clear the time in air text if the projectile has not been launched
                    timeInAirText.text = "Time in Air: ";
                }
            }
            else
            {
                // Clear the text fields if no ProjectileBody component is found
                velocityText.text = "Velocity: ";
                launchSpeedText.text = "Launch Speed: ";
                pitchText.text = "Pitch: ";
                yawText.text = "Yaw: ";
                timeInAirText.text = "Time in Air: ";
            }
        }
    }
}

