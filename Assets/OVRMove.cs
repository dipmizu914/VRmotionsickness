using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class OVRMove : MonoBehaviour
{
    CharacterController Controller;

    public float normalSpeed;
    public float boostSpeed;

    Vector3 forward;

    bool moving;

    public GameObject postprocessN;
    public GameObject postprocessA;
    public GameObject postprocessB;

    public GameObject renderCam;


    float rotationsum;

    public GameObject campas;


    // Start is called before the first frame update
    void Start()
    {

        Controller = gameObject.GetComponent<CharacterController>();
        moving = false;
        rotationsum = 0f;
    }

    // Update is called once per frame
    void Update()
    {
		Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        if(primaryAxis.y>0.0){
            if(!moving){
                forward = Camera.main.transform.TransformDirection(Vector3.forward);
                forward.Normalize();
                moving = true;
            }else{
                if(OVRInput.Get(OVRInput.Button.PrimaryThumbstick)){
                    //Boost
                    Controller.Move(forward * boostSpeed * Time.deltaTime);
                    postprocessN.SetActive(false);
                    postprocessA.SetActive(true);
                    postprocessB.SetActive(false);
                }else
                {
                    //Normal
                    Controller.Move(forward * normalSpeed * Time.deltaTime);
                    postprocessN.SetActive(false);
                    postprocessA.SetActive(false);
                    postprocessB.SetActive(true);
                }
            }  
        }else{
            moving = false;
            postprocessN.SetActive(true);
            postprocessA.SetActive(false);
            postprocessB.SetActive(false);
        }

        if(Mathf.Abs(rotationsum)>0.1f){
            campas.SetActive(true);
        }

        renderCam.transform.Rotate(0f, secondaryAxis.x, 0f);
        rotationsum += secondaryAxis.x;
        if(OVRInput.Get(OVRInput.Button.SecondaryThumbstick)){
            renderCam.transform.Rotate(0f, -rotationsum, 0f);
            transform.Rotate(0f, rotationsum, 0f);
            rotationsum = 0f;
            campas.SetActive(false);
        }
    }
}
