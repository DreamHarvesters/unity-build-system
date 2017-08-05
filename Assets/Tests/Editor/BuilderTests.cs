﻿﻿using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

namespace BuildSystem
{
	public class BuilderTests
	{

		[Test]
		public void BuilderTestSimplePass()
		{
			Builder b = ScriptableObject.CreateInstance<Builder>();

            b.SetPreBuildActions(new IBuildAction[]{});
            b.SetPostBuildActions(new IBuildAction[] {});

			BuildConfiguration config = ScriptableObject.CreateInstance<BuildConfiguration>();
            config.BuildSceneList = new EditorBuildSettingsScene[] {new EditorBuildSettingsScene()};
            config.BuildOptions = BuildOptions.None;
            config.BuildTarget = BuildTarget.StandaloneWindows;

            Assert.IsTrue(string.IsNullOrEmpty(b.Build(config)));
		}

		[Test]
		public void BuilderTestSimpleFail()
		{
			Builder b = ScriptableObject.CreateInstance<Builder>();

			b.SetPreBuildActions(new IBuildAction[] { });
			b.SetPostBuildActions(new IBuildAction[] { });

			BuildConfiguration config = ScriptableObject.CreateInstance<BuildConfiguration>();
			config.BuildSceneList = new EditorBuildSettingsScene[] { new EditorBuildSettingsScene() };
			config.BuildOptions = BuildOptions.None;
			config.BuildTarget = BuildTarget.StandaloneWindows;

            Assert.IsFalse(string.IsNullOrEmpty(b.Build(config)));
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
