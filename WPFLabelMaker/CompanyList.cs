using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace WPFLabelMaker
{
    class CompanyList : ObservableCollection<CompanyName> 
    {
        public CompanyList()
        {
            try
            {
                //讀取資料庫

                OleDbCommand com = new OleDbCommand();

                string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;";
                strConnection += @"Data Source=c://Label//company.mdb";
                OleDbConnection con = new OleDbConnection(strConnection);
                con.Open();



                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * From Main", con);
                OleDbCommandBuilder cmdBuiler = new OleDbCommandBuilder(adapter);
                DataTable ds = new DataTable();
                adapter.Fill(ds);

                con.Close();

                foreach (DataRow row in ds.Rows)
                {
                    CompanyName company = new CompanyName { ID = row[1].ToString(), Name = row[2].ToString() };
                    this.Add(company);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "錯誤", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public CompanyName GetCompanyByIDOrName(string key)
        {
            foreach (CompanyName company in this)
            {
                if (company.ID.ToLower() == key.ToLower().Trim() || company.Name == key.Trim())
                    return company;
            }
            return null;
        }
    }

    public class CompanyName
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public override string ToString() { return Name; }
    }
}
