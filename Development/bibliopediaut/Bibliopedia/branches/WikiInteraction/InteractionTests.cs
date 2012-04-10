﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NUnit.Framework;
//using DotNetWikiBot;
//using Data;
//using NHibernate;
//using WikiInteraction.Domain;
//using Data.FluentsNeeds;
//using System.IO;
//using Jstor;

//namespace WikiInteraction
//{
//    [TestFixture]
//    public class InteractionTests
//    {
//        IAutoDatabase Database;
//        ISession Session;
//        BiblioBot Bot = null;

//        [SetUp]
//        public void SetUp()
//        {
//            Session = Database.SessionFactory.OpenSession();
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            Session.Close();
//            Session.Dispose();
//        }

//        [TestFixtureSetUp]
//        public void FixtureSetup()
//        {
//            Bot = new BiblioBot();
//            bool buildSchema = !File.Exists("d:/JSTOR.sdf");
//            Database =
//                AutoDatabase<JstorDoi>.Create(
//                PersistenceConfigurer.PersistentFileBasedDb("d:/JSTOR"), buildSchema);
//            //PersistenceConfigurer.SqlExpress("localhost", "JSTOR"), true);
//        }

//        [Test]
//        public void CanConnectToWiki()
//        {
//            Assert.IsNotNull(Bot);
//        }
//    }
//}