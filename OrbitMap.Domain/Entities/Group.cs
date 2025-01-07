using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrbitMap.Domain.Entities;

public class Group
{
    public Group()
    {
    }
    
    public Group(string name)
    {
        Name = name;
    }
    
    [Key]
    public string Name { get; set; }
    public ICollection<Connection> Connections { get; set; } = new List<Connection>();
}

public class Connection
{
    public Connection()
    {
    }
    
    public Connection(string connectionId, string userName)
    {
        ConnectionId = connectionId;
        UserName = userName;
    }
    
    public string ConnectionId { get; set; }
    public string UserName { get; set; }
    
    public string GroupName { get; set; }
    
    [ForeignKey(nameof(GroupName))]
    public Group Group { get; set; }
    
}