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
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e) 
        {
            GraphicsPath forma = new GraphicsPath();
            forma.AddEllipse(0, 0, btnFechar.Width, btnFechar.Height);
            btnFechar.Region = new Region(forma);
        }


        private void iniciar_Click(object sender, EventArgs e)
        {
            SinglePlayer single = new SinglePlayer();
            this.Hide();
            single.ShowDialog();
            Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Deseja realmente SAIR ?", "SAIR", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (confirm.ToString().ToUpper() == "YES")
            {
                Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MultiPlayer multi = new MultiPlayer();
            this.Hide();
            multi.ShowDialog();
            Dispose();
        }

        private void TelaInicial_Load(object sender, EventArgs e)
        {
            
        }
    }
}
