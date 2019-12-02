using System.Diagnostics;
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
    partial class Weapons { WeaponDefinition DroneBay => new WeaponDefinition {
    Assignments = new ModelAssignments
    {
        MountPoints = new[]
        {
            MountPoint(subTypeId: "DroneBay", aimPartId: "MissileTurretBarrels", muzzlePartId: "MissileTurretBarrels"),
        },
        Barrels = Names("muzzle_missile_001"),
        EnableSubPartPhysics = false
    },
    HardPoint = new HardPointDefinition
    {
        WeaponId = "DroneBay", // name of weapon in terminal
        AmmoMagazineId = "Blank",
        Block = AimControl(trackTargets: true, turretAttached: false, turretController: false, primaryTracking: true, rotateRate: 0f, elevateRate: 0f, offset: Vector(x: 0, y: 0, z: 0), fixedOffset: false, debug: false),
        DeviateShotAngle = 70f,
        AimingTolerance = 180f,
        EnergyCost = 0.005f,
        Hybrid = false, //projectile based weapon with energy cost
        EnergyPriority = 0, //  0 = Lowest shares power with shields, 1 = Medium shares power with thrusters and over powers shields, 2 = Highest Does not share power will use all available power until energy requirements met
        RotateBarrelAxis = 0,
        AimLeadingPrediction = Advanced,
        DelayCeaseFire = 0,
        GridWeaponCap = 2,// 0 = unlimited, the smallest weapon cap assigned to a subTypeId takes priority.
        Ui = Display(rateOfFire: true, damageModifier: true, toggleGuidance: false, enableOverload: false),

        Loading = new AmmoLoading
        {
            RateOfFire = 800,
            BarrelsPerShot = 1,
            TrajectilesPerBarrel = 1,
            SkipBarrels = 0,
            ReloadTime = 0,
            DelayUntilFire = 0,
            HeatPerShot = 20, //heat generated per shot
            MaxHeat = 1800, //max heat before weapon enters cooldown (70% of max heat)
            Cooldown = .95f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
            HeatSinkRate = 1, //amount of heat lost per second
            DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
            ShotsInBurst = 13,
            DelayAfterBurst = 1500, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
        },
    },
    Targeting = new TargetingDefinition
    {
        Threats = Valid(Characters, Projectiles, Grids),
        SubSystems = Priority(Thrust, Utility, Offense, Power, Production, Any), //define block type targeting order
        ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
        MinimumDiameter = 1, // 0 = unlimited, Minimum radius of threat to engage.
        MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
        TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
        TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
        StopTrackingSpeed = 70, // do not track target threats traveling faster than this speed
    },
    DamageScales = new DamageScaleDefinition
    {
        MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
        DamageVoxels = false, // true = voxels are vulnerable to this weapon
        SelfDamage = false, // true = allow self damage.

        // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
        Characters = -1f,
        Grids = Options(largeGridModifier: -1f, smallGridModifier: -1f),
        Armor = Options(armor: -1f, light: -1f, heavy: -1f, nonArmor: -1f),
        Shields = Options(modifier: -1f, type: Kinetic), // Types: Heal, Kinetic, Energy, Emp or Bypass

        // ignoreOthers will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
        Custom = SubTypeIds(false),
    },
    Ammo = new AmmoDefinition
    {
        BaseDamage = 5000f, 		// how much damage the projectile does
        Mass = 0.05f,
        Health = 900,
        BackKickForce = 1f,
        Shape = Options(shape: Sphere, diameter: 0.5), //defines the collision shape of projectile, defaults to visual Line Length
        ObjectsHit = Options(maxObjectsHit: 1500, countBlocks: true), // 0 = disabled, value determines max objects (and/or blocks) penetrated per hit
        Shrapnel = Options(baseDamage: 0, fragments: 0, maxTrajectory: 2600, noAudioVisual: true, noGuidance: true, shape: FullMoon),

        AreaEffect = new AreaDamage
        {
            AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
            AreaEffectDamage = 0f, // 0 = use spillover from BaseDamage, otherwise apply this value after baseDamage.
            AreaEffectRadius = 0f,
            Pulse = Options(interval: 30, pulseChance: 25), // interval measured in game ticks (60 == 1 second)
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
            Guidance = Smart, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed
            TargetLossDegree = 180f,
            TargetLossTime = 3600, // time until trajectile death,  Measured in ticks (6 = 100ms, 60 = 1 seconds, etc..).
            AccelPerSec = 100f,
            DesiredSpeed = 1200f,
            MaxTrajectory = 7500f,
            FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest and remain for a time (Measured in game ticks, 60 = 1 second)
            SpeedVariance = Random(start: 0, end: 0),
            RangeVariance = Random(start: 0, end: 0),
            Smarts = new Smarts
            {
                Inaccuracy = 0.4f, // 0 = perfect, aim pos will be 0 - # meters from center, recalculates on miss.
                Aggressiveness = 1f, // controls how responsive tracking is.
                MaxLateralThrust = 0.5f, // controls how sharp the trajectile may turn (1 is max value)
                TrackingDelay = 20, // Measured in line length units traveled. How far to delay tracking once fired.
                MaxChaseTime = 1200, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
            },
            Mines = Options(detectRadius: 180, deCloakRadius: 200, fieldTime: 1800, cloak: true, persist: false),
        },
    },
    Graphics = new GraphicDefinition
    {
        ModelName = "\\Models\\Ammo\\Drone_Projectile.mwm", //\\Models\\Weapons\\Torpedo_Ammo_1st.mwm
        VisualProbability = 1f,
        ShieldHitDraw = true,
        Particles = new ParticleDefinition
        {
            Ammo = new Particle
            {
                Name = "ShipWelderArc",
                Color = Color(red: 245, green: 200, blue: 66, alpha: .02f),//245, 200, 66
                Offset = Vector(x: 0, y: 0, z: 0),
                Extras = Options(loop: true, restart: false, distance: 5000, duration: 12, scale: 1f),
            },
            Hit = new Particle
            {
                Name = "",
                Color = Color(red: 10, green: 1, blue: 0, alpha: 2),
                Offset = Vector(x: 0, y: 0, z: 0),
                Extras = Options(loop: false, restart: false, distance: 5000, duration: 1, scale: 2.5f),
            },
            Barrel1 = new Particle
            {
                Name = "", 
                Color = Color(red: 255, green: 0, blue: 0, alpha: 1),
                Offset = Vector(x: 0, y: -1, z: 0),
                Extras = Options(loop: false, restart: false, distance: 50, duration: 6, scale: 1f),
            },
            Barrel2 = new Particle
            {
                Name = "",
                Color = Color(red: 255, green: 0, blue: 0, alpha: 1),
                Offset = Vector(x: 0, y: -1, z: 0),
                Extras = Options(loop: false, restart: false, distance: 50, duration: 6, scale: 1f),
            },
        },
        Line = new LineDefinition
        {
            Tracer = Base(enable: false, length: 1f, width: 0.2f, color: Color(red: 2, green: 2, blue: 30, alpha: 1)),
            TracerMaterial = "WeaponLaser", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
            ColorVariance = Random(start: 1, end: 1), // multiply the color by random values within range.
            WidthVariance = Random(start: 0, end: 0), // adds random value to default width (negatives shrinks width)
            Trail = Options(enable: false, material: "WeaponLaser", decayTime: 600, color: Color(red: 8, green: 8, blue: 64, alpha: 8)),
            OffsetEffect = Options(maxOffset: 0, minLength: 5, maxLength: 15) // 0 offset value disables this effect
        },
    },
    Audio = new AudioDefinition
    {
        HardPoint = new AudioHardPointDefinition
        {
            FiringSound = "",
            FiringSoundPerShot = false,
            ReloadSound = "",
            NoAmmoSound = "",
            HardPointRotationSound = "",
            BarrelRotationSound = "",
        },

        Ammo = new AudioAmmoDefinition
        {
            TravelSound = "DroneBayHum",
            HitSound = "",
        }, // Don't edit below this line
    }, 
};}}