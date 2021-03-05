using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public Transform bossTarget;
    bool bossFight = false;

    public Camera cam;
    
    void LateUpdate()
    {
        if (bossFight == false){
            transform.position = target.position + offset;
        }
        else{
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, bossTarget.position.x, 2f * Time.deltaTime), Mathf.Lerp(transform.position.y, bossTarget.position.y, 2f * Time.deltaTime), transform.position.z);
            
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 9.64484f, 0.001f);
        }
    }


    public void BossCamera(){
        bossFight = true;
    }
}
