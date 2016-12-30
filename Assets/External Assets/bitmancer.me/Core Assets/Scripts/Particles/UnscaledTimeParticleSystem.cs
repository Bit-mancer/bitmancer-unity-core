using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bitmancer.Core.Particles {

    /// <summary>
    /// A realtime (unscaled) particle system driver.
    /// </summary>
    /// <remarks>
    /// Drives an attached <c>ParticleSystem</c> at realtime. Useful for particle systems in main menus (when the game is likely paused via timeScale = 0).
    /// </remarks>
    [DisallowMultipleComponent]
    [RequireComponent( typeof(ParticleSystem) )]
    public sealed class UnscaledTimeParticleSystem : BaseBehavior {

        private ParticleSystem _system;


        /**
         * Execution Order:
         * 
         *  Awake()
         *  OnEnable() / OnDisable()
         *  Start()
         *  FixedUpdate() [0..n times]
         *  Update()
         *  LateUpdate()
         * */

        void Awake() {
            _system = GetRequiredComponent<ParticleSystem>();
        }


        void Update() {
            _system.Simulate( Time.unscaledDeltaTime, true, false );
        }
    }
}
