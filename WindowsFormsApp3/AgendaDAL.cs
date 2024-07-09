using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp3
{
    class AgendaDAL
    {
        private static String strConexao = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BDAgenda.mdb";
        private static OleDbConnection conn = new OleDbConnection(strConexao);
        private static OleDbCommand strSQL;
        private static OleDbDataReader result;

        public static void conecta()
        {
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                Erro.setMsg("Problemas ao se conectar ao Banco de Dados");
            }

        }

        public static void desconecta()
        {
            conn.Close();
        }

        public static void InsertAgenda(Agenda umaAgenda)
        {
            String aux = "insert into TabAgenda(codigo,participante) values ('" + umaAgenda.getCodigo() + "','" + umaAgenda.getParticipante() + "')";

            strSQL = new OleDbCommand(aux, conn);
            Erro.setErro(false);
            try
            {
                strSQL.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Erro.setMsg("A inserção não foi possivel, chave duplicada");
            }
        }

        public static void InsertCompromisso(Agenda umaAgenda)
        {
            String aux = "insert into TabCompromisso(codigo,compromisso,agendaid) values ('" + umaAgenda.getCodigo() + "','" + umaAgenda.getCompromisso() + "','" + umaAgenda.getCodigo() + "')";

            strSQL = new OleDbCommand(aux, conn);
            Erro.setErro(false);
            try
            {
                strSQL.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Erro.setMsg("A inserção não foi possivel, chave duplicada");
            }
        }

        public static void InsertData(Agenda umaAgenda)
        {
            String aux = "insert into TabData(codigo,data,hora,agendaid) values ('" + umaAgenda.getCodigo() + "','" + umaAgenda.getData() + "','" + umaAgenda.getHora() + "','" + umaAgenda.getCodigo() + "')";

            strSQL = new OleDbCommand(aux, conn);
            Erro.setErro(false);
            try
            {
                strSQL.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Erro.setMsg("A inserção não foi possivel, chave duplicada");
            }
        }

        public static void consultarAgendaList(ListBox listBox, Agenda umaAgenda)
        {
            String aux = "SELECT ta.codigo, ta.participante,tc.compromisso, td.data, td.hora " +
                "FROM (TabAgenda ta " +
                "LEFT JOIN TabData td ON ta.codigo = td.agendaid) " +
                "LEFT JOIN TabCompromisso tc ON ta.codigo = tc.agendaid ";


            String where = " WHERE ";

            if (umaAgenda.getCodigo() != "")
                where += " ta.codigo = '" + umaAgenda.getCodigo() + "'";

            if (umaAgenda.getParticipante() != "")
            {
                if (where.Length > 7)
                    where += " AND ";
                where += " ta.participante = '" + umaAgenda.getParticipante() + "'";
            }

            if (umaAgenda.getCompromisso() != "")
            {
                if (where.Length > 7)
                    where += " AND ";
                where += " tc.compromisso = '" + umaAgenda.getCompromisso() + "'";
            }

            if (umaAgenda.getData() != "")
            {
                if (where.Length > 7)
                    where += " AND ";
                where += " td.data = '" + umaAgenda.getData() + "'";
            }

            if (umaAgenda.getHora() != "")
            {
                if (where.Length > 7)
                    where += " AND ";
                where += " td.hora = '" + umaAgenda.getHora() + "'";
            }

            if (where.Length > 7)
                aux += where;

            strSQL = new OleDbCommand(aux, conn);
            result = strSQL.ExecuteReader();
            Erro.setErro(false);
            int itemsResult = 0;
            while (true)
            {

                if (result.Read())
                {
                    var itemAgenda = result.GetString(3) + " - " + result.GetString(4) + "      " + result.GetString(2) + "      " + result.GetString(1);
                    listBox.Items.Add(itemAgenda);
                    itemsResult++;
                }
                else
                {
                    if (itemsResult == 0)
                        Erro.setMsg("Compromisso não cadastrado.");
                    break;
                }
            }
            itemsResult = 0;

        }

        public static void atualizaAgenda(Agenda umaAgenda)
        {
            string aux = "update TabAgenda set participante='" + umaAgenda.getParticipante() +  "' where codigo='" + umaAgenda.getCodigo() + "'";

            strSQL = new OleDbCommand(aux, conn);
            Erro.setErro(false);
            try
            {
                strSQL.ExecuteNonQuery();
                
            }
            catch (Exception) 
            {
                Erro.setMsg("Erro ao atualizar o Agenda.");
            }
        }

        public static void atualizaCompromisso(Agenda umaAgenda)
        {
            string aux = "update TabCompromisso set compromisso='" + umaAgenda.getCompromisso() + "' where agendaid='" + umaAgenda.getCodigo() + "'";
            strSQL = new OleDbCommand(aux, conn);
            Erro.setErro(false);
            try
            {
                strSQL.ExecuteNonQuery();              
            }
            catch (Exception)
            {
                Erro.setMsg("Erro ao atualizar o compromisso.");
            }
        }

        public static void atualizaData(Agenda umaAgenda)
        {
            string aux = "update TabData set  data='" + umaAgenda.getData() + "', hora='" + umaAgenda.getHora() + "' where agendaid='" + umaAgenda.getCodigo() + "'";

            strSQL = new OleDbCommand(aux, conn);
            Erro.setErro(false);
            try
            {
                strSQL.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Erro.setMsg("Erro ao atualizar o Data.");
            }
        }


        public static void excluirAgenda(Agenda umaAgenda)
        {
            string aux = "delete from TabAgenda where codigo='" + umaAgenda.getCodigo() + "'";

            strSQL = new OleDbCommand(aux, conn);
            Erro.setErro(false);

            try
            {
                strSQL.ExecuteNonQuery();
            }
            catch ( Exception) 
            {
                Erro.setMsg("Erro ao excluir o compromisso.");
            }
        }

        public static void excluirCompromisso(Agenda umaAgenda)
        {
            string aux = "delete from TabCompromisso where agendaid='" + umaAgenda.getCodigo() + "'";

            strSQL = new OleDbCommand(aux, conn);
            Erro.setErro(false);

            try
            {
                strSQL.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Erro.setMsg("Erro ao excluir o compromisso.");
            }
        }

        public static void excluirData(Agenda umaAgenda)
        {
            string aux = "delete from TabData where agendaid='" + umaAgenda.getCodigo() + "'";

            strSQL = new OleDbCommand(aux, conn);
            Erro.setErro(false);

            try
            {
                strSQL.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Erro.setMsg("Erro ao excluir o compromisso.");
            }
        }

        public static bool agendaExiste(Agenda umaAgenda)
        {
            string aux = "SELECT * FROM TabAgenda where codigo='" + umaAgenda.getCodigo() + "'";

            strSQL = new OleDbCommand(aux, conn);
            Erro.setErro(false);

            try
            {
                strSQL = new OleDbCommand(aux, conn);
                result = strSQL.ExecuteReader();

                if(result.Read())
                return false;
            }
            catch (Exception)
            {
                Erro.setMsg("Erro ao excluir o compromisso.");
            }

            return true;
        }
    }
}

    

