﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Maui.SegmentedControl.Control
{
    /// <summary>
    /// What happens when a segment is selected
    /// </summary>
    public enum GroupToggleBehavior
    {
        /// <summary>
        /// No buttons are Toggle
        /// </summary>
        None,
        /// <summary>
        /// Only one button can be in the selected state
        /// </summary>
        Radio
            //multiselect not supported
    }

}
