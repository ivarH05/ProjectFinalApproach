using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Rigidbody : Sprite
    {
        /// <summary>
        /// the velocity of the current object in pixels / second. 
        /// </summary>
        public Vec2 velocity;

        /// <summary>
        /// the momentumn of the object
        /// </summary>
        public Vec2 Momentum 
        { 
            get { return velocity * mass; } 
            set { velocity = value / mass; }
        }

        /// <summary>
        /// the collider of the rigidbody, boxcollider by default
        /// </summary>
        public new Collider collider = new BoxCollider(); //(private?)

        /// <summary>
        /// when set to true the object will not move at all, though will collide. 
        /// </summary>
        public bool isKinematic = false;
        /// <summary>
        /// when true the object will still be able to move and collide, though wont be affected by gravity
        /// </summary>
        public bool useGravity = true;

        /// <summary>
        /// the mass (weight) of the object
        /// </summary>
        public float mass = 1;
        /// <summary>
        /// the friction for any object sliding over this objects surface
        /// </summary>
        public float friction = 0.5f;
        /// <summary>
        /// the bounciness of this object.
        /// </summary>
        public float bounciness = 0.5f;

        public Rigidbody(string spritePath = "Square.png", Collider collider = null) : base(spritePath, false, false)
        {
            if (collider != null)
                this.collider = collider;

            PhysicsManager.AddBody(this);
        }

        /// <summary>
        /// do any necessary physics calculations - this might exclude collision based on the order and way we calculate it - 
        /// </summary>
        public void PhysicsUpdate()
        {
            if (isKinematic)
                return;

            if (useGravity)
                velocity += new Vec2(0, 981f) * Time.timeStep;

            Position += velocity * Time.timeStep;
        }

        /// <summary>
        /// add a certain amount of force to the object, the effectiveness will depend on the mass of this object.
        /// </summary>
        /// <param name="force">the force that will be applied</param>
        public void AddForce(Vec2 force)
        {
            Momentum += force;
        }

        protected override void OnDestroy()
        {
            PhysicsManager.RemoveBody(this);
        }
    }
}
