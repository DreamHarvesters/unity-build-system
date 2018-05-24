using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

namespace BuildSystem
{
    public class BuildSystemTests
    {

        [Test]
        public void BuildManagerBuildTestPass()
        {
            BuildManager bm = new BuildManager();

            IBuilder builder = Substitute.For<IBuilder>();

            BuildConfiguration config = ScriptableObject.CreateInstance<BuildConfiguration>();

            builder.Build(config).Returns(string.Empty);

            Assert.IsTrue(string.IsNullOrEmpty(bm.Build(builder)));
        }

        [Test]
        public void BuildManagerBuildTestFail()
        {
            BuildManager bm = new BuildManager();

            IBuilder builder = Substitute.For<IBuilder>();

            BuildConfiguration config = ScriptableObject.CreateInstance<BuildConfiguration>();

            builder.GetConfiguration().Returns(config);
            builder.Build(builder.GetConfiguration()).Returns("error");

            Assert.IsFalse(string.IsNullOrEmpty(bm.Build(builder)));
        }

        [Test]
        public void RunPreBuildActionsTestPass()
        {
            IBuilder builder = Substitute.For<IBuilder>();

            builder.RunPreBuildActions(null).Returns(true);

            Assert.IsTrue(builder.RunPreBuildActions(null));
        }

        [Test]
        public void RunPreBuildActionsTestFails()
        {
            IBuilder builder = Substitute.For<IBuilder>();

            builder.RunPreBuildActions(null).Returns(false);

            Assert.IsFalse(builder.RunPreBuildActions(null));
        }

        [Test]
        public void RunPostBuildActionsTestPass()
        {
            IBuilder builder = Substitute.For<IBuilder>();

            builder.RunPostBuildActions(null).Returns(true);

            Assert.IsTrue(builder.RunPostBuildActions(null));
        }

        [Test]
        public void RunPostBuildActionsTestFails()
        {
            IBuilder builder = Substitute.For<IBuilder>();

            builder.RunPostBuildActions(null).Returns(false);

            Assert.IsFalse(builder.RunPostBuildActions(null));
        }

        [Test]
        public void TestGetBuilderCountPass()
        {
            BuildManager b = new BuildManager();

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance("Builder"), "Assets/TestBuilder1.asset");
			AssetDatabase.CreateAsset(ScriptableObject.CreateInstance("Builder"), "Assets/TestBuilder2.asset");

			AssetDatabase.Refresh();

            int builderCount = b.GetBuilders("TestBuilder t:Builder").Length;

			AssetDatabase.DeleteAsset("Assets/TestBuilder1.asset");
			AssetDatabase.DeleteAsset("Assets/TestBuilder2.asset");

            Assert.AreEqual(2, builderCount);
		}

        [Test]
        public void TestGetBuilderPass()
        {
            BuildManager b = new BuildManager();

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance("Builder"), "Assets/TestBuilder1.asset");

            AssetDatabase.Refresh();

            Assert.AreSame(AssetDatabase.LoadAssetAtPath("Assets/TestBuilder1.asset", typeof(Builder)), b.GetBuilder("TestBuilder1"));

            AssetDatabase.DeleteAsset("Assets/TestBuilder1.asset");
        }


    }
}
