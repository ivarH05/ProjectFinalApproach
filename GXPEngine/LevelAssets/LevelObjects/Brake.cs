﻿using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class Brake : Rigidbody
    {
        public Brake(TiledObject obj = null) : base("Square.png", new BoxCollider(new Vec2(64, 64)))
        {
            isTrigger = true;
        }

        public override void OnTrigger(CollisionData collision)
        {
            Rigidbody other = collision.other.rigidbody;

            if (other.isKinematic)
                return;

            other.velocity = Vec2.Lerp(other.velocity, new Vec2(0, -164), Time.timeStep * 6);
        }
    }
}