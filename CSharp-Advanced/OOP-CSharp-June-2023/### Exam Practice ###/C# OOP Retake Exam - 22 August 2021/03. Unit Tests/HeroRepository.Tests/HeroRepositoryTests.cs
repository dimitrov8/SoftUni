using NUnit.Framework;
using System;

public class HeroRepositoryTests
{
    private string name;
    private int level;
    private HeroRepository heroes;

    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public void Test_Hero_Constructor_Works()
    {
        this.name = "The Best Hero";
        this.level = 30;

        var hero = new Hero(this.name, this.level);

        Assert.AreEqual(this.name, hero.Name);
        Assert.AreEqual(this.level, hero.Level);
    }

    [Test]
    public void Test_Hero_Repo_Constructor_Works()
    {
        this.heroes = new HeroRepository();
        Assert.IsNotNull(this.heroes);
    }

    [Test]
    public void Test_Throw_Exception_If_Trying_To_Create_Null_Hero()
    {
        this.heroes = new HeroRepository();
        Hero hero = null;
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.heroes.Create(hero);
        });
    }

    [Test]
    public void Test_Throw_Exception_If_Trying_To_Create_Existing_Hero()
    {
        this.heroes = new HeroRepository();
        var hero = new Hero("The Best Hero", 30);
        this.heroes.Create(hero);

        Assert.Throws<InvalidOperationException>(() =>
        {
            this.heroes.Create(hero);
        });
    }

    [TestCase(null)]
    [TestCase(" ")]
    public void Test_Throw_Exception_If_Trying_To_Remove_Null_Or_WhiteSpace_Hero_Name(string invalidName)
    {
        this.heroes = new HeroRepository();
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.heroes.Remove(invalidName);
        });
    }

    [Test]
    public void Test_Remove_Works()
    {
        this.heroes = new HeroRepository();
        var hero = new Hero("The Best Hero", 30);

        this.heroes.Create(hero);
        bool result = this.heroes.Remove(hero.Name);
        
        Assert.AreEqual(0, this.heroes.Heroes.Count);
        Assert.IsTrue(result);
    }

    [Test]
    public void Test_Get_Highest_Level_Hero_Works()
    {
        this.heroes = new HeroRepository();
        var lowLevel = new Hero("Newbie", 1);
        var bestHero = new Hero("The Best Hero", 30);
        var midLevel = new Hero("Hero", 10);

        this.heroes.Create(lowLevel);
        this.heroes.Create(bestHero);
        this.heroes.Create(midLevel);

        var result = this.heroes.GetHeroWithHighestLevel();

        Assert.AreEqual(bestHero, result);
    }

    [Test]
    public void Test_Get_Hero_Works()
    {
        this.heroes = new HeroRepository();
        var lowLevel = new Hero("Newbie", 1);
        var bestHero = new Hero("The Best Hero", 30);
        var midLevel = new Hero("Hero", 10);
        
        this.heroes.Create(lowLevel);
        this.heroes.Create(bestHero);
        this.heroes.Create(midLevel);

        var result = this.heroes.GetHero("Newbie");
        
        Assert.AreEqual(lowLevel, result);
    }
}