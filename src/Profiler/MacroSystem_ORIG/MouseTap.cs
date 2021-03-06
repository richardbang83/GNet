﻿using System;

using GNet.IO;
using GNet.PInvoke;

namespace GNet.Profiler.MacroSystem_ORIG
{
    public class MouseTap : Step
    {
        public MouseTap(int button)
        {
            int data = 0;

            switch (button)
            {
                case 0:
                case 1:
                    Inputs = new InputWrapper[] { InputSimulator.MouseWrapper(MouseEventFlags.LeftDown, data), InputSimulator.MouseWrapper(MouseEventFlags.LeftUp, data) };

                    toString = "MouseTap(LeftButton)";
                    break;

                case 2:
                    Inputs = new InputWrapper[] { InputSimulator.MouseWrapper(MouseEventFlags.RightDown, data), InputSimulator.MouseWrapper(MouseEventFlags.RightUp, data) };

                    toString = "MouseTap(RightButton)";
                    break;

                case 3:
                    Inputs = new InputWrapper[] { InputSimulator.MouseWrapper(MouseEventFlags.MiddleDown, data), InputSimulator.MouseWrapper(MouseEventFlags.MiddleUp, data) };

                    toString = "MouseTap(MiddleButton)";
                    break;

                default:
                    data = button - 3;
                    Inputs = new InputWrapper[] { InputSimulator.MouseWrapper(MouseEventFlags.XDown, data), InputSimulator.MouseWrapper(MouseEventFlags.XUp, data) };

                    toString = "MouseTap(XButton, " + data + ")";
                    break;
            }
        }
    }
}
