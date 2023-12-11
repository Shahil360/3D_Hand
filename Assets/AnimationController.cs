using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Net;
using UnityEngine.XR;
using System.Security.Policy;

public class AnimationController : MonoBehaviour
{
    float bone_max_rotation = -90;
    float left_index_degree = 0;
    private GameObject[] index;
    private GameObject[] middle;
    private GameObject[] thumb;
    private GameObject[] ring;
    private GameObject[] pinky;
    float index_val;
    float thumb_val;
    float middle_val;
    float pinky_val;
    float ring_val;

    float index_rest;
    float middle_rest;
    float ring_rest;
    float pinky_rest;
    float thumb_rest;

    float index_prev_val;
    float middle_prev_val;
    float ring_prev_val;
    float pinky_prev_val;
    float thumb_prev_val;

    public float delay = 0.02f; // The delay in seconds

    private float nextActionTime = 0.0f;

    public Transform boneToLimit;
    public Vector3 minRotationLimits = new Vector3(-45, -45, -45);
    public Vector3 maxRotationLimits = new Vector3(45, 45, 45);
    //float val; 
    private string COM_PORT = "COM3";   /////////////
    private SerialPort sp;

    public string SerialOutput;
    public string[] SerialOutputs;    //////////////////

    void rotate_finger(GameObject[] bones, float rotation_value)
    {
     
        foreach (GameObject bone in bones)
            bone.transform.Rotate(5, 0, 0);
            //bone.transform.Rotate(5, -1, 0);
    }

    void rotate_finger_back(GameObject[] bones, float rotation_value)
    {
        foreach (GameObject bone in bones)
            bone.transform.Rotate(-5, 0, 0);
            //bone.transform.Rotate(-5, 1, 0);
    }
    void rotate_pinky(GameObject[] bones, float rotation_value)
    {
        foreach (GameObject bone in bones)
            bone.transform.Rotate(5, 0, 0);
    }
    void rotate_pinky_back(GameObject[] bones, float rotation_value)
    {
        foreach (GameObject bone in bones)
            bone.transform.Rotate(-5, 0, 0);
    }

    void rotate_thumb(GameObject[] bones, float rotation_value)
    {
        foreach (GameObject bone in bones)
            bone.transform.Rotate(5, 0, 0);
    }
    void rotate_thumb_back(GameObject[] bones, float rotation_value)
    {
        foreach (GameObject bone in bones)
            bone.transform.Rotate(-5, 0, 0);
    }
    void move_finger(GameObject[] finger, float val,ref float rest,ref float prev_val)
    {
        float change = val - prev_val;
        if (change > 0)
        {
            for (int i = 0; i < change; i++)
            {
                if (rest < 18 && rest > -2)
                {
                    rest += 1;
                    rotate_finger(finger, val - left_index_degree);
                    left_index_degree = val;
                    prev_val = val;

                }

            }
        }
        else if (change < 0)
        {
            for (int i = 0; i > change; i--)
            {
                if (rest > -1 && rest < 19)
                {
                    rest -= 1;
                    rotate_finger_back(finger, val + 2 - left_index_degree);
                    left_index_degree = val;
                    prev_val = val;

                }

            }
        }



    }
    void move_pinky(GameObject[] finger, float val, ref float rest, ref float prev_val)
    {
        float change = val - prev_val;
        if (change > 0)
        {
            for (int i = 0; i < change; i++)
            {
                if (rest < 18 && rest > -2)
                {
                    rest += 1;
                    rotate_pinky(finger, val - left_index_degree);
                    left_index_degree = val;
                    prev_val = val;

                }

            }
        }
        else if (change < 0)
        {
            for (int i = 0; i > change; i--)
            {
                if (rest > -1 && rest < 19)
                {
                    rest -= 1;
                    rotate_pinky_back(finger, val + 2 - left_index_degree);
                    left_index_degree = val;
                    prev_val = val;

                }

            }
        }



    }

    void move_thumb(GameObject[] finger, float val, ref float rest, ref float prev_val)
    {
        float change = val - prev_val;
        if (change > 0)
        {
            for (int i = 0; i < change; i++)
            {
                if (rest < 9 && rest > -2)
                {
                    rest += 1;
                    rotate_thumb(finger, val - left_index_degree);
                    left_index_degree = val;
                    prev_val = val;

                }

            }
        }
        else if (change < 0)
        {
            for (int i = 0; i > change; i--)
            {
                if (rest > -1 && rest < 10)
                {
                    rest -= 1;
                    rotate_thumb_back(finger, val + 2 - left_index_degree);
                    left_index_degree = val;
                    prev_val = val;

                }

            }
        }



    }
    void Start()
    {
        sp = new SerialPort(COM_PORT, 9600); ///////////////////////

        index = GameObject.FindGameObjectsWithTag("index");
        ring = GameObject.FindGameObjectsWithTag("ring");
        thumb = GameObject.FindGameObjectsWithTag("thumb");
        middle = GameObject.FindGameObjectsWithTag("middle");
        pinky = GameObject.FindGameObjectsWithTag("pinky");

        sp.Open();
        sp.ReadTimeout = 5000;
    }

    void Update()
    {
        //SerialOutput = sp.ReadLine();
        //print(SerialOutput);

        //index_val = float.Parse(SerialOutput);


        ///////////////////////////////////////////////
        print(SerialOutput);
        SerialOutput = sp.ReadLine().Trim();
        SerialOutputs = SerialOutput.Split(',');
        ///////////////////////////////////////////////

        //if (SerialOutputs.Length != 5 )
        //{
        //    Console.WriteLine("Error");
        //}
        //else
        //{
        //    index_val = float.Parse(SerialOutputs[0]);
        //    middle_val = float.Parse(SerialOutputs[1]);
        //    ring_val = float.Parse(SerialOutputs[2]);
        //    thumb_val = float.Parse(SerialOutputs[3]);
        //    pinky_val = float.Parse(SerialOutputs[4]);
        //}

        //////////////////////////////////////////////////
        pinky_val = float.Parse(SerialOutputs[0]);
        ring_val = float.Parse(SerialOutputs[1]);
        middle_val = float.Parse(SerialOutputs[2]);
        index_val = float.Parse(SerialOutputs[3]);
        thumb_val = float.Parse(SerialOutputs[4]);
        ///////////////////////////////////////////////////

        move_finger(index, index_val,ref index_rest,ref index_prev_val);
        move_finger(ring, ring_val,ref ring_rest,ref ring_prev_val);
        move_finger(middle, middle_val,ref middle_rest,ref middle_prev_val);
        move_pinky(pinky, pinky_val, ref pinky_rest, ref pinky_prev_val);
        move_thumb(thumb, thumb_val, ref thumb_rest, ref thumb_prev_val);


        //if (Input.GetKey(KeyCode.A))
        //{
        //    if (val < 18 && val > -2)
        //    {
        //        val += 1;
        //        rotate_finger(left_index, val - left_index_degree);
        //        left_index_degree = val;
        //    }
        //}

        //if (Input.GetKey(KeyCode.B))
        //{
        //    if (val > -1 && val < 19)
        //    {
        //        val -= 1;
        //        rotate_finger_back(left_index, val+2 - left_index_degree);
        //        left_index_degree = val;
        //    }
        //}


        /////////////// limiting bones

        //if (boneToLimit != null)
        //{
        //    Get the current rotation of the bone
        //   Vector3 currentRotation = boneToLimit.localRotation.eulerAngles;

        //    Clamp the rotation within the specified limits
        //    currentRotation.x = Mathf.Clamp(currentRotation.x, minRotationLimits.x, maxRotationLimits.x);
        //    currentRotation.y = Mathf.Clamp(currentRotation.y, minRotationLimits.y, maxRotationLimits.y);
        //    currentRotation.z = Mathf.Clamp(currentRotation.z, minRotationLimits.z, maxRotationLimits.z);

        //    Apply the clamped rotation back to the bone
        //    boneToLimit.localRotation = Quaternion.Euler(currentRotation);

        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        val += 1;
        //        rotate_finger(left_index, val - left_index_degree);
        //        left_index_degree = val;
        //    }
        //}

        /////////////// Time

        //if (Input.GetKey(KeyCode.B))
        //{
        //    val += 1;
        //    rotate_finger_back(left_index, val - left_index_degree);
        //    left_index_degree = val;
        //}
        //if (Time.time > nextActionTime)
        //{
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        val += 1;
        //        rotate_finger(left_index, val - left_index_degree);
        //        left_index_degree = val;
        //    }
        //    nextActionTime = Time.time + delay;
        //}
    }
}




