using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bitmancer.Core;
using Bitmancer.Core.Util;

public class SingletonPlayModeTest {

    private const string kAnchorName = "__SingletonTest-Anchor__";

    [Test]
    public void testGetWithMissingAnchorWillCreateAnchorAndComponent() {

        var anchor = GameObject.Find( kAnchorName );
        Assert.IsNull( anchor );

        try {
            var baseBehavior = Singleton.getComponent<BaseBehavior>( kAnchorName );
            Assert.IsNotNull( baseBehavior );
        } finally {
            GameObject.Destroy( anchor );
        }
    }
}
