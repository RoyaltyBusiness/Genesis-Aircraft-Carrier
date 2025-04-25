using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VehicleFramework;
using VehicleFramework.VehicleParts;
using VehicleFramework.VehicleTypes;
using System.IO;
using System.Reflection;
using UnityEngine.U2D;
using System.Collections;
using UWE;
using static Nautilus.Assets.PrefabTemplates.FabricatorTemplate;
using UnityEngine.Assertions;
using VehicleFramework.Assets;
using VehicleFramework.Engines;
using RoyalCommonalities.Items.Materials;

namespace GenesisCarrier
{
    public class Genesis : Submarine
    {
        public static IEnumerator Register()
        {
            GetAssets();
            Submarine Genesis = assets.model.EnsureComponent<Genesis>();
            Genesis.name = "Genesis";
            yield return CoroutineHost.StartCoroutine(VehicleRegistrar.RegisterVehicle(Genesis));
            yield break;
        }
        public static VehicleFramework.Assets.VehicleAssets assets;
        public static void GetAssets()
        {
            assets = AssetBundleInterface.GetVehicleAssetsFromBundle("assets/genesis", "Genesis_vehicle");
        }
        public override string vehicleDefaultName
        {
            get
            {
                Language main = Language.main;
                bool flag = !(main != null);
                string result;
                if (flag)
                {
                    result = "Genesis";
                }
                else
                {
                    result = main.Get("Genesis");
                }
                return result;
            }
        }
        public override List<VehiclePilotSeat> PilotSeats
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehiclePilotSeat>();
                VehicleFramework.VehicleParts.VehiclePilotSeat vps = new VehicleFramework.VehicleParts.VehiclePilotSeat();
                Transform mainSeat = transform.Find("PilotSeat");
                vps.Seat = mainSeat.gameObject;
                vps.SitLocation = mainSeat.Find("SitLocation").gameObject;
                vps.LeftHandLocation = mainSeat;
                vps.RightHandLocation = mainSeat;
                vps.ExitLocation = mainSeat.Find("ExitLocation");
                list.Add(vps);
                return list;
            }
        }

        public override List<VehicleHatchStruct> Hatches
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleHatchStruct>();

                VehicleFramework.VehicleParts.VehicleHatchStruct interior_vhs = new VehicleFramework.VehicleParts.VehicleHatchStruct();
                Transform intHatch = transform.Find("Hatches/InsideHatch");
                interior_vhs.Hatch = intHatch.gameObject;
                interior_vhs.EntryLocation = intHatch.Find("Entry");
                interior_vhs.ExitLocation = intHatch.Find("Exit");
                interior_vhs.SurfaceExitLocation = intHatch.Find("SurfaceExit");

                VehicleFramework.VehicleParts.VehicleHatchStruct exterior_vhs = new VehicleFramework.VehicleParts.VehicleHatchStruct();
                Transform extHatch = transform.Find("Hatches/OutsideHatch");
                exterior_vhs.Hatch = extHatch.gameObject;
                exterior_vhs.EntryLocation = interior_vhs.EntryLocation;
                exterior_vhs.ExitLocation = interior_vhs.ExitLocation;
                exterior_vhs.SurfaceExitLocation = interior_vhs.SurfaceExitLocation;

                list.Add(interior_vhs);
                list.Add(exterior_vhs);
                return list;
            }
        }
        public override GameObject StorageRootObject
        {
            get
            {
                return transform.Find("StorageRoot").gameObject;
            }
        }

        public override GameObject ModulesRootObject
        {
            get
            {
                return transform.Find("ModulesRoot").gameObject;
            }
        }
        public override GameObject VehicleModel
        {
            get
            {
                return assets.model;
            }
        }

        public override GameObject CollisionModel
        {
            get
            {
                return transform.Find("Collider").gameObject;
            }
        }
        public override List<GameObject> TetherSources
        {
            get
            {
                var list = new List<GameObject>();
                foreach (Transform child in transform.Find("TetherSources"))
                {
                    list.Add(child.gameObject);
                }
                return list;
            }
        }

        public override BoxCollider BoundingBoxCollider
        {
            get
            {
                return transform.Find("BoundingBoxCollider").gameObject.GetComponent<BoxCollider>();
            }
        }
        public override List<VehicleStorage> InnateStorages
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleStorage>();

                Transform innate1 = transform.Find("InnateStorages/Storage1");

                VehicleFramework.VehicleParts.VehicleStorage IS1 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS1.Container = innate1.gameObject;
                IS1.Height = 10;
                IS1.Width = 10;
                list.Add(IS1);

                return list;
            }
        }
        public override List<VehicleUpgrades> Upgrades
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleUpgrades>();
                VehicleFramework.VehicleParts.VehicleUpgrades vu = new VehicleFramework.VehicleParts.VehicleUpgrades();
                vu.Interface = transform.Find("Upgrades").gameObject;
                vu.Flap = vu.Interface;
                list.Add(vu);
                return list;
            }
        }
        public override List<GameObject> CanopyWindows
        {
            get
            {
                var list = new List<GameObject>();
                list.Add(transform.Find("Model/Conopy").gameObject);
                return list;
            }
        }
        public override Dictionary<TechType, int> Recipe
        {

            get
            {
                Dictionary<TechType, int> recipe = new Dictionary<TechType, int>();
                recipe.Add(ModularHullPiece.Info.TechType, 6);
                recipe.Add(TechType.AdvancedWiringKit, 4);
                recipe.Add(TechType.ReactorRod, 2);
                return recipe;

            }
        }
        public override ModVehicleEngine Engine
        {
            get
            {
                return gameObject.EnsureComponent<ACarrierEngine>();

            }

        }
        public override Atlas.Sprite CraftingSprite => VehicleFramework.Assets.SpriteHelper.GetSprite("assets/GenesisCrafter.png");
        public override Atlas.Sprite PingSprite => VehicleFramework.Assets.SpriteHelper.GetSprite("assets/GenesisPing.png");
        public override float ExitVelocityLimit => 999999;
        public override int Mass => 1000000;
        public override int NumModules => 2;
        public override string Description => "A flipin Aircraft Carrier Baby!!!";
        public override string EncyclopediaEntry => "todo";
        public override int BaseCrushDepth => 9999;
        public override int MaxHealth => 290000;
        public override bool CanLeviathanGrab => false;
    }
}
