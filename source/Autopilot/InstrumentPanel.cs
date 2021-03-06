﻿using FSUIPC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfm
{
    public class InstrumentPanel
    {
        private double aslAltitude;
        public double AslAltitude
        {
            get
            {
                aslAltitude = Math.Round((double)Aircraft.Altitude.Value, 0);
                return aslAltitude;
            }
        }
        private double heading;
        public double Heading
        {
            get
            {
                heading = (double)Math.Round(Aircraft.CompassHeading.Value);
                return heading;
            }
        }
        private double aglAltitude;
        public double AglAltitude
        {
            get
            {
                double groundAlt = (double)Aircraft.GroundAltitude.Value / 256d * 3.28084d;
                aglAltitude = (double)Aircraft.Altitude.Value - groundAlt;
                return Math.Round(aglAltitude);
            }
        }
        private double indicatedAirspeed;
        public double IndicatedAirspeed
        {
            get
            {
                indicatedAirspeed = (double)Aircraft.AirspeedIndicated.Value / 128d;
                return Math.Round(indicatedAirspeed);
            }
        }
        private double trueAirspeed;
        public double TrueAirspeed
        {
            get
            {
                trueAirspeed = (double)Aircraft.AirspeedTrue.Value / 128d;
                return Math.Round(trueAirspeed);
            }
        }
        private double machSpeed;
        public double MachSpeed
        {
            get
            {
                machSpeed = (double)Aircraft.AirspeedMach.Value / 20480d;
                return Math.Round(machSpeed, 2);
            }
        }
        private double latitude;
        public double Latitude
        {
            get
            {
                latitude = Aircraft.aircraftLat.Value.DecimalDegrees;
                return latitude;
            }
        }
        
        private double longitude;
        public double Longitude
        {
            get
            {
                longitude = Aircraft.aircraftLon.Value.DecimalDegrees;
                return longitude;
            }
        }
        
        private double verticalSpeed;
        public double VerticalSpeed
        {
            get
            {
                verticalSpeed = (double)Aircraft.VerticalSpeed.Value * 3.28084d * -1;
                return Math.Round(verticalSpeed);
            }
        }

        private bool apMaster;

        [DisplayName("autopilot master switch")]
        public bool ApMaster
        {
            get
            {
                apMaster = Aircraft.ApMaster.Value != 0;
                return apMaster;
            }   
            set
            {
                Aircraft.ApMaster.Value = (value) ? (uint)1 : (uint)0;
                apMaster = value;
            }
        }

        private bool apNavLock;

        [DisplayName("nav lock")]
        public bool ApNavLock
        {
            get
            {
                apNavLock = (Aircraft.ApNavLock.Value == 0) ? false : true;
                return apNavLock;
            }
            set
            {
                Aircraft.ApNavLock.Value = (value) ? (uint)1 : (uint)0;
                apNavLock = value;
            }
        }
        private bool apWingLeveler;
        [DisplayName("Wing Leveler")]
        public bool ApWingLeveler
        {
            get
            {
                apWingLeveler = (Aircraft.ApWingLeveler.Value == 0) ? false : true;
                return apWingLeveler;
            }
            set
            {
                Aircraft.ApWingLeveler.Value = (value) ? (uint)1 : (uint)0;
                apWingLeveler = value;
            }
        }
        private bool apAttitudeHold;
        [DisplayName("attitude hold")]
        public bool ApAttitudeHold
        {
            get
            {
                apAttitudeHold = (Aircraft.ApAttitudeHold.Value == 0) ? false : true;
                return apAttitudeHold;
            }
            set
            {
                Aircraft.ApAttitudeHold.Value = (value) ? (uint)1 : (uint)0;
                apAttitudeHold = value;
            }
        }
        private bool apApproachHold;
        [DisplayName("Approach hold")]
        public bool ApApproachHold
        {
            get
            {
                apApproachHold = (Aircraft.ApApproachHold.Value == 0) ? false : true;
                return apApproachHold;
            }
            set
            {
                Aircraft.ApApproachHold.Value = (value) ? (uint)1 : (uint)0;
                apApproachHold = value;
            }
        
        }
        private double apHeading;
        [DisplayName("Heading")]
        public double ApHeading
        {
            get
            {
                string Name = Aircraft.AircraftName.Value;
                if (Name.Contains("PMDG"))
                {
if (Name.Contains("737"))
                    {
                        apHeading = Aircraft.pmdg737.MCP_Heading.Value;
                    }
if (Name.Contains("747"))
                    {
                        apHeading = Aircraft.pmdg747.MCP_Heading.Value;
                    }
if (Name.Contains("777"))
                    {
                        apHeading = Aircraft.pmdg777.MCP_Heading.Value;
                    }
                }
                else
                {
                    apHeading = (double)Math.Round(Aircraft.ApHeading.Value / 65536d * 360d);
                }
                
                return apHeading;
            }
            set
            {
                // convert the supplied heading into the proper FSUIPC format(degrees*65536/360)
                Aircraft.ApHeading.Value = (ushort)(value * 65536 / 360);
                apHeading = value;
            }
        }
        private bool apHeadingLock;
        [DisplayName("heading lock")]
        public bool ApHeadingLock
        {
            get
            {
                apHeadingLock = (Aircraft.ApHeadingLock.Value == 0) ? false : true;
                return apHeadingLock;
            }
            set
            {
                Aircraft.ApHeadingLock.Value = (value) ? (uint)1 : (uint)0;
                apHeadingLock = value;
            }
        }
        private double apAltitude;
        [DisplayName("Altitude")]
        public double ApAltitude
        {
            get
            {
                string Name = Aircraft.AircraftName.Value;
                if (Name.Contains("PMDG"))
                {
                    if (Name.Contains("737"))
                    {
                        apAltitude = Aircraft.pmdg737.MCP_Altitude.Value;
                    }
                    if (Name.Contains("747"))
                    {
                        apAltitude = Aircraft.pmdg747.MCP_Altitude.Value;
                    }
                    if (Name.Contains("777"))
                    {
                        apAltitude = Aircraft.pmdg777.MCP_Altitude.Value;
                    }
                }
                else
                {
                    apAltitude = Math.Round((double)Aircraft.ApAltitude.Value / 65536d * 3.28084d);
                }
                
                return apAltitude;
            }
            set
            {
                    Aircraft.ApAltitude.Value = (uint)(value / 3.28084 * 65536);
                    apAltitude = value;
            }
        }
        private bool apAltitudeLock;
        [DisplayName("altitude lock")]
        public bool ApAltitudeLock
        {
            get
            {
                apAltitudeLock = (Aircraft.ApAltitudeLock.Value == 0) ? false : true;
                return apAltitudeLock;
            }
            set
            {
                Aircraft.ApAltitudeLock.Value = (value) ? (uint)1 : (uint)0;
                apAltitudeLock = value;
            }
        }

        private double apAirspeed;
        [DisplayName("Airspeed")]
        public double ApAirspeed
        {
            get
            {
                string Name = Aircraft.AircraftName.Value;
                if (Name.Contains("PMDG"))
                {
                    if (Name.Contains("737"))
                    {
                        apAirspeed = Aircraft.pmdg737.MCP_IASMach.Value;
                    }
                    if (Name.Contains("747"))
                    {
                        apAirspeed = Aircraft.pmdg747.MCP_IASMach.Value;
                    }
                    if (Name.Contains("777"))
                    {
                        apAirspeed = Aircraft.pmdg777.MCP_IASMach.Value;
                    }
                }
else
                {
                    apAirspeed = Aircraft.ApAirspeed.Value;
                }
                
                return apAirspeed;
            }
            set
            {
                    Aircraft.ApAirspeed.Value = (short)value;
                    apAirspeed = value;
            }
        }
        private bool apAirspeedHold;
        [DisplayName("airspeed hold")]
        public bool ApAirspeedHold
        {
            get
            {
                apAirspeedHold = (Aircraft.ApSpeedHold.Value == 0) ? false : true;
                return apAirspeedHold;
            }
            set
            {
                Aircraft.ApSpeedHold.Value = (value) ? (uint)1 : (uint)0;
                apAirspeedHold = value;
            }
        }

        private double apMachSpeed;
        [DisplayName("Mach")]
        public double ApMachSpeed
        {
            get
            {
                double mach = (double)Aircraft.ApMach.Value / 65536d;
                string strMach = mach.ToString("F2");
                apMachSpeed = double.Parse(strMach);
                return apMachSpeed;
            }
            set
            {
                    // FSUIPC needs the mach multiplied by 65536            }
                    Aircraft.ApMach.Value = (uint)(value * 65536);
                    apMachSpeed = value;
            }
        }
        private bool apMachHold;
        [DisplayName("mach hold")]
        public bool ApMachHold
        {
            get
            {
                apMachHold = (Aircraft.ApMachHold.Value == 0) ? false : true;
                return apMachHold;
            }
            set
            {
                Aircraft.ApMachHold.Value = (value) ? (uint)1 : (uint)0;
                apMachHold = value;
            }
        }


        private double apVerticalSpeed;
        [DisplayName("Vertical speed")]
        public double ApVerticalSpeed
        {
            get
            {
                apVerticalSpeed = Aircraft.ApVerticalSpeed.Value;
                return apVerticalSpeed;
            }
            set
            {
                Aircraft.ApVerticalSpeed.Value = (short)value;
                apVerticalSpeed = value;

            }
        }
        private bool apVerticalSpeedHold;
        [DisplayName("vertical speed hold")]
        public bool ApVerticalSpeedHold
        {
            get
            {
                apVerticalSpeedHold = (Aircraft.ApVerticalSpeedHold.Value == 0) ? false : true;
                return apVerticalSpeedHold;
            }
            set
            {
                Aircraft.ApVerticalSpeedHold.Value = (value) ? (uint)1 : (uint)0;
                apVerticalSpeedHold = value;
            }
        }
        private decimal com1Freq;
        [DisplayName("com 1")]
        [Category("communications")]
        public decimal Com1Freq
        {
            get
            {
                FsFrequencyCOM com1Helper = new FsFrequencyCOM(Aircraft.Com1Freq.Value);
                com1Freq = com1Helper.ToDecimal();
                return com1Freq;
            }
            set
            {
                    // 1. Create a new instance of the COM helper class using the decimal value entered
                    FsFrequencyCOM com1Helper = new FsFrequencyCOM(value);
                    // 2. Now use the helper class to get the BCD value required by FSUIPC and set the offset to this new value
                    Aircraft.Com1Freq.Value = com1Helper.ToBCD();
                    com1Freq = value;
            }
        }
        
        private decimal com1StandbyFreq;
        [DisplayName("com 1 Standby")]
        [Category("communications")]
        public decimal Com1StandbyFreq
        {
            get
            {
                FsFrequencyCOM com1StandbyHelper = new FsFrequencyCOM(Aircraft.Com1StandbyFreq.Value);
                com1StandbyFreq = com1StandbyHelper.ToDecimal();
                return com1StandbyFreq;
            }
            set
            {
                    // 1. Create a new instance of the COM helper class using the decimal value entered
                    FsFrequencyCOM com1StandbyHelper = new FsFrequencyCOM(value);
                    // 2. Now use the helper class to get the BCD value required by FSUIPC and set the offset to this new value
                    Aircraft.Com1StandbyFreq.Value = com1StandbyHelper.ToBCD();
                    com1StandbyFreq = value;
            }
        }

        private decimal com2Freq;
        [DisplayName("com 2")]
        [Category("communications")]
        public decimal Com2Freq
        {
            get
            {
                FsFrequencyCOM com2Helper = new FsFrequencyCOM(Aircraft.Com2Freq.Value);
                com2Freq = com2Helper.ToDecimal();
                return com2Freq;
            }
            set
            {
                    // 1. Create a new instance of the COM helper class using the decimal value entered
                    FsFrequencyCOM com2Helper = new FsFrequencyCOM(value);
                    // 2. Now use the helper class to get the BCD value required by FSUIPC and set the offset to this new value
                    Aircraft.Com2Freq.Value = com2Helper.ToBCD();
                    com2Freq = value;
            }
        }
        private int transponder;
        [DisplayName("transponder")]
        [Category("communications")]
        public int Transponder
        {
            get
            {
                FsTransponderCode txHelper = new FsTransponderCode(Aircraft.Transponder.Value);
                transponder = txHelper.ToInteger();
                return transponder;
            }
            set
            {
                    // 1. Create a new instance of the Transponder helper class using the integer that was entered
                    //    Note the number box always returns the value as a 'decimal' type. So we have to cast to Int32
                    FsTransponderCode txHelper = new FsTransponderCode((int)value);
                    // 2. Now use the helper class to get the BCD value required by FSUIPC and set the offset to this new value
                    Aircraft.Transponder.Value = txHelper.ToBCD();
                    transponder = value;
            }
        }
        private decimal adf1Freq;
        [DisplayName("ADF frequency")]
        [Category("navigation")]
        public decimal Adf1Freq
        {
            get
            {
                // 1. Create a new instance of the helper ADF class using the values of the main AND extended offsets
                //    This is taking in the BCD values sent from FSUIPC
                FsFrequencyADF adf1Helper = new FsFrequencyADF(Aircraft.adf1Main.Value, Aircraft.adf1Extended.Value);
                // 2. Now use the helper class to get the string representation and show it on the form
                adf1Freq = adf1Helper.ToDecimal();
                return adf1Freq;
            }
            set
            {
                    // 1. Create a new instance of the ADF helper class using the decimal value entered
                    FsFrequencyADF adf1Helper = new FsFrequencyADF(value);
                    // 2. Now use the helper class to get the two BCD values required by FSUIPC (main and extended)
                    //    Set the offsets to these new values
                    Aircraft.adf1Main.Value = adf1Helper.ToBCDMain();
                    Aircraft.adf1Extended.Value = adf1Helper.ToBCDExtended();
                    adf1Freq = value;
            }
        }
        private double altimeterQNH;
        [DisplayName("Altimeter QNH")]
        public double AltimeterQNH
        {
            get
            {
                double AltQNH = (double)Aircraft.Altimeter.Value / 16d;
                altimeterQNH = Math.Floor(AltQNH + 0.5);
                return altimeterQNH;
            }
            set
            {
                Aircraft.Altimeter.Value = (ushort)(value * 16);
                altimeterQNH = value;
            }
        }
        private double altimeterInches;
        [DisplayName("Altimeter Inches")]
        public double AltimeterInches
        {
            get
            {
                double AltQNH = (double)Aircraft.Altimeter.Value / 16d;
                altimeterInches = Math.Floor(((100 * AltQNH * 29.92) / 1013.2) + 0.5);
                altimeterInches = altimeterInches / 100;
                return altimeterInches;
            }
            set
            {
                double qnh = value * 33.864;
                qnh = Math.Round(qnh, 1) * 16;
                Aircraft.Altimeter.Value = (ushort)qnh;
                altimeterInches = value;

            }
        }

        private decimal nav1Freq;
        [DisplayName("nav 1")]
        [Category("navigation")]
        public decimal Nav1Freq
        {
            get
            {
                FsFrequencyNAV nav1Helper = new FsFrequencyNAV(Aircraft.Nav1Freq.Value);
                nav1Freq = nav1Helper.ToDecimal();
                return nav1Freq;
            }
            set
            {
                    // 1. Create a new instance of the COM helper class using the decimal value entered
                    FsFrequencyNAV nav1Helper = new FsFrequencyNAV(value);
                    // 2. Now use the helper class to get the BCD value required by FSUIPC and set the offset to this new value
                    Aircraft.Nav1Freq.Value = nav1Helper.ToBCD();
                    nav1Freq = value;
            }
        }
        private ushort nav1Course;
        public ushort Nav1Course
        {
            get
            {
                nav1Course = Aircraft.Nav1OBS.Value;
                return nav1Course;
            }
            set
            {
                Aircraft.Nav1OBS.Value = value;
                nav1Course = value;
            }
        }
        private decimal nav2Freq;
        [DisplayName("nav 2")]
        [Category("navigation")]
        public decimal Nav2Freq
        {
            get
            {
                FsFrequencyNAV nav2Helper = new FsFrequencyNAV(Aircraft.Nav2Freq.Value);
                nav2Freq = nav2Helper.ToDecimal();
                return nav2Freq;
            }
            set
            {
                    // 1. Create a new instance of the NAV helper class using the decimal value entered
                    FsFrequencyNAV nav2Helper = new FsFrequencyNAV(value);
                    // 2. Now use the helper class to get the BCD value required by FSUIPC and set the offset to this new value
                    Aircraft.Nav2Freq.Value = nav2Helper.ToBCD();
                    nav2Freq = value;
            }
        }
        private ushort nav2Course;
        public ushort Nav2Course
        {
            get
            {
                nav2Course = Aircraft.Nav2OBS.Value;
                return nav2Course;
            }
            set
            {
                Aircraft.Nav2OBS.Value = value;
                nav2Course = value;
            }
        }

        // engine throttle properties
        private double engine1ThrottlePercent;
        public double Engine1ThrottlePercent
        {
            get
            {
                engine1ThrottlePercent = (double)Aircraft.Engine1ThrottleLever.Value / 16384d * 100d;
                return engine1ThrottlePercent;
            }
            set
            {
                value = value / 100 * 16384;
                Aircraft.Engine1ThrottleLever.Value = (short)value;
            }
        }
        private double engine2ThrottlePercent;
        public double Engine2ThrottlePercent
        {
            get
            {
                engine2ThrottlePercent = (double)Aircraft.Engine2ThrottleLever.Value / 16384d * 100d;
                return engine2ThrottlePercent;
            }
            set
            {
                value = value / 100 * 16384;
                Aircraft.Engine2ThrottleLever.Value = (short)value;
            }
        }
        private double engine3ThrottlePercent;
        public double Engine3ThrottlePercent
        {
            get
            {
                engine3ThrottlePercent = (double)Aircraft.Engine3ThrottleLever.Value / 16384d * 100d;
                return engine3ThrottlePercent;
            }
            set
            {
                value = value / 100 * 16384;
                Aircraft.Engine3ThrottleLever.Value = (short)value;
            }
        }
        private double engine4ThrottlePercent;
        public double Engine4ThrottlePercent
        {
            get
            {
                engine4ThrottlePercent = (double)Aircraft.Engine4ThrottleLever.Value / 16384d * 100d;
                return engine4ThrottlePercent;
            }
            set
            {
                value = value / 100 * 16384;
                Aircraft.Engine4ThrottleLever.Value = (short)value;
            }
        }

public uint  SpoilerPercent
        {
            get
            {

                uint currentSpoilers = 0;
                // Spoilers are retracted.
                if(Aircraft.Spoilers.Value == 0)
                {
                    currentSpoilers = 0;
                }

                // Spoilers are armed.
        else if(Aircraft.Spoilers.Value == 4800)
                {
                    currentSpoilers = 1;
                }

                // The effective range of spoilers is between 7% and 100%
                // Which starts at offset value 5620 (7%) and ends at offset value 16383 (100%).
                else if(Aircraft.Spoilers.Value >= 5620 && Aircraft.Spoilers.Value <= 16383)
                {
                    // Convert the current offset value into a percent.
                    // The formula is precise up to 14 decimal places
                    // However, the conversion to uint causes automatic rounding, so the results in a simulator
                    // may be slightly off. Example: 8% might truely be 6% with 10 decimal places added.
                    currentSpoilers = ((Aircraft.Spoilers.Value - 4800) * 100) / (16383 - 4800);
                }
                return currentSpoilers;
                            }
            set
            {
                
                // Providing 0% retracts spoilers.
                if(value == 0)
                {
                    Aircraft.Spoilers.Value = 0;
                }

                // Providing 1% arms spoilers.
                else if(value == 1)
                {
                    Aircraft.Spoilers.Value = 4800;
                }

                //A conversion in offset values to a percent is accurate up to 14 decimal places.
// offset value 5620 is equal to 7% with 8 decimal places added.
// Since we only want the whole portion of the percentage, 7.0...% is 5610
// in offset value terminology, so, set 7% to 5620 by default.
else                 if(value == 7)
                {
                    Aircraft.Spoilers.Value = 5620;                    
                }
                
                // Handle the effective range for spoilers.
                else if(value > 7 && value <= 100)
                {
                    var newSpoilers = (value * (16383 - 4800) / 100) + 4800;
                                        Aircraft.Spoilers.Value = (uint)newSpoilers;
                }
            }
        }


    }
}
