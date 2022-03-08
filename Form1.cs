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
            //funcion que muestra los Datos 
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
            //funcion que muestra los Datos 
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
            //función de mostrar los datos en los datagridview
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = empleados;

            dataGridView2.DataSource = null;
            dataGridView2.Refresh();
            dataGridView2.DataSource = sueldo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //llamamos a las funciones 
                MTrabajo();
                MDatos();
                mostrar();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Calcula_Click(object sender, EventArgs e)
        {
            //mediate la nueva clase en la lsita guardamos la nueva cifra del sueldo
            for (int a = 0; a < empleados.Count; a++)
            {
                for (int i = 0; i < sueldo.Count; i++)
                {
                    if (empleados[a].NumeroE == sueldo[i].NumeroE)
                    {
                        NSueldo dire = new NSueldo();
                        dire.NumeroE = sueldo[a].NumeroE;
                        dire.NombreE = sueldo[a].Nombre;
                        //se realisa la multiplicación necesaria
                        dire.Total_Sueldo = sueldo[i].SueldoE * empleados[a].Horasmes;

                        sueldonuevo.Add(dire);

                    }
                }
            }
            //mostramos la nueva tabla con el sueldo total mediate el sueldo y las horas trabajadas al mes 
            dataGridView3.DataSource = null;
            dataGridView3.Refresh();
            dataGridView3.DataSource = sueldonuevo;
        }
        private string[] Getnombre()
        {
            //mediante una función busca el nombre del empleado de la lists 
            var envolveN = new string[sueldonuevo.Count];
            for (int i = 0; i < sueldonuevo.Count; i++)
            {
                //la guardamos
                envolveN[i] = sueldonuevo[i].NombreE;
            }
            return envolveN;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //en el boton desplegamos el nombre de los diferentes empeados de la lista
            comboBox1.DataSource = Getnombre();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //buscamos y comparamos si son iguales lo de la lista y lo selecionado en el combobox
            int p = sueldonuevo.FindIndex(t => t.NombreE == comboBox1.Text);
            //si fuera diferente guardaria o mandaria un mensaje de que no existe 
            //pero como el combobox muetra los datos exitentes asi que no abrian problemas
            if (p == -1)
            {
                //mandamos mensaje
                MessageBox.Show("El nombre del empleado no existe", "Tome en cuenta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                //como vemos que existe lo mandamos a llamar y se coloca en un textbox
                // gracias a los "--" separamos los datos
                textBox1.Text = sueldonuevo[p].NumeroE.ToString()+ " -- " + sueldonuevo[p].NombreE.ToString()+ " -- " +sueldonuevo[p].Total_Sueldo.ToString();
            }
        }
    }
}
