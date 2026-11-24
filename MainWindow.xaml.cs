using System;
using System.Data;
using System.Windows;

namespace WPF_ADONET_XML_AuthorsArticles
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private dsData dsData;
		public MainWindow()
		{
			InitializeComponent();
			dsData = new dsData();
			Application.Current.Properties["dsData"] = dsData;
		}


		private void miLoad_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				dsData.ReadXml("Data.xml", XmlReadMode.Auto);
				MessageBox.Show("Data loaded into memory!");
			}
			catch (Exception)
			{
				var au1 = dsData.dtAuthors.AdddtAuthorsRow(1, "Erzsébet Major");
				var au2 = dsData.dtAuthors.AdddtAuthorsRow(2, "Irén Kőszegi");
				var au3 = dsData.dtAuthors.AdddtAuthorsRow(3, "Esmeralda Kadëna");
				var au4 = dsData.dtAuthors.AdddtAuthorsRow(4, "Attila Végh");

				var ar1 = dsData.dtArticles.AdddtArticlesRow(1, "A study on the popularity of handmade products among consumers","https://doi.org/10.47833/2020.3.AGR.002");
				var ar2 = dsData.dtArticles.AdddtArticlesRow(2, "On parallelotope configuration",
					"https://doi.org/10.47833/2020.3.CSC.002");
				var ar3 = dsData.dtArticles.AdddtArticlesRow(3, "On extraction and projection of Dirichlet-Voronoi cells of root - lattices",
					"http://gradus.kefo.hu/archive/2015-1/2015_1_CSC_002_Vegh.pdf");
				dsData.dtAuthorsArticles.AdddtAuthorsArticlesRow(au1, ar1);
				dsData.dtAuthorsArticles.AdddtAuthorsArticlesRow(au2, ar1);
				dsData.dtAuthorsArticles.AdddtAuthorsArticlesRow(au3, ar1);
				dsData.dtAuthorsArticles.AdddtAuthorsArticlesRow(au4, ar2);
				dsData.dtAuthorsArticles.AdddtAuthorsArticlesRow(au4, ar3);
				MessageBox.Show("Dataset created in the memory!");
			}
		}

		private void miSave_Click(object sender, RoutedEventArgs e)
		{
			dsData.WriteXml("Data.xml", XmlWriteMode.WriteSchema);
		}

		private void miExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void miAuthors_Click(object sender, RoutedEventArgs e)
		{
			dgArticles.Visibility = Visibility.Collapsed;
			ucCombined.Visibility = Visibility.Collapsed;
			dgAuthors.Visibility = Visibility.Visible;
			dgAuthors.ItemsSource = dsData.dtAuthors;
		}

		private void miArticles_Click(object sender, RoutedEventArgs e)
		{
			dgAuthors.Visibility = Visibility.Collapsed;
			ucCombined.Visibility = Visibility.Collapsed;
			dgArticles.Visibility = Visibility.Visible;
			dgArticles.ItemsSource = dsData.dtArticles;
		}

		private void miCombined_Click(object sender, RoutedEventArgs e)
		{
			dgAuthors.Visibility = Visibility.Collapsed;
			dgArticles.Visibility = Visibility.Collapsed;
			ucCombined.Visibility = Visibility.Visible;
		}
	}
}
