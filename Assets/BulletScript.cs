using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject prefabBullet;
    public float BulletSpeed = 100f;
    public float BulletInterval = 0.5f;
    public float LifeTime = 2f;
    public bool PelikanAttackActivated = true;
    public int Interval, Timing = 60;

    public List<GameObject> StartPos;
    public float x = 0, y = 0, z = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = GameObject.FindGameObjectsWithTag("Pelikan").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if(PelikanAttackActivated && Interval == Timing)
        {
            for (int i = 0; i < StartPos.Count; i++)
            {
                Vector3 direction = new Vector3(Random.Range(-360, 360), Random.Range(0,270),0);
                Quaternion rot = Quaternion.FromToRotation(transform.position, direction);
                GameObject bullet = Instantiate(prefabBullet, StartPos[i].transform.position, rot) as GameObject;
                Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
                bulletRB.AddForce(direction * BulletSpeed, ForceMode.Impulse);
                Destroy(bullet, LifeTime);
            }
            Interval++;
        } else
        {
            if(Interval > Timing)
            {
                Interval = 0;
            } else
            {
                Interval++;
            }
        }
    }
}
