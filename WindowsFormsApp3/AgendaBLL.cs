using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp3
{
    class AgendaBLL
    {
        public static void conecta()
        {
            AgendaDAL.conecta();
        }

        public static void desconecta()
        {
            AgendaDAL.desconecta();
        }

        public static bool validaDados(Agenda umaAgenda)
        {
            Erro.setErro(false);
            if (umaAgenda.getCodigo().Equals(""))
            {
                Erro.setMsg("O código é de preenchimento obrigatório!");
                return false;
            }
            if (umaAgenda.getCompromisso().Equals(""))
            {
                Erro.setMsg("O compromisso é de preenchimento obrigatório!");
                return false;
            }
            if (!DateTime.TryParse(umaAgenda.getData(), out _))
            {
                Erro.setMsg("A data inválida!");
                return false;
            }
            if (umaAgenda.getHora().Equals(""))
            {
                Erro.setMsg("A hora é de preenchimento obrigatório!");
                return false;
            }
            else
            {
                if (!(DateTime.TryParseExact(umaAgenda.getHora(), "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime resultado)))
                {
                    Erro.setMsg("A hora inválida!");
                    return false;
                }
            }


            return true;

        }
        public static void excluirCompromisso(Agenda umaAgenda)
        {
            Erro.setErro(false); 

            if (umaAgenda.getCodigo().Equals(""))
            {
                Erro.setMsg("O código do compromisso é obrigatório para exclusão!");
                return;
            }
            AgendaDAL.excluirAgenda(umaAgenda);
            AgendaDAL.excluirCompromisso(umaAgenda);
            AgendaDAL.excluirData(umaAgenda);

        }

        public static void validaDadosConsultaList(ListBox listbox, Agenda umaAgenda)
        {
            AgendaDAL.consultarAgendaList(listbox, umaAgenda);
        }

        public static void validaDadosAtualizar(Agenda umaAgenda)
        {
            if (validaDados(umaAgenda))
            {
                AgendaDAL.atualizaAgenda(umaAgenda);
                AgendaDAL.atualizaCompromisso(umaAgenda);
                AgendaDAL.atualizaData(umaAgenda);
            }
        }

        public static void validaDadosInserir(Agenda umaAgenda)
        {
            if (validaDados(umaAgenda))
            {

                if (AgendaDAL.agendaExiste(umaAgenda))
                {
                    AgendaDAL.InsertAgenda(umaAgenda);
                    AgendaDAL.InsertCompromisso(umaAgenda);
                    AgendaDAL.InsertData(umaAgenda);
                }
                else
                {
                    Erro.setMsg("A inserção não foi possivel, chave duplicada");
                }

            }
                

        }
    }
}


    

