using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunRotation : MonoBehaviour
{
    public float _anglePerFrame = 0.1f;    // 1フレームに何度回すか[unit : deg]
    public float _exposurePerFrame = 0.1f;   
    float _rot = 0.0f;
    float _exp = 1.0f;

    bool expFlag = true;

    // Update is called once per frame
    void Update()
    {
        _rot += _anglePerFrame;
        if (_rot >= 360.0f) {    // 0～360°の範囲におさめたい
            _rot -= 360.0f;
        }
        RenderSettings.skybox.SetFloat("_Rotation", _rot);    // 回す

        
        if (expFlag == true) {
            _exp += _exposurePerFrame;
		if(_exp >= 1.0f){
			expFlag = false;
		}
        }
	if(expFlag == false){
		_exp -=_exposurePerFrame;
		if(_exp <= 0.0f){
			expFlag = true;
		}
	}
        RenderSettings.skybox.SetFloat("_Exposure", _exp); 

    }
}
