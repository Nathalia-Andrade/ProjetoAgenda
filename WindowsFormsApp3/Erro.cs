using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class Erro
    {
        private static string msg;
        private static bool erro; 

        public static void setMsg(string _mensagem)
        {
            erro = true;
            msg = _mensagem; 
        }
        public static void setErro(bool _erro) { Erro.erro = _erro; }
        public static string getMsg() { return msg; }
        public static bool getErro() { return erro; }


    }
}
