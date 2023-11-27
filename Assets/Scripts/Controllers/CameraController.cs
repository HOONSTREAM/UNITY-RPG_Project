using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
     Define.CameraMode _mode = Define.CameraMode.QuarterView;
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f); // 플레이어로부터 카메라가 얼마나 떨어져있는지
    [SerializeField]
    GameObject _player = null;

    public void SetPlayer(GameObject player) { _player = player; }
   
    void LateUpdate()
    {
        if(_player.IsValid()==false)
        {
            return;
        }


        if(_mode == Define.CameraMode.QuarterView)
        {
            RaycastHit hit;
            if(Physics.Raycast(_player.transform.position,_delta, out hit, _delta.magnitude, LayerMask.GetMask("Block")))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;

            }
            else
            {
                transform.position = _player.transform.position + _delta;
            }
            

        }

    }

    public void SetQuarterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
