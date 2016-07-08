using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Sprites;
using System.Collections.Generic;

// TightPackerPolicy will tightly pack non-rectangle Sprites unless their packing tag contains "[RECT]".
class TightPackerPolicySample : DefaultPackerPolicySample
{
	protected override string TagPrefix { get { return "[RECT]"; } }
	protected override bool AllowTightWhenTagged { get { return false; } }
}
