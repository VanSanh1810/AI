using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class DB
    {
        public DataTableCollection DBs;
        public DataTable LOCATION;
        public DataTable CONNECTION;
        public DataTable COST;
        public DataTable NAME;
        public DB()
        {
            using (var stream = File.Open("DATA.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                    });
                    DBs = dataSet.Tables;
                    LOCATION = DBs["LOCATION"];
                    CONNECTION = DBs["CONNECTION"];
                    COST = DBs["COST"];
                    NAME = DBs["NAME"];
                }
            }
        }
    }
}
/*using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "(*.xls)|*.xls|(*.xlsx)|*.xlsx" })
            {
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //MessageBox.Show(openFileDialog.FileName.ToString());
                    using (var stream = File.Open("TEST.xlsx", FileMode.Open, FileAccess.Read))
                    {
                        using(IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            dataTableCollection = dataSet.Tables;
                            dataGridView1.DataSource = dataTableCollection["Sheet1"];
                        }
                    }
                }
            }*/