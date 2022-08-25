using System.Collections;
using System.Collections.Generic;
using System.Web.UI.Design;
using UnityEngine;

public class WristMenu : MonoBehaviour
{
    #region Public Variables
    
    [Header("Wrist Transform")]
    [SerializeField] private Transform wrist;

    [Header("Max Angle the menu will show")]
    [Range(0,1)]
    [SerializeField] private float anglePrecision = .25f;
    
    [Range(0,3)]
    [SerializeField] private float maxDistance = .4f;

    [Header("Peripheral Precision")] 
    [Range(0, 1)] [SerializeField]
    private float peripheralViewPrecision = .6f;

    [Header("Wrist Menu")]
    [SerializeField] private GameObject wristUI;
    
    #endregion
    
    #region Private Variables
    
    private Camera _hmd;
    
    private Vector3 _handPos;
    private Vector3 _headPos;

    private float _lookingAngle;
    private float _distance;
    private float _viewAngle;

    private bool _isShowing;
    private bool _found;
    private bool _iswristNull;
    private bool _isHmdNull;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _hmd = Camera.main;
        _isHmdNull = _hmd == null;
        _iswristNull = wrist == null;
        
    }


    void Update() {
        if (_iswristNull || _isHmdNull)
            return;

        _handPos = wrist.transform.position;
        _headPos = _hmd.transform.position;

        // Determine if the wrist is angled to the HMD
        _lookingAngle = Vector3.Dot((_headPos - _handPos).normalized, -wrist.transform.forward);
        _distance = Vector3.Distance(_headPos, wrist.transform.position);
        // determine if the wrist is outside the peripheral vision
        _viewAngle = Vector3.Dot(_hmd.transform.forward, -(_headPos - _handPos).normalized);
        _found = _lookingAngle >= anglePrecision && _distance < maxDistance;
        
        


        if (!_isShowing && _found && _viewAngle > peripheralViewPrecision)
        {
            wristUI.SetActive(true);
            _isShowing = true;
        }
        else if (_isShowing && !_found)
        {
            wristUI.SetActive(false);
            _isShowing = false;
        }
        else if (_viewAngle < peripheralViewPrecision)
        {
            wristUI.SetActive(false);
            _isShowing = false;
        }

    }
    
    #endregion
}
