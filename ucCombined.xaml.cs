using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataRowView = System.Data.DataRowView;

namespace WPF_ADONET_XML_AuthorsArticles
{
	/// <summary>
	/// Interaction logic for ucCombined.xaml
	/// </summary>
	public partial class ucCombined : UserControl
	{
		private dsData dsData;
		public ucCombined()
		{
			InitializeComponent();
		}


		private void ucCombined_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!IsVisible) return;
			dsData=Application.Current.Properties["dsData"] as dsData;
			cbAuthors.ItemsSource = dsData?.dtAuthors;
			cbAuthors.DisplayMemberPath = "Name";
			cbAuthors.SelectedIndex = 0;
		}

		private void cbAuthors_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var Author = (cbAuthors.SelectedItem as DataRowView)?.Row  as 
				dsData.dtAuthorsRow;
			var Articles = from x in dsData.dtAuthorsArticles
									 where x.dtAuthorsRow == Author
									 select
									 new
									 { Authors=(from y in dsData.dtAuthorsArticles
										   where y.dtArticlesRow==x.dtArticlesRow
										   select 
										     y.dtAuthorsRow.Name).Aggregate("", 
										       (a, c) => a + c + ", "),
										 x.dtArticlesRow.Title,
										 x.dtArticlesRow.URL
									 };
			
			tbArticles.Text = Articles.Aggregate("",(a,c)=>a+c.Authors+c.Title+", "+c.URL+"\r\n\r\n");
		}
	}
}
