using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.NETSDK;

namespace UC.NETSDK.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            UserApi user = new UserApi("qqqqq", "12345", "http://127.0.0.1:8888/api/v1/");
            var u = user.GetByIdAsync(1).Result;
            Console.WriteLine(u.Id+" "+u.NickName+" "+u.PhoneNum+" ");
            Console.ReadKey();
        }
    }
}
