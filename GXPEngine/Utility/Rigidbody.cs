using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

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

        Vec2 nextPosition = new Vec2();
        float nextRotation = 0;

        public bool isTrigger = false;

        private CollisionData[] Collisions = new CollisionData[0];

        public Rigidbody(string spritePath = "Square.png", Collider collider = null) : base(spritePath == "" ? "Square.png" : spritePath, false, false)
        {
            centerOrigin();
            if (spritePath == "")
                visible = false;
            if (collider != null)
                SetCollider(collider);
            if (collider is LineCollider)
                isKinematic = true;
            if (collider is CircleCollider cc)
            {
                width = (int)cc.radius * 2; 
                height = (int)cc.radius * 2;
            }
            PhysicsManager.AddBody(this);
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
            Vec2 oldPosition = Position;
            float oldRotation = rotation;
            nextPosition = Position;
            nextRotation = rotation;
            if (isKinematic)
                return;

            if (useGravity)
                velocity += new Vec2(0, 981f) * Time.timeStep * PhysicsManager.GravityMultiplier;

            float time = Time.timeStep;
            int count = 0;

            rotation += angularVelocity * time;
            Position += velocity * time;

            Move:
            List<CollisionData> data = PhysicsManager.GetCollisions(this);
            Collisions = data.ToArray();
            if (data.Count == 0)
                goto End;

            foreach (CollisionData dat in data)
            {
                Position -= dat.normal * (dat.penetrationDepth + 0.001f);
                time -= dat.penetrationDepth / velocity.magnitude;
                velocity = velocity.Reflect(dat.normal);
                Vec2 relativeVelocity = dat.other.Velocity - velocity;
                velocity *= 0.96f;

                if (Vec2.Dot(velocity.normalized, relativeVelocity.normalized) >= 0)
                {
                    velocity += relativeVelocity;
                }
            }

            count++;
            if (count < 5)
                goto Move;

            End:
            velocity = Vec2.Lerp(velocity, Vec2.Zero, Time.timeStep * PhysicsManager.AirFriction);
            velocity.magnitude = Mathf.Clamp(velocity.magnitude, 0, 2500);
            nextPosition = Position;
            nextRotation = rotation;
            Position = oldPosition;
            rotation = oldRotation;
        }

        public void LatePhysicsUpdate()
        {
            Position = nextPosition;
            rotation = nextRotation;

            foreach (CollisionData dat in Collisions)
                if (isTrigger)
                    OnTrigger(dat);
                else
                    OnCollision(dat);
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

        public virtual void OnCollision(CollisionData collision)
        {

        }

        public virtual void OnTrigger(CollisionData collision)
        {

        }
    }
}
