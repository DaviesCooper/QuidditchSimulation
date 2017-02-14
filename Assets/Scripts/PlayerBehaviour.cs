using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    bool death;
    private Rigidbody body;
    Vector3 originalPos;
    Quaternion originalRot;
    Attributes atts;
    public GameObject snitch;
    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
        originalPos = transform.position;
        originalRot = transform.rotation;
        death = false;
        atts = transform.parent.GetComponent<Attributes>();
        body.mass = atts.Weight;


    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    /// <summary>
    /// This is the directional behaviour the players follow. It determines where they will move to next
    /// </summary>
    private void FixedUpdate()
    {
        if (death)
        {
            body.useGravity = true;

            if(transform.position.y <= 0.8f)
            {
                goToStart();
            }
        }
        else
        {
            ///Calculate our accuracy. This vector is how close we want to get to the ball
            Vector3 snPos = GameObject.Find("Snitch").transform.position;
            float xcord = snPos.x;
            float ycord = snPos.y;
            float zcord = snPos.z;
            float randNum = UnityEngine.Random.Range(atts.Accuracy, 1);
            xcord *= randNum;
            randNum = UnityEngine.Random.Range(atts.Accuracy, 1);
            ycord *= randNum;
            randNum = UnityEngine.Random.Range(atts.Accuracy, 1);
            zcord *= randNum;
            Vector3 toFollow = new Vector3(xcord, ycord, zcord);
            ///


            Vector3 force = new Vector3(0, 0, 0);
            force = force + ((snPos - transform.position) * UnityEngine.Random.value * atts.Accuracy * 75f);

            ///Use our vector as a force and update our movement/facing direction
            body.AddForce(force, ForceMode.Force);
            body.velocity = Vector3.ClampMagnitude(body.velocity, atts.MaxVelocity);
            transform.rotation = Quaternion.LookRotation(body.velocity);
        }
    }

    /// <summary>
    /// Sends the player back to their starting point
    /// </summary>
    public void goToStart()
    {
        body.isKinematic = true;
        body.useGravity = false;
        transform.position = originalPos;
        transform.rotation = originalRot;
        body.velocity = new Vector3(0,0,0);
        body.isKinematic = false;
        death = false;
    }

    /// <summary>
    /// What happens during a collision.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (death)
        {

        }
        //We have collided with a player on the opposite team
        if((collision.gameObject.tag == "Slytherin" || collision.gameObject.tag == "Gryffindor") && collision.gameObject.tag != tag)
        {
            float prob = collision.gameObject.transform.parent.GetComponent<Attributes>().TacklingProbability;
            if (UnityEngine.Random.value < prob)
            {
                //we have been tackled
                death = true;
                body.AddForce(new Vector3(0,50,0), ForceMode.Impulse);
            }
        }
        //We have collided with a player on the opposite team
        if (collision.gameObject.tag == tag)
        {
            Vector3 force = ((transform.position - collision.gameObject.transform.position) * UnityEngine.Random.value * atts.Aggression * 25f);
            ///Use our vector as a force and update our movement/facing direction
            body.AddForce(force, ForceMode.VelocityChange);
        }
    }
}
