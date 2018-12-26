using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{

    AudioSource sonarSource;

    [SerializeField]
    AudioSource discreteSonarSource;

    [SerializeField]
    AudioClip[] materialClips;

    SonarMaterials hitMaterial;
    float hitDistance;

    private void Awake()
    {
        sonarSource = GetComponentInChildren<AudioSource>();
        discreteSonarSource = GetComponent<AudioSource>();
        sonarOn = true;
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(PlaySonar());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaySonar()
    {
        if (sonarOn)
        {
            discreteSonarSource.Play();
        }
        yield return new WaitForSeconds(sonarDelay);
        StartCoroutine(PlaySonar());
    }


    public float sonarReach = 120f;
    public float sonarMinPitch = 0.005f;
    public float sonarMaxPitch = 0.6f;
    private bool sonarOn;
    public float sonarDelay = 1f;

    public void CastRay()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);

        if (Physics.Raycast(transform.position, transform.forward, out hit, sonarReach))
        {
            //if (hit.transform.tag == "Wall")
            //{

            //    Vector3 hitPoint = hit.point;
            //    float hitDistance = Vector3.Distance(hitPoint, transform.position);
            //    float newConstant = Mathf.Log(hitDistance, 2);
            //    if (newConstant < .7f)
            //    {
            //        newConstant = .7f;
            //    }
            //    //Debug.Log(newConstant);

            //    sonarSource.pitch = (sonarMaxPitch / sonarReach) * (sonarReach - hitDistance) * 0.5f + sonarMaxPitch * (1f / newConstant);


            //}

            if (hit.transform.parent.GetComponent<SonarObject>())
            {
                SonarObject sonarObject = hit.transform.parent.GetComponent<SonarObject>();

                Debug.Log((int)sonarObject.material);

                Vector3 hitPoint = hit.point;
                hitDistance = Vector3.Distance(hitPoint, transform.position);

                if (sonarObject.material != hitMaterial)
                {
                    changeMaterial(sonarObject.material);
                }
            }

        }

        //else
        //{
        //    sonarSource.pitch = sonarMinPitch;
        //}
    }

    private void changeMaterial(SonarMaterials material)
    {
        StopAllCoroutines();
        hitMaterial = material;
        discreteSonarSource.clip = materialClips[(int)material];
        StartCoroutine(PlaySonar());
        
    }


}
