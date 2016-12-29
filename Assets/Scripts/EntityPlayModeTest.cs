using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlaymodeTests;
using UnityEngine.Assertions;
using Bitmancer.Core.Util;

public class EntityPlayModeTest : MonoBehaviour {

    private IEnumerator entityTestCoroutine() {

        var go = new GameObject( string.Format( "{0} - Entity", this.GetType().Name ) );
        var entity = go.AddComponent<Bitmancer.Core.Entity>();
        var handle = new Bitmancer.Core.Handle( entity );
        Assert.IsNotNull( handle.Target );

        entity = null;
        GameObject.Destroy( go );
        go = null;

        yield return new WaitForSecondsRealtime( 2 ); // ugh

        // Try our best to force the release
        Resources.UnloadUnusedAssets();
        System.GC.Collect();

        // We can check if a GameObject (or the parent transform of a Component) has been destroyed by using the == operator (which is overriden) to compare against null (which the operator will evaluate to true if the GO has been destroyed)
        Assert.IsTrue( go == null );
        Assert.IsTrue( entity == null );

        Assert.IsNull( handle.Target );
    }


    private IEnumerator derivedTestCoroutine() {

        var go = new GameObject( string.Format( "{0} - DerivedEntity", this.GetType().Name ) );
        var entity = go.AddComponent<DerivedEntity>();
        var handle = new Bitmancer.Core.Handle( entity );
        Assert.IsNotNull( handle.Target );

        entity = null;
        GameObject.Destroy( go );
        go = null;

        yield return new WaitForSecondsRealtime( 2 ); // ugh

        // Try our best to force the release
        Resources.UnloadUnusedAssets();
        System.GC.Collect();

        // We can check if a GameObject (or the parent transform of a Component) has been destroyed by using the == operator (which is overriden) to compare against null (which the operator will evaluate to true if the GO has been destroyed)
        Assert.IsTrue( go == null );
        Assert.IsTrue( entity == null );

        Assert.IsNull( handle.Target );
    }


    private IEnumerator runTestsCoroutine() {
        yield return StartCoroutine( entityTestCoroutine() );
        yield return StartCoroutine( derivedTestCoroutine() );
        this.enabled = false;
    }


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

    void Start() {
        StartCoroutine( runTestsCoroutine() );
    }
}
