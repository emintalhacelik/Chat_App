using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace Sunucu
{
    public partial class Form1 : Form
    {
        // Nesneleri kodla tanımlıyoruz
        TcpListener server;
        TcpClient client;
        ListBox lstMesajlar;
        Button btnBaslat;

        public Form1()
        {
            this.Text = "Chat_App Sunucu";
            this.Size = new Size(400, 450);
            Control.CheckForIllegalCrossThreadCalls = false;

            // Buton
            btnBaslat = new Button();
            btnBaslat.Text = "SUNUCUYU BAŞLAT";
            btnBaslat.Size = new Size(360, 40);
            btnBaslat.Location = new Point(12, 12);
            btnBaslat.BackColor = Color.LightGreen;
            btnBaslat.Click += BtnBaslat_Click;
            this.Controls.Add(btnBaslat);

            // Listeler
            lstMesajlar = new ListBox();
            lstMesajlar.Size = new Size(360, 320);
            lstMesajlar.Location = new Point(12, 60);
            this.Controls.Add(lstMesajlar);
        }

        private void BtnBaslat_Click(object sender, EventArgs e)
        {
            server = new TcpListener(IPAddress.Any, 9595);
            server.Start();
            lstMesajlar.Items.Add(">> Sunucu dinleniyor... (Port: 9595)");

            Thread dinle = new Thread(MesajBekle);
            dinle.Start();
        }

        void MesajBekle()
        {
            client = server.AcceptTcpClient();
            lstMesajlar.Items.Add(">> Bir kullanıcı bağlandı!");

            while (client.Connected)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int okunan = stream.Read(buffer, 0, buffer.Length);
                    if (okunan > 0)
                    {
                        string mesaj = Encoding.UTF8.GetString(buffer, 0, okunan);
                        lstMesajlar.Items.Add("Gelen: " + mesaj);
                    }
                }
                catch { break; }
            }
        }
    }
}