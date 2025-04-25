using GenesisCarrier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VehicleFramework.Engines;

namespace GenesisCarrier
{
    public class ACarrierEngine : SurfaceVessel
    {
        protected override float FORWARD_TOP_SPEED => 3000;
        protected override float REVERSE_TOP_SPEED => 200;
        protected override float STRAFE_MAX_SPEED => 0;
        protected override float VERT_MAX_SPEED => 10;
        protected override float FORWARD_ACCEL => FORWARD_TOP_SPEED / 30f;
        protected override float REVERSE_ACCEL => REVERSE_TOP_SPEED / 30f;
        protected override float STRAFE_ACCEL => STRAFE_MAX_SPEED / 10f;
        protected override float VERT_ACCEL => VERT_MAX_SPEED / 10f;

        protected override float waterDragDecay => 10f;
        protected override float airDragDecay => 5f;

    }
}