using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour {

    /// <summary>
    /// The maximum velocity achieved by the team
    /// </summary>
    public float MaxVelocity;

    /// <summary>
    /// The acceleration of the team essentially
    /// </summary>
    public float Weight;

    /// <summary>
    /// The likelihood of tackling an opposing teams player
    /// </summary>
    public float TacklingProbability;

    /// <summary>
    /// Determines the accuracy a team has in pursuing the ball. The higher the number, the less variation from the ball
    /// </summary>
    public float Accuracy;


    /// <summary>
    /// How far teammates will push each other physically
    /// </summary>
    public float Aggression;

    private int score;
    
	// Use this for initialization
	void Start () {
        MaxVelocity = Mathf.Clamp(MaxVelocity, 0, 20);
        Weight = Mathf.Clamp(Weight, 0, 100);
        TacklingProbability = Mathf.Clamp(TacklingProbability, 0, 1);
        Accuracy = Mathf.Clamp(Accuracy, 0, 1);
        Aggression = Mathf.Clamp(Aggression, 0, 1);
        score = 0;
	}

    public void scored()
    {
        score++;
    }

    public int getScore()
    {
        return score;
    }
}
