using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerParameters", fileName = "PlayerParameters", order = 0)]
public class PlayerParameters : ScriptableObject
{
    [FoldoutGroup("BasicMovement")] [Tooltip("Defines the Speed with witch the player will move")]
    public float Speed = 5f;
    [FoldoutGroup("BasicMovement")] [Tooltip("The rate with witch the player accelerate and decelerate")]
    public float Acceleration = 1f;
    [FoldoutGroup("BasicMovement")] /*[Range(0f, 1f)]*/
    public float Friction = 0.1f;
    [FoldoutGroup("BasicMovement")] 
    public AnimationCurve SpeedDotMultiplier;

    [FoldoutGroup("Charging")] 
    public float StopRate = 0.1f;
    [FoldoutGroup("Charging")]
    public float ChargingFriction;
    
    [FoldoutGroup("Aim")] [Tooltip("Max angle in degrees the player can rotate in a single frame")]
    public float MaxAngle = 10f;

    [FoldoutGroup("Dash")]
    public float DashDistance = 10f;
    [FoldoutGroup("Dash")]
    public float DashTime = 0.5f;
    [FoldoutGroup("Dash")] 
    public AnimationCurve DashCurve;
    [FoldoutGroup("Dash")] 
    public AnimationCurve DashVelocityCurve;
    [FoldoutGroup("Dash")] 
    public float DashStartVelocity;
    [FoldoutGroup("Dash")] 
    public float DashEndVelocity;

    [BoxGroup("Dash/Collision")]
    public float CollisionCenterOffset;
    [BoxGroup("Dash/Collision")]
    public float CollisionRadius;
    
    [FoldoutGroup("Recovering")]
    public float RecoverVelocity = 1f;
    [FoldoutGroup("Recovering")] /*[Range(0f, 1f)]*/
    public float RecoveryFriction = 1f;
    
    [FoldoutGroup("Gizmos")] 
    public bool CollisionGizmo = true;
    [FoldoutGroup("Gizmos/Parameters")] 
    public Color CollisionGizmoColor = Color.green;
}