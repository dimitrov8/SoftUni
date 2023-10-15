namespace FootballTeam.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class Tests
    {
        private FootballTeam defaultTeam;
        private List<FootballTeam> footballTeams;
        private FootballPlayer defaultPlayer;
        private List<FootballPlayer> players;

        [SetUp]
        public void Setup()
        {
            this.defaultTeam = new FootballTeam("Real Madrid", 18);
            this.defaultPlayer = new FootballPlayer("Ronaldo", 7, "Forward");
            this.footballTeams = new List<FootballTeam>();
            this.players = new List<FootballPlayer>();
        }

        [Test]
        public void Test_Team_Constructor_Works_Correct()
        {
            this.footballTeams.Add(this.defaultTeam);

            Assert.IsInstanceOf<FootballTeam>(this.defaultTeam);
            Assert.AreEqual("Real Madrid", this.defaultTeam.Name);
            Assert.AreEqual(18, this.defaultTeam.Capacity);
            Assert.AreEqual(1, this.footballTeams.Count);
            Assert.IsNotNull(this.players);
        }

        [Test]
        public void Test_Can_Create_A_Team()
        {
            var juventusTeam = new FootballTeam("Juventus", 20);
            this.footballTeams.Add(this.defaultTeam);
            this.footballTeams.Add(juventusTeam);
            Assert.AreEqual(2, this.footballTeams.Count);
        }

        [Test]
        public void Test_Throw_Exception_If_Player_Position_Is_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                this.defaultPlayer = new FootballPlayer("Ronaldo", 7, "invalidPosition");
            });

            Assert.AreEqual("Invalid Position", ex.Message);
        }

        [Test]
        public void Test_Can_Add_New_Player()
        {
            this.defaultTeam.AddNewPlayer(this.defaultPlayer);
            Assert.AreEqual(1, this.defaultTeam.Players.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Throw_Exception_If_Team_Name_Is_Null_Or_Empty(string name)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var footballTeam = new FootballTeam(name, 20);
            });

            Assert.AreEqual("Name cannot be null or empty!", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Capacity_Is_Lower_Than_15()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var footballTeam = new FootballTeam("Team", 14);
            });

            Assert.AreEqual("Capacity min value = 15", ex.Message);
        }

        [Test]
        public void Test_Correct_Output_If_Trying_To_Add_More_Players()
        {
            this.defaultTeam = this.CreateTestTeam();
            string msg = this.defaultTeam.AddNewPlayer(this.defaultPlayer);

            Assert.AreEqual("No more positions available!", msg);
        }

        [Test]
        public void Test_Correct_Output_When_Adding_A_New_Player()
        {
            string msg = this.defaultTeam.AddNewPlayer(this.defaultPlayer);
            Assert.AreEqual(
                $"Added player {this.defaultPlayer.Name} in position {this.defaultPlayer.Position} with number {this.defaultPlayer.PlayerNumber}",
                msg);
        }

        [Test]
        public void Test_Can_Pick_A_Player()
        {
            var extraPlayer = new FootballPlayer("Benzema", 9, "Forward");
            this.defaultTeam.AddNewPlayer(this.defaultPlayer);
            this.defaultTeam.AddNewPlayer(extraPlayer);

            var pickedPlayer = this.defaultTeam.PickPlayer("Ronaldo");
            Assert.AreEqual(this.defaultPlayer, pickedPlayer);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Throw_Exception_If_Player_Name_Is_Null_Or_Empty(string name)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                this.defaultPlayer = new FootballPlayer(name, 7, "Forward");
            });

            Assert.AreEqual("Name cannot be null or empty!", ex.Message);
        }

        [Test]
        public void Test_Football_Player_Constructor_Works_Correct()
        {
            Assert.AreEqual("Ronaldo", this.defaultPlayer.Name);
            Assert.AreEqual(7, this.defaultPlayer.PlayerNumber);
            Assert.AreEqual("Forward", this.defaultPlayer.Position);
            Assert.AreEqual(0, this.defaultPlayer.ScoredGoals);
        }

        [Test]
        public void Test_Score_Works_Correct()
        {
            this.defaultPlayer.Score();
            Assert.AreEqual(1, this.defaultPlayer.ScoredGoals);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_Throw_Exception_If_Player_Number_Is_Zero_Or_Negative(int number)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                this.defaultPlayer = new FootballPlayer("Ronaldo", number, "Forward");
            });

            Assert.AreEqual("Player number must be in range [1,21]", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Player_Number_Is_More_Than_21()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                this.defaultPlayer = new FootballPlayer("Ronaldo", 22, "Forward");
            });

            Assert.AreEqual("Player number must be in range [1,21]", ex.Message);
        }

        [Test]
        public void Test_Player_Score_Works_Correct()
        {
            this.defaultTeam.AddNewPlayer(this.defaultPlayer);
            string msg = this.defaultTeam.PlayerScore(7);

            Assert.AreEqual(1, this.defaultPlayer.ScoredGoals);

            Assert.AreEqual($"{this.defaultPlayer.Name} scored and now has {this.defaultPlayer.ScoredGoals} for this season!", msg);
        }

        public FootballTeam CreateTestTeam()
        {
            this.defaultTeam = new FootballTeam("Test", 18);
            for (int i = 0; i < this.defaultTeam.Capacity; i++)
            {
                this.defaultTeam.AddNewPlayer(new FootballPlayer(i.ToString(), i + 1, "Forward"));
            }

            return this.defaultTeam;
        }
    }
}