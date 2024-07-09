using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class Agenda
    {
        private String codigo;
        private String participante;
        private String compromisso;
        private String data;
        private String hora;

        public void setCodigo(String _codigo) { codigo = _codigo; }
        public void setParticipante (String _participante) {  participante = _participante; }
        public void setCompromisso(String _compromisso) { compromisso = _compromisso; }
        public void setData(String _data) { data = _data; }
        public void setHora(String _hora) { hora = _hora; }
        public void setlogal(String _hora) { hora = _hora; }

        public String getCodigo() { return codigo; }
        public String getParticipante() {  return participante; }
        public String getCompromisso() { return compromisso; }
        public String getData() { return data; }
        public String getHora() { return hora; }

    }
}
