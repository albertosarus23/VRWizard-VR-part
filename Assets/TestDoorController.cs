using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoorController : MonoBehaviour
{
    [SerializeField]
    private float _init_x;
    
    [SerializeField]
    private float _max_x;
    
    // Start is called before the first frame update
    [SerializeField]
    private GameObject door;
    
    private Vector3 _intialPosition;
    
    private Vector3 _maxPosition;
    
    [SerializeField] 
    private float ratiospeed = 1f; // what ratio will the door move in each frame?
    
    private Vector3 _curPosition;
    
    private float _curX;
    
    
    enum Direction
    {
        left,
        right
    }

    [SerializeField] 
    private Direction _doorDirection;


    private void Awake()
    {
        
    }

    void Start()
    {
        // get the position of the door at first frame
        _intialPosition = door.transform.localPosition;
        _intialPosition.x = _init_x;
        door.transform.localPosition = _intialPosition;
        // make sure the x of the doorframe is set to initial position
        _maxPosition.x = _max_x;
        Debug.Log(door.transform.localPosition);
    }
    
    void FixedUpdate()
    {
        // if this is the left door 
        if (_doorDirection == Direction.left)
        {
            // Debug.Log($"left door moved {Pro_DistConvertor(defaultTest(Time.time), _init_x, _max_x)}");
            _curX = _init_x+ratiospeed * Pro_DistConvertor(defaultTest(Time.time), _intialPosition.x, _maxPosition.x);
            _curPosition = new Vector3(_curX, _intialPosition.y, _intialPosition.z);
            // update the current position of the doorframe
            door.transform.localPosition = _curPosition;
        }
        // if this is the right door
        else
        {
            // Debug.Log($"right door moved {Pro_DistConvertor(defaultTest(Time.time), _init_x, _max_x)}");
            _curX = _init_x+ratiospeed * Pro_DistConvertor(defaultTest(Time.time), _intialPosition.x, _maxPosition.x);
            _curPosition = new Vector3(_curX, _intialPosition.y, _intialPosition.z);
            // update the current position of the doorframe
            door.transform.localPosition = _curPosition;
        }
        
    }
    
    
    private float Pro_DistConvertor(float pro, float init, float far)
    {
        float totalDistance = far - init;
        float currentDistance = pro * totalDistance;
        return currentDistance;
    }

    private float defaultTest(float time)
    {
        return (float)Math.Abs(Math.Sin(time));
    }
    

    IEnumerator wait()
    {
        yield return null;
    }
}
