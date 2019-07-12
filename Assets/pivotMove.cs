using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pivotMove : MonoBehaviour
{
     CharacterController Controller;

    public GameObject player;

    public float normalSpeed;
    public float boostSpeed;

    Vector3 forward;

    bool moving;

    public GameObject postprocessN;
    public GameObject postprocessA;
    public GameObject postprocessB;

    //public GameObject renderCam;


    float rotationsum;

    float theta;



    //public GameObject campas;


    // Start is called before the first frame update
    void Start()
    {

        Controller = GetComponent<CharacterController>();
        moving = false;
        rotationsum = 0f;
        theta = 0f;
    }

    // Update is called once per frame
    void Update()
    {
		Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        Vector3 move = new Vector3(primaryAxis.x, 0f, primaryAxis.y);
        if(Mathf.Abs(primaryAxis.y)>0.0||Mathf.Abs(primaryAxis.x)>0.0){
            Vector3 dir = Quaternion.Euler(0f, rotationsum, 0f) * move;
            dir = dir.normalized;
             if(OVRInput.Get(OVRInput.Button.PrimaryThumbstick)){
                    //Boost
                Controller.Move(dir* boostSpeed * Time.deltaTime);
                player.transform.position = transform.position;
                postprocessN.SetActive(false);
                postprocessA.SetActive(true);
                postprocessB.SetActive(false);
            }else
            
            {
                //Normal
                Controller.Move(dir* normalSpeed * Time.deltaTime);
                player.transform.position = transform.position;
                postprocessN.SetActive(false);
                postprocessA.SetActive(false);
                postprocessB.SetActive(true);
            }
            
        }else if(Mathf.Abs(secondaryAxis.x)>0.0f){
            postprocessN.SetActive(false);
                postprocessA.SetActive(false);
                postprocessB.SetActive(true);
        }
        else{

            postprocessN.SetActive(true);
            postprocessA.SetActive(false);
            postprocessB.SetActive(false);
        }

       

        //renderCam.transform.Rotate(0f, secondaryAxis.x, 0f);
        rotationsum += secondaryAxis.x;
        

        transform.Rotate(0f, secondaryAxis.x, 0f);
        player.transform.Rotate(0f, secondaryAxis.x, 0f);

    }
}
