using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.Formatting.CollectionJson.Models
{
    public interface IFriendRepository
    {
        IEnumerable<Friend> GetAll();
        Friend Get(int id);
        int Add(Friend friend);
        void Update(Friend friend);
        void Remove(int id);
    }
}
