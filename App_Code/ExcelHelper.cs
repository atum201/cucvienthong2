using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Collections;
using ExcelLibrary.SpreadSheet;

namespace Cuc_QLCL
{
    /// <summary>
    ///
    /// </summary>
    ///  <Modifield>
    /// Author                         Date             Action
    ///  ?                               ?              Created
    /// Nguyễn Trung Tuyến           12/05/2009         repared
    /// </Modifield>
    public class ExcelHelper
    {
        //Row limits older excel verion per sheet, the row limit for excel 2003 is 65536
        const int rowLimit = 65000;

        private static string getWorkbookTemplate()
        {
            StringBuilder sb = new StringBuilder(818);
            sb.AppendFormat(@"<?xml version=""1.0""?>{0}", Environment.NewLine);
            sb.AppendFormat(@"<?mso-application progid=""Excel.Sheet""?>{0}", Environment.NewLine);
            sb.AppendFormat(@"<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
            sb.AppendFormat(@" xmlns:o=""urn:schemas-microsoft-com:office:office""{0}", Environment.NewLine);
            sb.AppendFormat(@" xmlns:x=""urn:schemas-microsoft-com:office:excel""{0}", Environment.NewLine);
            sb.AppendFormat(@" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
            sb.AppendFormat(@" xmlns:html=""http://www.w3.org/TR/REC-html40"">{0}", Environment.NewLine);
            sb.AppendFormat(@" <Styles>{0}", Environment.NewLine);
            sb.AppendFormat(@"  <Style ss:ID=""Default"" ss:Name=""Normal"">{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Alignment ss:Vertical=""Bottom""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Borders/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Font ss:FontName=""Times New Roman"" x:Family=""Roman"" ss:Size=""11"" ss:Color=""#000000""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Interior/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <NumberFormat/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Protection/>{0}", Environment.NewLine);
            sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
            sb.AppendFormat(@"  <Style ss:ID=""s62"">{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Font ss:FontName=""Times New Roman"" x:Family=""Roman"" ss:Size=""11"" ss:Color=""#000000""{0}", Environment.NewLine);
            sb.AppendFormat(@"      ss:Bold=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
            sb.AppendFormat(@"  <Style ss:ID=""s63"">{0}", Environment.NewLine);
            sb.AppendFormat(@"      <NumberFormat ss:Format=""Short Date""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
            ///thiết lập định dạng cho tiêu đề của trang
            sb.AppendFormat(@"  <Style ss:ID=""s26"">{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Font x:Family=""Roman"" ss:Size=""16""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
            ///thiết lập định dạng cho hàng tiêu đề cột
            sb.AppendFormat(@" <Style ss:ID=""s51"">{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Borders>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      </Borders>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Font x:Family=""Roman"" ss:Size=""11"" ss:Color=""#3366FF"" ss:Bold=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Interior ss:Color=""#33CCCC"" ss:Pattern=""Solid""/>{0}", Environment.NewLine);
            sb.AppendFormat(@" </Style>{0}", Environment.NewLine);
            ///thiết lập định dạng cho các hàng trong bảng 
            sb.AppendFormat(@" <Style ss:ID=""s52"">{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Alignment ss:Vertical=""Top"" ss:WrapText=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Borders>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"      </Borders>{0}", Environment.NewLine);
            sb.AppendFormat(@" </Style>{0}", Environment.NewLine);
            sb.AppendFormat(@" </Styles>{0}", Environment.NewLine);
            sb.Append(@"{0}\r\n</Workbook>");
            return sb.ToString();
        }
        private static string replaceXmlChar(string input)
        {
            input = input.Replace("&", "&amp");
            input = input.Replace("<", "&lt;");
            input = input.Replace(">", "&gt;");
            input = input.Replace("\"", "&quot;");
            input = input.Replace("'", "&apos;");
            return input;
        }
        private static string getCell(Type type, object cellData)
        {
            Object data = (cellData is DBNull) ? "" : cellData;
            if (type.Name.Contains("Int") || type.Name.Contains("Double") || type.Name.Contains("Decimal")) return string.Format("<Cell  ss:StyleID=\"s52\"><Data ss:Type=\"Number\">{0}</Data></Cell>", data);
            if (type.Name.Contains("Date") && data.ToString() != string.Empty)
            {
                return string.Format("<Cell ss:StyleID=\"s52\"><Data ss:Type=\"DateTime\">{0}</Data></Cell>", Convert.ToDateTime(data).ToString("yyyy-MM-dd"));
            }
            return string.Format("<Cell  ss:StyleID=\"s52\"><Data ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(data.ToString()));
        }
        private static string getWorksheets(DataSet source)
        {
            StringWriter sw = new StringWriter();
            if (source == null || source.Tables.Count == 0)
            {
                sw.Write("<Worksheet ss:Name=\"Sheet1\">\r\n<Table>\r\n<Row><Cell><Data ss:Type=\"String\"></Data></Cell></Row>\r\n</Table>\r\n</Worksheet>");
                return sw.ToString();
            }
            foreach (System.Data.DataTable dt in source.Tables)
            {
                if (dt.Rows.Count == 0)
                    sw.Write("<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) + "\">\r\n<Table>\r\n<Row><Cell  ss:StyleID=\"s62\"><Data ss:Type=\"String\"></Data></Cell></Row>\r\n</Table>\r\n</Worksheet>");
                else
                {
                    //write each row data                
                    int sheetCount = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((i % rowLimit) == 0)
                        {
                            //add close tags for previous sheet of the same data table
                            if ((i / rowLimit) > sheetCount)
                            {
                                sw.Write("\r\n</Table>\r\n</Worksheet>");
                                sheetCount = (i / rowLimit);
                            }
                            sw.Write("\r\n<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) +
                                     (((i / rowLimit) == 0) ? "" : Convert.ToString(i / rowLimit)) + "\">\r\n<Table>");
                            //write column name row
                            sw.Write("\r\n<Row>");
                            foreach (DataColumn dc in dt.Columns)
                                sw.Write(string.Format("<Cell ss:StyleID=\"s62\"><Data ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(dc.ColumnName)));
                            sw.Write("</Row>");
                        }
                        sw.Write("\r\n<Row>");
                        foreach (DataColumn dc in dt.Columns)
                            sw.Write(getCell(dc.DataType, dt.Rows[i][dc.ColumnName]));
                        sw.Write("</Row>");
                    }
                    sw.Write("\r\n</Table>\r\n</Worksheet>");
                }
            }

            return sw.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ExceptColunms"> Danh sách các cột("cột1,côt2,...") không muốn in ra file</param>
        /// <param name="TitleOfPage"></param>
        /// <returns></returns>
        ///  <Modifield>
        /// Author                         Date             Action
        /// Nguyễn Trung Tuyến          12/05/2009          Overried
        /// </Modifield>
        private static string getWorksheets(DataSet source, String ExceptColunms, String TitleOfPage)
        {
            ExceptColunms = ExceptColunms.ToUpper();
            string[] arrColumns = ExceptColunms.Split(',');
            StringWriter sw = new StringWriter();
            if (source == null || source.Tables.Count == 0)
            {
                sw.Write("<Worksheet ss:Name=\"Sheet1\">\r\n<Table>\r\n<Row><Cell><Data ss:Type=\"String\"></Data></Cell></Row>\r\n</Table>\r\n</Worksheet>");
                return sw.ToString();
            }
            foreach (System.Data.DataTable dt in source.Tables)
                if (dt.Rows.Count == 0)
                    sw.Write("<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) + "\">\r\n<Table>\r\n<Row><Cell  ss:StyleID=\"s62\"><Data ss:Type=\"String\"></Data></Cell></Row>\r\n</Table>\r\n</Worksheet>");
                else
                {
                    //write each row data                
                    int sheetCount = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((i % rowLimit) == 0)
                        {
                            //add close tags for previous sheet of the same data table
                            if ((i / rowLimit) > sheetCount)
                            {
                                sw.Write("\r\n</Table>\r\n</Worksheet>");
                                sheetCount = (i / rowLimit);
                            }
                            sw.Write("\r\n<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) +
                                     (((i / rowLimit) == 0) ? "" : Convert.ToString(i / rowLimit)) + "\">\r\n<Table>");
                            //write the title of page
                            String colcount = Convert.ToString(dt.Columns.Count - arrColumns.Length);
                            sw.Write("\r\n<Row ss:Height=\"20.25\">");
                            sw.Write("<Cell ss:MergeAcross=\"" + colcount + "\" ss:StyleID=\"s26\"><Data ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(TitleOfPage));
                            sw.Write("</Row>");
                            //write column name row
                            sw.Write("\r\n<Row ss:Height=\"15\">");
                            //insert row content for each column
                            foreach (DataColumn dc in dt.Columns)
                                if (Array.IndexOf(arrColumns, dc.ColumnName.ToUpper()) < 0)
                                    sw.Write(string.Format("<Cell ss:StyleID=\"s51\"><Data ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(dc.Caption).ToUpper()));
                            sw.Write("</Row>");
                        }
                        sw.Write("\r\n<Row>");

                        foreach (DataColumn dc in dt.Columns)
                            if (Array.IndexOf(arrColumns, dc.ColumnName.ToUpper()) < 0)
                                sw.Write(getCell(dc.DataType, dt.Rows[i][dc.ColumnName]));
                        sw.Write("</Row>");
                    }
                    sw.Write("\r\n</Table>\r\n</Worksheet>");
                }


            return sw.ToString();
        }
        public static string GetExcelXml(System.Data.DataTable dtInput, string filename)
        {
            String excelTemplate = getWorkbookTemplate();
            DataSet ds = new DataSet();
            ds.Tables.Add(dtInput.Copy());
            String worksheets = getWorksheets(ds);
            String excelXml = string.Format(excelTemplate, worksheets);
            return excelXml;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ExceptColunms"> Danh sách các cột("cột1,côt2,...") không muốn in ra file</param>
        /// <param name="TitleOfPage"></param>
        /// <returns></returns>
        ///  <Modifield>
        /// Author                         Date             Action
        /// Nguyễn Trung Tuyến          12/05/2009          Overried
        /// </Modifield>
        public static string GetExcelXml(System.Data.DataTable dtInput, String ExceptColumns, String TitleOfPage, string filename)
        {
            String excelTemplate = getWorkbookTemplate();
            DataSet ds = new DataSet();
            ds.Tables.Add(dtInput.Copy());
            String worksheets = getWorksheets(ds, ExceptColumns, TitleOfPage);
            String excelXml = string.Format(excelTemplate, worksheets);
            return excelXml;
        }
        public static string GetExcelXml(DataSet dsInput, string filename)
        {
            String excelTemplate = getWorkbookTemplate();
            String worksheets = getWorksheets(dsInput);
            String excelXml = string.Format(excelTemplate, worksheets);
            return excelXml;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ExceptColunms"> Danh sách các cột("cột1,côt2,...") không muốn in ra file</param>
        /// <param name="TitleOfPage"></param>
        /// <returns></returns>
        ///  <Modifield>
        /// Author                         Date             Action
        /// Nguyễn Trung Tuyến          12/05/2009          Overried
        /// </Modifield>
        public static string GetExcelXml(DataSet dsInput, String ExceptColumns, String TitleOfPage, string filename)
        {
            String excelTemplate = getWorkbookTemplate();
            String worksheets = getWorksheets(dsInput, ExceptColumns, TitleOfPage);
            String excelXml = string.Format(excelTemplate, worksheets);
            return excelXml;
        }

        public static void ToExcel(DataSet dsInput, string filename, HttpResponse response)
        {
            String excelXml = GetExcelXml(dsInput, filename);
            response.Clear();
            response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            response.AppendHeader("Content-disposition", "attachment; filename=" + filename);
            response.Write(excelXml);
            response.Flush();
            response.End();
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ExceptColunms"> Danh sách các cột(cột1,côt2,...) không muốn in ra file</param>
        /// <param name="TitleOfPage">Tiêu đề của trang khi in ra file</param>
        /// <returns></returns>
        ///  <Modifield>
        /// Author                         Date             Action
        /// Nguyễn Trung Tuyến          12/05/2009          overried
        /// </Modifield>
        public static void ToExcel(DataSet dsInput, String ExceptColumns, String TitleOfPage, string filename, HttpResponse response)
        {
            //
            String excelXml = GetExcelXml(dsInput, ExceptColumns, TitleOfPage, filename);
            response.Clear();
            response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            response.AppendHeader("Content-disposition", "attachment; filename=" + filename);
            response.Write(excelXml);
            response.Flush();
            response.End();
        }
        public static void ToExcel(System.Data.DataTable dtInput, string filename, HttpResponse response)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dtInput.Copy());
            ToExcel(ds, filename, response);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ExceptColunms"> Danh sách các cột(cột1,côt2,...) không muốn in ra file</param>
        /// <param name="TitleOfPage">Tiêu đề của bảng khi in ra file</param>
        /// <returns></returns>
        ///  <Modifield>
        /// Author                         Date             Action
        /// Nguyễn Trung Tuyến          12/05/2009          Overried
        /// </Modifield>
        public static void ToExcel(System.Data.DataTable dtInput, String ExceptColumn, String TitleOfPage, string filename, HttpResponse response)
        {
            //
            DataSet ds = new DataSet();
            ds.Tables.Add(dtInput.Copy());
            ToExcel(ds, ExceptColumn, TitleOfPage, filename, response);
        }
        #region Xử lý xuất dữ liệu từ DataTable to Excel
        //Tổng số row từ DataTable
        //private static int m_dataRowCount;
        ///// <summary>
        ///// Hàm xử lý xuất ra file excel
        ///// </summary>
        ///// <param name="db"></param>
        ///// <param name="fileName"></param>
        ///// <param name="pb"></param>
        ///// <returns></returns>
        ///// <Modified>
        /////Author           Date            Comment 
        /////Hoàng Văn Út      30/12/2010         Tạo mới
        /////</Modified>
        //public static void ExportDataTableToExcel(System.Data.DataTable dataTable, Microsoft.Office.Interop.Excel.Worksheet sheetToAddTo)
        //{
        //    //create the object to store the column names
        //    object[,] columnNames;

        //    columnNames = new object[1, dataTable.Columns.Count];

        //    // Code to update progress bar
        //    // fire the event to modify the progress bar
        //    //if (OnUpdateBar != null)
        //    //    OnUpdateBar(10);

        //    //add the columns names from the datatable
        //    for (int index = 0; index < dataTable.Columns.Count; index++)
        //    {
        //        columnNames[0, index] = dataTable.Columns[index].Caption;
        //    }

        //    //get a range object that the columns will be added to
        //    Microsoft.Office.Interop.Excel.Range columnsNamesRange = sheetToAddTo.get_Range(sheetToAddTo.Cells[1, 1], sheetToAddTo.Cells[1, dataTable.Columns.Count]);

        //    //if (OnUpdateBar != null)
        //    //    OnUpdateBar(20);

        //    //a simple assignement allows the data to be transferred quickly
        //    columnsNamesRange.Value2 = columnNames;
        //    columnsNamesRange.EntireRow.Font.Bold = true;

        //    //release the columsn range object now it is finished with
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(columnsNamesRange);
        //    columnsNamesRange = null;

        //    //create the object to store the dataTable data
        //    object[,] rowData;

        //    rowData = new object[dataTable.Rows.Count, dataTable.Columns.Count];

        //    m_dataRowCount = dataTable.Rows.Count;

        //    //insert the data into the object[,]
        //    for (int iRow = 0; iRow < dataTable.Rows.Count; iRow++)
        //    {
        //        for (int iCol = 0; iCol < dataTable.Columns.Count; iCol++)
        //        {
        //            rowData[iRow, iCol] = dataTable.Rows[iRow][iCol];
        //        }
        //    }

        //    //if (OnUpdateBar != null)
        //    //    OnUpdateBar(40);
        //    //get a range to add the table data into 
        //    //it is one row down to avoid the previously added columns
        //    Microsoft.Office.Interop.Excel.Range dataCells = sheetToAddTo.get_Range(sheetToAddTo.Cells[2, 1],
        //    sheetToAddTo.Cells[dataTable.Rows.Count + 1, dataTable.Columns.Count]);

        //    // for formating the columns before polulating the data
        //    short colIndex = 1;
        //    string colType = string.Empty;
        //    foreach (System.Data.DataColumn dcol in dataTable.Columns)
        //    {
        //        colType = string.Empty;

        //        if (dcol.DataType.Equals(typeof(string))) // if data type is string 
        //            colType = "@";
        //        else if (dcol.DataType.Equals(typeof(DateTime))) // if data type is datetime
        //            colType = "dd-MM-yyyy";

        //        if (!string.IsNullOrEmpty(colType))
        //            FormatColumn(dataCells, colIndex, colType);

        //        ++colIndex;
        //    }

        //    //if (OnUpdateBar != null)
        //    //    OnUpdateBar(50);

        //    //assign data to worksheet
        //    dataCells.Value2 = rowData;

        //    //if (OnUpdateBar != null)
        //    //    OnUpdateBar(80);

        //    //release range
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(dataCells);

        //    dataCells = null;

        //    //if (OnUpdateBar != null)
        //    //    OnUpdateBar(100);
        //}

        //// Method to format the cell
        ///// <summary>
        ///// Hàm xử lý xuất ra file excel
        ///// </summary>
        ///// <param name="db"></param>
        ///// <param name="fileName"></param>
        ///// <param name="pb"></param>
        ///// <returns></returns>
        ///// <Modified>
        /////Author           Date            Comment 
        /////Hoàng Văn Út      30/12/2010         Tạo mới
        /////</Modified>
        //public static void FormatColumn(Microsoft.Office.Interop.Excel.Range excelRange, int col, string format)
        //{
        //    ((Microsoft.Office.Interop.Excel.Range)excelRange.Cells[1, col]).EntireColumn.NumberFormat = format;
        //}
        ////Method which actual initiate export of data
        ///// <summary>
        ///// Hàm xử lý xuất ra file excel
        ///// </summary>
        ///// <param name="db"></param>
        ///// <param name="fileName"></param>
        ///// <param name="pb"></param>
        ///// <returns></returns>
        ///// <Modified>
        /////Author           Date            Comment 
        /////Hoàng Văn Út      30/12/2010         Tạo mới
        /////</Modified>
        //public static bool ExportData(System.Data.DataTable dt, string fileName, PageBase pb)
        //{
        //    try
        //    {
        //        if (dt.Rows.Count >= 65000)
        //        {
        //            return false;
        //        }
        //        string filePath = pb.Server.MapPath("../FileDownload/");
        //        filePath += Guid.NewGuid().ToString() + ".xls";
        //        // Fill the data table            
        //        System.Data.DataTable m_dtWarrantyDetails = dt.Copy();

        //        //Create excel sheet object
        //        Microsoft.Office.Interop.Excel.Application xelApp = null;
        //        Microsoft.Office.Interop.Excel.Workbook xelBook = null;
        //        Microsoft.Office.Interop.Excel.Worksheet xelSheet = null;
        //        Object emptyItem = System.Reflection.Missing.Value;

        //        xelApp = new Microsoft.Office.Interop.Excel.Application();
        //        xelBook = xelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
        //        xelSheet = (Microsoft.Office.Interop.Excel.Worksheet)xelBook.Worksheets[1];

        //        //To suppress the save as alert
        //        xelApp.DisplayAlerts = false;
        //        xelApp.AlertBeforeOverwriting = false;

        //        xelBook.SaveAs(filePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, emptyItem, emptyItem, emptyItem, emptyItem, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, emptyItem, emptyItem, emptyItem, emptyItem, emptyItem);

        //        //Call above export method m_dtWarrantyDetails - this is datatable
        //        ExportDataTableToExcel(m_dtWarrantyDetails, xelSheet);

        //        xelSheet.Cells.EntireColumn.AutoFit();

        //        //Save excel file.
        //        xelSheet.SaveAs(filePath, emptyItem, emptyItem, emptyItem, emptyItem, emptyItem, emptyItem, emptyItem, emptyItem, emptyItem);
        //        FileInfo file = new FileInfo(filePath);
        //        if (file.Exists)
        //        {
        //            string attachment = "attachment; filename=" + fileName;
        //            pb.Response.ClearContent();
        //            pb.Response.AddHeader("content-disposition", attachment);
        //            pb.Response.ContentType = "application/vnd.ms-excel";
        //            pb.Response.WriteFile(filePath);
        //            pb.Response.Flush();
        //            file.Delete();
        //            pb.Response.End();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        pb.ClientScript.RegisterClientScriptBlock(pb.GetType(), "Lỗi", "<script>alert('" + ex.Message + "');</script>");
        //        return false;
        //    }
        //    finally
        //    {
        //        //Release the objects
        //        //xelApp.Quit();
        //        //System.Runtime.InteropServices.Marshal.ReleaseComObject(xelSheet);
        //        //System.Runtime.InteropServices.Marshal.ReleaseComObject(xelBook);
        //        //System.Runtime.InteropServices.Marshal.ReleaseComObject(xelApp);

        //        //xelSheet = null;
        //        //xelBook = null;
        //        //xelApp = null;
        //        //System.GC.Collect();
        //    }
        //    return true;
        //}
        #endregion
    }
}
