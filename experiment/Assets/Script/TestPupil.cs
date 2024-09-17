using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class TestPupil : MonoBehaviour
{
    private InputDevice _leftEye;
    private InputDevice _rightEye;
    public TextMeshPro timeTMP;
    // Start is called before the first frame update
    void Start()
    {
        var leftEyeCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.EyeTracking;
        var rightEyeCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.EyeTracking;

        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(leftEyeCharacteristics, inputDevices);
        if (inputDevices.Count > 0)
        {
            _leftEye = inputDevices[0];
        }

        inputDevices.Clear();
        InputDevices.GetDevicesWithCharacteristics(rightEyeCharacteristics, inputDevices);
        if (inputDevices.Count > 0)
        {
            _rightEye = inputDevices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_leftEye.isValid)
        {
            InputFeatureUsage<float> leftEyePupilDiameterUsage = new InputFeatureUsage<float>("LeftEyePupilDiameter");

            InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.LeftEye);
            if (device != null && device.TryGetFeatureValue(leftEyePupilDiameterUsage, out float leftEyePupilDiameter))
            {
                //Debug.Log("Left eye pupil diameter: " + leftEyePupilDiameter);
                timeTMP.SetText("×óÍ«¿×" + leftEyePupilDiameter);
            }
        }

        if (_rightEye.isValid)
        {
            InputFeatureUsage<float> rightEyePupilDiameterUsage = new InputFeatureUsage<float>("RightEyePupilDiameter");

            InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightEye);
            if (device != null && device.TryGetFeatureValue(rightEyePupilDiameterUsage, out float rightEyePupilDiameter))
            {
                // Debug.Log("Right eye pupil diameter: " + rightEyePupilDiameter);
                timeTMP.SetText("ÓÒÍ«¿×" + rightEyePupilDiameter);
            }

        }
    }
}
