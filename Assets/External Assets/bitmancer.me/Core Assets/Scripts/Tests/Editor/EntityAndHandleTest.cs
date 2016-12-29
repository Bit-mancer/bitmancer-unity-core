using UnityEngine;
using UnityEditor;
using NUnit.Framework;

namespace Bitmancer.Core.Tests {

    public class EntityAndHandleTest {

        private Bitmancer.Core.Entity createEntity() {
            var go = new GameObject( "TEST - Entity" );
            return go.AddComponent<Bitmancer.Core.Entity>();
        }
    

        [Test]
        public void instantiationTest() {
            var entity = createEntity();
            Assert.AreNotEqual( entity.Generation, Bitmancer.Core.Entity.kExpiredGeneration );
        }

    
        [Test]
        public void emptyHandleTest() {
            var emptyHandle = new Bitmancer.Core.Handle();
            Assert.IsNull( emptyHandle.Target );

            emptyHandle = new Bitmancer.Core.Handle( null );
            Assert.IsNull( emptyHandle.Target );
        }


        [Test]
        public void newHandleTest() {
            var entity = createEntity();
            var handle = new Bitmancer.Core.Handle( entity );
            Assert.IsNotNull( handle.Target );
        }

    
        [Test]
        public void assignedHandleTest() {
            var entity = createEntity();
            var handle = new Bitmancer.Core.Handle();
            Assert.IsNull( handle.Target );
            handle.Target = entity;
            Assert.IsNotNull( handle.Target );
        }

    
        [Test]
        public void newGenerationTest() {
            var entity = createEntity();
            var handle = new Bitmancer.Core.Handle( entity );
            Assert.IsNotNull( handle.Target );
            entity.recycleEntity();
            Assert.IsNull( handle.Target );
        }

    
        [Test]
        public void expireTest() {
            var entity = createEntity();
            var handle = new Bitmancer.Core.Handle( entity );
            Assert.IsNotNull( handle.Target );
            handle.expire();
            Assert.IsNull( handle.Target );
        }


        [Test]
        public void pooledTest() {

            var pool = new Bitmancer.Core.Util.ObjectPool<Entity>();
            Assert.AreEqual( 0, pool.Count );

            var entity = createEntity();
            entity.gameObject.SetActive( false );

            entity.spawnEntity( pool );
            Assert.AreEqual( 0, pool.Count );
            var generation = entity.Generation;

            var handle = new Bitmancer.Core.Handle( entity );
            Assert.IsNotNull( handle.Target );

            entity.recycleEntity();
            entity = null;
            Assert.AreEqual( 1, pool.Count );
            Assert.IsNull( handle.Target );

            entity = pool.claim();
            Assert.IsNotNull( entity );
            Assert.AreEqual( 0, pool.Count );
            Assert.AreNotEqual( generation, entity.Generation );
            Assert.IsNull( handle.Target );

            handle.Target = entity;
            Assert.IsNotNull( handle.Target );

            handle.expire();
            pool.clear();
        }
    }
}
