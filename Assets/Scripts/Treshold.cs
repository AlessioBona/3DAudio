using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treshold : MonoBehaviour {

    public bool doIt = false;

    [SerializeField]
    Room room1;

    [SerializeField]
    Room room2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (doIt)
        {

            Instantiate(room1.insiders[0].GetComponent<SoundEmitter>().audioPosition, this.transform);



            doIt = false;
        }
	}
}
