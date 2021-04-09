using static WeaponThread.WeaponStructure.WeaponDefinition;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.ShapeDef.Shapes;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.GraphicDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.TrajectoryDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.TrajectoryDef.GuidanceType;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.DamageScaleDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.DamageScaleDef.ShieldDef.ShieldType;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.AreaDamageDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.AreaDamageDef.AreaEffectType;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.TracerBaseDef;
namespace WeaponThread
{ // Don't edit above this line
    partial class Weapons
    {
        private AmmoDef EDrone => new AmmoDef
        {
            AmmoMagazine = "Energy",
            AmmoRound = "EDrone",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.002f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 10000f,
            Mass = 0.05f, // in kilograms
            Health = 100, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1f,
            HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, //SphereShape, LineShape
                Diameter = 2,
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 10000, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
			Shrapnel = new ShrapnelDef
            {
                AmmoRound = "",
                Fragments = 2,
                Degrees = 10,
                Reverse = false,
                RandomizeDir = false,
            },
            Pattern = new AmmoPatternDef
            {
                Ammos = new[] {
                    "", // use other ammo types here, can spawn different ammos from primary
                },
                Enable = false,
                TriggerChance = 1f, //percent of change chance
                Random = false, //randomizes ammo
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, //skips primary ammo
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
                FallOff = new FallOffDef
                {
                    Distance = 1000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0.45f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f,
                    Small = -1f,
                },
                Armor = new ArmorDef
                {
                    Armor = -1f,
                    Light = 0.9f,
                    Heavy = 0.7f,
                    NonArmor = -1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 0.0f,
                    Type = Emp, // Energy, Kinetic, Emp, Bypass
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                AreaEffectDamage = 0f, // 0 = use spillover from BaseDamage, otherwise use this value.
                AreaEffectRadius = 0f,
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 60,
                    PulseChance = 75,
                    GrowTime = 100,
                    HideModel = false,
                    ShowParticle = false,
                    Particle = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    Scale = 1,
                    CustomParticle = "Energy_Explosion",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = true,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 1000,
                    DetonationRadius = 5,
                },
                EwarFields = new EwarFieldsDef
                {
                    Duration = 600,
                    StackDuration = true,
                    Depletable = true,
                    MaxStacks = 1,
                    TriggerRange = 5f,
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = Smart, // None, TravelTo, Smart
                TargetLossDegree = 360f,
                TargetLossTime = 7200, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 7200, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 100f, // 0 is instant, goes by meters per second
                DesiredSpeed = 100, // meters per second
                MaxTrajectory = 7500f, // in meters
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.8f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 0.2f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.1f, // controls how sharp the trajectile may turn
                    TrackingDelay = 50, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 3600, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
					MaxTargets = 200,
                    NoTargetExpire = true, // Expire without ever having a target at TargetLossTime
                    Roam = true, // Roam current area after target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 1800,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\Ammo\\Drone_Projectile.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "DroneGlow", //ShipWelderArc
                        ShrinkByDistance = true,
                        Color = Color(red: 245, green: 200, blue: 11, alpha: 1.02f),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 12,
                            Scale = 1f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 10, green: 3, blue: 0, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                            HitPlayChance = 0.1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "WeaponLaser", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.025f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = false,
                        Length = 1f,
                        Width = 0.2f,
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Segmentation = new SegmentDef
                        {
                            Material = "WeaponLaser",
                            SegmentLength = 5f,
                            SegmentGap = 3f,
                            Speed = 15f,
                            Color = Color(red: 2, green: 2, blue: 2, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Material = "ProjectileTrailLine",
                        DecayTime = 512,
                        Color = Color(red: 2.45f, green: 2.00f, blue: 0.66f, alpha: 1f),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 5f,
                        MaxLength = 15,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "DroneTravel",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };
		
		private AmmoDef DroneMag => new AmmoDef
        {
            AmmoMagazine = "DroneMag",
            AmmoRound = "PhyDrone",
            HybridRound = true, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.0000000002f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 10000f,
            Mass = 0.05f, // in kilograms
            Health = 100, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1f,
            HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, //SphereShape, LineShape
                Diameter = 2,
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 10000, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
			Shrapnel = new ShrapnelDef
            {
                AmmoRound = "",
                Fragments = 2,
                Degrees = 10,
                Reverse = false,
                RandomizeDir = false,
            },
            Pattern = new AmmoPatternDef
            {
                Ammos = new[] {
                    "", // use other ammo types here, can spawn different ammos from primary
                },
                Enable = false,
                TriggerChance = 1f, //percent of change chance
                Random = false, //randomizes ammo
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, //skips primary ammo
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
                FallOff = new FallOffDef
                {
                    Distance = 1000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0.45f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f,
                    Small = -1f,
                },
                Armor = new ArmorDef
                {
                    Armor = -1f,
                    Light = 0.9f,
                    Heavy = 0.7f,
                    NonArmor = -1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 0.5f,
                    Type = Emp, // Energy, Kinetic, Emp, Bypass
                    BypassModifier = 0.1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                AreaEffectDamage = 0f, // 0 = use spillover from BaseDamage, otherwise use this value.
                AreaEffectRadius = 0f,
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 60,
                    PulseChance = 75,
                    GrowTime = 100,
                    HideModel = false,
                    ShowParticle = false,
                    Particle = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    Scale = 1,
                    CustomParticle = "Energy_Explosion",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = true,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 1000,
                    DetonationRadius = 5,
                },
                EwarFields = new EwarFieldsDef
                {
                    Duration = 600,
                    StackDuration = true,
                    Depletable = true,
                    MaxStacks = 1,
                    TriggerRange = 5f,
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = Smart, // None, TravelTo, Smart
                TargetLossDegree = 360f,
                TargetLossTime = 7200, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 7200, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 100f, // 0 is instant, goes by meters per second
                DesiredSpeed = 100, // meters per second
                MaxTrajectory = 7500f, // in meters
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.8f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 0.2f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.1f, // controls how sharp the trajectile may turn
                    TrackingDelay = 50, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 3600, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
					MaxTargets = 200,
                    NoTargetExpire = true, // Expire without ever having a target at TargetLossTime
                    Roam = true, // Roam current area after target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 1800,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\Ammo\\Drone_Projectile.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "DroneGlow", //ShipWelderArc
                        ShrinkByDistance = true,
                        Color = Color(red: 245, green: 220, blue: 66, alpha: 0.02f),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 12,
                            Scale = 1f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 10, green: 3, blue: 0, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                            HitPlayChance = 0.1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "WeaponLaser", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.025f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = false,
                        Length = 1f,
                        Width = 0.2f,
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Segmentation = new SegmentDef
                        {
                            Material = "WeaponLaser",
                            SegmentLength = 5f,
                            SegmentGap = 3f,
                            Speed = 15f,
                            Color = Color(red: 2, green: 2, blue: 2, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Material = "ProjectileTrailLine",
                        DecayTime = 512,
                        Color = Color(red: 2.45f, green: 2.00f, blue: 0.66f, alpha: 1f),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 5f,
                        MaxLength = 15,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "DroneTravel",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };
	

// Super Drones, not craftable shield penetration of 100%
        private AmmoDef SuperDrones => new AmmoDef
        {
            AmmoMagazine = "SuperDroneMag",
            AmmoRound = "Super",
            HybridRound = true, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.0000000002f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 100000f,
            Mass = 0.05f, // in kilograms
            Health = 1000, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1f,
            HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, //SphereShape, LineShape
                Diameter = 2,
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 10000, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
			Shrapnel = new ShrapnelDef
            {
                AmmoRound = "",
                Fragments = 2,
                Degrees = 10,
                Reverse = false,
                RandomizeDir = false,
            },
            Pattern = new AmmoPatternDef
            {
                Ammos = new[] {
                    "", // use other ammo types here, can spawn different ammos from primary
                },
                Enable = false,
                TriggerChance = 1f, //percent of change chance
                Random = false, //randomizes ammo
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, //skips primary ammo
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
                FallOff = new FallOffDef
                {
                    Distance = 1000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0.45f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f,
                    Small = -1f,
                },
                Armor = new ArmorDef
                {
                    Armor = -1f,
                    Light = 0.9f,
                    Heavy = 0.7f,
                    NonArmor = -1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f,
                    Type = Emp, // Energy, Kinetic, Emp, Bypass
                    BypassModifier = 1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                AreaEffectDamage = 0f, // 0 = use spillover from BaseDamage, otherwise use this value.
                AreaEffectRadius = 0f,
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 60,
                    PulseChance = 75,
                    GrowTime = 100,
                    HideModel = false,
                    ShowParticle = false,
                    Particle = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    Scale = 1,
                    CustomParticle = "Energy_Explosion",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = true,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 1000,
                    DetonationRadius = 5,
                },
                EwarFields = new EwarFieldsDef
                {
                    Duration = 600,
                    StackDuration = true,
                    Depletable = true,
                    MaxStacks = 1,
                    TriggerRange = 5f,
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = Smart, // None, TravelTo, Smart
                TargetLossDegree = 360f,
                TargetLossTime = 7200, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 7200, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 100f, // 0 is instant, goes by meters per second
                DesiredSpeed = 100, // meters per second
                MaxTrajectory = 7500f, // in meters
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.8f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 0.2f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.1f, // controls how sharp the trajectile may turn
                    TrackingDelay = 50, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 3600, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
					MaxTargets = 200,
                    NoTargetExpire = true, // Expire without ever having a target at TargetLossTime
                    Roam = true, // Roam current area after target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 1800,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\Ammo\\Drone_Projectile.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "DroneGlow", //ShipWelderArc
                        ShrinkByDistance = true,
                        Color = Color(red: 10, green: 30, blue: 256, alpha: 1.02f),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 12,
                            Scale = 1f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 10, green: 3, blue: 0, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                            HitPlayChance = 0.1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "WeaponLaser", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.025f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = false,
                        Length = 1f,
                        Width = 0.2f,
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Segmentation = new SegmentDef
                        {
                            Material = "WeaponLaser",
                            SegmentLength = 5f,
                            SegmentGap = 3f,
                            Speed = 15f,
                            Color = Color(red: 2, green: 2, blue: 2, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Material = "ProjectileTrailLine",
                        DecayTime = 512,
                        Color = Color(red: 2.45f, green: 2.00f, blue: 0.66f, alpha: 1f),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 5f,
                        MaxLength = 15,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "DroneTravel",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };














    }
}
