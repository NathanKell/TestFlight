﻿using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using KSPPluginFramework;
using TestFlightAPI;

namespace TestFlight
{
    public class TestFlightFailure_ReducedMaxThrust : TestFlightFailureBase
    {
        private float maxThrust;
        /// <summary>
        /// Triggers the failure controlled by the failure module
        /// </summary>
        public override void DoFailure()
        {
            List<ModuleEngines> partEngines = this.part.Modules.OfType<ModuleEngines>().ToList();
            List<ModuleEnginesFX> partEnginesFX = this.part.Modules.OfType<ModuleEnginesFX>().ToList();
            foreach (ModuleEngines engine in partEngines)
            {
                maxThrust = engine.maxThrust;
                engine.maxThrust = maxThrust * 0.5f;
            }
            foreach (ModuleEnginesFX engineFX in partEnginesFX)
            {
                maxThrust = engineFX.maxThrust;
                engineFX.maxThrust = maxThrust * 0.5f;
            }
        }

        /// <summary>
        /// Asks the repair module if all condtions have been met for the player to attempt repair of the failure.  Here the module can verify things such as the conditions (landed, eva, splashed), parts requirements, etc
        /// </summary>
        /// <returns><c>true</c> if this instance can attempt repair; otherwise, <c>false</c>.</returns>
        public override bool CanAttemptRepair()
        {
            return base.CanAttemptRepair();
        }

        /// <summary>
        /// Trigger a repair ATTEMPT of the module's failure.  It is the module's responsability to take care of any consumable resources, data transmission, etc required to perform the repair
        /// </summary>
        /// <returns>Should return true if the failure was repaired, false otherwise</returns>
        public override bool AttemptRepair()
        {
            bool success = base.AttemptRepair();
            if (success)
            {
                List<ModuleEngines> partEngines = this.part.Modules.OfType<ModuleEngines>().ToList();
                List<ModuleEnginesFX> partEnginesFX = this.part.Modules.OfType<ModuleEnginesFX>().ToList();
                foreach (ModuleEngines engine in partEngines)
                {
                    engine.maxThrust = maxThrust;
                }
                foreach (ModuleEnginesFX engineFX in partEnginesFX)
                {
                    engineFX.maxThrust = maxThrust;
                }
            }
            return success;
        }
    }
}

