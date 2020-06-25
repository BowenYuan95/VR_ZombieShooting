using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using HTC.UnityPlugin.Pointer3D;

public class Move : MonoBehaviour
{

    public Transform player;

    public Transform dic;

    public float speed;

    public GameObject targetObj = null;

    private ReticlePoser rp;

    void Awake()
    {
        rp = GameObject.Find("VivePointers/Right/Reticle").GetComponent<ReticlePoser>();
    }
    
    void FixedUpdated ()
    {
        targetObj = rp.raycaster.FirstRaycastResult().gameObject;

        if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.PadTouch))
        {
            Vector2 cc = ViveInput.GetPadTouchAxis(HandRole.RightHand);
            float angle = VectorAngle(new Vector2(1, 0), cc);

            //down
            if (angle > 45 && angle < 135)
            {
                //Debug.log("down")
                player.Translate(-dic.forward * Time.deltaTime * speed);
            }
            //up  
            else if (angle < -45 && angle > -135)
            {
                //Debug.Log("up");
                player.Translate(dic.forward * Time.deltaTime * speed);
            }
            //left  
            else if ((angle < 180 && angle > 135) || (angle < -135 && angle > -180))
            {
                //Debug.Log("left");
                player.Translate(-dic.right * Time.deltaTime * speed);
            }
            //right  
            else if ((angle > 0 && angle < 45) || (angle > -45 && angle < 0))
            {
                //Debug.Log("right");
                player.Translate(dic.right * Time.deltaTime * speed);
            }
        }

        if (ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Pad))
        {
            Vector2 cc = ViveInput.GetPadTouchAxis(HandRole.LeftHand);
            float angle = VectorAngle(new Vector2(1, 0), cc);

            //下
            if (angle > 45 && angle < 135)
            {
                player.Translate(-dic.up * Time.deltaTime * speed);
            }
            //上  
            else if (angle < -45 && angle > -135)
            {
                //Debug.Log("上");
                player.Translate(dic.up * Time.deltaTime * speed);
            }
        }
    }    

    float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;
        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }
}
