using GenesisCarrier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GenesisCarrier
{
    public class ACarrierEngine : VehicleFramework.Engines.ModVehicleEngine
    {

        protected override float FORWARD_TOP_SPEED => 400000;
        protected override float REVERSE_TOP_SPEED => 100000;
        protected override float STRAFE_MAX_SPEED => 100000;
        protected override float VERT_MAX_SPEED => 100000;

        protected override float FORWARD_ACCEL => FORWARD_TOP_SPEED / 100.33f;
        protected override float REVERSE_ACCEL => REVERSE_TOP_SPEED / 100.33f;
        protected override float STRAFE_ACCEL => STRAFE_MAX_SPEED / 100.33f;
        protected override float VERT_ACCEL => VERT_MAX_SPEED / 100.33f;

        private const float DEAD_ZONE_SOAK = 0;
        public override bool CanMoveAboveWater => true;
        public override bool CanRotateAboveWater => true;


        protected override float ForwardMomentum
        {
            get
            {
                if (Mathf.Abs(_forwardMomentum) < DEAD_ZONE_SOAK)
                {
                    _forwardMomentum = 0;
                }
                return _forwardMomentum;
            }
            set
            {
                if (value < -REVERSE_TOP_SPEED)
                {
                    _forwardMomentum = -REVERSE_TOP_SPEED;
                }
                else if (FORWARD_TOP_SPEED < value)
                {
                    _forwardMomentum = FORWARD_TOP_SPEED;
                }
                else
                {
                    _forwardMomentum = value;
                }
            }
        }
        protected override float RightMomentum
        {
            get
            {
                if (Mathf.Abs(_rightMomentum) < DEAD_ZONE_SOAK)
                {
                    _rightMomentum = 0;
                }
                return _rightMomentum;
            }
            set
            {
                if (value < -STRAFE_MAX_SPEED)
                {
                    _rightMomentum = -STRAFE_MAX_SPEED;
                }
                else if (STRAFE_MAX_SPEED < value)
                {
                    _rightMomentum = STRAFE_MAX_SPEED;
                }
                else
                {
                    _rightMomentum = value;
                }
            }
        }
        protected override float UpMomentum
        {
            get
            {
                if (Mathf.Abs(_upMomentum) < DEAD_ZONE_SOAK)
                {
                    _upMomentum = 0;
                }
                return _upMomentum;
            }
            set
            {
                if (value < -VERT_MAX_SPEED)
                {
                    _upMomentum = -VERT_MAX_SPEED;
                }
                else if (VERT_MAX_SPEED < value)
                {
                    _upMomentum = VERT_MAX_SPEED;
                }
                else
                {
                    _upMomentum = value;
                }
            }
        }
        public float GetCurrentPercentOfTopSpeed()
        {
            float totalMomentumNow = Mathf.Abs(ForwardMomentum) + Mathf.Abs(RightMomentum) + Mathf.Abs(UpMomentum);
            float topMomentum = FORWARD_TOP_SPEED + STRAFE_MAX_SPEED + VERT_MAX_SPEED;
            return totalMomentumNow / topMomentum;
        }
        public override void ControlRotation()
        {
            //Control rotation
            float pitchFactor = 0f;

            float yawFactor = 1.5f * (1.5f - GetCurrentPercentOfTopSpeed());
            Vector2 mouseDir = GameInput.GetLookDelta();
            float xRot = mouseDir.x;
            float yRot = mouseDir.y;
            rb.AddTorque(mv.transform.up * xRot * yawFactor * Time.deltaTime, ForceMode.VelocityChange);
            rb.AddTorque(mv.transform.right * yRot * -pitchFactor * Time.deltaTime, ForceMode.VelocityChange);

        }
        public override void DrainPower(Vector3 moveDirection)
        {
            float scalarFactor = 0.36f;
            float basePowerConsumptionPerSecond = moveDirection.x + moveDirection.y + moveDirection.z;
            float upgradeModifier = Mathf.Pow(0.85f, mv.numEfficiencyModules);
            mv.GetComponent<VehicleFramework.PowerManager>().TrySpendEnergy(scalarFactor * basePowerConsumptionPerSecond * upgradeModifier * Time.deltaTime);
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

        }
        protected override void ApplyDrag(Vector3 move)
        {
            return;
        }
        public override void ExecutePhysicsMove()
        {
            rb.AddForce(damageModifier * mv.transform.forward * (ForwardMomentum / 100f) * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddForce(damageModifier * mv.transform.right * (RightMomentum / 100f) * Time.fixedDeltaTime, ForceMode.VelocityChange);
            if (transform.position.y < 0)
            {
                rb.AddForce(damageModifier * mv.transform.up * (UpMomentum / 100f) * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
            VehicleFramework.Logger.Log("forward momentum: " + ForwardMomentum.ToString());
        }
    }
}