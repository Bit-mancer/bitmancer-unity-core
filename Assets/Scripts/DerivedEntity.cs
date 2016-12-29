using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;

public class DerivedEntity : Bitmancer.Core.Entity {

    /**
     * Execution Order:
     *
     *  Awake()
     *  OnEnable() / OnDisable()
     *  Start()
     *  FixedUpdate() [0..n times]
     *  Update()
     *  LateUpdate()
     *  OnDestroy()
     * */

    void Awake() {
    }
}
