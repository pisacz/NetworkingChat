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

            server?.Close();
            server = new UdpClient(port);

            if (!IPAddress.TryParse(ip.Text, out IPAddress parsedIP))
            {
                chatlog.AppendText("[Error] Invalid IP address. Please enter a valid IP." + Environment.NewLine);
                return;
            }

            remoteIP = new IPEndPoint(parsedIP, remotPort);
            server.BeginReceive(new AsyncCallback(onRecieve), server);

            chatlog.AppendText("[Info] IP address set up: " + parsedIP.ToString() + Environment.NewLine);
            setaddress.Hide();
        }

        private void onRecieve(IAsyncResult ar)
        {
            try
            {
                UdpClient udp = (UdpClient)ar.AsyncState;
                byte[] buff = udp.EndReceive(ar, ref remoteIP);
                string receivedMessage = Encoding.UTF8.GetString(buff);

                Controlinvoke(chatlog, () => chatlog.AppendText(receivedMessage + Environment.NewLine));

                udp.BeginReceive(new AsyncCallback(onRecieve), udp);
            }
            catch (ObjectDisposedException)
            {

            }
            catch (Exception ex)
            {
                Controlinvoke(chatlog, () => chatlog.AppendText("[Error] Connection failed." + Environment.NewLine));
            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                chatlog.AppendText("[Error] IP Address not set up." + Environment.NewLine);
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
