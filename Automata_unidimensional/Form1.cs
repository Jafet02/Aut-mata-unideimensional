using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automata_unidimensional
{
    public partial class Form1 : Form
    {
        const int Ren = 15;
        const int Col = 30;
        int generacion = 0;

        public Form1()
        {
            InitializeComponent();
        }

        PictureBox[,] celula = new PictureBox[Ren, Col];
        int[,] vida = new int[Ren, Col];
        int[,] sig_gen = new int[Ren, Col];
        int[,] vecinos = new int[Ren, Col];
        int sobrepobla = 0;
        

        public void Inicializar_tablero()
        {
            int i, j;
            i = 0;
            for (j=0; j <=29; j++)
            {
                celula[i, j] = new PictureBox();
                Controls.Add(celula[i, j]);
                celula[i, j].Width = 30;
                celula[i, j].Height = 30;
                celula[i, j].Click += new EventHandler(this.Cambio_Click);

                celula[i, j].Top = 150;
                celula[i, j].Left = 150 + (30 * j);
                celula[i, j].BackColor = Color.White;
            }
            
        }


        public void Vida_inicio()
        {
            int j;
            for (j = 0; j <= 29; j++)
            {
                generacion = 0;
                vida[generacion, j] = 0;
                celula[generacion, j].BackColor = Color.White;
                celula[generacion, j].Image = Image.FromFile("muerto.png");
            }
        }

        public void Evaluar_estado()
        {
            string index;
            int j;
            for (j = 0; j <= 29; j++)
            {
                index = celula[generacion, j].BackColor.ToString();
                switch (index)
                {
                    case "Color [White]":
                        vida[generacion, j] = 0;
                        break;
                    case "Color [Green]":
                        vida[generacion, j] = 1;
                        break;
                    default:
                        vida[generacion, j] = 0;
                        break;
                }
            }
        }

        public void Evaluar_vecinos()
        {
            int indice;
            int j;
            for (j = 0; j <= 29; j++)
            {
                indice = j;
                vecinos[generacion, j] = 0;
                //Para 2 vecinos de casilla 0
                if (j == 0)
                {
                    if (vida[generacion, indice + 1] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                    if (vida[generacion, indice + 2] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                }
                //Para 3 vecinos de casilla 1
                if (j == 1)
                {
                    if (vida[generacion, indice - 1] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                    if (vida[generacion, indice + 1] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                    if (vida[generacion, indice + 2] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                }
                //Para 3 vecinos de casilla 28
                if (j == 28)
                {
                    if (vida[generacion, indice - 2] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                    if (vida[generacion, indice - 1] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                    if (vida[generacion, indice + 1] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                }
                //Para 2 vecinos de casilla 29
                if (j == 29)
                {
                    if (vida[generacion, indice - 2] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                    if (vida[generacion, indice - 1] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                }
                //Para 4 vecinos de las demás casillas
                if (j != 0 && j != 1 && j != 28 && j != 29)
                {
                    if (vida[generacion, indice - 2] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                    if (vida[generacion, indice - 1] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                    if (vida[generacion, indice + 1] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                    if (vida[generacion, indice + 2] == 1)
                    {
                        vecinos[generacion, j]++;
                    }
                }
            }
        }


        public void Nueva_generacion()
        {
            if (generacion == 14 || sobrepobla == 30)
            {
                timer1.Enabled = false;
                if (generacion == 14)
                {
                    MessageBox.Show("Generaciones terminadas.\nDebe reiniciar.");
                }
                if (sobrepobla == 30)
                {
                    MessageBox.Show("SOBREPOBLACIÓN\nDebe reiniciar.");
                }
            }
            else
            {
                int j, ngen;
                int listo = 0;

                ngen = generacion;
                ngen++;

                for (j = 0; j <= 29; j++)
                {
                    celula[ngen, j] = new PictureBox();
                    Controls.Add(celula[ngen, j]);
                    celula[ngen, j].Width = 30;
                    celula[ngen, j].Height = 30;
                    celula[ngen, j].Click += new EventHandler(this.Cambio_Click);

                    celula[ngen, j].Top = 150 + (ngen * 30);
                    celula[ngen, j].Left = 150 + (30 * j);
                    celula[ngen, j].BackColor = Color.White;
                }

                for (j = 0; j <= 29; j++)
                {
                    //Regla 1
                    if (vida[generacion, j] == 1 && vecinos[generacion, j] >= 2)
                    {
                        vida[ngen, j] = 1;
                        celula[ngen, j].BackColor = Color.Green;
                        celula[ngen, j].Image = Image.FromFile("vivo.png");
                        listo = 1;
                    }

                    //Regla 2
                    if (vida[generacion, j] == 1 && vecinos[generacion, j] < 2 && listo == 0)
                    {
                        vida[ngen, j] = 0;
                        celula[ngen, j].BackColor = Color.White;
                        celula[ngen, j].Image = Image.FromFile("muerto.png");
                        listo = 1;
                    }
                    //Regla 3
                    if (vida[generacion, j] == 0 && vecinos[generacion, j] == 3 && listo == 0)
                    {
                        vida[ngen, j] = 1;
                        celula[ngen, j].BackColor = Color.Green;
                        celula[ngen, j].Image = Image.FromFile("vivo.png");
                        listo = 1;
                    }

                    //Regla 4
                    if (vida[generacion, j] == 0 && vecinos[generacion, j] == 0 && listo == 0)
                    {
                        vida[ngen, j] = 0;
                        celula[ngen, j].BackColor = Color.White;
                        celula[ngen, j].Image = Image.FromFile("muerto.png");
                        listo = 1;
                    }
                    listo = 0;
                }
                generacion = ngen;

                }
            //Revisar sobrepobacion
            if (sobrepobla != 30)
            {
                int s = 0;
                sobrepobla = 0;
                for (s = 0; s <= 29; s++)
                {
                    sobrepobla += vida[generacion, s];
                }
                if (sobrepobla == 30)
                {
                    timer1.Enabled = false;
                    MessageBox.Show("SOBREPOBLACIÓN\nDebe reiniciar.");
                }
                else
                    sobrepobla = 0;
            }
        }

        private void Cambio_Click(object sender, EventArgs e)
        {
            string index;
            PictureBox clickedPicture = (PictureBox)sender;
            index = clickedPicture.BackColor.ToString();

            switch (index)
            {
                case "Color [White]":
                    clickedPicture.BackColor = Color.Green;
                    clickedPicture.Image = Image.FromFile("vivo.png");
                    break;

                case "Color [Green]":
                    clickedPicture.BackColor = Color.White;
                    clickedPicture.Image = Image.FromFile("muerto.png");
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Inicializar_tablero();
            Vida_inicio();
        }

        private void button_ngen_Click(object sender, EventArgs e)
        {
            Evaluar_estado();
            Evaluar_vecinos();
            Nueva_generacion();
        }

        private void buttonreinicio_Click(object sender, EventArgs e)
        {
            int i, j;
            for(i=0; i<=14; i++)
            {
                for(j=0; j<=29; j++)
                {
                    Controls.Remove(celula[i, j]);
                    vida[i, j] = 0;
                    sig_gen[i, j] = 0;
                }
            }
            sobrepobla = 0;
            generacion = 0;
            Inicializar_tablero();
            timer1.Enabled = false;
        }

        private void buttongensuse_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Evaluar_estado();
            Evaluar_vecinos();
            Nueva_generacion();
        }

        private void buttonpause_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void buttonsalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
