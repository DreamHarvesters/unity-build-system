using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

namespace DH.BuildSystem
{
	public class BuilderTests
	{

		[Test]
		public void BuilderTestSimplePass()
		{
            Scene s = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            EditorSceneManager.SaveScene(s, "Assets/TestScene.unity", false);

            System.IO.Directory.CreateDirectory("TestBuild");

			Builder b = ScriptableObject.CreateInstance<Builder>();

            b.SetPreBuildActions(new IBuildAction[]{});
            b.SetPostBuildActions(new IBuildAction[] {});

			BuildConfiguration config = ScriptableObject.CreateInstance<BuildConfiguration>();
            config.BuildSceneList = new EditorBuildSettingsScene[] {new EditorBuildSettingsScene("Assets/TestScene.unity", true)};
            config.SetBuildOptions(BuildOptions.None);
            config.BuildTarget = BuildTarget.StandaloneWindows;
            config.TargetPath = "TestBuild/TestBuild.exe";

            Assert.IsTrue(string.IsNullOrEmpty(b.Build(config)));

            AssetDatabase.DeleteAsset("Assets/TestScene.unity");
            System.IO.Directory.Delete("TestBuild", true);
		}

		[Test]
		public void RunPreBuildActionsPass()
		{
			Builder b = ScriptableObject.CreateInstance<Builder>();

            IBuildAction actionSuccess = Substitute.For<IBuildAction>();
            actionSuccess.Execute().Returns(true);

            Assert.IsTrue(b.RunPreBuildActions(new IBuildAction[]{actionSuccess}));
		}

		[Test]
		public void RunPreBuildActionsFail()
		{
			Builder b = ScriptableObject.CreateInstance<Builder>();

			IBuildAction actionFails = Substitute.For<IBuildAction>();
            actionFails.Execute().Returns(false);

            Assert.IsFalse(b.RunPreBuildActions(new IBuildAction[]{actionFails}));
		}

		[Test]
		public void RunPostBuildActionsPass()
		{
			Builder b = ScriptableObject.CreateInstance<Builder>();

			IBuildAction actionSuccess = Substitute.For<IBuildAction>();
			actionSuccess.Execute().Returns(true);

            Assert.IsTrue(b.RunPostBuildActions(new IBuildAction[]{actionSuccess}));
		}

		[Test]
		public void RunPostBuildActionsFail()
		{
			Builder b = ScriptableObject.CreateInstance<Builder>();

			IBuildAction actionFails = Substitute.For<IBuildAction>();
			actionFails.Execute().Returns(false);

            Assert.IsFalse(b.RunPostBuildActions(new IBuildAction[] { actionFails}));
		}
	}

}
