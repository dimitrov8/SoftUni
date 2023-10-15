namespace RobotFactory.Tests
{
    using NUnit.Framework;
    using System.Linq;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        // Factory.cs tests
        [Test]
        public void Test_Can_Create_Factory()
        {
            var factory = new Factory("Samsung", 20);
            Assert.IsInstanceOf<Factory>(factory);
        }

        [Test]
        public void Test_Factory_Name_Is_Set_Correct()
        {
            var factory = new Factory("Samsung", 20);

            Assert.AreEqual("Samsung", factory.Name);
        }

        [Test]
        public void Test_Factory_Capacity_Is_Set_Correct()
        {
            var factory = new Factory("Samsung", 20);

            Assert.AreEqual(20, factory.Capacity);
        }

        [Test]
        public void Test_Factory_Robot_List_Is_Initialized_Correct()
        {
            var factory = new Factory("Samsung", 20);

            Assert.IsNotNull(factory.Robots);
        }

        [Test]
        public void Test_Factory_Supplement_List_Is_Initialized_Correct()
        {
            var factory = new Factory("Samsung", 20);

            Assert.IsNotNull(factory.Supplements);
        }

        [Test]
        public void Test_Factory_Produce_Robots_Cannot_Create_Robots_If_Capacity_Is_Full()
        {
            var factory = new Factory("Samsung", 2);

            factory.ProduceRobot("Optimus", 300, 1444);
            factory.ProduceRobot("Sparky", 200, 1500);

            string expectedResult = factory.ProduceRobot("X-9", 500, 3500);
            string actualResult = "The factory is unable to produce more robots for this production day!";

            Assert.AreEqual(expectedResult, actualResult);

            Assert.AreEqual(2, factory.Robots.Count);
        }

        [Test]
        public void Test_Factory_Produce_Robots_Can_Create_Robots_If_Capacity_Is_Not_Full()
        {
            var factory = new Factory("Samsung", 6);
            factory.ProduceRobot("Optimus", 300, 1444);

            string expectedResult = "Produced --> Robot model: Sparky IS: 1500, Price: 200.00";
            string actualResult = factory.ProduceRobot("Sparky", 200, 1500);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_Factory_Produce_Supplements_Method_Works_Correct()
        {
            var factory = new Factory("Samsung", 6);
            factory.ProduceSupplement("AI-5", 542);

            Assert.AreEqual(1, factory.Supplements.Count);
        }

        [Test]
        public void Test_Factory_Produce_Supplement_String_Works_Correct()
        {
            var factory = new Factory("Samsung", 6);

            string expectedResult = "Supplement: AI-5 IS: 542";
            string actualResult = factory.ProduceSupplement("AI-5", 542);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_Factory_Upgrade_Robot_Method_Returns_False_If_Robot_Has_The_Supplement()
        {
            var factory = new Factory("Samsung", 6);

            factory.ProduceRobot("Optimus", 300, 542);

            var robot = factory.Robots.First(r => r.Model == "Optimus");
            var supplement = new Supplement("AI-5", 542);
            robot.Supplements.Add(supplement);

            bool actualResult = factory.UpgradeRobot(robot, supplement);

            Assert.AreEqual(false, actualResult);
        }

        [Test]
        public void Test_Factory_Upgrade_Robot_Method_Returns_False_If_Robot_Interface_Id_Doesnt_Match_With_Supplement_Id()
        {
            var factory = new Factory("Samsung", 6);
            factory.ProduceRobot("Optimus", 300, 1444);
            var robot = factory.Robots.First(r => r.Model == "Optimus");
            var supplement = new Supplement("AI-5", 542);

            bool actualResult = factory.UpgradeRobot(robot, supplement);

            Assert.AreEqual(false, actualResult);
        }

        [Test]
        public void Test_Factory_Upgrade_Robot_Method_Works_Correct()
        {
            var factory = new Factory("Samsung", 6);
            factory.ProduceRobot("Optimus", 300, 542);
            var robot = factory.Robots.First(r => r.Model == "Optimus");
            var supplement = new Supplement("AI-5", 542);

            bool actualResult = factory.UpgradeRobot(robot, supplement);

            Assert.AreEqual(1, robot.Supplements.Count);
            Assert.AreEqual(true, actualResult);
        }

        [TestCase(300)]
        [TestCase(500)]
        public void Test_Sell_Robot_Method_Return_The_Right_Robot(double price)
        {
            var factory = new Factory("Samsung", 6);
            factory.ProduceRobot("Optimus", 300, 542);
            var robot = factory.Robots.First(r => r.Model == "Optimus");

            var expectedRobot = robot;
            var actualRobot = factory.SellRobot(price);

            Assert.AreEqual(expectedRobot, actualRobot);
        }

        // Robot.cs tests
        [Test]
        public void Test_Can_Create_Robot()
        {
            var robot = new Robot("Optimus", 300, 542);
            Assert.IsInstanceOf<Robot>(robot);
        }

        [Test]
        public void Test_Robot_Model_Is_Set_Correct()
        {
            var robot = new Robot("Optimus", 300, 542);
            Assert.AreEqual("Optimus", robot.Model);
        }

        [Test]
        public void Test_Robot_Price_Is_Set_Correct()
        {
            var robot = new Robot("Optimus", 300, 542);
            Assert.AreEqual(300, robot.Price);
        }


        [Test]
        public void Test_Robot_Interface_Id_Is_Set_Correct()
        {
            var robot = new Robot("Optimus", 300, 542);
            Assert.AreEqual(542, robot.InterfaceStandard);
        }

        [Test]
        public void Test_Robot_Supplements_List_Is_Initialized_Correct()
        {
            var robot = new Robot("Optimus", 300, 542);

            Assert.IsNotNull(robot.Supplements);
        }

        // Supplement.cs tests
        [Test]
        public void Test_Can_Create_Supplement()
        {
            var supplement = new Supplement("AI-5", 542);
            Assert.IsInstanceOf<Supplement>(supplement);
        }

        [Test]
        public void Test_Supplement_Name_Is_Set_Correct()
        {
            var supplement = new Supplement("AI-5", 542);
            Assert.AreEqual("AI-5", supplement.Name);
        }

        [Test]
        public void Test_Supplement_Interface_Id_Is_Set_Correct()
        {
            var supplement = new Supplement("AI-5", 542);
            Assert.AreEqual(542, supplement.InterfaceStandard);
        }

        [Test]
        public void Test_Supplement_To_String_Method_Is_Working_Correct()
        {
            var supplement = new Supplement("AI-5", 542);
            
            string expectedResult = "Supplement: AI-5 IS: 542";
            string actualResult = supplement.ToString();
            
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}