using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GBT.Localization
{
	// Courtesy http://www.codeproject.com/Articles/22967/WPF-Runtime-Localization
	public class CultureResources
	{
		//only fetch installed cultures once
		private static bool bFoundInstalledCultures = false;

		private static List<CultureInfo> pSupportedCultures = new List<CultureInfo>();
		/// <summary>
		/// List of available cultures, enumerated at startup
		/// </summary>
		public static List<CultureInfo> SupportedCultures
		{
			get { return pSupportedCultures; }
		}

		public CultureResources()
		{
			if (!bFoundInstalledCultures)
			{
				SupportedCultures.Add(GBT.Localization.Properties.Settings.Default.DefaultCulture);

				//determine which cultures are available to this application
				Debug.WriteLine("Get Installed cultures:");
				CultureInfo tCulture = new CultureInfo("");
				foreach (string dir in Directory.GetDirectories(System.AppDomain.CurrentDomain.BaseDirectory))
				{
					try
					{
						//see if this directory corresponds to a valid culture name
						DirectoryInfo dirinfo = new DirectoryInfo(dir);
						tCulture = CultureInfo.GetCultureInfo(dirinfo.Name);

						//determine if a resources dll exists in this directory that matches the executable name
						var filename = System.Reflection.Assembly.GetEntryAssembly().Location;
						if (dirinfo.GetFiles(Path.GetFileNameWithoutExtension(filename) + ".resources.dll").Length > 0)
						{
							pSupportedCultures.Add(tCulture);
							Debug.WriteLine(string.Format(" Found Culture: {0} [{1}]", tCulture.DisplayName, tCulture.Name));
						}
					}
					catch (ArgumentException) //ignore exceptions generated for any unrelated directories in the bin folder
					{
					}
				}
				bFoundInstalledCultures = true;
			}
		}

		/// <summary>
		/// The Resources ObjectDataProvider uses this method to get an instance of the WPFLocalize.Properties.Resources class
		/// </summary>
		/// <returns></returns>
		public ResourceDictionary GetResourceInstance()
		{
			var test = Application.Current.FindResource("Cultures.Resources");
			return (ResourceDictionary)Application.Current.FindResource("Resources");
		}
		
		private static ObjectDataProvider m_provider;
		public static ObjectDataProvider ResourceProvider
		{
			get
			{
				if (m_provider == null)
					m_provider = (ObjectDataProvider)Application.Current.FindResource("Resources");
				return m_provider;
			}
		}

		/// <summary>
		/// Change the current culture used in the application.
		/// If the desired culture is available all localized elements are updated.
		/// </summary>
		/// <param name="culture">Culture to change to</param>
		public static void ChangeCulture(CultureInfo culture)
		{
			//remain on the current culture if the desired culture cannot be found
			// - otherwise it would revert to the default resources set, which may or may not be desired.
			if (pSupportedCultures.Contains(culture))
			{
				//GBT.Localization.Strings.Culture = culture;
				//var instance = GetResourceInstance();
				//ResourceProvider.Refresh();
			}
			else
				Debug.WriteLine(string.Format("Culture [{0}] not available", culture));
		}
	}
}
