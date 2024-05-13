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
        public float angularVelocity;

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
        public new Collider collider = new BoxCollider(new Vec2(64, 64)); //(private?)

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

        public Rigidbody(string spritePath = "Square.png", Collider collider = null) : base(spritePath == "" ? "Square.png" : spritePath, false, false)
        {
            if (spritePath == "")
                visible = false;
            if (collider != null)
                SetCollider(collider);
            if (collider is LineCollider)
                isKinematic = true;
            PhysicsManager.AddBody(this);
            centerOrigin();
        }

        public void SetCollider(Collider collider)
        {
            collider.rigidbody = this;
            this.collider = collider;
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
            rotation += angularVelocity * Time.timeStep;
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
