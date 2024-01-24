using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float gravity = 9.81f;
    public bool bCollided = false;
    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;

        rb = GetComponent<Rigidbody>();
        bCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bCollided)
        {
            /*// Smoothly tilts a transform towards a target rotation.
            float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
            float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);*/

            transform.rotation = Input.gyro.attitude;

            rb.AddForce(transform.up * -gravity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
    }
}
