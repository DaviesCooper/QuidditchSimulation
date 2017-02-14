using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollower : MonoBehaviour {



    private GameObject[] possible;
    private GameObject[] temp;
    private Transform target;
    private int index;

    public Text follows;
    public Text score;

    Attributes gryfAtt;
    Attributes slyAtt;

    private enum cameraState
    { Steady, Action, Moving};

    cameraState state;

    public GameObject steadyCamLooking;


    // Use this for initialization
    void Start () {
        //Setting up the array of things we can toggle between;
        index = 0;
        possible = GameObject.FindGameObjectsWithTag("Snitch");
        temp = GameObject.FindGameObjectsWithTag("Gryffindor");
        int length = possible.Length;
        Array.Resize<GameObject>(ref possible, possible.Length + temp.Length);
        Array.Copy(temp, 0, possible, length, temp.Length);
        length = possible.Length;
        temp = GameObject.FindGameObjectsWithTag("Slytherin");
        Array.Resize<GameObject>(ref possible, possible.Length + temp.Length);
        Array.Copy(temp, 0, possible, length, temp.Length);


        target = possible[0].transform;
        transform.localPosition = target.position + new Vector3(0, 4, 4);
        transform.LookAt(target);
        gryfAtt = GameObject.Find("GryffindorTeam").GetComponent<Attributes>();
        slyAtt = GameObject.Find("SlytherinTeam").GetComponent<Attributes>();
        state = cameraState.Steady;

    }
	
	// Update is called once per frame
    /// <summary>
    /// Changes what the camera does based on the input controls
    /// </summary>
	void Update () {

        #region key state set
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = cameraState.Action;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            state = cameraState.Moving;
            index++;
            index = index % possible.Length;
            target = possible[index].transform;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            state = cameraState.Moving;
            if (index == 0)
            {
                index = possible.Length - 1;
            }
            else
            {
                index--;
            }
            
            target = possible[index].transform;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            state = cameraState.Moving;
            target = GameObject.Find("Snitch").transform;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            state = cameraState.Moving;
            index = index + 10;
            index = index % possible.Length;
            target = possible[index].transform;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            state = cameraState.Moving;
            if (index - 10 < 0)
            {
                index = possible.Length - 1 -  (10 - index);
            }
            else
            {
                index = index - 10;
            }

            target = possible[index].transform;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            state = cameraState.Steady;
        }
        #endregion



        #region state modes
        if (state == cameraState.Moving)
        {
            transform.localPosition = target.position + new Vector3(0, 4, 4);
            if (target.name == "Snitch")
            {
                transform.localPosition = target.position + new Vector3(0, 10, 0);
            }
            
            transform.LookAt(target);
            follows.text = "Following: " + target.name;
            score.text = "gryffindor " + gryfAtt.getScore() + ":" + slyAtt.getScore() + " slytherin";
        }
        else if (state == cameraState.Action)
        {
            Vector3 total = new Vector3(0, 0, 0);
            float maxDistance = 0f;
            foreach(GameObject obj in possible)
            {
                total += obj.transform.position;
                foreach (GameObject obj2 in possible)
                {
                    float distance = Vector3.Distance(obj2.transform.position, obj.transform.position);
                    if (maxDistance < distance)
                    {
                        maxDistance = distance;
                    }
                }
            }
            total = total / possible.Length;
            transform.position = Vector3.Lerp(transform.position, total + new Vector3(0,maxDistance*0.75f,0), Time.smoothDeltaTime);

            Vector3 toAvgFromPos = total - transform.position;
            Vector3 verticalFwd = Vector3.ProjectOnPlane(toAvgFromPos, transform.right);


            //Quaternion temp = Quaternion.LookRotation(total-transform.position, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(verticalFwd, transform.up), Time.smoothDeltaTime);
            //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z));

            follows.text = "Action Cam";
            score.text = "gryffindor "+gryfAtt.getScore() + ":" + slyAtt.getScore()+" slytherin";
        }
        else if (state == cameraState.Steady)
        {
            transform.position = new Vector3(0, 20, 0);
            transform.LookAt(steadyCamLooking.transform);
            transform.Rotate(0, 0, 90);
            follows.text = "Fixed Cam";
            score.text = "gryffindor " + gryfAtt.getScore() + ":" + slyAtt.getScore() + " slytherin";
        }
        #endregion

    }

}
