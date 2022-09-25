using System.Collections.Generic;
using Linq;
using NUnit.Framework;

namespace LinqTest;

public class Tests
{
    private BusinessLogic logic;
    [SetUp]
    public void Setup()
    {
        var testUser = new User(0, "Alice", "Red");
        var users = new List<User>
        {
            testUser,
            new User(2, "Bob", "White"),
            new User(1, "John", "Brown"),
            new User(3, "Fred", "Bob"),
            new User(4, "Bobby", "Orange"),
            new User(5, "Alice", "Wolf")
        };

        var records = new List<Record>
        {
            new (testUser, "bye world")
        };

        logic = new BusinessLogic(users, records);
    }

    [Test]
    public void Test_GetUsersBySurname()
    {
        var requiredUsers = logic.GetUsersBySurname("Red");
        Assert.AreEqual(1,requiredUsers.Count);
        Assert.AreEqual(0, requiredUsers[0].ID);
    }
    
    [Test]
    public void Test_GetUsersBySubstring()
    {
        var requiredUsers = logic.GetUsersBySubstring("Bob");
        Assert.AreEqual(3, requiredUsers.Count);
    }
    
    [Test]
    public void Test_GetUserById()
    {
        var requiredUser = logic.GetUserByID(0);
        Assert.AreEqual("Alice", requiredUser.Name);
    }
    
    [Test]
    public void Test_GetAllAuthors()
    {
        var requiredUsers = logic.GetAllAuthors();
        Assert.AreEqual(1, requiredUsers.Count);
        Assert.AreEqual("Alice", requiredUsers[0].Name);
    }
    
    [Test]
    public void Test_GetAllUniqueNames()
    {
        var requiredUsers = logic.GetAllUniqueNames();
        Assert.AreEqual(5, requiredUsers.Count);
    }
    
    [Test]
    public void Test_GetMaxId()
    {
        var requiredId = logic.GetMaxID();
        Assert.AreEqual(5, requiredId);
    }
    
    [Test]
    public void Test_GetOrderedUsers()
    {
        var users = logic.GetOrderedUsers();
        Assert.AreEqual(6, users.Count);
        Assert.AreEqual(0, users[0].ID);
        Assert.AreEqual(1, users[1].ID);
        Assert.AreEqual(2, users[2].ID);
        Assert.AreEqual(3, users[3].ID);
        Assert.AreEqual(4, users[4].ID);
        Assert.AreEqual(5, users[5].ID);
    }
    
    [Test]
    public void Test_GetDescendingOrderedUsers()
    {
        var users = logic.GetDescendingOrderedUsers();
        Assert.AreEqual(6, users.Count);
        Assert.AreEqual(5, users[0].ID);
        Assert.AreEqual(4, users[1].ID);
        Assert.AreEqual(3, users[2].ID);
        Assert.AreEqual(2, users[3].ID);
        Assert.AreEqual(1, users[4].ID);
        Assert.AreEqual(0, users[5].ID);
    }
    
    [Test]
    public void Test_GetReversedUsers()
    {
        var users = logic.GetReversedUsers();
        Assert.AreEqual(6, users.Count);
        Assert.AreEqual(5, users[0].ID);
        Assert.AreEqual(4, users[1].ID);
        Assert.AreEqual(3, users[2].ID);
        Assert.AreEqual(2, users[3].ID);
        Assert.AreEqual(1, users[4].ID);
        Assert.AreEqual(0, users[5].ID);
    }
    
    [Test]
    public void Test_GetUsersPage()
    {
        var users = logic.GetUsersPage(1, 1);
        Assert.AreEqual(1, users.Count);
        Assert.AreEqual("Bob", users[0].Name);
        
        users = logic.GetUsersPage(2, 1);
        Assert.AreEqual(2, users.Count);
        Assert.AreEqual("John", users[0].Name);
        Assert.AreEqual("Fred", users[1].Name);
    }
    
    [Test]
    public void Test_GetUsersDictionary()
    {
        var users = logic.GetUsersDictionary();
        var expectedIds = new [] { 0, 2, 1, 3, 4, 5 };
        Assert.AreEqual(6, users.Count);
        var i = 0;
        foreach (var (key, _) in users)
        {
            Assert.AreEqual(expectedIds[i], key);
            i++;
        }
    }
}