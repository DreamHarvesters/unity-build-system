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
            AssetDatabase.CreateFolder("Assets/TestMoveFolder", "TestSubMoveFolder");

            AssetDatabase.Refresh();

            MoveFolders mf = ScriptableObject.CreateInstance<MoveFolders>();
            mf.FoldersToMove = new string[] { Application.dataPath + "/TestMoveFolder" };
            mf.TargetFolder = Application.dataPath + "/TestTargetFolder";
            mf.CreateTargetFolder = true;

            bool result = mf.Execute();

            AssetDatabase.Refresh();

			FileUtil.DeleteFileOrDirectory("Assets/TestMoveFolder");
			FileUtil.DeleteFileOrDirectory("Assets/TestTargetFolder");

            AssetDatabase.Refresh();

            Assert.IsTrue(result);
        }
	}
}