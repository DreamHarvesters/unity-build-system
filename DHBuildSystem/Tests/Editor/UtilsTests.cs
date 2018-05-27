using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace DH.BuildSystem
{
    public class UtilTest
    {
        [Test]
        public void Test_GetFolderAndParentPath_WithoutApplicationPath_Pass()
        {
            string path = "Assets/Test/TestSub";
            string folderName, parentPath;

            Utils.GetFolderAndParentPath(path, out parentPath, out folderName);

            Assert.IsTrue("Assets/Test".Equals(parentPath) && "TestSub".Equals(folderName));
        }

        [Test]
        public void Test_GetFolderAndParentPath_WithoutApplicationPath_WithForwardSlash_Pass()
        {
            string path = "Assets\\Test\\TestSub";
            string folderName, parentPath;

            Utils.GetFolderAndParentPath(path, out parentPath, out folderName);

            Assert.IsTrue("Assets/Test".Equals(parentPath) && "TestSub".Equals(folderName));
        }

        [Test]
        public void Test_GetFileNameWithoutExtension_Pass()
        {
            string path = "Assets/Test/Test1/test.asset";

            Assert.IsTrue("test".Equals(Utils.GetFileName(path)));
        }
    }
}