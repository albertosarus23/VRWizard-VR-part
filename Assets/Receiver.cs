using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    public MQTTReceiver receiver;
    
    public enum Direction
    {
        Left,Right
    }

    public Direction direction;
    
    private string msg;
    public string Msg
    {
        get
        {
            return msg;
        }
        set
        {
            msg = value;
        }
    }

    // store the right door data
    private float ratio = 0f;

    // could be accessed externally through this method
    public float Ratio
    {
        get
        {
            return StringConvertor(Msg);
        }
    }

    private float previous;
    void Start()
    {
        // InvokeRepeating("procceedMqttinfo",5f,1f);
        receiver.OnMessageArrived += onMessageArrivedNullFilter;
        receiver.OnMessageArrived += onMessageArrivedHandler;
        Debug.Log("Web Module activated");
    }

    
    // method to transfer data string into float
    
    private static float StringConvertor(string newMsg)
    {
        Debug.Log(float.Parse(newMsg, CultureInfo.InvariantCulture.NumberFormat));
        return float.Parse(newMsg, CultureInfo.InvariantCulture.NumberFormat);
    }
    
    // delegate method

    private void onMessageArrivedNullFilter([CanBeNull] string newMsg)
    {
        if (IsNull(newMsg))
        {
            Debug.Log("Null message receive! Check if the monitor camera is open.");
            Msg = "0";
        }
    }
    
    private void onMessageArrivedHandler(string newMsg)
    {

        Debug.Log(null);
        Debug.Log("2");
        string[] arr = newMsg.Split(':');
        
        if (arr[0] == "Right")
        {
            if (direction == Direction.Right)
            {
                Msg = arr[1];
            }
        }
        else
        {
            if (direction == Direction.Left)
            {
                Msg = arr[1];
            }
        }
        Debug.Log($"Message arrived: {arr[0]} door, {arr[1]}");
    }

    #nullable enable

    public static bool IsNull(String? str)
    {
        return (str == null);
    }

#nullable disable

    // private void onMessageArrivedFilter(string newMsg)
    // {
    //     var value = StringConvertor(newMsg);
    //     
    //     if ((value >= 0f)&&(value <=1f))
    //     {
    //         // the acceptable range. Ideal data should fall in 0-1
    //         Msg = newMsg;
    //     }
    //     else
    //     {
    //         // if is number and out of range. Make it back in scope.
    //         if (value > 1f)
    //         {
    //             Msg = "1";
    //         }
    //         else if (value < 0f)
    //         {
    //             Msg = "0";
    //         }
    //         // if is not number
    //         else
    //         {
    //             Msg = $"{previous}";
    //         }
    //     }
    // }
}
