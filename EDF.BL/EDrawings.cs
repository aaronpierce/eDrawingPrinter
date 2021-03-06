﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDF.UI
{
    public class EDrawings
    {
        // Creates eDrawingHostControl for providing eDrawing functions.
        public EDrawings()
        {
            try
            {
                Control = new eDrawingHostControl.eDrawingControl();
                PreviewControl = new eDrawingHostControl.eDrawingControl();
                //For testing application terminating on bad controls.
                //throw new Exception("Test Exception for missing eDrawings installation.");
            }
            catch
            {
                Control = null;
                PreviewControl = null;
            }
            
        }
        // Main control used for opening/printing files.
        public eDrawingHostControl.eDrawingControl Control { get; set; }

        // Secondary control utilized for preview files on the side of UI
        public eDrawingHostControl.eDrawingControl PreviewControl { get; set; }
    }
}

