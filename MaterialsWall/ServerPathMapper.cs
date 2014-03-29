using System;
using System.Web;

namespace Granta.MaterialsWall
{
    public interface IServerPathMapper
    {
        string MapPath(string path);
    }

    public sealed class ServerPathMapper : IServerPathMapper
    {
        private readonly HttpServerUtility server;

        public ServerPathMapper(HttpServerUtility server)
        {
            if (server == null)
            {
                throw new ArgumentNullException("server");
            }
            
            this.server = server;
        }

        public string MapPath(string path)
        {
            return server.MapPath(path);
        }
    }
}
