using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Cadastro
{
    public partial class frmCadastroCliente : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // height of ellipse
           int nHeightEllipse // width of ellipse
       );

        public frmCadastroCliente()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                //Atribui True no Handled para cancelar o evento
                e.Handled = true;
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            var novoArquivo = txtNome.Text + ".txt";

            if(File.Exists(novoArquivo))
            {
                MessageBox.Show("O cliente já existe!");
                return;
            }

            if(txtNome.Text.Contains(" ") && txtNome.Text.Length > 8)
            {
                lblNome.Text = "";
                lbl1.Text = "✅";
            } 
            else
            {
                lblNome.ForeColor = Color.DarkRed;
                lblNome.Text = "*Digite seu nome completo";
                lbl1.Text = "";
            }

            if(txtEmail.Text.Contains("@gmail.com") || txtEmail.Text.Contains("@outlook.com") || txtEmail.Text.Contains("@hotmail.com"))
            {
                lblEmail.Text = "";
                lbl2.Text = "✅";
            } 
            else
            {
                lblEmail.ForeColor = Color.DarkRed;
                lblEmail.Text = "*Digite seu e-mail";
                lbl2.Text = "";
            }

            if(txtSenha.Text != txtSenha2.Text && txtSenha.Text.Length < 8 && txtSenha2.Text.Length < 8)
            {
                lblSenha3.ForeColor = Color.DarkRed;
                lblSenha3.Text = "*Verifique se as senhas estão diferentes";
                lbl3.Text = "";
                lbl4.Text = "";
            } else
            {
                lblSenha3.Text = "";
                lbl3.Text = "";
                lbl4.Text = "";
            }

            if(txtSenha.Text.Length >= 8 && txtSenha2.Text.Length >= 8 && (txtSenha.Text == txtSenha2.Text))
            {
                lblSenha.Text = "";
                lblSenha2.Text = "";
                lbl3.Text = "✅";
                lbl4.Text = "✅";
            } else
            {
                lblSenha.ForeColor = Color.DarkRed;
                lblSenha2.ForeColor = Color.DarkRed;
                lblSenha.Text = "*A senha deve conter no mínimo 8 digitos";
                lblSenha2.Text = "*A senha deve conter no mínimo 8 digitos";
                lbl3.Text = "";
                lbl4.Text = "";
            }

            if(txtNome.Text.Contains(" ") && txtNome.Text.Length > 8 && (txtEmail.Text.Contains("@gmail.com") || txtEmail.Text.Contains("@outlook.com") || txtEmail.Text.Contains("@hotmail.com")) && txtSenha.Text.Length >= 8 && txtSenha2.Text.Length >= 8 && (txtSenha.Text == txtSenha2.Text))
            {
                var arquivo = File.CreateText(novoArquivo);

                arquivo.WriteLine(txtNome.Text);
                arquivo.WriteLine(txtEmail.Text);
                arquivo.WriteLine(txtSenha.Text);
                arquivo.Flush();
                arquivo.Close();

                MessageBox.Show("O cliente foi cadastrado com sucesso!");
                return;
            }
        }
    }
}
