using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            AgendaBLL.conecta();
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Agenda agenda = new Agenda();
            agenda.setCodigo(textBox1.Text);
            agenda.setParticipante(textBox2.Text);
            agenda.setCompromisso(comboBox1.Text);
            agenda.setData(textBox4.Text);
            agenda.setHora(textBox3.Text);
           
            AgendaBLL.validaDadosConsultaList(listBox1, agenda);

            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Agenda agenda = new Agenda();
            agenda.setCodigo(textBox1.Text);
            agenda.setCompromisso(comboBox1.Text);
            agenda.setData(textBox4.Text);
            agenda.setHora(textBox3.Text);
            agenda.setParticipante(textBox2.Text);
            AgendaBLL.validaDadosInserir(agenda);

            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
            else
                MessageBox.Show("Compromisso inserido com sucesso!");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LimparFormulario();
        }
        private void LimparFormulario()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedItem = null;
            listBox1.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Agenda agenda = new Agenda();
            agenda.setCodigo(textBox1.Text);
            agenda.setCompromisso(comboBox1.Text);
            agenda.setData(textBox4.Text);
            agenda.setHora(textBox3.Text);
            agenda.setParticipante(textBox2.Text);

            AgendaBLL.validaDadosAtualizar(agenda);
            if (!Erro.getErro())
            {
                MessageBox.Show("Compromisso atualizado com sucesso!", "Sucesso");
            }
            else
            {
                MessageBox.Show(Erro.getMsg(), "Erro ao Atualizar");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string codigo = textBox1.Text;

            Agenda agenda = new Agenda(); 
            agenda.setCodigo(codigo);

            AgendaBLL.excluirCompromisso(agenda); 

            if (!Erro.getErro())
            {
                MessageBox.Show("Compromisso excluído com sucesso!", "Sucesso");
            }
            else
            {
                MessageBox.Show(Erro.getMsg(), "Erro ao Excluir");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string imagem = "";

            if (comboBox1.Text == "viagem")
                imagem = "viagem.jpg";
            else if (comboBox1.Text == "passeio")
                imagem = "passeio.jpg";
            else if (comboBox1.Text == "médico")
                imagem = "médico.jpg";
            else if (comboBox1.Text == "salão")
                imagem = "salão.jpg";
            else if (comboBox1.Text == "treino")
                imagem = "treino.jpg";
            else if (comboBox1.Text == "reunião")
                imagem = "reunião.jpg";
            else if (comboBox1.Text == "shopping")
                imagem = "shopping.jpg";

            if (comboBox1.Text != "")
                pictureBox1.Image = Image.FromFile(imagem);
            else
                pictureBox1.Image = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            preview.ShowDialog();
        }

        private void relatorio_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            SolidBrush nCaneta = new SolidBrush(Color.Black);
            Font font = new Font("Arial", 14);
            int linha = 10;
            foreach (object agenda in listBox1.Items)
            {
                e.Graphics.DrawString(agenda.ToString(), font, nCaneta, 10, linha);
                linha += 20;
            }
        }
    }
 }