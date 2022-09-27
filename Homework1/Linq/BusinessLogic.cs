namespace Linq;

public class BusinessLogic
{
    private List<User> users;
    private List<Record> records;

    public BusinessLogic(List<User> users, List<Record> records)
    {
        this.users = users;
        this.records = records;
    }

    public List<User> GetUsersBySurname(string surname) => 
        users.Where(user => user.Surname == surname)
            .ToList();

    public User GetUserByID(int id) =>
        users.Single(user => user.ID == id);
    
    public List<User> GetUsersBySubstring(string substring) =>
        users.Where(user => user.Surname.Contains(substring) || user.Name.Contains(substring))
            .ToList();


    public List<string> GetAllUniqueNames() =>
        users.Select(user => user.Name)
            .Distinct()
            .ToList();
    
    public List<User> GetAllAuthors() =>
        users.Where(user => records.Any(record => record.Author == user))
            .ToList();

    public Dictionary<int, User> GetUsersDictionary() =>
        users.ToDictionary(user => user.ID);

 
    public int GetMaxID() => 
        users.MaxBy(user => user.ID).ID;
    
    public List<User> GetOrderedUsers() => 
        users.OrderBy(user => user.ID)
            .ToList();

    public List<User> GetDescendingOrderedUsers() => 
        users.OrderByDescending(user => user.ID)
            .ToList();

    public List<User> GetReversedUsers() => 
        users.OrderBy(user => user.ID)
            .Reverse()
            .ToList();

    public List<User> GetUsersPage(int pageSize, int pageIndex) =>
        users.Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToList();
}