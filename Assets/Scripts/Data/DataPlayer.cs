using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataPlayer : ScriptableObject
{
    public float walkSpeed;
    public float runSpeed;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode runKey;

    public float jumpForce;
    public KeyCode jumpKeyCode;

    public KeyCode primarySlotKey;
}
