using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;
using UnityEditor;
using NUnit.Framework;

namespace BuildSystem
{
	public class BuildActionTests
	{
		[Test]
        public void Test_MoveFolders_MoveWithoutTargetFolder_Pass()
        {
            AssetDatabase.CreateFolder("Assets", "TestMoveFolder");
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BuildConfiguration>(), "Assets/TestMoveFolder/BuildConfiguration.asset");

            MoveFolders mf = ScriptableObject.CreateInstance<MoveFolders>();
            mf.FoldersToMove = new string[] { "Assets/TestMoveFolder" };
            mf.TargetFolder = "Assets/TestTargetFolder";
            mf.CreateTargetFolder = true;

            bool result = mf.Execute();

			FileUtil.DeleteFileOrDirectory("Assets/TestMoveFolder");
			FileUtil.DeleteFileOrDirectory("Assets/TestTargetFolder");

            Assert.IsTrue(result);
        }
	}
}