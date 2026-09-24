using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Writer
{
	internal class FtpConnection : IDisposable
	{
		public FtpConnection(string host, string userID, string password, int port)
		{
			this.host = host;
			this.userID = userID;
			this.password = password;
			this.port = port;
			this.Connect();
		}
		public void Connect()
		{
			this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint ipendPoint = new IPEndPoint(Dns.GetHostEntry(this.host).AddressList[0], this.port);
			try
			{
				this.clientSocket.Connect(ipendPoint);
			}
			catch
			{
				throw new IOException("Couldn't connect to remote server");
			}
			this.Reply();
			if (this.retrievedValue != 220)
			{
				this.Disconnect();
				throw new IOException(this.reply.Substring(4));
			}
			this.Command("USER " + this.userID);
			if (this.retrievedValue != 331 && this.retrievedValue != 230)
			{
				this.Dispose();
				throw new IOException(this.reply.Substring(4));
			}
			if (this.retrievedValue != 230)
			{
				this.Command("PASS " + this.password);
				if (this.retrievedValue != 230 && this.retrievedValue != 202)
				{
					this.Dispose();
					throw new IOException(this.reply.Substring(4));
				}
			}
			this.Command("TYPE I");
			if (this.retrievedValue != 200)
			{
				throw new IOException(this.reply.Substring(4));
			}
		}
		public void Upload(string fileName)
		{
			Socket socket = this.DataSocket();
			this.Command("STOR " + Path.GetFileName(fileName));
			if (this.retrievedValue != 125 && this.retrievedValue != 150)
			{
				throw new IOException(this.reply.Substring(4));
			}
			FileStream fileStream = new FileStream(fileName, FileMode.Open);
			while ((this.bytes = fileStream.Read(this.buffer, 0, this.buffer.Length)) > 0)
			{
				socket.Send(this.buffer, this.bytes, SocketFlags.None);
			}
			fileStream.Close();
			if (socket.Connected)
			{
				socket.Close();
			}
			this.Reply();
			if (this.retrievedValue != 226 && this.retrievedValue != 250)
			{
				throw new IOException(this.reply.Substring(4));
			}
		}
		public void ChangeDirectory(string directory)
		{
			this.Command("CWD " + directory);
			if (this.retrievedValue != 250)
			{
				throw new IOException(this.reply.Substring(4));
			}
			this.directory = directory;
		}
		public void Dispose()
		{
			if (this.clientSocket != null)
			{
				this.clientSocket.Close();
				this.clientSocket = null;
			}
		}
		public void Disconnect()
		{
			if (this.clientSocket != null)
			{
				this.Command("QUIT");
			}
			this.Dispose();
		}
		private void Reply()
		{
			this.message = "";
			this.reply = this.GetLine();
			this.retrievedValue = int.Parse(this.reply.Substring(0, 3));
		}
		private string GetLine()
		{
			do
			{
				this.bytes = this.clientSocket.Receive(this.buffer, this.buffer.Length, SocketFlags.None);
				this.message += this.ascii.GetString(this.buffer, 0, this.bytes);
			}
			while (this.bytes >= this.buffer.Length);
			char[] array = new char[] { '\n' };
			string[] array2 = this.message.Split(array);
			if (this.message.Length > 2)
			{
				this.message = array2[array2.Length - 2];
			}
			else
			{
				this.message = array2[0];
			}
			string line;
			if (!this.message.Substring(3, 1).Equals(" "))
			{
				line = this.GetLine();
			}
			else
			{
				line = this.message;
			}
			return line;
		}
		private void Command(string command)
		{
			byte[] array = Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
			this.clientSocket.Send(array, array.Length, SocketFlags.None);
			this.Reply();
		}
		private Socket DataSocket()
		{
			this.Command("PASV");
			if (this.retrievedValue != 227)
			{
				throw new IOException(this.reply.Substring(4));
			}
			int num = this.reply.IndexOf('(');
			int num2 = this.reply.IndexOf(')');
			string text = this.reply.Substring(num + 1, num2 - num - 1);
			int[] array = new int[6];
			int length = text.Length;
			int num3 = 0;
			string text2 = "";
			int num4 = 0;
			while (num4 < length && num3 <= 6)
			{
				char c = char.Parse(text.Substring(num4, 1));
				if (char.IsDigit(c))
				{
					text2 += c;
				}
				else if (c != ',')
				{
					throw new IOException("Malformed PASV reply: " + this.reply);
				}
				if (c == ',' || num4 + 1 == length)
				{
					try
					{
						array[num3++] = int.Parse(text2);
						text2 = "";
					}
					catch
					{
						throw new IOException("Malformed PASV reply: " + this.reply);
					}
				}
				num4++;
			}
			string text3 = string.Concat(new object[]
			{
				array[0],
				".",
				array[1],
				".",
				array[2],
				".",
				array[3]
			});
			int num5 = (array[4] << 8) + array[5];
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint ipendPoint = new IPEndPoint(Dns.GetHostEntry(text3).AddressList[0], num5);
			try
			{
				socket.Connect(ipendPoint);
			}
			catch
			{
				throw new IOException("Can't connect to remote server");
			}
			return socket;
		}
		private string host;
		private string directory;
		private string userID;
		private string password;
		private string message;
		private string reply;
		private int port;
		private int bytes;
		private int retrievedValue;
		private Socket clientSocket;
		private byte[] buffer = new byte[512];
		private Encoding ascii = Encoding.ASCII;
	}
}
