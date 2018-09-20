using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElonaRaceCreator;
using System.Data.SQLite;

namespace TestSkill
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateSkill()
        {
            Skill skl = new Skill("1060004");
            System.Diagnostics.Debug.WriteLine(skl.ToString());
        }
    }
}
