using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public List<Transform> insiders;

    private void Awake()
    {
        insiders = new List<Transform>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.name);
        if (!insiders.Contains(other.transform.parent))
        {
            insiders.Add(other.transform.parent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.name == "Player")
        {
            Debug.Log("Player Entered");
        }
    }
}
