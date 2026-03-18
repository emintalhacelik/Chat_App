using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Chat_App
{
    public partial class Form1 : Form
    {
        TcpClient istemci;
        ListBox lstChat;
        TextBox txtMesaj;
        Button btnBaglan, btnGonder;

        public Form1()
        {
            this.Text = "Chat_App";
            this.Size = new Size(400, 500);
            this.BackColor = Color.WhiteSmoke;

            // Kodla Arayüz Elemanları
            btnBaglan = new Button() { Text = "BAĞLAN", Location = new Point(12, 12), Size = new Size(360, 35), BackColor = Color.Teal, ForeColor = Color.White };
            btnBaglan.Click += (s, e) => {
                try
                {
                    istemci = new TcpClient("127.0.0.1", 9595);
                    MessageBox.Show("Chat_App'a Giriş Yapıldı!");
                }
                catch { MessageBox.Show("Sunucu kapalı!"); }
            };

            lstChat = new ListBox() { Location = new Point(12, 60), Size = new Size(360, 300) };
            txtMesaj = new TextBox() { Location = new Point(12, 380), Size = new Size(260, 30) };
            btnGonder = new Button() { Text = "GÖNDER", Location = new Point(280, 378), Size = new Size(90, 25), BackColor = Color.SeaGreen, ForeColor = Color.White };
            btnGonder.Click += BtnGonder_Click;

            this.Controls.Add(btnBaglan);
            this.Controls.Add(lstChat);
            this.Controls.Add(txtMesaj);
            this.Controls.Add(btnGonder);
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            if (istemci != null && istemci.Connected && !string.IsNullOrEmpty(txtMesaj.Text))
            {
                NetworkStream stream = istemci.GetStream();
                byte[] veri = Encoding.UTF8.GetBytes(txtMesaj.Text);
                stream.Write(veri, 0, veri.Length);
                lstChat.Items.Add("Siz: " + txtMesaj.Text);
                txtMesaj.Clear();
            }
        }
    }
}