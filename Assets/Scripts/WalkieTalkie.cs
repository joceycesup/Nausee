using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkieTalkie : MonoBehaviour {
    private micInput micro;
    private float lastUpdateTime;
    private float currentTime;

	public float loudnessThreshold; // sensibilité du micro
    public float soundTimeBeforeDetect; //Vérifie qu'il y a un son toutes les x secondes;
    
	// Use this for initialization
	void Start () {
        micro = gameObject.GetComponent<micInput>();
	}
	
	// Update is called once per frame
	void Update () {
        if (currentTime > soundTimeBeforeDetect)
        {
            currentTime = 0;
            if (isTalking())
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f);
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
            }
            lastUpdateTime = Time.time;
        } else
        {
            currentTime = Time.time - lastUpdateTime;
        }


    }


    public bool isTalking () {
		return micro.volume > loudnessThreshold;
	}
}
