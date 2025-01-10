using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string zAxisName = "Vertical";
    public static readonly string xAxisName = "Horizontal";
    public static readonly string fireButton = "Fire1";
    public static readonly string reloadButton = "Reload";

    
    public float ForthMove { get; private set; }
    public float SideMove  { get; private set; }
    public bool Fire       { get; private set; }
    public bool Reload     { get; private set; }

    
    void Update()
    {
        ForthMove = Input.GetAxis(zAxisName);
        SideMove = Input.GetAxis(xAxisName);
        
        Vector2 move = new Vector2(ForthMove, SideMove);
        if (move.magnitude > 1f)
        {
            move.Normalize();
            ForthMove = move.x;
            SideMove = move.y;
        }
            
        
        Fire = Input.GetButton(fireButton);
        Reload = Input.GetButton(reloadButton);
    }
}
