using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour {

    AudioSource sonarSource;

    private void Awake()
    {
        sonarSource = GetComponentInChildren<AudioSource>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public float sonarReach = 120f;
    public float sonarMinPitch = 0.005f;
    public float sonarMaxPitch = 0.6f;

    public void Play()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hit, sonarReach))
        {
            if (hit.transform.tag == "Wall")
            {

                Vector3 hitPoint = hit.point;
                float hitDistance = Vector3.Distance(hitPoint, transform.position);
                float newConstant = Mathf.Log(hitDistance, 2);
                if (newConstant < .7f)
                {
                    newConstant = .7f;
                }
                Debug.Log(newConstant);

                sonarSource.pitch = (sonarMaxPitch / sonarReach) * (sonarReach - hitDistance) * 0.5f + sonarMaxPitch * (1f / newConstant);


            }

        }
        else
        {
            sonarSource.pitch = sonarMinPitch;
        }
    }

    private void CastRay()
    {

    }
}
