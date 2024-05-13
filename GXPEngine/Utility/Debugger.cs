using GXPEngine.Core;
using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace GXPEngine
{
    public class Debugger : GameObject
    {
        bool _enabled = false;
        EasyDraw _debuggerCanvas;

        public Debugger()
        {
            _debuggerCanvas = new EasyDraw(1920, 1080, false);
        }

        public void DrawMouseCoords()
        {
            _debuggerCanvas.TextSize(24);
            _debuggerCanvas.Fill(Color.White);
            _debuggerCanvas.SetOrigin(0, 0);
            _debuggerCanvas.SetXY(0, 0);
            _debuggerCanvas.Text("(Mouse X: " + Input.mouseX + ", Mouse Y: " + Input.mouseY + ")", true); 
            parent.AddChild(_debuggerCanvas);
        }

        void CheckForDebugToggle()
        {
            if (Scene.inputManager.ToggleDebugMode())
            {
                _enabled = !_enabled;
                Scene.debugMode = _enabled;
                string debugMode = _enabled ? "enabled" : "disabled";
                Console.WriteLine("Debug mode: " + debugMode);
            }
        }

        public void Update()
        {
            CheckForDebugToggle();
            if (_enabled) {
                DrawMouseCoords();
            }
        }
    }
}