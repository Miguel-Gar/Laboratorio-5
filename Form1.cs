using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio_5
{
    public partial class Form1 : Form
    {
        List<TrabajoE> empleados = new List<TrabajoE>(); 
        List<DatosE> sueldo = new List<DatosE>();
        List<NSueldo> sueldonuevo = new List<NSueldo>();
        public Form1()
        {
            InitializeComponent();
        }
        private void MDatos()
        {
            FileStream stream = new FileStream("Datos.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                DatosE dire = new DatosE();
                dire.NumeroE = Convert.ToInt16(reader.ReadLine());
                dire.Nombre = (reader.ReadLine());
                dire.SueldoE = Convert.ToInt16(reader.ReadLine());
                sueldo.Add(dire);
            }
            reader.Close();
        }
        private void MTrabajo()
        {
            FileStream stream = new FileStream("Trabajo.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                TrabajoE dire = new TrabajoE();
                dire.NumeroE = Convert.ToInt16(reader.ReadLine());
                dire.Horasmes = Convert.ToInt16(reader.ReadLine());
                dire.mes = (reader.ReadLine());
                empleados.Add(dire);
            }
            reader.Close();
        }
        private void mostrar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = empleados;

            dataGridView2.DataSource = null;
            dataGridView2.Refresh();
            dataGridView2.DataSource = sueldo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
                MTrabajo();
                MDatos();
                mostrar();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Calcula_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < empleados.Count; a++)
            {
                for (int i = 0; i < sueldo.Count; i++)
                {
                    if (empleados[a].NumeroE == sueldo[i].NumeroE)
                    {
                        NSueldo dire = new NSueldo();
                        dire.NumeroE = sueldo[a].NumeroE;
                        dire.NombreE = sueldo[a].Nombre;
                        dire.Total_Sueldo = sueldo[i].SueldoE * empleados[a].Horasmes;

                        sueldonuevo.Add(dire);

                    }
                }
            }
            dataGridView3.DataSource = null;
            dataGridView3.Refresh();
            dataGridView3.DataSource = sueldonuevo;
        }
        private string[] Getnombre()
        {
            var envolveN = new string[sueldonuevo.Count];
            for (int i = 0; i < sueldonuevo.Count; i++)
            {
                envolveN[i] = sueldonuevo[i].NombreE;
            }
            return envolveN;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.DataSource = Getnombre();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int p = sueldonuevo.FindIndex(t => t.NombreE == comboBox1.Text);
            if (p == -1)
            {
                NSueldo dire = new NSueldo();
                dire.NumeroE = 1;
                dire.NombreE = "6";
                dire.Total_Sueldo = 1;
                sueldonuevo.Add(dire);
            }
            else
            {
                textBox1.Text = sueldonuevo[p].NumeroE.ToString()+ " -- " + sueldonuevo[p].NombreE.ToString()+ " -- " +sueldonuevo[p].Total_Sueldo.ToString();
            }
        }
    }
}
