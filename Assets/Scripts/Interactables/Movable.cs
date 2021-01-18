using UnityEngine;

namespace Interactables
{
    public class Movable : MonoBehaviour
    {
        private enum MovementType { MoveTowards, Lerp }

        private enum Direction { Forward = 1, Backwards = -1 }

        private enum PositionRelativeTo { Parent, Itself }

        [SerializeField] private PositionRelativeTo positionRelativeTo;
        [SerializeField] private Vector2 targetPosition;
        [SerializeField] private float velocity = 3f;

        [Header("Movement Config")]
        [SerializeField] private MovementType movementType;
        [Tooltip("Goes back to main position after reaching destination")]
        [SerializeField] private bool loop;
        [Tooltip("If checked starts to move on awake")]
        [SerializeField] private bool startOnAwake;

        private Vector2 p_initialPosition;
        private bool p_isMoving;

        //Only for loop
        private Direction p_pDirection = Direction.Forward;

        private bool pbReachedDestination =>
            p_pDirection == Direction.Forward ?
                (Vector2)transform.localPosition == targetPosition : (Vector2)transform.localPosition == p_initialPosition;

        private void Awake()
        {
            SetInitialPosition();
            if (startOnAwake) Move();
        }

        private void Update()
        {
            if (!p_isMoving) return;
            if (loop) CheckDirection();
            else if (pbReachedDestination) { Stop(); return; }
            transform.localPosition = GetNextMovement();
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
                        (transform.localPosition, p_pDirection == Direction.Forward ? targetPosition : p_initialPosition, velocity * Time.deltaTime);
                case MovementType.MoveTowards:
                    return Vector2.MoveTowards
                        (transform.localPosition, p_pDirection == Direction.Forward ? targetPosition : p_initialPosition, velocity * Time.deltaTime);
                default: return transform.localPosition;
            }
        }

        /// <summary>
        /// Changes Direction if it reached the target position
        /// </summary>
        private void CheckDirection()
        {
            if (pbReachedDestination)
                p_pDirection = p_pDirection == Direction.Forward ? Direction.Backwards : Direction.Forward;
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
        public void Teleport() => transform.localPosition = targetPosition;

        /// <summary>
        /// Permits the procediment of moving the object
        /// </summary>
        public virtual void Move() => p_isMoving = true;

        /// <summary>
        /// Stops The Object
        /// </summary>
        public virtual void Stop() => p_isMoving = false;

        /// <summary>
        /// Starts the forward movement of the object
        /// </summary>
        public virtual void MoveForward()
        {
            p_pDirection = Direction.Forward;
            Move();
        }

        /// <summary>
        /// Starts the backward movement of the object
        /// </summary>
        public virtual void MoveBackwards()
        {
            p_pDirection = Direction.Backwards;
            Move();
        }

        /// <summary>
        /// Changes the velocity to which the object moves
        /// </summary>
        /// <param name="speed">New velocity to set</param>
        public void SetMovableSpeed(int speed) => velocity = speed;
    }
}
