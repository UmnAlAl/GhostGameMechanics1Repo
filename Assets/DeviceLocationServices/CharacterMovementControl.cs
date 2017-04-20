using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementControl : MonoBehaviour {

    public bool isGPSMovement = false;
    public bool isAccMovement = true;

    //for GPS
    public GameObject gpsControlObject;
    public GameObject cameraContainer;
    public float moveSpeed = 30;

    public float _debug_DeltaGPS;

    private GPSControl gpsControl;
    private CharacterController characterController;
    private GameObject cameraObject;
    private Camera cam;

    private bool isGPSReadFirstTime = true;

    private Vector3 prevGPS;
    private Vector3 curGPS;

    //for gyro
    public GyroControl gyroControl;
    public AccelerationCleaner acCleaner;

    public AccelerationCleaner.MovementStepData _debug_md_;

    // Use this for initialization
    void Start () {
        gpsControl = gpsControlObject.GetComponent<GPSControl>();
		characterController = cameraContainer.GetComponent<CharacterController>();
        cameraObject = cameraContainer.transform.GetChild(0).gameObject;
        cam = cameraObject.GetComponent<Camera>();
        gyroControl = gameObject.GetComponent<GyroControl>();
        acCleaner = cameraContainer.GetComponent<AccelerationCleaner>();
        acCleaner.Init(gyroControl);
	}
	
	// Update is called once per frame
	void Update () {
        //moveWithKeys();
        //rotateWithKeys();
        if(isGPSMovement)
        {
            doGPSMovement();
        }
        if (isAccMovement)
        {

            doAccelerometerMovement();
        }

	}

    void doGPSMovement()
    {
        float lat = gpsControl.latitude;
        float lon = gpsControl.longitude;
        float alt = gpsControl.altitude;

        if((lat == 0) || (lon == 0)) //low error probability
        {
            return;
        }

        if(isGPSReadFirstTime)
        {
            prevGPS = new Vector3(lat, lon, alt);
            curGPS = new Vector3(lat, lon, alt);
            isGPSReadFirstTime = false;
            return;
        }

        curGPS = new Vector3(lat, lon, alt); //update current position
        float deltaGPSval = (curGPS - prevGPS).magnitude; //delta position between prev and current read
        float gpsScaleFactor = 100000f;
        float deltaGPSCutStep = 9 / gpsScaleFactor;

        _debug_DeltaGPS = deltaGPSval * gpsScaleFactor; //for debug output

        if (deltaGPSval < deltaGPSCutStep) //if we moved too short - return
        {
            return;
        }
        else //else set new prev(base) position and continue
        {
            prevGPS = curGPS;
        }

        Vector3 moveVector = cameraObject.transform.forward * deltaGPSval * moveSpeed * gpsScaleFactor;
        moveVector *= Time.deltaTime;
        characterController.Move(moveVector);
    }

/*
*************************************************************************************************************************************
*/


    void doAccelerometerMovement()
    {
        if(!gyroControl.gyroEnabled)
        {
            return;
        }

        acCleaner.insertData(gyroControl);

        AccelerationCleaner.MovementStepData md;
        Vector3 userAcc = acCleaner.getFilteredAcceleration(out md);
        _debug_md_ = md;

        //float accelerationScaleFactor = 10f;
        Vector3 moveVector;
        moveVector = -cameraObject.transform.forward * userAcc.z * moveSpeed;
        moveVector *= Time.deltaTime;
        characterController.Move(moveVector);

        
    }


/*
*************************************************************************************************************************************
*/

    //for test on PC
    void moveWithKeys()
    {
        float translation = Input.GetAxis("Vertical"); //value of vert axis
        Vector3 moveVector = cameraObject.transform.forward * translation * moveSpeed; //vector of movement
        moveVector *= Time.deltaTime; //expression in time units instead of frame units
        characterController.Move(moveVector);
    }

    //for test on PC
    void rotateWithKeys()
    {
        float rotationSpeed = 100f;
        float rotation = Input.GetAxis("Horizontal");
        rotation *= rotationSpeed * Time.deltaTime;
        cameraObject.transform.Rotate(0, rotation, 0);
    }

    public void switchMovementMode()
    {
        if(isAccMovement)
        {
            isAccMovement = false;
            isGPSMovement = true;
        }
        else
        {
            isAccMovement = true;
            isGPSMovement = false;
        }
    }

    private static float mod(float x)
    {
        if (x >= 0)
        {
            return x;
        }
        return -x;
    }

}
