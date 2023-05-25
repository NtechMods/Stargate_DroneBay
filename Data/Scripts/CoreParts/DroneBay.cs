using static Scripts.Structure;
using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.ModelAssignmentsDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.Prediction;
using static Scripts.Structure.WeaponDefinition.TargetingDef.BlockTypes;
using static Scripts.Structure.WeaponDefinition.TargetingDef.Threat;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef.HardwareType;

namespace Scripts
{   // Don't edit above this line
    partial class Parts
    {
        WeaponDefinition DroneBay => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "DroneBay",
                        SpinPartId = "",
                        MuzzlePartId = "DroneEle",
                        AzimuthPartId = "DroneAzi",
                        ElevationPartId = "DroneEle", 
                        DurabilityMod = 0.1f,
                        IconName = "Plac_empty.dds" 
                    },
                },
                Muzzles = new []
                {
                    "muzzle_missile_001", //muzzle_missile_001
                },
                Ejector = "",
                Scope = "", // Where line of sight checks are performed from. Must be clear of block collision.
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Grids, Meteors, // threats percieved automatically without changing menu settings
                },
                SubSystems = new[]
                {
                    Offense, Power, Thrust, Utility, Production, Any, // subsystems the gun targets
                },
                ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                IgnoreDumbProjectiles = false, // Don't fire at non-smart projectiles.
                LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
                MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                MaxTargetDistance = 0, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
                MinTargetDistance = 0, // 0 = unlimited, Min target distance that targets will be automatically shot at.
                TopTargets = 0, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 0, // 0 = unlimited, max number of blocks to randomize between
                StopTrackingSpeed = 200, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                PartName = "DroneBay", // name of weapon in terminal
                DeviateShotAngle = 10f,
                AimingTolerance = 180f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = true,

                Ui = new UiDef
                {
                    RateOfFire = true,
                    DamageModifier = true,
                    ToggleGuidance = false,
                    EnableOverload =  true,
                },
                Ai = new AiDef
                {
                    TrackTargets = true, // Whether this weapon tracks its own targets, or (for multiweapons) relies on the weapon with PrimaryTracking enabled for target designation.
                    TurretAttached = true, // Whether this weapon is a turret and should have the UI and API options for such.
                    TurretController = true, // Whether this weapon can physically control the turret's movement.
                    PrimaryTracking = false, // For multiweapons: whether this weapon should designate targets for other weapons on the platform without their own tracking.
                    LockOnFocus = true, // Whether this weapon should automatically fire at a target that has been locked onto via HUD.
                    SuppressFire = false, // If enabled, weapon can only be fired manually.
                    OverrideLeads = true, // Disable target leading on fixed weapons, or allow it for turrets.
                    DefaultLeadGroup = 0, // Default LeadGroup setting, range 0-5, 0 is disables lead group.  Only useful for fixed weapons or weapons set to OverrideLeads.
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.06f,
                    ElevateRate = 0.06f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -85,
                    MaxElevation = 85,
                    HomeAzimuth = 0, // Default resting rotation angle
                    HomeElevation = 0, // Default resting elevation
                    FixedOffset = false,
                    IdlePower = 0.25f, // Constant base power draw in MW.
                    InventorySize = 1.4f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    Type = BlockWeapon, // BlockWeapon, HandWeapon, Phantom 
                    CriticalReaction = new CriticalDef
                    {
                        Enable = true, // Enables Warhead behaviour
                        DefaultArmedTimer = 120,
                        PreArmed = true,
                        TerminalControls = true,
                        AmmoRound = "", // Optional. If specified, the warhead will always use this ammo on detonation rather than the currently selected ammo.
                    },
                },
                Other = new OtherDef
                {
                    ConstructPartCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = false,
                    RestrictionRadius = 10, // Meters, radius of sphere disable this gun if another is present
                    CheckInflatedBox = false, // if true, the bounding box of the gun is expanded by the RestrictionRadius
                    CheckForAnyWeapon = false, // if true, the check will fail if ANY gun is present, false only looks for this subtype
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 300,
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 30, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 2, //heat generated per shot
                    MaxHeat = 1000, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = .95f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 30, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 8,
                    DelayAfterBurst = 30, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFull = true,
                    GiveUpAfter = false,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
                    MagsToLoad = 1, // Number of physical magazines to consume on reload.
                    DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                    SpinFree = false, // Spin barrel while not firing.
                    StayCharged = false, // Will start recharging whenever power cap is not full.
                    MaxActiveProjectiles = 50, // Maximum number of drones in flight (only works for drone launchers)
                    MaxReloads = 0, // Maximum number of reloads in the LIFETIME of a weapon
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "DroneTravel",
                    FiringSound = "DroneFire", // subtype name from sbc 
                    FiringSoundPerShot = true,
                    ReloadSound = "",
                    NoAmmoSound = "ArcWepShipGatlingNoAmmo",
                    HardPointRotationSound = "",
                    BarrelRotationSound = "",
                    FireSoundEndDelay = 0, // Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef
                    {
                        Name = "", // Smoke_LargeGunShot
                        Color = Color(red: 2.5f, green: 2f, blue: 0.6f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0), // Offsets the effect from the muzzle empty.
                        DisableCameraCulling = false, // If not true will not cull when not in view of camera, be careful with this and only use if you know you need it
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false, // Whether to end a looping effect instantly when firing stops.
                            MaxDistance = 800,
                            MaxDuration = 0,
                            Scale = 1.0f,
                        },
                    },
                    Effect2 = new ParticleDef
                    {
                        Name = "",//Muzzle_Flash_Large
                        Color = Color(red: 2.5f, green: 2f, blue: 0.6f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0), // Offsets the effect from the muzzle empty.
                        DisableCameraCulling = false, // If not true will not cull when not in view of camera, be careful with this and only use if you know you need it
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false, // Whether to end a looping effect instantly when firing stops.
                            MaxDistance = 800,
                            MaxDuration = 0,
                            Scale = 1f,
                        },
                    },
                },
            },

            Ammos = new [] {
                DroneMag,
				EDrone,
                SuperDroneMag
            },
             Animations = DroneAnimation,
            // Don't edit below this line
        };
    }
}