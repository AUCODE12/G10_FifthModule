using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Dal.Entites;

public class Project
{
    public long ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? StartingTime { get; set; }
    public DateTime? EndingTime { get; set; }

    public long UserInfoId { get; set; }
    public UserInfo UserInfo { get; set; }
}
