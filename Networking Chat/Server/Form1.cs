using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ip.Text = "127.0.0.1";
        }

        UdpClient server;
        IPEndPoint remoteIP;
        int remotPort = 55555;
        int port = 44444;

        private void setaddress_Click(object sender, EventArgs e)
        {
            try
            {
                server?.Close();
                server = new UdpClient(new IPEndPoint(IPAddress.Any, port));

                if (!IPAddress.TryParse(ip.Text, out IPAddress parsedIP))
                {
                    chatlog.AppendText("[Error] Bad IP." + Environment.NewLine);
                    return;
                }

                remoteIP = new IPEndPoint(parsedIP, remotPort);
                server.BeginReceive(new AsyncCallback(onRecieve), server);
                chatlog.AppendText("[Info] Server started at " + parsedIP + Environment.NewLine);
                setaddress.Hide();
            }
            catch (SocketException ex)
            {
                chatlog.AppendText("[Error] Port in use." + Environment.NewLine);
            }
            catch
            {
                chatlog.AppendText("[Error] Failed to start." + Environment.NewLine);
            }
        }

        private void onRecieve(IAsyncResult ar)
        {
            try
            {
                byte[] buff = server.EndReceive(ar, ref remoteIP);
                string receivedMessage = Encoding.UTF8.GetString(buff);
                Controlinvoke(chatlog, () => chatlog.AppendText(receivedMessage + Environment.NewLine));
                server.BeginReceive(new AsyncCallback(onRecieve), server);
            }
            catch
            {
                Controlinvoke(chatlog, () => chatlog.AppendText("[Error] Failed to receive." + Environment.NewLine));
            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                chatlog.AppendText("[Error] Server off." + Environment.NewLine);
                return;
            }

            if (remoteIP == null)
            {
                chatlog.AppendText("[Error] No client." + Environment.NewLine);
                return;
            }

            string name = username.Text.Trim();
            string message = textbox.Text.Trim();

            if (!string.IsNullOrEmpty(message))
            {
                byte[] data = Encoding.UTF8.GetBytes(name + ">> " + message);
                server.Send(data, data.Length, remoteIP);
                chatlog.AppendText(name + ">> " + message + Environment.NewLine);
                textbox.Clear();
            }
        }

        delegate void UniversalVoidDelegate();

        public static void Controlinvoke(Control control, Action function)
        {
            if (control.IsDisposed || control.Disposing)
            {
                return;
            }
            if (control.InvokeRequired)
            {
                control.Invoke(new UniversalVoidDelegate(() => Controlinvoke(control, function)));
                return;
            }
            function();
        }
    }
}
