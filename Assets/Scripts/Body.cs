using UnityEngine;
using UnityEngine.UI;

public class Body : MonoBehaviour
{
    public Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);
    public float mass;
    public float drag;

    public bool launched;
    public bool landed;

    public Text distanceText;

    private bool showDistanceLines = false;

    private void Update()
    {
        //Pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale < 1)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;

            showDistanceLines = !showDistanceLines;
        }
    }

    public void Simulate(Vector3 acc, float dt)
    {
        if (transform.position.y > 0)
        {
            vel = vel + acc * mass * dt;
            transform.position = transform.position + vel * dt;
        }
        else
            landed = true;
    }
    private void OnDrawGizmos()
    {
        if (showDistanceLines)
        {
            Gizmos.color = Color.green;
            Vector3 yLineStart = new Vector3(0, 0, 0);
            Vector3 yLineEnd = new Vector3(transform.position.x, 0, transform.position.z);
            Gizmos.DrawLine(yLineStart, yLineEnd);

            // Calculate the green line distance
            float greenLineDistance = Vector3.Distance(Vector3.zero, yLineEnd);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(yLineEnd, transform.position);

            // Calculate the blue line distance
            float blueLineDistance = Vector3.Distance(yLineEnd, transform.position);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(Vector3.zero, transform.position);

            // Calculate the red line distance
            float redLineDistance = Vector3.Distance(Vector3.zero, transform.position);

            // Display the distances in the UI Text
            distanceText.text = "Adjacent Length(Green): " + greenLineDistance.ToString("F2") + "\n" +
                                "Opposite Length(Blue): " + blueLineDistance.ToString("F2") + "\n" +
                                "Hypotenuse Length(Red): " + redLineDistance.ToString("F2");
            
        }
    }
}
