using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithConstSpeedDir : MonoBehaviour
{
    [Tooltip("Euler angles by which the object should be rotated by.")]
    [SerializeField]
    private Vector3 RotateByEulerAngles = Vector3.zero;

    [Tooltip("Rotation speed factor.")]
    [SerializeField]
    public float speed = 3.09f;

    //public void SetSpeed(float newSpeed)
   // {
      //  speed = newSpeed;
   // }

    private bool isRotating = false;

    // private bool isRotating = false;
    // Start is called before the first frame update
    void Start()
    {
        //isRotating = true;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // RotateTarget();
       // if (isRotating)
       // {
            RotateTarget();
       // }
    }

    /// <summary>
    /// Rotate game object based on specified rotation speed and Euler angles.
    /// </summary>
    // public void RotateTarget()
    // {
    //   transform.eulerAngles = transform.eulerAngles + RotateByEulerAngles * speed;
    //  transform.Rotate(transform.eulerAngles*Time.deltaTime);
    // }

    public void RotateTarget()
    {
        isRotating = true;
        transform.eulerAngles = transform.eulerAngles + RotateByEulerAngles * speed;
    }

    /// <summary>
    /// Stop rotating the game object.
    /// </summary>
    public void StopRotating()
    {
        isRotating = false;
    }
}
