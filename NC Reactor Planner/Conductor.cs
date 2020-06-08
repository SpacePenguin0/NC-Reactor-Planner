﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace NC_Reactor_Planner
{
    public class Conductor : Block
    {
        public bool HasPathToCasing { get; set; }
        public int GroupID { get; set; }
        public bool Active { get => HasPathToCasing; }

        public Conductor(string displayName, Bitmap texture, Vector3 position) : base(displayName, BlockTypes.Conductor, texture, position)
        {
            GroupID = -1;
        }

        public Conductor(Conductor parent, Vector3 newPosition) : this(parent.DisplayName, parent.Texture, newPosition)
        {

        }

        public override void RevertToSetup()
        {
            SetCluster(-1);
            GroupID = -1;
            HasPathToCasing = false;
        }

        public override string GetToolTip()
        {
            if (Position == Palette.dummyPosition)
                return base.GetToolTip() + 
                    "Conducts heat from clusters,\r\n" +
                    "use these to make a path\r\n" +
                    "to a casing if a cluster isn't\r\n" +
                    "touching one directly.\r\n" +
                    "Connects clusters together.";

            return string.Format("Conductor \r\n" +
                                //"Group: " + GroupID.ToString() +"\r\n" +
                                base.GetToolTip());
        }

        public override Block Copy(Vector3 newPosition)
        {
            return new Conductor(this, newPosition);
        }

    }
}
