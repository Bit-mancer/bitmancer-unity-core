using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bitmancer.Core;
using Bitmancer.Core.Util;

public class SingletonTest {

    private const string kAnchorName = "__SingletonTest-Anchor__";
    private const string kRootName = "__TestControllers__";

    [Test]
    public void testGetByGameObject() {

        var anchor = new GameObject( kAnchorName );

        try {
            var baseBehavior = anchor.AddComponent<BaseBehavior>();

            var comp = Singleton.getComponent<BaseBehavior>( anchor );
            Assert.IsNotNull( comp );
            Assert.AreEqual( baseBehavior, comp );

        } finally {
            GameObject.DestroyImmediate( anchor );
        }
    }

    [Test]
    public void testGetByName() {

        var anchor = new GameObject( kAnchorName );

        try {
            var baseBehavior = anchor.AddComponent<BaseBehavior>();

            var comp = Singleton.getComponent<BaseBehavior>( kAnchorName );
            Assert.IsNotNull( comp );
            Assert.AreEqual( baseBehavior, comp );
    
        } finally {
            GameObject.DestroyImmediate( anchor );
        }
    }

    [Test]
    public void testGetWithMissingComponentWillCreateComponent() {

        var anchor = new GameObject( kAnchorName );

        try {
            var comp = Singleton.getComponent<BaseBehavior>( anchor );
            Assert.IsNotNull( comp );
        } finally {
            GameObject.DestroyImmediate( anchor );
        }
    }

    [Test]
    public void testNestedAnchors() {

        var root = new GameObject( kRootName );
        var anchor = new GameObject( kAnchorName );
        anchor.transform.parent = root.transform;
        var baseBehavior = anchor.AddComponent<BaseBehavior>();

        try {
            var comp = Singleton.getComponent<BaseBehavior>( kRootName + "/" + kAnchorName );
            Assert.IsNotNull( comp );
            Assert.AreEqual( baseBehavior, comp );
        } finally {
            GameObject.DestroyImmediate( anchor );
            GameObject.DestroyImmediate( root );
        }
    }
}
