using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class TestBlink : MonoBehaviour
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
        Eyes eyes = new Eyes();
        
        if (_leftEye.isValid)
        {
            InputFeatureUsage<float> leftEyeOpenAmountUsage = new InputFeatureUsage<float>("leftEyeOpenAmount");
            InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.LeftEye);

            //获取左眼张开值
            bool flag = eyes.TryGetLeftEyeOpenAmount(out float leftOpenAmount);

            if (device != null && flag != false && device.TryGetFeatureValue(leftEyeOpenAmountUsage,out leftOpenAmount))
            {
               // Debug.Log("Left eye open amount: " + leftOpenAmount);
                timeTMP.SetText("左眨眼" + leftOpenAmount);
            }
        }

        if(_rightEye.isValid)
        {
            InputFeatureUsage<float> rightEyeOpenAmountUsage = new InputFeatureUsage<float>("rightEyeOpenAmount");
            InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightEye);

            //获取右眼张开值
            bool flag = eyes.TryGetRightEyeOpenAmount(out float rightOpenAmount);
            if(device != null && flag != false && device.TryGetFeatureValue(rightEyeOpenAmountUsage,out rightOpenAmount))
            {
                // Debug.Log("Right eye open amount: " + rightOpenAmount);
                timeTMP.SetText("右眨眼" + rightOpenAmount);
            }

        }

    }
}
