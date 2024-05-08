using GXPEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Managers
{
    public class InputManager 
    {
        public const int shootKey = Key.SPACE;           // also UI select
        public const int leftBumper = Key.LEFT;          // also UI left
        public const int rightBumper = Key.RIGHT;        // also UI right
        const int debugToggleKey = Key.F1;
        bool debugModeToggle = false;
        Dictionary<int, bool> isHeld;

        public InputManager () 
        {
            isHeld = new Dictionary<int, bool>()
            {
                {shootKey, Input.GetKey(shootKey)},
                {leftBumper, Input.GetKey(leftBumper)},
                {rightBumper, Input.GetKey(rightBumper)}
            };
        }


        void UpdateHeldKeys()
        {
            isHeld[shootKey] = Input.GetKey(shootKey);
            isHeld[leftBumper] = Input.GetKey(leftBumper);
            isHeld[rightBumper] = Input.GetKey(rightBumper);
        }

        public bool ToggleDebugMode()
        {
            if (Input.GetKeyDown(debugToggleKey)) {
                debugModeToggle = !debugModeToggle;
                return debugModeToggle;
            }
            return debugModeToggle;
        }

        public bool IsHeld(int key)
        {
            return isHeld[key];
        }

        public bool IsShot() 
        {
            return Input.GetKeyDown(shootKey);
        }

        public bool LeftBumperPressed()
        {

            return Input.GetKeyDown(leftBumper);
        } 

        public bool RightBumperPressed()
        {
            return Input.GetKeyDown(rightBumper);
        }

        public bool BothBumpersPressed()
        {
            return Input.GetKey(leftBumper) && Input.GetKey(rightBumper);
        }

        void DebugInput()
        {
            Console.WriteLine( "Shoot key is pressed:" + IsShot() + "\n" +
                                "Left bumper is pressed:" + LeftBumperPressed() + "\n" +
                                "Right bumper is pressed:" + RightBumperPressed() + "\n" +
                                "Both bumpers are pressed:" + BothBumpersPressed());
        }

        void DebugInput(int keyName, bool heldMode = false)
        {
            if ( !heldMode )
            {
                switch (keyName)
                {
                    case shootKey:
                        Console.WriteLine( "Shoot key is pressed:" + IsShot());
                        break;
                    case leftBumper:
                        Console.WriteLine( "Left bumper is pressed:" + LeftBumperPressed());
                        break;
                    case rightBumper:
                        Console.WriteLine( "Right bumper is pressed:" + RightBumperPressed());
                        break;
                }
            } else {
                switch (keyName)
                {
                    case shootKey:
                        Console.WriteLine( "Shoot key is held:" + IsHeld(shootKey));
                        break;
                    case leftBumper:
                        Console.WriteLine( "Left bumper is held:" + IsHeld(leftBumper));
                        break;
                    case rightBumper:
                        Console.WriteLine( "Right bumper is held:" + IsHeld(rightBumper));
                        break;
                }
            }
        }

        public void Update()
        {
            UpdateHeldKeys();
        }
    }
}