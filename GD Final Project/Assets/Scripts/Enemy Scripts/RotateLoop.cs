using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLoop : MonoBehaviour
{
    public float rotateRate = 1f;
    public bool useMaxRotation;
    public float maxRotationAngle = 180f;
    [SerializeField] float startingAngle;
    [SerializeField] bool reversing;
    //Transform myTransform;
    // Start is called before the first frame update
    void Start()
    {
        startingAngle = transform.localEulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (useMaxRotation == true)
        {
            Vector3 t = GetComponent<Transform>().localEulerAngles;
            if (rotateRate > 0)
            {
                if (reversing == true)
                {
                    t.z -= rotateRate;
                    if (t.z <= startingAngle - maxRotationAngle)
                    {
                        reversing = false;
                    }
                }
                else
                {
                    t.z += rotateRate;
                    if (t.z >= maxRotationAngle)
                    {
                        reversing = true;
                    }
                }
            }
            transform.localEulerAngles = t;
        }
        else
        {
            Vector3 t = GetComponent<Transform>().localEulerAngles;
            t.z += rotateRate;
            transform.localEulerAngles = t;
        }
        
    }
}
