using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace SqliteSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        static DataSet dataset = new DataSet();//DataSetのインスタンスを作る
        DataTable table = dataset.Tables.Add();//DataSetにテーブルを追加する

        public MainWindow()
        {
            InitializeComponent();
        }


        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            //テーブルデータ読み込み、いったんクリア
            table.Clear();
            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SQLite");
                using (DbConnection connection = factory.CreateConnection())
                {

                    connection.ConnectionString = "Data Source =D:\\person.db";
                    using (connection)
                    {
                        DbCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT * FROM person";
                        command.CommandType = CommandType.Text;
                        command.Connection = connection;

                        DbDataAdapter adapter = factory.CreateDataAdapter();
                        adapter.SelectCommand = command;

                        adapter.Fill(table);
                    }
                    if (dataGrid.DataContext == null)
                    {
                        dataGrid.DataContext = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SQLite");
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source  =D:\\person.db";
                    using (connection)
                    {
                        DbCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT * FROM person";
                        command.CommandType = CommandType.Text;
                        command.Connection = connection;

                        DbDataAdapter adapter = factory.CreateDataAdapter();
                        adapter.SelectCommand = command;

                        DbCommandBuilder builder = factory.CreateCommandBuilder();
                        builder.DataAdapter = adapter;

                        adapter.InsertCommand = builder.GetInsertCommand();
                        adapter.UpdateCommand = builder.GetUpdateCommand();
                        adapter.DeleteCommand = builder.GetDeleteCommand();

                        adapter.Update(table);
                    }
                    MessageBox.Show("Saved/");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = table.NewRow();
            row[1] = "add";
            table.Rows.Add(row);
        }

    }
}
