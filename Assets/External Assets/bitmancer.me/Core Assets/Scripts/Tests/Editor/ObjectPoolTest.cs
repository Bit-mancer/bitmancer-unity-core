using UnityEngine;
using UnityEditor;
using NUnit.Framework;

namespace Bitmancer.Core.Tests {

    public class ObjectPoolTest {

        [Test]
        public void implementsInterfaceTest() {
            Bitmancer.Core.Util.IObjectPool<string> pool = new Bitmancer.Core.Util.ObjectPool<string>(); // compile-time check
            Assert.AreEqual( 0, pool.Count );
        }


        [Test]
        public void emptyPoolTest() {
            var pool = new Bitmancer.Core.Util.ObjectPool<string>();
            Assert.AreEqual( 0, pool.Count );
            Assert.IsNull( pool.claim() );
        }

    
        [Test]
        public void releaseAndClaimTest() {
            var pool = new Bitmancer.Core.Util.ObjectPool<string>();
            pool.release( "test1" );
            pool.release( "test2" );
            pool.release( "test3" );
            Assert.AreEqual( 3, pool.Count );

            string s = pool.claim();
            Assert.IsNotNull( s );
            // Note that the pool makes no guarantee as to what order you receive objects, so we shouldn't check the exact value...
            Assert.IsTrue( s.StartsWith( "test" ) );
            Assert.AreEqual( 2, pool.Count );

            s = pool.claim();
            Assert.IsNotNull( s );
            // Note that the pool makes no guarantee as to what order you receive objects, so we shouldn't check the exact value...
            Assert.IsTrue( s.StartsWith( "test" ) );
            Assert.AreEqual( 1, pool.Count );

            s = pool.claim();
            Assert.IsNotNull( s );
            // Note that the pool makes no guarantee as to what order you receive objects, so we shouldn't check the exact value...
            Assert.IsTrue( s.StartsWith( "test" ) );
            Assert.AreEqual( 0, pool.Count );

            s = pool.claim();
            Assert.IsNull( s );
            Assert.AreEqual( 0, pool.Count );
        }


        [Test]
        public void clearTest() {
            var pool = new Bitmancer.Core.Util.ObjectPool<string>();
            pool.release( "test1" );
            pool.release( "test2" );
            pool.release( "test3" );
            Assert.AreEqual( 3, pool.Count );

            pool.clear();
            Assert.AreEqual( 0, pool.Count );
        }


        [Test]
        public void sharedPoolTest() {
            try {
                Bitmancer.Core.Util.ObjectPool<string>.Shared.clear();
                Assert.AreEqual( 0, Bitmancer.Core.Util.ObjectPool<string>.Shared.Count );

                Bitmancer.Core.Util.ObjectPool<string>.Shared.release( "test1" );
                Assert.AreEqual( 1, Bitmancer.Core.Util.ObjectPool<string>.Shared.Count );

                Assert.AreEqual( "test1", Bitmancer.Core.Util.ObjectPool<string>.Shared.claim() );
                Assert.AreEqual( 0, Bitmancer.Core.Util.ObjectPool<string>.Shared.Count );

            } finally {
                Bitmancer.Core.Util.ObjectPool<string>.Shared.clear();
            }
        }
    }
}
