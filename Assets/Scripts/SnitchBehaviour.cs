using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchBehaviour : MonoBehaviour {


    private Rigidbody body;
    Quaternion originalRot;
    /// <summary>
    /// The "randomness" of the snitch.
    /// </summary>
    public float SnitchRandomness;
    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
        originalRot = transform.rotation;
        SnitchRandomness = Mathf.Clamp(SnitchRandomness, 10, 30);
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    private void FixedUpdate()
    {
        float xcord = UnityEngine.Random.Range(-SnitchRandomness, SnitchRandomness);
        float ycord = UnityEngine.Random.Range(-SnitchRandomness, SnitchRandomness);
        float zcord = UnityEngine.Random.Range(-SnitchRandomness, SnitchRandomness);
        body.AddForce(new Vector3(xcord, ycord, zcord), ForceMode.Force);
        transform.rotation = Quaternion.LookRotation(body.velocity);
    }

    public void goToStart()
    {
        body.isKinematic = true;
        float x =UnityEngine.Random.Range(-10f, 10f);
        float z =UnityEngine.Random.Range(-10f, 10f);
        float y =UnityEngine.Random.Range(0f, 2f);
        transform.position = new Vector3(x,y,z);
        transform.rotation = originalRot;
        body.isKinematic = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            return;
        }
        if(collision.gameObject.transform.parent.name == "stands")
        {
            return;
        }
        if (collision.gameObject.transform.parent.tag == "atts")
        {
            goToStart();
            collision.gameObject.transform.parent.GetComponent<Attributes>().scored();
        }
    }
}
