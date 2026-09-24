using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Writer
{
	internal class FtpConfiguration
	{
		public IConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
			set
			{
				this.configuration = value;
				this.host = this.configuration["Host"];
				this.userName = this.configuration["UserName"];
				this.password = this.DecryptPassword(this.configuration["Password"]);
				this.directory = this.configuration["Directory"];
				this.port = this.configuration["Port"];
			}
		}
		public bool IsEmpty
		{
			get
			{
				return this.host == null || this.userName == null || this.directory == null || this.port == null;
			}
		}
		public string Host
		{
			get
			{
				return this.host;
			}
			set
			{
				this.host = value;
				this.UpdateConfiguation();
			}
		}
		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				this.userName = value;
				this.UpdateConfiguation();
			}
		}
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
				this.UpdateConfiguation();
			}
		}
		public string Directory
		{
			get
			{
				return this.directory;
			}
			set
			{
				this.directory = value;
				this.UpdateConfiguation();
			}
		}
		public string Port
		{
			get
			{
				return this.port;
			}
			set
			{
				this.port = value;
				this.UpdateConfiguation();
			}
		}
		private void UpdateConfiguation()
		{
			this.configuration.Clear();
			this.configuration["Host"] = this.host;
			this.configuration["UserName"] = this.userName;
			this.configuration["Password"] = this.EncryptPassword(this.password);
			this.configuration["Directory"] = this.directory;
			this.configuration["Port"] = this.port;
		}
		private string EncryptPassword(string password)
		{
			string text;
			if (password != null)
			{
				ICryptoTransform cryptoTransform = this.symAlgorithm.CreateEncryptor(Convert.FromBase64String(this._key), Convert.FromBase64String(this._iv));
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				byte[] bytes = asciiencoding.GetBytes(password);
				byte[] array = this.Crypto(cryptoTransform, bytes);
				text = Convert.ToBase64String(array);
			}
			else
			{
				text = null;
			}
			return text;
		}
		private string DecryptPassword(string password)
		{
			string text;
			if (password != null)
			{
				ICryptoTransform cryptoTransform = this.symAlgorithm.CreateDecryptor(Convert.FromBase64String(this._key), Convert.FromBase64String(this._iv));
				byte[] array = Convert.FromBase64String(password);
				byte[] array2 = this.Crypto(cryptoTransform, array);
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				text = asciiencoding.GetString(array2);
			}
			else
			{
				text = null;
			}
			return text;
		}
		private byte[] Crypto(ICryptoTransform op, byte[] input)
		{
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, op, CryptoStreamMode.Write);
			cryptoStream.Write(input, 0, input.Length);
			cryptoStream.Close();
			return memoryStream.ToArray();
		}
		private IConfiguration configuration;
		private SymmetricAlgorithm symAlgorithm = new RijndaelManaged();
		private string _iv = "edwq7SgLQYYffT/RN3Y5cA==";
		private string _key = "9Ojb8DCkF6VxgBNq2i5c67dZ3+nJq/GBaGOTrPEthnI=";
		private string host;
		private string userName;
		private string password;
		private string directory;
		private string port;
	}
}
