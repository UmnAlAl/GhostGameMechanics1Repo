using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour {

	public Text text;
	public Text text2;
	public Text text3;
	public Text text4;
    public Text textBtnSwitchMvMode;
	public GameObject gyroControlObj;
    public GameObject cmcObj;
    private GyroControl gyroControl;
    private CharacterMovementControl cmc;

	void Start() {
		gyroControl = gyroControlObj.GetComponent<GyroControl> ();
        cmc = cmcObj.GetComponent<CharacterMovementControl>();
	}

	// Update is called once per frame
	void Update () {
		text.text = "GPS Lat: " + GPSControl.Instance.latitude.ToString ()
			+ " Long: " + GPSControl.Instance.longitude.ToString ()
			+ " Alt: " + GPSControl.Instance.altitude.ToString ();
		if (gyroControl.gyroEnabled) {
            //Vector3 userAcc = gyroControl.gyro.userAcceleration;
            //text2.text = "Acceleration magn: " + cmc._debug_AcelerationMagn.ToString();
            text2.text = "Acc avg: " + cmc._debug_md_.linearAccAverage.ToString("0.0000")
                + "\t -cnst: " + cmc._debug_md_.linearAccClearedFromConstant.ToString("0.0000")
                + "\t energ: " + cmc._debug_md_.linearAccEnergy.ToString();
            /*text2.text = "Acceleration z: " + gyroControl.gyro.userAcceleration.z.ToString()
				+ " y: " + gyroControl.gyro.userAcceleration.y.ToString()
				+ " x: " + gyroControl.gyro.userAcceleration.x.ToString();*/
            /*text4.text =  "Cam_frwrd: " + gyroControl.cameraObject.transform.forward.ToString()
				+ " Position: " + gyroControl.cameraObject.transform.position.ToString();*/
            text4.text = "Gyro avg-cnst: " + cmc._debug_md_.gyroRotationSpeedClearedFromConstant.ToString()
                + "\t energ: " + cmc._debug_md_.gyroRotationEnergy.ToString()
                + "\t var: " + cmc._debug_md_.gyroRotationVariance.ToString();
        }
        text3.text = "Move speed: " + cmc.moveSpeed.ToString() + " AccVar: " + cmc._debug_md_.linearAccVariance;
    }

	public void OnSpeedChange(float delta) {
        //gyroControl.speed += delta;
        cmc.moveSpeed += delta;
	}

    public void OnChengeMovModeClick()
    {
        cmc.switchMovementMode();
        if(cmc.isAccMovement)
        {
            textBtnSwitchMvMode.text = "Switch mov to GPS";
        }
        else
        {
            textBtnSwitchMvMode.text = "Switch mov to Acclrmtr";
        }
    }

}//class
