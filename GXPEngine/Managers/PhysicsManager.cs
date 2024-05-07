using GXPEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Managers
{
    public static class PhysicsManager
    {
        private static List<Rigidbody> _bodies = new List<Rigidbody>();
        private static float updateTimer = 0;

        public static void PhysicsUpdate()
        {
            updateTimer += Time.DeltaSeconds;
            while (updateTimer > Time.timeStep)
            {
                updateTimer -= Time.timeStep;
            }
        }
    }
}
