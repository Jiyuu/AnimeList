using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniDBAPI.Data
{
    public class AuthCommand:Command
    {
        string user     { get; set; }
        string password { get; set; }
        int protover    { get; set; }
        string client   { get; set; }
        int clientVer   { get; set; }
        bool nat        { get; set; }
        string encoding { get; set; }
        int mtu         { get; set; }
        bool imgserver  { get; set; }
        //bool comp
        public AuthCommand()
        {
            this.commandType = CommandEnum.AUTH;
            //com
        }
    
    }
    public class Command
    {
        public CommandEnum commandType { get; set; }
    }

    public enum CommandEnum
    { 
    AUTH
    }
}
