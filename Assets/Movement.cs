using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float force = 9.81f;
    public bool bCollided = false;
    float smooth = 5.0f;
    float tiltAngle = 60.0f;
    private GameObject DebugText;
    private GameObject GoalText;
    public float RotationThreshold = 1.635f;
    float xRot, yRot;
    public bool bTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Input.gyro.enabled = true;

        rb = GetComponent<Rigidbody>();
        bCollided = false;

        DebugText = GameObject.FindGameObjectWithTag("DebugRotation"); 
        DebugText.GetComponent<TextMeshProUGUI>().text = "";
        GoalText = GameObject.FindGameObjectWithTag("Goal");
        GoalText.GetComponent<TextMeshProUGUI>().text = "";

    }

    // Update is called once per frame
    void Update()
    {
        if (!bTrigger)
        {
            
            // Smoothly tilts a transform towards a target rotation.
            /*float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
            float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);*/

            //transform.rotation = Input.gyro.attitude;
            transform.Rotate(-Input.gyro.rotationRateUnbiased.x, Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z,Space.Self);

            /*if (transform.rotation.x > RotationThreshold || 
                transform.rotation.y > RotationThreshold || 
                transform.rotation.x < -RotationThreshold || 
                transform.rotation.y < -RotationThreshold)

            {*/
                rb.AddForce(-transform.up * force * Time.deltaTime);
            //}

            if((Input.touchCount > 0 || Input.GetKey(KeyCode.Space)) && rb.velocity.y < 2)
            {
                Debug.Log("KeyDown");

                rb.AddForce(transform.up * force * 20 * Time.deltaTime);
            }
            //DebugText.GetComponent<TextMeshProUGUI>().text = new string("gyroX: " + Input.gyro.rotationRateUnbiased.x * 100 + " gyroy: " +Input.gyro.rotationRateUnbiased.y * 100 + " gyroz: " + Input.gyro.rotationRateUnbiased.z * 100 + "\n\n x:" +transform.rotation.x * 100 + " y:" + transform.rotation.y * 100 + " z:" + transform.rotation.z * 100 + "\n\n x:" + rb.velocity.x.ToString() + " y:" + rb.velocity.y.ToString() + " z:" + rb.velocity.z.ToString());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 PointOfCollision = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        bCollided = true;
        
        if (collision.gameObject.tag == "Finish" && rb.velocity.y > -3)
        {
            
            
            GoalText.GetComponent<TextMeshProUGUI>().text = "Mission Completed!\n\nHit: " +((int)Math.Round((25-Vector3.Distance(new Vector3(-35.89f,10.8f,-5.24f), PointOfCollision))*4)).ToString() +"%";
        } else if(collision.gameObject.tag == "Pelikan")
        {
            GoalText.GetComponent<TextMeshProUGUI>().text = "Pelikan Hit The Fan And You Died!";
        } else
        {
            GoalText.GetComponent<TextMeshProUGUI>().text = "You Missed The Platform And You Died!";
        }


        Time.timeScale = 0;
    }
}
