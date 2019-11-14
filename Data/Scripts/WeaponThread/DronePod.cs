using static WeaponThread.Session.ShieldDefinition.ShieldType;
using static WeaponThread.Session.AmmoTrajectory.GuidanceType;
using static WeaponThread.Session.HardPointDefinition.Prediction;
using static WeaponThread.Session.AreaDamage.AreaEffectType;
using static WeaponThread.Session.TargetingDefinition.BlockTypes;
using static WeaponThread.Session.TargetingDefinition.Threat;
using static WeaponThread.Session.Shrapnel.ShrapnelShape;
using static WeaponThread.Session.ShapeDefinition.Shapes;
using static WeaponThread.Session;

namespace WeaponThread
{   // Don't edit above this line
    partial class Weapons
    {
        WeaponDefinition DronePod => new WeaponDefinition
        {
            Assignments = new ModelAssignments
            {
                MountPoints = new[]
                {
                    MountPoint(subTypeId: "DronePod", aimPartId: "MissileTurretBarrels", muzzlePartId: "MissileTurretBarrels"),
                },
                Barrels = Names("muzzle_missile_001"),
                EnableSubPartPhysics = false
            },
            HardPoint = new HardPointDefinition
            {
                WeaponId = "DronePod", // name of weapon in terminal
                AmmoMagazineId = "Blank",
                Block = AimControl(trackTargets: true, turretAttached: false, turretController: false, primaryTracking: true, rotateRate: 0f, elevateRate: 0f, minAzimuth: 0, maxAzimuth: 0, minElevation: 0, maxElevation: 0, offset: Vector(x: 0, y: 0, z: 0), fixedOffset: false, debug: false),
                DeviateShotAngle = 0f,
                AimingTolerance = 180f, // 0 - 180 firing angle
                EnergyCost = 0.0002f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                Hybrid = false, //projectile based weapon with energy cost
                EnergyPriority = 0, //  0 = Lowest shares power with shields, 1 = Medium shares power with thrusters and over powers shields, 2 = Highest Does not share power will use all available power until energy requirements met
                RotateBarrelAxis = 0, // 0 = off, 1 = xAxis, 2 = yAxis, 3 = zAxis
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                GridWeaponCap = 0,// 0 = unlimited, the smallest weapon cap assigned to a subTypeId takes priority.
                Ui = Display(rateOfFire: true, damageModifier: true, toggleGuidance: false, enableOverload: false),

                Loading = new AmmoLoading
                {
                    RateOfFire = 200,
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 0, //heat generated per shot
                    MaxHeat = 1800, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = .95f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 200, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 10,
                    DelayAfterBurst = 2800, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                },
            },
            Targeting = new TargetingDefinition
            {
                Threats = Valid(Grids),
                SubSystems = Priority(Power, Utility, Offense, Thrust), //define block type targeting order
                ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                MinimumDiameter = 10, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
                StopTrackingSpeed = 99, // do not track target threats traveling faster than this speed
            },
            DamageScales = new DamageScaleDefinition
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
                Grids = Options(largeGridModifier: 10f, smallGridModifier: 5f),
                Armor = Options(armor: 1f, light: .1f, heavy: 100f, nonArmor: .04f),
                Shields = Options(modifier: .01f, type: Energy), // Types: Kinetic, Energy, Emp or Bypass

                // ignoreOthers will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = SubTypeIds(false),
            },
            Ammo = new AmmoDefinition
            {
                BaseDamage = 200f,
                Mass = 100f, // in kilograms
                Health = 10, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
                BackKickForce = 0f,
                Shape = Options(shape: Sphere, diameter: 2.5), //defines the collision shape of projectile, defaults to visual Line Length
                ObjectsHit = Options(maxObjectsHit: 1, countBlocks: false), // 0 = disabled, value determines max objects (and/or blocks) penetrated per hit
                Shrapnel = Options(baseDamage: 0, fragments: 0, maxTrajectory: 100, noAudioVisual: true, noGuidance: true, shape: HalfMoon),

                AreaEffect = new AreaDamage
                {
                    AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive is keens, Radiant is not.
                    AreaEffectDamage = 0f, // 0 = use spillover from BaseDamage, otherwise use this value.
                    AreaEffectRadius = 0f,
                    Explosions = Options(noVisuals: false, noSound: false, scale: 1, customParticle: "Energy_Explosion", customSound: ""),
                    Detonation = Options(detonateOnEnd: true, armOnlyOnHit: false, detonationDamage: 1000, detonationRadius: 5),
                    EwarFields = Options(duration: 600, stackDuration: true, depletable: true)
                },
                Beams = new BeamDefinition
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new AmmoTrajectory
                {
                    Guidance = Smart,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 60f,
                    DesiredSpeed = 1000f,
                    MaxTrajectory = 15000f,
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new Smarts
                    {
                        Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 1f, // controls how responsive tracking is.
                        MaxLateralThrust = 0.3f, // controls how sharp the trajectile may turn
                        TrackingDelay = 1200, // Measured in line length units traveled.
                        MaxChaseTime = 9600, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoints.
                    },
                    Mines = Options(detectRadius: 200, deCloakRadius: 100, fieldTime: 1800, cloak: true, persist: false),
                },
            },
            Graphics = new GraphicDefinition
            {
                ModelName = "\\Models\\Weapons\\Drone_Projectile.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new ParticleDefinition
                {
                    Ammo = new Particle
                    {
                        Name = "ShipWelderArc",
                        Color = Color(red: 245, green: 200, blue: 66, alpha: .02f),//245, 200, 66
                        Offset = Vector(x: 0, y: 0, z: -0.5),
                        Extras = Options(loop: true, restart: false, distance: 5000, duration: 12, scale: 1.1f)
                    },
                    Hit = new Particle
                    {
                        Name = "", //"MaterialHit_Metal_GatlingGun",
                        Color = Color(red: 10, green: 1, blue: 0, alpha: 2),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = Options(loop: false, restart: false, distance: 5000, duration: 1, scale: 0.2f),
                    },
                    Barrel1 = new Particle
                    {
                        Name = "", // Smoke_LargeGunShot
                        Color = Color(red: 255, green: 0, blue: 0, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = Options(loop: false, restart: false, distance: 50, duration: 6, scale: 1f),
                    },
                    Barrel2 = new Particle
                    {
                        Name = "",//Muzzle_Flash_Large
                        Color = Color(red: 255, green: 0, blue: 0, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = Options(loop: false, restart: false, distance: 50, duration: 6, scale: 1f),
                    },
                },
                Line = new LineDefinition
                {
                    Tracer = Base(enable: false, length: 1f, width: 0.2f, color: Color(red: 2, green: 2, blue: 30, alpha: 1)),
                    TracerMaterial = "WeaponLaser", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 1f, end: 1f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Trail = Options(enable: false, material: "WeaponLaser", decayTime: 600, color: Color(red: 8, green: 8, blue: 64, alpha: 8)),
                    OffsetEffect = Options(maxOffset: 0, minLength: 5, maxLength: 15), // 0 offset value disables this effect
                },
            },
            Audio = new AudioDefinition
            {
                HardPoint = new AudioHardPointDefinition
                {
                    FiringSound = "", // WepShipGatlingShot
                    FiringSoundPerShot = true,
                    ReloadSound = "",
                    NoAmmoSound = "",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "WepShipGatlingRotation",
                },

                Ammo = new AudioAmmoDefinition
                {
                    TravelSound = "",
                    HitSound = "",
                }, // Don't edit below this line
            },
        };
    }
}
