using NetFamre.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Cache
{
    public interface ICacheFight
    {
        void AddID(int id, UserToken token);
        void RemoveID(int id);
        List<int> AddPlayer(int id);
        void ReMovePlayer(int id);
        UserToken Get(int id);
        int GetID(UserToken token);
        void OffLine(UserToken token);
        void AddDicRoom(int id,int value);
        void RemoveDicRoom(int id);
    }
}
