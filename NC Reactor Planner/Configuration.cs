﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace NC_Reactor_Planner
{
    /// <summary>
    /// This struct holds the data that we save/load by stringifying it into json.
    /// </summary>
    public struct ConfigFile
    {
        public Version saveVersion;
        public FissionValues Fission;
        public CraftingMaterials ResourceCosts;
        public Dictionary<string, FuelValues> Fuels;
        public Dictionary<string, NeutronSourceValues> NeutronSources;
        public Dictionary<string, ReflectorValues> Reflectors;
        public Dictionary<string, CoolantRecipeValues> CoolantRecipes;
        public Dictionary<string, HeatSinkValues> HeatSinks;
        public Dictionary<string, ModeratorValues> Moderators;
        public Dictionary<string, NeutronShieldValues> NeutronShields;

        public ConfigFile(Version sv, FissionValues fs, Dictionary<string, FuelValues> f, Dictionary<string, NeutronSourceValues> ns, Dictionary<string, ReflectorValues> rfs, Dictionary<string, CoolantRecipeValues> cr, Dictionary<string, HeatSinkValues> c, Dictionary<string, ModeratorValues> m, CraftingMaterials cm, Dictionary<string, NeutronShieldValues> nsh)
        {
            saveVersion = sv;
            Fission = fs;
            Fuels = f;
            NeutronSources = ns;
            Reflectors = rfs;
            CoolantRecipes = cr;
            HeatSinks = c;
            Moderators = m;
            ResourceCosts = cm;
            NeutronShields = nsh;
        }
    }

    public struct FuelValues
    {
        public double BaseEfficiency;
        public double BaseHeat;
        public double FuelTime;
        public int CriticalityFactor;
        public bool SelfPriming;

        public FuelValues(double be, double bh, double ft, int cf, bool sp = false)
        {
            BaseEfficiency = be;
            BaseHeat = bh;
            FuelTime = ft;
            CriticalityFactor = cf;
            SelfPriming = sp;
        }

        public FuelValues(List<object> values)
        {
            BaseEfficiency = Convert.ToDouble(values[0]);
            BaseHeat = Convert.ToDouble(values[1]);
            FuelTime = Convert.ToDouble(values[2]);
            CriticalityFactor = Convert.ToInt32(values[3]);
            if (values.Count > 4)
                SelfPriming = Convert.ToBoolean(values[4]);
            else
                SelfPriming = false;
        }
    }

    public struct NeutronSourceValues
    {
        public double Efficiency;

        public NeutronSourceValues(double e)
        {
            Efficiency = e;
        }

        public NeutronSourceValues(List<object> values)
        {
            Efficiency = Convert.ToDouble(values[0]);
        }
    }

    public struct ReflectorValues
    {
        public double ReflectivityMultiplier;
        public double EfficiencyMultiplier;

        public ReflectorValues(double rm, double em)
        {
            ReflectivityMultiplier = rm;
            EfficiencyMultiplier = em;
        }

        public ReflectorValues(List<object> values)
        {
            ReflectivityMultiplier = Convert.ToDouble(values[0]);
            EfficiencyMultiplier = Convert.ToDouble(values[1]);
        }
    }

    public struct CoolantRecipeValues
    {
        public string InputName;
        public string OutputName;
        public double HeatCapacity;
        public double OutToInRatio;

        public CoolantRecipeValues(string iname, string oname, double hcap, double otiratio)
        {
            InputName = iname;
            OutputName = oname;
            HeatCapacity = hcap;
            OutToInRatio = otiratio;
        }

        public CoolantRecipeValues(List<object> values)
        {
            InputName = Convert.ToString(values[0]);
            OutputName = Convert.ToString(values[1]);
            HeatCapacity = Convert.ToDouble(values[2]);
            OutToInRatio = Convert.ToDouble(values[3]);
        }
    }

    public struct HeatSinkValues
    {
        public double HeatPassive;
        public string Requirements;

        public HeatSinkValues(double hp, string req)
        {
            HeatPassive = hp;
            Requirements = req;
        }

        public HeatSinkValues(List<object> values)
        {
            HeatPassive = Convert.ToDouble(values[0]);
            Requirements = Convert.ToString(values[1]);
        }
    }

    public struct ModeratorValues
    {
        public int FluxFactor;
        public double EfficiencyFactor;

        public ModeratorValues(int ff, double ef)
        {
            FluxFactor = ff;
            EfficiencyFactor = ef;
        }

        public ModeratorValues(List<object> fieldValues)
        {
            FluxFactor = Convert.ToInt32(fieldValues[0]);
            EfficiencyFactor = Convert.ToDouble(fieldValues[1]);
        }
    }

    public struct NeutronShieldValues
    {
        public int HeatPerFlux;
        public double EfficiencyFactor;

        public NeutronShieldValues(int hpf, double ef)
        {
            HeatPerFlux = hpf;
            EfficiencyFactor = ef;
        }

        public NeutronShieldValues(List<object> fieldValues)
        {
            HeatPerFlux = Convert.ToInt32(fieldValues[0]);
            EfficiencyFactor = Convert.ToDouble(fieldValues[1]);
        }
    }

    public struct IrradiatorValues
    {
        public int HeatPerFlux;
        public double EfficiencyMultiplier;

        public IrradiatorValues(int hpf, double em)
        {
            HeatPerFlux = hpf;
            EfficiencyMultiplier = em;
        }

        public IrradiatorValues(List<object> fieldValues)
        {
            HeatPerFlux = Convert.ToInt32(fieldValues[0]);
            EfficiencyMultiplier = Convert.ToDouble(fieldValues[1]);
        }

        public override bool Equals(object obj)
        {
            if (obj is IrradiatorValues iv)
            {
                return iv.EfficiencyMultiplier == this.EfficiencyMultiplier && iv.HeatPerFlux == this.HeatPerFlux;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public struct FissionValues
    {
        public double Power;
        public double FuelUse;
        public double HeatGeneration;
        public int MinSize;
        public int MaxSize;
        public int NeutronReach;
        public double MaxSparsityPenaltyMultiplier;
        public double SparsityPenaltyThreshold;
        public double CoolingPenaltyLeniency;
        public int IrradiatorHeatPerFlux;
        public double IrradiatorEfficiencyMultiplier;

        public FissionValues(double p, double fu, double hg, int ms, int mxs, int nr, double mspm, double spt, double cpl, int ihpf, double ie)
        {
            Power = p;
            FuelUse = fu;
            HeatGeneration = hg;
            MinSize = ms;
            MaxSize = mxs;
            NeutronReach = nr;
            MaxSparsityPenaltyMultiplier = mspm;
            SparsityPenaltyThreshold = spt;
            CoolingPenaltyLeniency = cpl;
            IrradiatorHeatPerFlux = ihpf;
            IrradiatorEfficiencyMultiplier = ie;
        }

        public FissionValues(List<object> values)
        {
            Power = Convert.ToDouble(values[0]);
            FuelUse = Convert.ToDouble(values[1]);
            HeatGeneration = Convert.ToDouble(values[2]);
            MinSize = Convert.ToInt32(values[3]);
            MaxSize = Convert.ToInt32(values[4]);
            NeutronReach = Convert.ToInt32(values[5]);
            MaxSparsityPenaltyMultiplier = Convert.ToDouble(values[6]);
            SparsityPenaltyThreshold = Convert.ToDouble(values[7]);
            CoolingPenaltyLeniency = Convert.ToDouble(values[8]);
            IrradiatorHeatPerFlux = Convert.ToInt32(values[9]);
            IrradiatorEfficiencyMultiplier = Convert.ToDouble(values[10]);
        }
    }

    public struct CraftingMaterials
    {
        public Dictionary<string, Dictionary<string, int>> HeatSinkCosts;
        public Dictionary<string, Dictionary<string, int>> ModeratorCosts;
        public Dictionary<string, int> FuelCellCosts;
        public Dictionary<string, int> CasingCosts;

        public CraftingMaterials(Dictionary<string, Dictionary<string, int>> clc, Dictionary<string, Dictionary<string, int>> mc, Dictionary<string, int> fcc, Dictionary<string, int> csc)
        {
            HeatSinkCosts = clc;
            ModeratorCosts = mc;
            FuelCellCosts = fcc;
            CasingCosts = csc;
        }
    }

    /// <summary>
    /// Holds the currently active planner configuration.
    /// Responsible for save\loading the configuration, holds the default values for regenerating the config.
    /// This class is referenced by reflection to build tabs/fields of ConfigurationUI!
    /// </summary>
    public static class Configuration
    {
        //TODO: incapsulation .\_/.
        public static FissionValues Fission;
        public static CraftingMaterials ResourceCosts;
        public static Dictionary<string, FuelValues> Fuels;
        public static Dictionary<string, NeutronSourceValues> NeutronSources;
        public static Dictionary<string, ReflectorValues> Reflectors;
        public static Dictionary<string, CoolantRecipeValues> CoolantRecipes;
        public static Dictionary<string, HeatSinkValues> HeatSinks;
        public static Dictionary<string, ModeratorValues> Moderators;
        public static Dictionary<string, NeutronShieldValues> NeutronShields;

        private static FileInfo configFileInfo;

        public static bool Load(FileInfo file)
        {
            configFileInfo = file;
            ConfigFile cf;
            using (StreamReader sr = File.OpenText(file.FullName))
            {
                JsonSerializer jss = new JsonSerializer();
                try
                {
                    cf = (ConfigFile)jss.Deserialize(sr, typeof(ConfigFile));
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\r\nConfig file was corrupt or there were major changes to structure! Reverting to defaults...");
                    return false;
                }
            }

            if(cf.saveVersion < new Version(2,0,0,0))
            {
                System.Windows.Forms.MessageBox.Show("Pre-overhaul configurations aren't supported!\r\nDelete your BetaConfig.json to regenerate a new one.");
                return false;
            }
            if(cf.saveVersion < new Version(2, 1, 7, 0))
            {
                System.Windows.Forms.MessageBox.Show("Ignoring old config file as the values have changed, please overwrite BetaConfig.json!");
                return false;
            }

            if((cf.Fuels == null) | (cf.HeatSinks == null))
            {
                System.Windows.Forms.MessageBox.Show("Invalid config file contents!");
                return false;
            }

            Fission = cf.Fission;
            ResourceCosts = cf.ResourceCosts;
            if (ResourceCosts.CasingCosts == null)
                SetDefaultResourceCosts();
            Fuels = cf.Fuels;
            NeutronSources = cf.NeutronSources;
            Reflectors = cf.Reflectors;
            CoolantRecipes = cf.CoolantRecipes;
            HeatSinks = cf.HeatSinks;
            Moderators = cf.Moderators;
            NeutronShields = cf.NeutronShields;

            Palette.Load();
            Palette.SetHeatSinkUpdateOrder();
            Palette.PaletteControl.ResetSize();
            Reactor.UI.UpdateStatsUIPosition();
            return true;
        }

        public static void Save(FileInfo file)
        {
            configFileInfo = file;
            using (TextWriter tw = File.CreateText(file.FullName))
            {
                JsonSerializer jss = new JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full
                };

                ConfigFile cf = new ConfigFile(Reactor.saveVersion, Fission, Fuels, NeutronSources, Reflectors, CoolantRecipes, HeatSinks, Moderators, ResourceCosts, NeutronShields);
                jss.Serialize(tw, cf);
            }
        }

        public static void ResetToDefaults()
        {
            configFileInfo = null;

            SetDefaultHeatSinks();

            SetDefaultFuels();

            SetDefaultNeutronSources();

            SetDefaultReflectors();

            SetDefaultCoolantRecipes();

            SetDefaultModerators();

            SetDefaultFission();

            SetDefaultNeutronShields();

            SetDefaultResourceCosts();

            Palette.Load();
            Palette.SetHeatSinkUpdateOrder();
        }

        private static void SetDefaultFuels()
        {
            Fuels = new Dictionary<string, FuelValues>();
            Fuels.Add("[OX]TBU", new FuelValues(1.25, 40, 14400, 234));
            Fuels.Add("[OX]LEU-233", new FuelValues(1.1, 216, 2666, 78));
            Fuels.Add("[OX]HEU-233", new FuelValues(1.15, 648, 2666, 39));
            Fuels.Add("[OX]LEU-235", new FuelValues(1, 120, 4800, 102));
            Fuels.Add("[OX]HEU-235", new FuelValues(1.05, 360, 4800, 51));
            Fuels.Add("[OX]LEN-236", new FuelValues(1.1, 292, 1972, 70));
            Fuels.Add("[OX]HEN-236", new FuelValues(1.15, 876, 1972, 35));
            Fuels.Add("[OX]LEP-239", new FuelValues(1.2, 126, 4572, 99));
            Fuels.Add("[OX]HEP-239", new FuelValues(1.25, 378, 4572, 49));
            Fuels.Add("[OX]LEP-241", new FuelValues(1.25, 182, 3164, 84));
            Fuels.Add("[OX]HEP-241", new FuelValues(1.3, 546, 3164, 42));
            Fuels.Add("[OX]MOX-239", new FuelValues(1.05, 132, 4354, 94));
            Fuels.Add("[OX]MOX-241", new FuelValues(1.15, 192, 3014, 80));
            Fuels.Add("[OX]LEA-242", new FuelValues(1.35, 390, 1476, 65));
            Fuels.Add("[OX]HEA-242", new FuelValues(1.4, 1170, 1476, 32));
            Fuels.Add("[OX]LECm-243", new FuelValues(1.45, 384, 1500, 66));
            Fuels.Add("[OX]HECm-243", new FuelValues(1.5, 1152, 1500, 33));
            Fuels.Add("[OX]LECm-245", new FuelValues(1.5, 238, 2420, 75));
            Fuels.Add("[OX]HECm-245", new FuelValues(1.55, 714, 2420, 37));
            Fuels.Add("[OX]LECm-247", new FuelValues(1.55, 268, 2150, 72));
            Fuels.Add("[OX]HECm-247", new FuelValues(1.6, 804, 2150, 36));
            Fuels.Add("[OX]LEB-248", new FuelValues(1.65, 266, 2166, 73));
            Fuels.Add("[OX]HEB-248", new FuelValues(1.7, 798, 2166, 36));
            Fuels.Add("[OX]LECf-249", new FuelValues(1.75, 540, 1066, 60, true));
            Fuels.Add("[OX]HECf-249", new FuelValues(1.8, 1620, 1066, 30, true));
            Fuels.Add("[OX]LECf-251", new FuelValues(1.8, 288, 2000, 71, true));
            Fuels.Add("[OX]HECf-251", new FuelValues(1.85, 864, 2000, 35, true));

            Fuels.Add("[NI]TBU", new FuelValues(1.25, 32, 18000, 293));
            Fuels.Add("[NI]LEU-233", new FuelValues(1.1, 172, 3348, 98));
            Fuels.Add("[NI]HEU-233", new FuelValues(1.15, 516, 3348, 49));
            Fuels.Add("[NI]LEU-235", new FuelValues(1, 96, 6000, 128));
            Fuels.Add("[NI]HEU-235", new FuelValues(1.05, 288, 6000, 64));
            Fuels.Add("[NI]LEN-236", new FuelValues(1.1, 234, 2462, 88));
            Fuels.Add("[NI]HEN-236", new FuelValues(1.15, 702, 2462, 44));
            Fuels.Add("[NI]LEP-239", new FuelValues(1.2, 100, 5760, 124));
            Fuels.Add("[NI]HEP-239", new FuelValues(1.25, 300, 5760, 62));
            Fuels.Add("[NI]LEP-241", new FuelValues(1.25, 146, 3946, 105));
            Fuels.Add("[NI]HEP-241", new FuelValues(1.3, 438, 3946, 52));
            Fuels.Add("[NI]MNI-239", new FuelValues(1.05, 106, 5486, 118));
            Fuels.Add("[NI]MNI-241", new FuelValues(1.15, 154, 3758, 100));
            Fuels.Add("[NI]LEA-242", new FuelValues(1.35, 312, 1846, 81));
            Fuels.Add("[NI]HEA-242", new FuelValues(1.4, 936, 1846, 40));
            Fuels.Add("[NI]LECm-243", new FuelValues(1.45, 308, 1870, 83));
            Fuels.Add("[NI]HECm-243", new FuelValues(1.5, 924, 1870, 41));
            Fuels.Add("[NI]LECm-245", new FuelValues(1.5, 190, 3032, 94));
            Fuels.Add("[NI]HECm-245", new FuelValues(1.55, 570, 3032, 47));
            Fuels.Add("[NI]LECm-247", new FuelValues(1.55, 214, 2692, 90));
            Fuels.Add("[NI]HECm-247", new FuelValues(1.6, 642, 2692, 45));
            Fuels.Add("[NI]LEB-248", new FuelValues(1.65, 212, 2716, 91));
            Fuels.Add("[NI]HEB-248", new FuelValues(1.7, 636, 2716, 45));
            Fuels.Add("[NI]LECf-249", new FuelValues(1.75, 432, 1334, 75, true));
            Fuels.Add("[NI]HECf-249", new FuelValues(1.8, 1296, 1334, 37, true));
            Fuels.Add("[NI]LECf-251", new FuelValues(1.8, 230, 2504, 89, true));
            Fuels.Add("[NI]HECf-251", new FuelValues(1.85, 690, 2504, 44, true));

            Fuels.Add("[ZA]TBU", new FuelValues(1.25, 50, 11520, 199));
            Fuels.Add("[ZA]LEU-233", new FuelValues(1.1, 270, 2134, 66));
            Fuels.Add("[ZA]HEU-233", new FuelValues(1.15, 810, 2134, 33));
            Fuels.Add("[ZA]LEU-235", new FuelValues(1, 150, 3840, 87));
            Fuels.Add("[ZA]HEU-235", new FuelValues(1.05, 450, 3840, 43));
            Fuels.Add("[ZA]LEN-236", new FuelValues(1.1, 366, 1574, 60));
            Fuels.Add("[ZA]HEN-236", new FuelValues(1.15, 1098, 1574, 30));
            Fuels.Add("[ZA]LEP-239", new FuelValues(1.2, 158, 3646, 84));
            Fuels.Add("[ZA]HEP-239", new FuelValues(1.25, 474, 3646, 42));
            Fuels.Add("[ZA]LEP-241", new FuelValues(1.25, 228, 2526, 71));
            Fuels.Add("[ZA]HEP-241", new FuelValues(1.3, 684, 2526, 35));
            Fuels.Add("[ZA]MZA-239", new FuelValues(1.05, 166, 3472, 80));
            Fuels.Add("[ZA]MZA-241", new FuelValues(1.15, 240, 2406, 68));
            Fuels.Add("[ZA]LEA-242", new FuelValues(1.35, 488, 1180, 55));
            Fuels.Add("[ZA]HEA-242", new FuelValues(1.4, 1464, 1180, 27));
            Fuels.Add("[ZA]LECm-243", new FuelValues(1.45, 480, 1200, 56));
            Fuels.Add("[ZA]HECm-243", new FuelValues(1.5, 1440, 1200, 28));
            Fuels.Add("[ZA]LECm-245", new FuelValues(1.5, 298, 1932, 64));
            Fuels.Add("[ZA]HECm-245", new FuelValues(1.55, 894, 1932, 32));
            Fuels.Add("[ZA]LECm-247", new FuelValues(1.55, 336, 1714, 61));
            Fuels.Add("[ZA]HECm-247", new FuelValues(1.6, 1008, 1714, 30));
            Fuels.Add("[ZA]LEB-248", new FuelValues(1.65, 332, 1734, 62));
            Fuels.Add("[ZA]HEB-248", new FuelValues(1.7, 996, 1734, 31));
            Fuels.Add("[ZA]LECf-249", new FuelValues(1.75, 676, 852, 51, true));
            Fuels.Add("[ZA]HECf-249", new FuelValues(1.8, 2028, 852, 25, true));
            Fuels.Add("[ZA]LECf-251", new FuelValues(1.8, 360, 1600, 60, true));
            Fuels.Add("[ZA]HECf-251", new FuelValues(1.85, 1080, 1600, 30, true));

            Fuels.Add("[F4]TBU", new FuelValues(2.5, 32, 18000, 234));
            Fuels.Add("[F4]LEU-233", new FuelValues(2.2, 172, 3348, 78));
            Fuels.Add("[F4]HEU-233", new FuelValues(2.3, 516, 3348, 39));
            Fuels.Add("[F4]LEU-235", new FuelValues(2, 96, 6000, 102));
            Fuels.Add("[F4]HEU-235", new FuelValues(2.1, 288, 6000, 51));
            Fuels.Add("[F4]LEN-236", new FuelValues(2.2, 234, 2462, 70));
            Fuels.Add("[F4]HEN-236", new FuelValues(2.3, 702, 2462, 35));
            Fuels.Add("[F4]LEP-239", new FuelValues(2.4, 100, 5760, 99));
            Fuels.Add("[F4]HEP-239", new FuelValues(2.5, 300, 5760, 49));
            Fuels.Add("[F4]LEP-241", new FuelValues(2.5, 146, 3946, 84));
            Fuels.Add("[F4]HEP-241", new FuelValues(2.6, 438, 3946, 42));
            Fuels.Add("[F4]MF4-239", new FuelValues(2.1, 106, 5486, 94));
            Fuels.Add("[F4]MF4-241", new FuelValues(2.3, 154, 3758, 80));
            Fuels.Add("[F4]LEA-242", new FuelValues(2.7, 312, 1846, 65));
            Fuels.Add("[F4]HEA-242", new FuelValues(2.8, 936, 1846, 32));
            Fuels.Add("[F4]LECm-243", new FuelValues(2.9, 308, 1870, 66));
            Fuels.Add("[F4]HECm-243", new FuelValues(3, 924, 1870, 33));
            Fuels.Add("[F4]LECm-245", new FuelValues(3, 190, 3032, 75));
            Fuels.Add("[F4]HECm-245", new FuelValues(3.1, 570, 3032, 37));
            Fuels.Add("[F4]LECm-247", new FuelValues(3.1, 214, 2692, 72));
            Fuels.Add("[F4]HECm-247", new FuelValues(3.2, 642, 2692, 36));
            Fuels.Add("[F4]LEB-248", new FuelValues(3.3, 212, 2716, 73));
            Fuels.Add("[F4]HEB-248", new FuelValues(3.4, 636, 2716, 36));
            Fuels.Add("[F4]LECf-249", new FuelValues(3.5, 432, 1334, 60, true));
            Fuels.Add("[F4]HECf-249", new FuelValues(3.6, 1296, 1334, 30, true));
            Fuels.Add("[F4]LECf-251", new FuelValues(3.6, 230, 2504, 71, true));
            Fuels.Add("[F4]HECf-251", new FuelValues(3.7, 690, 2504, 35, true));
        }

        private static void SetDefaultNeutronSources()
        {
            NeutronSources = new Dictionary<string, NeutronSourceValues>();
            NeutronSources.Add("Ra-Be", new NeutronSourceValues(0.9));
            NeutronSources.Add("Po-Be", new NeutronSourceValues(0.95));
            NeutronSources.Add("Cf-252", new NeutronSourceValues(1));
        }

        private static void SetDefaultReflectors()
        {
            Reflectors = new Dictionary<string, ReflectorValues>();
            Reflectors.Add("Beryllium-Carbon", new ReflectorValues(1.0, 0.5));
            Reflectors.Add("Lead-Steel", new ReflectorValues(0.5, 0.25));
        }

        private static void SetDefaultCoolantRecipes()
        {
            CoolantRecipes = new Dictionary<string, CoolantRecipeValues>();
            CoolantRecipes.Add("Water to High Pressure Steam", new CoolantRecipeValues("Water", "High Pressure Steam", 64, 4));
            CoolantRecipes.Add("Preheated Water to High Pressure Steam", new CoolantRecipeValues("Preheated Water", "High Pressure Steam", 32, 4));
            CoolantRecipes.Add("IC2 Coolant to Hot IC2 Coolant", new CoolantRecipeValues("IC2 Coolant", "Hot IC2 Coolant", 160, 1));
        }

        private static void SetDefaultHeatSinks()
        {
            HeatSinks = new Dictionary<string, HeatSinkValues>();
            HeatSinks.Add("Water", new HeatSinkValues(55, "One FuelCell"));
            HeatSinks.Add("Iron", new HeatSinkValues(50, "One Moderator"));
            HeatSinks.Add("Redstone", new HeatSinkValues(85, "One FuelCell; One Moderator"));
            HeatSinks.Add("Quartz", new HeatSinkValues(80, "One Redstone heatsink"));
            HeatSinks.Add("Obsidian", new HeatSinkValues(70, "Axial Glowstone heatsinks"));
            HeatSinks.Add("NetherBrick", new HeatSinkValues(105, "One Obsidian heatsink"));
            HeatSinks.Add("Glowstone", new HeatSinkValues(90, "Two Moderators"));
            HeatSinks.Add("Lapis", new HeatSinkValues(100, "One FuelCell; One Casing"));
            HeatSinks.Add("Gold", new HeatSinkValues(110, "Exactly Two Iron heatsinks"));
            HeatSinks.Add("Prismarine", new HeatSinkValues(115, "Two Water heatsinks"));
            HeatSinks.Add("Slime", new HeatSinkValues(145, "Exactly One Water heatsink; Two Lead heatsinks"));
            HeatSinks.Add("EndStone", new HeatSinkValues(65, "One Reflector"));
            HeatSinks.Add("Purpur", new HeatSinkValues(95, "One Iron heatsink; One Reflector"));
            HeatSinks.Add("Diamond", new HeatSinkValues(200, "One Gold heatsink; One FuelCell"));
            HeatSinks.Add("Emerald", new HeatSinkValues(195, "One Prismarine heatsink; One Moderator"));
            HeatSinks.Add("Copper", new HeatSinkValues(75, "One Water heatsink"));
            HeatSinks.Add("Tin", new HeatSinkValues(120, "Axial Lapis heatsinks"));
            HeatSinks.Add("Lead", new HeatSinkValues(60, "One Iron heatsink"));
            HeatSinks.Add("Boron", new HeatSinkValues(160, "Exactly One Quartz heatsink; One Casing"));
            HeatSinks.Add("Lithium", new HeatSinkValues(130, "Exact-Axial Two Lead heatsinks; One Casing"));
            HeatSinks.Add("Magnesium", new HeatSinkValues(125, "Exactly One Moderator; One Casing"));
            HeatSinks.Add("Manganese", new HeatSinkValues(150, "Two FuelCells"));
            HeatSinks.Add("Aluminum", new HeatSinkValues(175, "One Quartz heatsink; One Lapis heatsink"));
            HeatSinks.Add("Silver", new HeatSinkValues(170, "Two Glowstone heatsinks; One Tin heatsink"));
            HeatSinks.Add("Fluorite", new HeatSinkValues(165, "One Gold heatsink; One Prismarine heatsink"));
            HeatSinks.Add("Villiaumite", new HeatSinkValues(180, "One EndStone heatsink; One Redstone heatsink"));
            HeatSinks.Add("Carobbiite", new HeatSinkValues(140, "One Copper heatsink; One EndStone heatsink"));
            HeatSinks.Add("Arsenic", new HeatSinkValues(135, "Axial Reflectors"));
            HeatSinks.Add("Nitrogen", new HeatSinkValues(185, "Two Copper heatsinks; One Purpur heatsink"));
            HeatSinks.Add("Helium", new HeatSinkValues(190, "Exactly Two Redstone heatsinks"));
            HeatSinks.Add("Enderium", new HeatSinkValues(155, "Three Moderators"));
            HeatSinks.Add("Cryotheum", new HeatSinkValues(205, "Three FuelCells"));
        }

        private static void SetDefaultModerators()
        {
            Moderators = new Dictionary<string, ModeratorValues>();
            Moderators.Add("Beryllium", new ModeratorValues(22, 1.05));
            Moderators.Add("Graphite", new ModeratorValues(10, 1.1));
            Moderators.Add("HeavyWater", new ModeratorValues(36, 1.0));
        }

        private static void SetDefaultNeutronShields()
        {
            NeutronShields = new Dictionary<string, NeutronShieldValues>();
            NeutronShields.Add("Boron-Silver", new NeutronShieldValues(5, 0.5));
        }

        private static void SetDefaultFission()
        {
            Fission.Power = 1.0;
            Fission.FuelUse = 1.0;
            Fission.HeatGeneration = 1.0;
            Fission.MinSize = 1;
            Fission.MaxSize = 24;
            Fission.NeutronReach = 4;
            Fission.MaxSparsityPenaltyMultiplier = 0.5;
            Fission.SparsityPenaltyThreshold = 0.75;
            Fission.CoolingPenaltyLeniency = 10;
        }

        private static void SetDefaultResourceCosts()
        {
            ResourceCosts.FuelCellCosts = DefaultFuelCellCosts();
            ResourceCosts.CasingCosts = DefaultCasingCosts();
            ResourceCosts.ModeratorCosts = DefaultModeratorCosts();
            ResourceCosts.HeatSinkCosts = DefaultHeatSinkCosts();
        }

        private static Dictionary<string, int> DefaultFuelCellCosts()
        {
            Dictionary<string, int> dfcc = new Dictionary<string, int>();
            dfcc.Add("Glass", 4);
            dfcc.Add("Tough Alloy", 4);
            return dfcc;
        }

        private static Dictionary<string, int> DefaultCasingCosts()
        {
            Dictionary<string, int> dcc = new Dictionary<string, int>();
            dcc.Add("Tough Alloy", 1);
            dcc.Add("Basic Plating", 4);
            return dcc;
        }

        private static Dictionary<string, Dictionary<string, int>> DefaultModeratorCosts()
        {
            Dictionary<string, Dictionary<string, int>> dmc = new Dictionary<string, Dictionary<string, int>>();
            dmc.Add("Graphite", new Dictionary<string, int>());
            dmc["Graphite"].Add("Graphite Ingot", 9);
            dmc.Add("Beryllium", new Dictionary<string, int>());
            dmc["Beryllium"].Add("Beryllium Ingot", 9);
            return dmc;
        }

        private static Dictionary<string, Dictionary<string, int>> DefaultHeatSinkCosts()
        {
            Dictionary<string, Dictionary<string, int>> dcc = new Dictionary<string, Dictionary<string, int>>();

            foreach (KeyValuePair<string, HeatSinkValues> kvp in HeatSinks)
            {
                string heatSinkName = kvp.Key;
                dcc.Add(heatSinkName, new Dictionary<string, int>());
                dcc[heatSinkName].Add("Empty HeatSink", 1);
            }

            dcc["Water"].Add("Water Bucket", 1);

            dcc["Redstone"].Add("Redstone", 2);
            dcc["Redstone"].Add("Block of Redstone", 2);

            dcc["Quartz"].Add("Block of Quartz", 2);
            dcc["Quartz"].Add("Crushed Quartz", 2);

            dcc["Gold"].Add("Gold Ingot", 8);

            dcc["Glowstone"].Add("Glowstone", 2);
            dcc["Glowstone"].Add("Glowstone Dust", 6);

            dcc["Lapis"].Add("Lapis Lazuli Block", 2);

            dcc["Diamond"].Add("Diamond", 8);

            dcc["Helium"].Add("Liquid Helium Bucket", 1);

            dcc["Iron"].Add("Iron Ingot", 8);

            dcc["Emerald"].Add("Emerald", 6);

            dcc["Copper"].Add("Copper Ingot", 8);

            dcc["Tin"].Add("Tin Ingot", 8);

            dcc["Magnesium"].Add("Magnesium Ingot", 8);

            dcc["Boron"].Add("Boron Ingot", 8);

            dcc["Obsidian"].Add("Obsidian", 8);

            dcc["Prismarine"].Add("Prismarine Shard", 8);

            dcc["Lead"].Add("Lead Ingot", 8);

            dcc["Enderium"].Add("Enderium Ingot", 8);

            dcc["Cryotheum"].Add("Cryotheum Dust", 8);



            return dcc;
        }

        public static Dictionary<string, int> CalculateTotalResourceCosts()
        {
            Dictionary<string, int> totals = new Dictionary<string, int>();
            foreach (KeyValuePair<string,List<HeatSink>> c in Reactor.heatSinks)
            {
                foreach (KeyValuePair<string,int> resource in ResourceCosts.HeatSinkCosts[c.Key])
                {
                    if (!totals.ContainsKey(resource.Key))
                        totals.Add(resource.Key, 0);
                    totals[resource.Key] += resource.Value * c.Value.Count();
                }
            }

            if(Reactor.fuelCells.Count >0)
                foreach (KeyValuePair<string, int> resource in ResourceCosts.FuelCellCosts)
                {
                    if (!totals.ContainsKey(resource.Key))
                        totals.Add(resource.Key, 0);
                    totals[resource.Key] += resource.Value * Reactor.fuelCells.Count;
                }

            foreach (KeyValuePair<string, int> resource in ResourceCosts.CasingCosts)
            {
                if (!totals.ContainsKey(resource.Key))
                    totals.Add(resource.Key, 0);
                totals[resource.Key] += resource.Value * Reactor.totalCasings;
            }


            foreach (KeyValuePair<string, List<Moderator>> m in Reactor.moderators)
            {
                if(m.Value.Count > 0)
                    foreach (KeyValuePair<string, int> resource in ResourceCosts.ModeratorCosts[m.Key])
                    {
                        if (!totals.ContainsKey(resource.Key))
                            totals.Add(resource.Key, 0);
                        totals[resource.Key] += resource.Value * m.Value.Count;
                    }
            }

            return totals;
        }
    }
}
