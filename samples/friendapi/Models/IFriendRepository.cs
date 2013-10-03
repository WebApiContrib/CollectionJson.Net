using System.Collections.Generic;

namespace FriendApi.Models
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
