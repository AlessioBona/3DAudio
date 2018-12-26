using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float rotationSpeed;
    public float walkSpeed;

    public float rotationCorrection;

    public bool moveForward = false;
    public bool moveBackward = false;

    //public GameObject[] otherFriends;

    public AudioSource sonar;

    Vector3 rotation = new Vector3(0f, 0f, 0f);
    float zAngle = 0;
    Rigidbody rb;

    private void Awake()
    {
        sonar = GetComponentInChildren<AudioSource>();
        //foreach(GameObject friend in otherFriends)
        //{
        //    friend.GetComponent<AudioSource>().volume = 0f;
        //}
    }

    // Use this for initialization
    void Start () {
        Input.gyro.enabled = true;
        rb = GetComponent<Rigidbody>();
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
                Debug.Log(newConstant);

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
        Debug.Log("back");
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.transform.parent.name)
        {
            case "Jeremy":
                Debug.Log(other.gameObject.transform.parent.name);
                if (!GetComponentInChildren<AudioSource>().isPlaying)
                {
                    GetComponentInChildren<AudioSource>().Play();
                }
                break;
            default:
                break;

        }

        //switch (other.gameObject.transform.parent.name)
        //{
        //    case "Jeremy":
        //        Debug.Log(other.gameObject.transform.parent.name);
        //        otherFriends[0].GetComponent<AudioSource>().volume = 1f;
        //        break;
        //    case "Luis":
        //        Debug.Log(other.gameObject.transform.parent.name);
        //        otherFriends[1].GetComponent<AudioSource>().volume = 1f;
        //        break;
        //    case "TinkerBell":
        //        Debug.Log(other.gameObject.transform.parent.name);
        //        otherFriends[2].GetComponent<AudioSource>().volume = 1f;
        //        break;
        //    case "Karl":
        //        Debug.Log(other.gameObject.transform.parent.name);
        //        if (!GetComponentInChildren<AudioSource>().isPlaying)
        //        {
        //            GetComponentInChildren<AudioSource>().Play();
        //        }
        //        break;
        //    default:
        //        break;

        //}
    }

    }
