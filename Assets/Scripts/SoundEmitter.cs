using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour {

    AudioSource audioSource;
    public Transform audioPosition;

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        audioPosition = audioSource.transform;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
