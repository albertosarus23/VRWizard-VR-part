using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private float _init_x;
    
    [SerializeField]
    private float _max_x;
    
    [SerializeField]
    private GameObject door;

    [SerializeField] 
    private float deltaTime;

    [SerializeField] 
    private float waitSec;
    
    [SerializeField]
    private Receiver receiver;
    
    private Vector3 _initialPosition;
    
    private Vector3 _maxPosition;

    private Vector3 _curPosition;
    
    private float _curX;

    private float _curRatio;
    
    private bool _isWake;
    enum Direction
    {
        left,
        right
    }

    [SerializeField] 
    private Direction _doorDirection;


    private void Awake()
    {
        Time.fixedDeltaTime = deltaTime;
        StartCoroutine(waitforSecs());
    }

    void Start()
    {
        // get the position of the door at first frame
        _initialPosition = door.transform.localPosition;
        _initialPosition.x = _init_x;
        door.transform.localPosition = _initialPosition;
        // make sure the x of the doorframe is set to initial position
        _maxPosition.x = _max_x;
        Debug.Log(door.transform.localPosition);
    }
    
    void FixedUpdate()
    {
        if (_isWake)
        {
            // get the current ratio of position
            RatioRetriever();
            DoorControl(_doorDirection);
        }
    }

    private void DoorControl(Direction direction)
    {
        // if this is the left door 
        if (direction == Direction.left)
        {
            // Debug.Log($"right door moved {Pro_DistConvertor(defaultTest(Time.time), _init_x, _max_x)}");
            _curX = _init_x+Pro_DistConvertor(_curRatio, _initialPosition.x, _maxPosition.x);
            _curPosition = new Vector3(_curX, _initialPosition.y, _initialPosition.z);
            // update the current position of the doorframe
            door.transform.localPosition = _curPosition;
        }
        // if this is the right door
        else
        {
            // Debug.Log($"right door moved {Pro_DistConvertor(defaultTest(Time.time), _init_x, _max_x)}");
            _curX = _init_x+Pro_DistConvertor(_curRatio, _initialPosition.x, _maxPosition.x);
            _curPosition = new Vector3(_curX, _initialPosition.y, _initialPosition.z);
            door.transform.localPosition = _curPosition;
        }
    }
    private void RatioRetriever()
    {
        
        if (receiver.Msg == null)
        {
            Debug.Log("0");
            _curRatio = 0;
        }
        else
        {
            Debug.Log(receiver.Msg);
            _curRatio = receiver.Ratio;
        }
    }
    
    // Ratio to Distance convertor
    private float Pro_DistConvertor(float pro, float init, float far)
    {
        float totalDistance = far - init;
        float currentDistance = pro * totalDistance;
        return currentDistance;
    }

    IEnumerator waitforSecs()
    {
        yield return new WaitForSeconds(waitSec);
        _isWake = true;
    }
}
