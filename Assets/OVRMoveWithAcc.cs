using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRMoveWithAcc : MonoBehaviour
{
    CharacterController Controller;

    public float maxSpeed;

    private float speed;

    public float accel;
    // Start is called before the first frame update
    void Start()
    {
        Controller = gameObject.GetComponent<CharacterController>();
        speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Button.PrimaryTouchpad)){
            speed = Mathf.Min(speed + accel * Time.deltaTime, maxSpeed);
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);

            forward.Normalize();
            forward *= speed * Time.deltaTime;
            Controller.Move(forward);
        }else{
            speed = Mathf.Max(speed - accel * Time.deltaTime, 0f);
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);

            forward.Normalize();
            forward *= speed * Time.deltaTime;
            Controller.Move(forward);
        }
    }
}
