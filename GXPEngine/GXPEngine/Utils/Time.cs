using System.Diagnostics;
using System;

namespace GXPEngine
{
	/// <summary>
	/// Contains various time related functions.
	/// </summary>
	public class Time
	{
        /// <summary>
        /// the time inbetween fixed updates, like the physics updates. 
        /// </summary>
		public static float timeStep = 0.0032f;

        private static int previousTime;

        private static float previousTimeSeconds;

        static Time() {
		}
		
		/// <summary>
		/// Returns the current system time in milliseconds
		/// </summary>
		public static int now {
			get { return System.Environment.TickCount; }
		}

        /// <summary>
        /// Returns this time in milliseconds since the program started		
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public static int time
        {
            get { return (int)(OpenGL.GL.glfwGetTime() * 1000); }
        }
        public static float timeSeconds
        {
            get { return (float)OpenGL.GL.glfwGetTime(); }
        }

        /// <summary>
        /// Returns the time in milliseconds that has passed since the previous frame
        /// </summary>
        /// <value>
        /// The delta time.
        /// </value>
        private static int previousFrameTime;
        public static int deltaTime
        {
            get
            {
                return previousFrameTime;
            }
        }

        /// <summary>
        /// get the deltatime in seconds rather than milliseconds.
        /// </summary>
        public static float DeltaSeconds { get; private set; }

        internal static void newFrame() {
			previousFrameTime = time - previousTime;
			previousTime = time;
            DeltaSeconds = timeSeconds - previousTimeSeconds;
            previousTimeSeconds = timeSeconds;
        }
	}
}

