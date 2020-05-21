using NetFamre.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProtocol.dto;
namespace MyServer.biz
{
    public interface IBizFight
    {    
        /// <summary>
        /// 玩家和小球移动，实时传输
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        UserToken PlayerBallMove(int ID);
        /// <summary>
        /// 请求对局
        /// </summary>
        List<int> FindFight(UserToken token,int id);

        /// <summary>
        /// 取消对局
        /// </summary>
        void CallFight(int id);

        /// <summary>
        /// 获取对应的UserToken
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserToken GetUserToken(int id);

        /// <summary>
        /// 获取对应的ID
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        int GetID(UserToken token);

        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="token"></param>
        /// <param name="id"></param>
        void OffLine(UserToken token);

        void AddDicRoom(int id, int value);
    }
}
