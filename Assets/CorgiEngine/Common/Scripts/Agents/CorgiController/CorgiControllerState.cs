using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace MoreMountains.CorgiEngine
{
    /// <summary>
    /// The various states you can use to check if your character is doing something at the current frame
    /// </summary>

    public class CorgiControllerState
    {
        /// is the character colliding right ?
        public bool IsCollidingRight { get; set; }
        /// is the character colliding left ?
        public bool IsCollidingLeft { get; set; }
        /// is the character colliding with something above it ?
        public bool IsCollidingAbove { get; set; }
        /// is the character colliding with something above it ?
        public bool IsCollidingBelow
        {
            get
            {
                return isCollidingBelow;
            }
            set
            {
                if (isCollidingBelow == value) return;
                OnGrounded.Invoke(value);
                isCollidingBelow = value;
            }
        }
        /// is the character colliding with anything ?
        public bool HasCollisions { get { return IsCollidingRight || IsCollidingLeft || IsCollidingAbove || IsCollidingBelow; } }

        /// returns the distance to the left collider, equals -1 if not colliding left
        public float DistanceToLeftCollider;
        /// returns the distance to the right collider, equals -1 if not colliding right
        public float DistanceToRightCollider;

        /// returns the slope angle met horizontally
        public float LateralSlopeAngle { get; set; }
        /// returns the slope the character is moving on angle
        public float BelowSlopeAngle
        {
            get
            {
                return belowSlopeAngle;
            }
            set
            {
                if (NearlyEqual(belowSlopeAngle, value, 0.01f)) return;
                OnSlopeChanged.Invoke(value);
                belowSlopeAngle = value;
            }
        }
        /// returns the normal of the slope the character is currently on
        public Vector2 BlowSlopeNormal { get; set; }
        /// returns the slope the character is moving on angle, relative to Vector2.Up, from 0 to 360
        public float BelowSlopeAngleAbsolute { get; set; }
        /// returns true if the slope angle is ok to walk on
        public bool SlopeAngleOK { get; set; }
        /// returns true if the character is standing on a moving platform
        public bool OnAMovingPlatform { get; set; }

        /// Is the character grounded ? 
        public bool IsGrounded { get { return IsCollidingBelow; } }
        /// is the character falling right now ?
        public bool IsFalling
        {
            get; set;
            //get
            //{
            //    return isFalling;
            //}
            //set
            //{
            //    if (IsGrounded)
            //    {
            //        isFalling = false;
            //        return;
            //    }

            //    if (isFalling == value) return;
            //    OnFalling.Invoke(value);
            //    isFalling = value;
            //}
        }
        /// is the character falling right now ?
        public bool IsJumping { get; set; }
        /// was the character grounded last frame ?
        public bool WasGroundedLastFrame { get; set; }
        /// was the character grounded last frame ?
        public bool WasTouchingTheCeilingLastFrame { get; set; }
        /// did the character just become grounded ?
        public bool JustGotGrounded { get; set; }
        /// is the character being resized to fit in tight spaces?
        public bool ColliderResized { get; set; }
        /// is the character touching level bounds?
        public bool TouchingLevelBounds { get; set; }


        public UnityEvent<bool> OnGrounded = new UnityEvent<bool>();
        //public UnityEvent<bool> OnFalling = new UnityEvent<bool>();
        public UnityEvent<float> OnSlopeChanged = new UnityEvent<float>();


        private bool isCollidingBelow;
        private float belowSlopeAngle;
        private bool isFalling;

        /// <summary>
        /// Reset all collision states to false
        /// </summary>
        public virtual void Reset()
        {
            IsCollidingLeft = false;
            IsCollidingRight = false;
            IsCollidingAbove = false;
            DistanceToLeftCollider = -1;
            DistanceToRightCollider = -1;
            SlopeAngleOK = false;
            JustGotGrounded = false;
            IsFalling = true;
            LateralSlopeAngle = 0;
        }

        /// <summary>
        /// Serializes the collision states
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current collision states.</returns>
        public override string ToString()
        {
            return string.Format("(controller: collidingRight:{0} collidingLeft:{1} collidingAbove:{2} collidingBelow:{3} lateralSlopeAngle:{4} belowSlopeAngle:{5} isGrounded: {6}",
                IsCollidingRight,
                IsCollidingLeft,
                IsCollidingAbove,
                IsCollidingBelow,
                LateralSlopeAngle,
                BelowSlopeAngle,
                IsGrounded);
        }


        private bool NearlyEqual(float a, float b, float epsilon)
        {
            float absA = Mathf.Abs(a);
            float absB = Mathf.Abs(b);
            float diff = Mathf.Abs(a - b);

            if (a == b)
            { // shortcut, handles infinities
                return true;
            }
            else if (a == 0 || b == 0 || (absA + absB < 1.17549435E-38))
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (epsilon * float.MinValue);
            }
            else
            { // use relative error
                return diff / Mathf.Min((absA + absB), float.MaxValue) < epsilon;
            }
        }
    }
}