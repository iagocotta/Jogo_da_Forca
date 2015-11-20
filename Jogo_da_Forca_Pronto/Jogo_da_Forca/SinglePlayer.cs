using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jogo_da_Forca
{
    public partial class SinglePlayer : Form
    {
        public SinglePlayer()
        {

            InitializeComponent();
            pictureBox1.Visible = false;
            FPanel.Visible = false;
            label1.Visible = false;
            label4.Visible = false;

        }

        bool start;
        PictureBox[] imagem;
        string palavra, teclaSalva = "";
        string[] banco = new string[]
        {
            "Cachorro", "Gato", "Baleia","Macaco","Girafa", "Tatu", "Vaca", "Cabra", "Coelho", "Elefante",
            "Azul", "Amarelo", "Preto", "Roxo", "Branco","Laranja", "Marrom", "Vermelho", "Verde", "Cinza",
            "Ipatinga", "Sao Joao", "Guanhaes", "Belo Horizonte","Itabira", "Capelinha", "Turmalina", "Sardoa", "Governador Valadares", "Itaobim",
            "Cruzeiro","Atletico Mineiro","Corinthians", "Flamengo", "Coritiba", "Vasco", "Palmeiras", "Sao Paulo", "Botafogo", "Avai",
            "O Rappa", "Engenheiros do Hawai","David Guetta", "Steve Aoki","Jota Quest", "Skank", "Charlie Brown", "Linkin Park", "Dimitri Vegas", "Like Mike"

        };
        string dicas;


        int Erro = 1;

        public bool Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e) // Colocar botão Redondo
        {
            GraphicsPath forma = new GraphicsPath();
            forma.AddEllipse(0, 0, button1.Width, button1.Height);
            button1.Region = new Region(forma);


            GraphicsPath forma2 = new GraphicsPath();
            forma2.AddEllipse(0, 0, button2.Width, button2.Height);
            button2.Region = new Region(forma2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start = true;
            label4.Visible = true;
            FPanel.Visible = true;
            pictureBox1.Visible = true;
            pictureBox1.Image = Properties.Resources.inicio;


            Random gerar = new Random();
            int rnd = gerar.Next(0, banco.Length);
            palavra = banco[rnd].ToUpper();

            if (rnd < 10) { dicas = "Animais"; }
            else if (rnd >= 10 && rnd <= 19) { dicas = "Cores"; }
            else if (rnd >= 20 && rnd <= 29) { dicas = "Cidades"; }
            else if (rnd >= 30 && rnd <= 39) { dicas = "Times de Futebol"; }
            else { dicas = "Bandas de Música"; }

            label3.Text = dicas;

            imagem = new PictureBox[palavra.Length];

            for (int i = 0; i < palavra.Length; i++)
            {
                imagem[i] = new PictureBox();
                imagem[i].Paint += new PaintEventHandler(Pintar);
                FPanel.Controls.Add(imagem[i]);
            }

            iniciar.Enabled = false;


        }

        private void Pintar(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Black, 3), 0, 30, 30, 30);
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!Start)
            {
                return false;
            }
            if (palavra.ToUpper().Contains(keyData.ToString().ToUpper()) && !teclaSalva.ToUpper().Contains(keyData.ToString().ToUpper()))
            {
                Acertou(keyData.ToString());
            }
            else
            {
                if (teclaSalva.ToUpper().Contains(keyData.ToString().ToUpper()))
                {
                    MessageBox.Show("A letra digitada já está na tela");
                }
                else
                {
                    Errou(keyData.ToString());
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Reiniciar()
        {
            pictureBox1.Image = null;
            pictureBox1.Visible = false;
            FPanel.Visible = false;
            iniciar.Enabled = true;
            label1.Visible = false;
            label4.Visible = false;
            Start = false;
            Erro = 0;
            FPanel.Controls.Clear();
            teclaSalva = "";
            label2.Text = "";
            label3.Text = "";
        }

        private void Errou(string key)
        {
            if (label2.Text.ToUpper().Contains(key.ToUpper()))
            {
                MessageBox.Show("A letra já foi escolhida");
            }
            else
            {
                label1.Visible = true;
                label2.Text += key.ToUpper() + " - ";
                switch (Erro)
                {
                    case 1:
                        pictureBox1.Image = Properties.Resources.cabeca;
                        break;
                    case 2:
                        pictureBox1.Image = Properties.Resources.tronco;
                        break;
                    case 3:
                        pictureBox1.Image = Properties.Resources.besq;
                        break;
                    case 4:
                        pictureBox1.Image = Properties.Resources.bdir_esq;
                        break;
                    case 5:
                        pictureBox1.Image = Properties.Resources._short;
                        break;
                    case 6:
                        pictureBox1.Image = Properties.Resources.pesq;
                        break;
                    case 7:
                        pictureBox1.Image = Properties.Resources.pdir_esq;
                        pictureBox1.Image = Properties.Resources.perdeu;
                        MessageBox.Show("Você Perdeu ! A Palavra era : " + palavra.ToUpper());
                        Reiniciar();
                        break;
                }
                Erro++;
            }


        }



        private void Acertou(String key)

        {

            for (int cont = 0; cont < palavra.Length; cont++)
            {
                if (palavra[cont].ToString().ToUpper().Contains(key.ToUpper()))
                {
                    Graphics Gra = imagem[cont].CreateGraphics();
                    Gra.DrawString(key, new Font("Tahoma", 20), new SolidBrush(Color.Orange), 0, 0);
                    teclaSalva += key.ToUpper();
                }
            }

            if (teclaSalva.Length == palavra.Replace(" ", "").Length)
            {
                pictureBox1.Image = Properties.Resources.ganhou;
                MessageBox.Show("Parabéns, Você Acertou!");
                Reiniciar();
            }

        }

        private void label1_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Deseja realmente voltar ao MENU ?", "MENU", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (confirm.ToString().ToUpper() == "YES")
            {
                TelaInicial inicial = new TelaInicial();
                this.Hide();
                inicial.ShowDialog();
            }
        }

        private void SinglePlayer_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Deseja realmente SAIR ?", "SAIR", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (confirm.ToString().ToUpper() == "YES")
            {
                Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e) { }


    }
}
