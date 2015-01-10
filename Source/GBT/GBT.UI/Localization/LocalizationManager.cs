using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace GBT.UI.Localization
{
	public class LocalizationManager : DependencyObject, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(name));
			}
		}

		private List<Language> _Languages = new List<Language>();
		public List<Language> Languages
		{
			get { return this._Languages; }
			set { this._Languages = value; OnPropertyChanged("Languages"); }
		}

		private Language _CurrentLanguage = LoadDictionary(DefaultLanguage);
		public Language CurrentLanguage
		{
			get { return this._CurrentLanguage; }
			set { this._CurrentLanguage = value; OnPropertyChanged("CurrentLanguage"); }
		}

		private static Uri DefaultLanguage
		{
			get
			{
				return new Uri("GBT.UI.Localization.Strings.xml", UriKind.Relative);
			}
		}

		private LanguageCollection _LanguageProviders = new LanguageCollection();
		public LanguageCollection LanguageProviders
		{
			get
			{
				return this._LanguageProviders;
			}
			set
			{
				this._LanguageProviders = value;
				this.Languages = this.LanguageProviders
					.Select(p => LoadDictionary(p.Source))
					.ToList();
				var currentCulture = CultureInfo.CurrentCulture.ToString();
				this.CurrentLanguage = this.Languages.Where(l => currentCulture == l.Culture).FirstOrDefault();
				if (this.CurrentLanguage == null)
					this.CurrentLanguage = this.Languages.Where(l => currentCulture.StartsWith(l.Culture + "-")).FirstOrDefault();
				if (this.CurrentLanguage == null)
					this.CurrentLanguage = this.Languages.FirstOrDefault();
			}
		}

		private static Language LoadDictionary(Uri source)
		{
			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(source.ToString()))
				return Deserialize(stream);
		}

		public static Language Deserialize(Stream stream)
		{
			if (stream == null)
				return null;
			Language Output = new Language();

			//create the xmlDocument
			XmlDocument xd = new XmlDocument();
			xd.Load(XmlReader.Create(stream));

			Output.Description = xd.LastChild.Attributes["Description"].Value;
			Output.Culture = xd.LastChild.Attributes["Culture"].Value;

			//Scan all the nodes in the main doc and add them to the dictionary
			//you can recursively check child nodes if your document requires.
			Output.Strings = new Dictionary<string, string>();
			foreach (XmlNode node in xd.DocumentElement)
				Output.Strings.Add(node.Attributes["Key"].Value, node.Attributes["Value"].Value);

			return Output;
		}

		public static string GetResourceTextFile(string filename)
		{
			string result = string.Empty;

			using (Stream stream = typeof(LocalizationManager).Assembly.GetManifestResourceStream("LanguageTest." + filename))
			{
				using (StreamReader sr = new StreamReader(stream))
				{
					result = sr.ReadToEnd();
				}
			}
			return result;
		}
	}
}
