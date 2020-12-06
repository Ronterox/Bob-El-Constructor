using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    private enum MovementType
    {
        MoveTowards,
        Lerp
    }

    private enum Direction
    {
        Forward = 1,
        Backwards = -1
    }

    private enum PositionRelativeTo
    {
        Parent,
        Itself
    }

    [SerializeField] private PositionRelativeTo positionRelativeTo;
    public Vector2 targetPosition;
    public float velocity = 3f;

    [Header("Movement Config")]
    [SerializeField] private MovementType movementType;
    [Tooltip("Goes back to main position after reaching destination")]
    [SerializeField] private bool loop;
    [Tooltip("If checked starts to move on awake")]
    [SerializeField] private bool startOnAwake;

    private Vector2 p_initialPosition;
    private bool pb_isMoving;

    //Only for loop
    private Direction p_direction = Direction.Forward;

    private bool pb_reachedDestination
    {
        get
        {
            return p_direction == Direction.Forward ?
                (Vector2)transform.localPosition == targetPosition : (Vector2)transform.localPosition == p_initialPosition;
        }
    }

    private void Awake()
    {
        SetInitialPosition();
        if (startOnAwake) Move();
    }

    private void Update()
    {
        if (pb_isMoving)
        {
            if (loop) CheckDirection();
            else if (pb_reachedDestination) { Stop(); return; }
            transform.localPosition = GetNextMovement();
        }
    }

    /// <summary>
    /// Get the Vector2 for the next Movement towards your XY or initial position (if looping)
    /// </summary>
    /// <returns>Vector2 for next movement</returns>
    private Vector2 GetNextMovement()
    {
        switch (movementType)
        {
            case MovementType.Lerp:
                return Vector2.Lerp
                    (transform.localPosition, p_direction == Direction.Forward ? targetPosition : p_initialPosition, velocity * Time.deltaTime);
            case MovementType.MoveTowards:
                return Vector2.MoveTowards
                    (transform.localPosition, p_direction == Direction.Forward ? targetPosition : p_initialPosition, velocity * Time.deltaTime);
            default: return transform.localPosition;
        }
    }

    /// <summary>
    /// Changes Direction if it reached the target position
    /// </summary>
    private void CheckDirection()
    {
        if (pb_reachedDestination)
            p_direction = p_direction == Direction.Forward ? Direction.Backwards : Direction.Forward;
    }

    /// <summary>
    /// Sets the object actual position as initial position, also sets the relative destination if needed
    /// </summary>
    public void SetInitialPosition()
    {
        p_initialPosition = transform.localPosition;
        if (positionRelativeTo == PositionRelativeTo.Itself) targetPosition = new Vector2(p_initialPosition.x + targetPosition.x, p_initialPosition.y + targetPosition.y);
    }

    /// <summary>
    /// Teleports the object to the XY position
    /// </summary>
    public void Teleport() { transform.position = targetPosition; }

    /// <summary>
    /// Permits the procediment of moving the object
    /// </summary>
    public void Move() { pb_isMoving = true; }

    /// <summary>
    /// Stops The Object
    /// </summary>
    public void Stop() { pb_isMoving = false; }

    /// <summary>
    /// Starts the forward movement of the object
    /// </summary>
    public void MoveForward()
    {
        p_direction = Direction.Forward;
        Move();
    }

    /// <summary>
    /// Starts the backward movement of the object
    /// </summary>
    public void MoveBackwards()
    {
        p_direction = Direction.Backwards;
        Move();
    }
}
