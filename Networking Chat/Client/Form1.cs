using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Client
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

        UdpClient client;
        IPEndPoint remoteIP;
        int remotPort = 44444;
        int port = 55555;

        private void setaddress_Click(object sender, EventArgs e)
        {
            try
            {
                client?.Close();
                client = new UdpClient(port);

                if (!IPAddress.TryParse(ip.Text, out IPAddress parsedIP))
                {
                    chatlog.AppendText("[Error] Bad IP." + Environment.NewLine);
                    return;
                }

                remoteIP = new IPEndPoint(parsedIP, remotPort);
                client.BeginReceive(new AsyncCallback(onRecieve), client);
                chatlog.AppendText("[Info] Connected to " + parsedIP + Environment.NewLine);
                setaddress.Hide();
            }
            catch (SocketException)
            {
                chatlog.AppendText("[Error] Port in use." + Environment.NewLine);
            }
            catch
            {
                chatlog.AppendText("[Error] Failed to connect." + Environment.NewLine);
            }
        }

        private void onRecieve(IAsyncResult ar)
        {
            try
            {
                IPEndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, remotPort);
                byte[] buff = client.EndReceive(ar, ref senderEndPoint);
                string receivedMessage = Encoding.UTF8.GetString(buff);

                Controlinvoke(chatlog, () => chatlog.AppendText(receivedMessage + Environment.NewLine));
                client.BeginReceive(new AsyncCallback(onRecieve), client);
            }
            catch (ObjectDisposedException)
            {
                Controlinvoke(chatlog, () => chatlog.AppendText("[Error] Connection closed." + Environment.NewLine));
            }
            catch
            {
                Controlinvoke(chatlog, () => chatlog.AppendText("[Error] Failed to receive." + Environment.NewLine));
            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                chatlog.AppendText("[Error] Not connected." + Environment.NewLine);
                return;
            }

            if (remoteIP == null)
            {
                chatlog.AppendText("[Error] No server." + Environment.NewLine);
                return;
            }

            string name = username.Text.Trim();
            string message = textbox.Text.Trim();

            if (!string.IsNullOrEmpty(message))
            {
                byte[] data = Encoding.UTF8.GetBytes(name + ">> " + message);
                client.Send(data, data.Length, remoteIP);
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
