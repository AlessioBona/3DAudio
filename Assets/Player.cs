using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float rotationSpeed;
    public float walkSpeed;

    public float rotationCorrection;

    public bool moveForward = false;
    public bool moveBackward = false;

    public float globalPitch = 0.5f;
    public GameObject[] allFriends;
    private int friendIndex = 0;
    private int i;

    public AudioSource sonar;

    Vector3 rotation = new Vector3(0f, 0f, 0f);
    float zAngle = 0;
    Rigidbody rb;

    private void Awake()
    {
        sonar = GetComponentInChildren<AudioSource>();
    }


    public void SetupFriends() {
        i = 1;
        foreach (GameObject friend in allFriends) {
            friend.GetComponent<AudioSource>().volume = 0f;

            // Harmonic pitches
            friend.GetComponent<AudioSource>().pitch = globalPitch * i;
            i += 1;
        }
    }

    public void TriggerFriend() {
        allFriends[friendIndex].GetComponent<AudioSource>().volume = 1f;
        friendIndex += 1;
    }


    void Start () {
        SetupFriends();
        TriggerFriend();

        Input.gyro.enabled = true;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.parent.name == allFriends[friendIndex - 1].name) {
            TriggerFriend();
        }
    }


    private void Update()
    {
        Sonar();
    }

    public float sonarReach = 80f;
    public float sonarMinPitch = 0.01f;
    public float sonarMaxPitch = 0.5f;

    private void Sonar()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward*10, Color.green);
        if(Physics.Raycast(transform.position, transform.forward, out hit, sonarReach))
        {
            if(hit.transform.tag == "Wall")
            {
                
                Vector3 hitPoint = hit.point;
                float hitDistance = Vector3.Distance(hitPoint, transform.position);
                float newConstant = Mathf.Log(hitDistance, 2);
                if (newConstant < .7f)
                {
                    newConstant = .7f;
                }

                sonar.pitch = (sonarMaxPitch / sonarReach) * (sonarReach - hitDistance) * 0.5f + sonarMaxPitch * (1f / newConstant);


            }
            
        }
        else
        {
            sonar.pitch = sonarMinPitch;
        }
    }


    void UpdateRotation()
    {
        zAngle += Input.gyro.rotationRate.x;
    }

    public void forwadPress()
    {
        moveForward = true;
    }

    public void forwardReleased()
    {
        moveForward = false;
    }

    public void backwardPress()
    {
        Debug.Log("pressed Back");

        moveBackward = true;
    }

    public void backwardReselased()
    {
        moveBackward = false;
    }

    // Update is called once per frame
    void FixedUpdate () {

        rotation = transform.rotation.eulerAngles;


        UpdateRotation();

        //rotation.y -= Input.gyro.rotationRateUnbiased.z;
        rotation.y -= Input.gyro.rotationRateUnbiased.z * rotationCorrection;


        transform.eulerAngles = rotation;



        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TurnRight(false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            TurnRight(true);
        }
        if (Input.GetKey(KeyCode.UpArrow) || moveForward)
        {
            WalkForward();
        }
        if (Input.GetKey(KeyCode.DownArrow) || moveBackward)
        {
            WalkBackward();
        }

    }

    void TurnRight(bool toTheRight)
    {
        if (toTheRight)
        {
            gameObject.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        if (!toTheRight)
        {
            gameObject.transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
    }

    public void WalkForward()
    {
            gameObject.transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
    }

    public void WalkBackward()
    {
        gameObject.transform.Translate(-Vector3.forward * walkSpeed * Time.deltaTime);
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    switch (other.gameObject.transform.parent.name)
    //    {
    //        case "Jeremy":
    //            Debug.Log(other.gameObject.transform.parent.name);
    //            if (!GetComponentInChildren<AudioSource>().isPlaying)
    //            {
    //                GetComponentInChildren<AudioSource>().Play();
    //            }
    //            break;
    //        default:
    //            break;

    //    }

    //    //switch (other.gameObject.transform.parent.name)
    //    //{
    //    //    case "Jeremy":
    //    //        Debug.Log(other.gameObject.transform.parent.name);
    //    //        otherFriends[0].GetComponent<AudioSource>().volume = 1f;
    //    //        break;
    //    //    case "Luis":
    //    //        Debug.Log(other.gameObject.transform.parent.name);
    //    //        otherFriends[1].GetComponent<AudioSource>().volume = 1f;
    //    //        break;
    //    //    case "TinkerBell":
    //    //        Debug.Log(other.gameObject.transform.parent.name);
    //    //        otherFriends[2].GetComponent<AudioSource>().volume = 1f;
    //    //        break;
    //    //    case "Karl":
    //    //        Debug.Log(other.gameObject.transform.parent.name);
    //    //        if (!GetComponentInChildren<AudioSource>().isPlaying)
    //    //        {
    //    //            GetComponentInChildren<AudioSource>().Play();
    //    //        }
    //    //        break;
    //    //    default:
    //    //        break;

    //    //}
    //}

    }
