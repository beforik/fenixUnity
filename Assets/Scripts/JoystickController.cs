using System;
using UnityEngine;

public enum MoveTypes
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}
public class JoystickController : MonoBehaviour
{
    public event Action<MoveTypes> OnDoMove;
    
    public void OnLeftPressed()
    {
        InvokeEvent(MoveTypes.Left);
    }
    
    public void OnRightPressed()
    {
        InvokeEvent(MoveTypes.Right);
    }
    
    public void OnDownPressed()
    {
        InvokeEvent(MoveTypes.Down);
    }
    
    public void OnUpPressed()
    {
        InvokeEvent(MoveTypes.Up);
    }

    private void InvokeEvent(MoveTypes moveType)
    {
        OnDoMove?.Invoke(moveType);
        print("JoystickController InvokeEvent = " + moveType);
    }
}
