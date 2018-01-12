namespace Micua.Infrastructure.Utility
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Excel操作类
    /// </summary>
    /// Microsoft Excel 11.0 Object Library
    public class ExcelHelper
    {
        #region 数据导出至Excel文件
        /// <summary> 
        /// 导出Excel文件，自动返回可下载的文件流 
        /// </summary> 
        public static void DataTable1Excel(DataTable dtData)
        {
            HttpContext curContext = HttpContext.Current;
            if (dtData != null)
            {
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                curContext.Response.Charset = "utf-8";
                StringWriter strWriter = new StringWriter();
                HtmlTextWriter htmlWriter = new HtmlTextWriter(strWriter);
                GridView gvExport = new GridView { DataSource = dtData.DefaultView, AllowPaging = false };
                gvExport.DataBind();
                gvExport.RenderControl(htmlWriter);
                curContext.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=gb2312\"/>" + strWriter.ToString());
                curContext.Response.End();
            }
        }

        /// <summary>
        /// 导出Excel文件，转换为可读模式
        /// </summary>
        public static void DataTable2Excel(DataTable dtData)
        {
            DataGrid dgExport;
            HttpContext curContext = HttpContext.Current;
            StringWriter strWriter;
            HtmlTextWriter htmlWriter;

            if (dtData != null)
            {
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
                curContext.Response.Charset = string.Empty;
                strWriter = new StringWriter();
                htmlWriter = new HtmlTextWriter(strWriter);
                dgExport = new DataGrid { DataSource = dtData.DefaultView, AllowPaging = false };
                dgExport.DataBind();
                dgExport.RenderControl(htmlWriter);
                curContext.Response.Write(strWriter.ToString());
                curContext.Response.End();
            }
        }

        /// <summary>
        /// 导出Excel文件，并自定义文件名
        /// </summary>
        /// <param name="dtData">
        /// The dt Data.
        /// </param>
        /// <param name="FileName">
        /// The File Name.
        /// </param>
        public static void DataTable3Excel(DataTable dtData, String FileName)
        {
            HttpContext curContext = HttpContext.Current;

            if (dtData != null)
            {
                HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8);
                curContext.Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
                curContext.Response.ContentType = "application nd.ms-excel";
                curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
                curContext.Response.Charset = "GB2312";
                StringWriter strWriter = new StringWriter();
                HtmlTextWriter htmlWriter = new HtmlTextWriter(strWriter);
                GridView dgExport = new GridView { DataSource = dtData.DefaultView, AllowPaging = false };
                dgExport.DataBind();
                dgExport.RenderControl(htmlWriter);
                curContext.Response.Write(strWriter.ToString());
                curContext.Response.End();
            }
        }

        /// <summary>
        /// 将数据导出至Excel文件
        /// </summary>
        /// <param name="Table">DataTable对象</param>
        /// <param name="ExcelFilePath">Excel文件路径</param>
        public static bool OutputToExcel(DataTable Table, string ExcelFilePath)
        {
            if (File.Exists(ExcelFilePath))
            {
                throw new Exception("该文件已经存在！");
            }

            if ((Table.TableName.Trim().Length == 0) || (Table.TableName.ToLower() == "table"))
            {
                Table.TableName = "Sheet1";
            }

            // 数据表的列数
            int colCount = Table.Columns.Count;

            // 用于记数，实例化参数时的序号
            int i = 0;

            // 创建参数
            var para = new OleDbParameter[colCount];

            // 创建表结构的SQL语句
            string tableStructStr = @"Create Table " + Table.TableName + "(";

            // 连接字符串
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0;";
            var objConn = new OleDbConnection(connString);

            // 创建表结构
            var objCmd = new OleDbCommand();

            // 数据类型集合
            var dataTypeList = new ArrayList
                                {
                                    "System.Decimal",
                                    "System.Double",
                                    "System.Int16",
                                    "System.Int32",
                                    "System.Int64",
                                    "System.Single"
                                };

            // 遍历数据表的所有列，用于创建表结构
            foreach (DataColumn col in Table.Columns)
            {
                // 如果列属于数字列，则设置该列的数据类型为double
                if (dataTypeList.IndexOf(col.DataType.ToString()) >= 0)
                {
                    para[i] = new OleDbParameter("@" + col.ColumnName, OleDbType.Double);
                    objCmd.Parameters.Add(para[i]);

                    // 如果是最后一列
                    if (i + 1 == colCount)
                    {
                        tableStructStr += col.ColumnName + " double)";
                    }
                    else
                    {
                        tableStructStr += col.ColumnName + " double,";
                    }
                }
                else
                {
                    para[i] = new OleDbParameter("@" + col.ColumnName, OleDbType.VarChar);
                    objCmd.Parameters.Add(para[i]);

                    // 如果是最后一列
                    if (i + 1 == colCount)
                    {
                        tableStructStr += col.ColumnName + " varchar)";
                    }
                    else
                    {
                        tableStructStr += col.ColumnName + " varchar,";
                    }
                }

                i++;
            }

            // 创建Excel文件及文件结构
            objCmd.Connection = objConn;
            objCmd.CommandText = tableStructStr;

            if (objConn.State == ConnectionState.Closed)
            {
                objConn.Open();
            }

            objCmd.ExecuteNonQuery();

            // 插入记录的SQL语句
            string insertSql1 = "Insert into " + Table.TableName + " (";
            string insertSql2 = " Values (";

            // 遍历所有列，用于插入记录，在此创建插入记录的SQL语句
            for (int colId = 0; colId < colCount; colId++)
            {
                if (colId + 1 == colCount)
                {
                    // 最后一列
                    insertSql1 += Table.Columns[colId].ColumnName + ")";
                    insertSql2 += "@" + Table.Columns[colId].ColumnName + ")";
                }
                else
                {
                    insertSql1 += Table.Columns[colId].ColumnName + ",";
                    insertSql2 += "@" + Table.Columns[colId].ColumnName + ",";
                }
            }

            string insertSql = insertSql1 + insertSql2;

            // 遍历数据表的所有数据行
            for (int rowId = 0; rowId < Table.Rows.Count; rowId++)
            {
                for (int colId = 0; colId < colCount; colId++)
                {
                    if (para[colId].DbType == DbType.Double && Table.Rows[rowId][colId].ToString().Trim() == string.Empty)
                    {
                        para[colId].Value = 0;
                    }
                    else
                    {
                        para[colId].Value = Table.Rows[rowId][colId].ToString().Trim();
                    }
                }

                try
                {
                    objCmd.CommandText = insertSql;
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }

            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }

            return true;
        }

        /// <summary>
        /// 将数据导出至Excel文件
        /// </summary>
        /// <param name="Table">DataTable对象</param>
        /// <param name="Columns">要导出的数据列集合</param>
        /// <param name="ExcelFilePath">Excel文件路径</param>
        public static bool OutputToExcel(DataTable Table, ArrayList Columns, string ExcelFilePath)
        {
            if (File.Exists(ExcelFilePath))
            {
                throw new Exception("该文件已经存在！");
            }

            // 如果数据列数大于表的列数，取数据表的所有列
            if (Columns.Count > Table.Columns.Count)
            {
                for (int s = Table.Columns.Count + 1; s <= Columns.Count; s++)
                {
                    Columns.RemoveAt(s);   // 移除数据表列数后的所有列
                }
            }

            // 遍历所有的数据列，如果有数据列的数据类型不是 DataColumn，则将它移除
            for (int j = 0; j < Columns.Count; j++)
            {
                try
                {
                }
                catch (Exception)
                {
                    Columns.RemoveAt(j);
                }
            }

            if ((Table.TableName.Trim().Length == 0) || (Table.TableName.ToLower() == "table"))
            {
                Table.TableName = "Sheet1";
            }

            // 数据表的列数
            int colCount = Columns.Count;

            // 创建参数
            var para = new OleDbParameter[colCount];

            // 创建表结构的SQL语句
            string tableStructStr = @"Create Table " + Table.TableName + "(";

            // 连接字符串
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0;";
            var objConn = new OleDbConnection(connString);

            // 创建表结构
            var objCmd = new OleDbCommand();

            // 数据类型集合
            var dataTypeList = new ArrayList
                        {
                            "System.Decimal",
                            "System.Double",
                            "System.Int16",
                            "System.Int32",
                            "System.Int64",
                            "System.Single"
                        };

            // 遍历数据表的所有列，用于创建表结构
            for (int k = 0; k < colCount; k++)
            {
                var col = (DataColumn)Columns[k];

                // 列的数据类型是数字型
                if (dataTypeList.IndexOf(col.DataType.ToString().Trim()) >= 0)
                {
                    para[k] = new OleDbParameter("@" + col.Caption.Trim(), OleDbType.Double);
                    objCmd.Parameters.Add(para[k]);

                    // 如果是最后一列
                    if (k + 1 == colCount)
                    {
                        tableStructStr += col.Caption.Trim() + " Double)";
                    }
                    else
                    {
                        tableStructStr += col.Caption.Trim() + " Double,";
                    }
                }
                else
                {
                    para[k] = new OleDbParameter("@" + col.Caption.Trim(), OleDbType.VarChar);
                    objCmd.Parameters.Add(para[k]);

                    // 如果是最后一列
                    if (k + 1 == colCount)
                    {
                        tableStructStr += col.Caption.Trim() + " VarChar)";
                    }
                    else
                    {
                        tableStructStr += col.Caption.Trim() + " VarChar,";
                    }
                }
            }

            // 创建Excel文件及文件结构
            objCmd.Connection = objConn;
            objCmd.CommandText = tableStructStr;

            if (objConn.State == ConnectionState.Closed)
            {
                objConn.Open();
            }

            objCmd.ExecuteNonQuery();

            // 插入记录的SQL语句
            string insertSql1 = "Insert into " + Table.TableName + " (";
            string insertSql2 = " Values (";

            // 遍历所有列，用于插入记录，在此创建插入记录的SQL语句
            for (int colId = 0; colId < colCount; colId++)
            {
                if (colId + 1 == colCount)
                {
                    // 最后一列
                    insertSql1 += Columns[colId].ToString().Trim() + ")";
                    insertSql2 += "@" + Columns[colId].ToString().Trim() + ")";
                }
                else
                {
                    insertSql1 += Columns[colId].ToString().Trim() + ",";
                    insertSql2 += "@" + Columns[colId].ToString().Trim() + ",";
                }
            }

            string insertSql = insertSql1 + insertSql2;

            // 遍历数据表的所有数据行
            for (int rowId = 0; rowId < Table.Rows.Count; rowId++)
            {
                for (int colId = 0; colId < colCount; colId++)
                {
                    // 因为列不连续，所以在取得单元格时不能用行列编号，列需得用列的名称
                    var dataCol = (DataColumn)Columns[colId];
                    if (para[colId].DbType == DbType.Double && Table.Rows[rowId][dataCol.Caption].ToString().Trim() == string.Empty)
                    {
                        para[colId].Value = 0;
                    }
                    else
                    {
                        para[colId].Value = Table.Rows[rowId][dataCol.Caption].ToString().Trim();
                    }
                }

                try
                {
                    objCmd.CommandText = insertSql;
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }

            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 获取Excel文件数据表列表
        /// </summary>
        /// <param name="ExcelFileName">
        /// The Excel File Name.
        /// </param>
        public static ArrayList GetExcelTables(string ExcelFileName)
        {
            var tablesList = new ArrayList();
            if (File.Exists(ExcelFileName))
            {
                using (var conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + ExcelFileName))
                {
                    conn.Open();
                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                    // 获取数据表个数
                    if (dt != null)
                    {
                        int tablecount = dt.Rows.Count;
                        for (int i = 0; i < tablecount; i++)
                        {
                            string tablename = dt.Rows[i][2].ToString().Trim().TrimEnd('$');
                            if (tablesList.IndexOf(tablename) < 0)
                            {
                                tablesList.Add(tablename);
                            }
                        }
                    }
                }
            }

            return tablesList;
        }

        /// <summary>
        /// 将Excel文件导出至DataTable(第一行作为表头)
        /// </summary>
        /// <param name="ExcelFilePath">Excel文件路径</param>
        /// <param name="TableName">数据表名，如果数据表名错误，默认为第一个数据表名</param>
        public static DataTable InputFromExcel(string ExcelFilePath, string TableName)
        {
            if (!File.Exists(ExcelFilePath))
            {
                throw new Exception("Excel文件不存在！");
            }

            // 如果数据表名不存在，则数据表名为Excel文件的第一个数据表
            ArrayList tableList = GetExcelTables(ExcelFilePath);

            if (TableName.IndexOf(TableName, StringComparison.Ordinal) < 0)
            {
                TableName = tableList[0].ToString().Trim();
            }

            var table = new DataTable();
            var dbcon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0");
            var cmd = new OleDbCommand("select * from [" + TableName + "$]", dbcon);
            var adapter = new OleDbDataAdapter(cmd);

            try
            {
                if (dbcon.State == ConnectionState.Closed)
                {
                    dbcon.Open();
                }

                adapter.Fill(table);
            }
            finally
            {
                if (dbcon.State == ConnectionState.Open)
                {
                    dbcon.Close();
                }
            }

            return table;
        }

        /// <summary>
        /// 获取Excel文件指定数据表的数据列表
        /// </summary>
        /// <param name="ExcelFileName">Excel文件名</param>
        /// <param name="TableName">数据表名</param>
        public static ArrayList GetExcelTableColumns(string ExcelFileName, string TableName)
        {
            var colsList = new ArrayList();
            if (File.Exists(ExcelFileName))
            {
                using (var conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + ExcelFileName))
                {
                    conn.Open();
                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, TableName, null });

                    // 获取列个数
                    int colcount = dt.Rows.Count;
                    for (int i = 0; i < colcount; i++)
                    {
                        string colname = dt.Rows[i]["Column_Name"].ToString().Trim();
                        colsList.Add(colname);
                    }
                }
            }

            return colsList;
        }
    }
}