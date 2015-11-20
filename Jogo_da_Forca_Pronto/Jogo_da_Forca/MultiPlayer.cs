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
    public partial class MultiPlayer : Form
    {
        public MultiPlayer()
        {
            InitializeComponent();
            pictureBox1.Visible = false;
            FPanel.Visible = false;
            label1.Visible = false;
        }

        bool start;
        PictureBox[] imagem;
        string palavra, teclaSalva = "";
        byte Erro = 1;
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void iniciar_Click(object sender, EventArgs e)
        {
            if (textBox1.Text== "")
            {
                start = false;
                MessageBox.Show("ERRO! Digite uma Palavra para INICIAR ");
            }
            else
            {
                start = true;
                
                FPanel.Visible = true;
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.inicio;

                imagem = new PictureBox[textBox1.Text.Length];
                palavra = textBox1.Text.ToUpper();
                for (int i = 0; i < textBox1.Text.Length; i++)
                {
                    imagem[i] = new PictureBox();
                    imagem[i].Paint += new PaintEventHandler(Pintar);
                    FPanel.Controls.Add(imagem[i]);
                }

                iniciar.Enabled = false;
                textBox1.Enabled = false;
            }
        }

        private void Pintar(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Black, 3), 0, 30 , 30, 30);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!start)
            {
                return false;
            }
            if (textBox1.Text.ToUpper().Contains(keyData.ToString().ToUpper()) && !teclaSalva.ToUpper().Contains(keyData.ToString().ToUpper()))
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
            textBox1.Enabled = true;
            start = false;
            textBox1.Text = "";
            Erro = 0;
            teclaSalva = "";
            FPanel.Controls.Clear();
            label1.Visible = false;
            label2.Text = "";
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
                        MessageBox.Show("Você Perdeu ! A Palavra era : " + textBox1.Text.ToUpper());
                        Reiniciar();
                        break;

                }
                Erro++;
            }

        }

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

        private void MultiPlayer_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnPaint(PaintEventArgs e) 
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
            DialogResult confirm = MessageBox.Show("Deseja realmente SAIR ?", "SAIR", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (confirm.ToString().ToUpper() == "YES")
            {
                Close();
            }
        }

        private void Acertou(String key)

        {
            for (int cont = 0; cont < textBox1.Text.Length; cont++)
            {
                if (textBox1.Text[cont].ToString().ToUpper() == key.ToUpper())
                {
                    Graphics Gra = imagem[cont].CreateGraphics();
                    Gra.DrawString(key, new Font("Tahoma", 20), new SolidBrush(Color.Orange), 0, 0);
                    palavra = palavra.Replace(key.ToUpper(), " ");
                }
            }
            if (palavra.Trim() == "")
            {
                pictureBox1.Image = Properties.Resources.ganhou;
                MessageBox.Show("Parabéns, Você Acertou!");
                Reiniciar();
            }

        }
    }
}
