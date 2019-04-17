using System;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.XSSF.Streaming;

namespace ExchangeTest
{
    /// <summary>
    /// Excel文件和DataTable之间转换帮助类
    /// </summary>
    public class ExcelHelper : IDisposable
    {

        /// <summary>
        /// 把DataTable的数据写入到指定的excel文件中
        /// </summary>
        /// <param name="path">目标文件excel的路径</param>
        /// <param name="dt">要写入的数据</param>
        /// <param name="sheetName">excel表中的sheet的名称，可以根据情况自己起</param>
        /// <param name="IsWriteColumnName">是否写入DataTable的列名称</param>
        /// <returns>返回写入的行数</returns>
        public static int DataTableToExcel(string path, DataTable dt, string sheetName, bool IsWriteColumnName)
        {

            //数据验证
            if (!File.Exists(path))
            {
                //excel文件的路径不存在
                throw new ArgumentException("excel文件的路径不存在或者excel文件没有创建好");
            }
            if (dt == null)
            {
                throw new ArgumentException("要写入的DataTable不能为空");
            }

            if (sheetName == null && sheetName.Length == 0)
            {
                throw new ArgumentException("excel中的sheet名称不能为空或者不能为空字符串");
            }



            ////根据Excel文件的后缀名创建对应的workbook
            IWorkbook workbook = null;
            //if (path.IndexOf(".xlsx") > 0)
            //{  //2007版本的excel
            //    workbook = new XSSFWorkbook();
            //}
            //else if (path.IndexOf(".xls") > 0) //2003版本的excel
            //{
            //    workbook = new HSSFWorkbook();
            //}
            //else
            //{
            //    return -1;    //都不匹配或者传入的文件根本就不是excel文件，直接返回
            //}

            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }


            //excel表的sheet名
            //ISheet sheet = workbook.CreateSheet(sheetName);
            ISheet sheet = workbook.GetSheet(sheetName);


            if (sheet == null) return -1;   //无法创建sheet，则直接返回

            //eg: java code; http://poi.apache.org/components/spreadsheet/quick-guide.html#Validation
            //XSSFName name = workbook.createName();
            //name.setNameName("data");
            //name.setRefersToFormula("'Data Validation'!$B$1:$F$1");
            //XSSFDataValidationHelper dvHelper = new XSSFDataValidationHelper(sheet);
            //XSSFDataValidationConstraint dvConstraint = (XSSFDataValidationConstraint)dvHelper.createFormulaListConstraint("data");
            //CellRangeAddressList addressList = new CellRangeAddressList(0, 0, 0, 0);
            //XSSFDataValidation validation = (XSSFDataValidation)dvHelper.createValidation(dvConstraint, addressList);
            //validation.setSuppressDropDownArrow(true);
            //validation.setShowErrorBox(true);
            //sheet.addValidationData(validation);


            IName namedRange = workbook.CreateName();
            namedRange.NameName = "list";
            namedRange.RefersToFormula = "'问题Main-分类'!$A$2:$A$8";
            XSSFDataValidationHelper dvHelper = new XSSFDataValidationHelper((XSSFSheet)sheet);
            XSSFDataValidationConstraint dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateFormulaListConstraint("list");
            CellRangeAddressList addressList = new CellRangeAddressList(1, 10000, 4, 4);
            XSSFDataValidation validation = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, addressList);
            validation.SuppressDropDownArrow = true;
            validation.ShowErrorBox = true;
            sheet.AddValidationData(validation);

            //IName namedRangeSub = workbook.CreateName();
            //namedRange.NameName = "sub";
            //namedRange.RefersToFormula = "INDIRECT(E4)";
            //XSSFDataValidationConstraint dvConstraintSub = (XSSFDataValidationConstraint)dvHelper.CreateFormulaListConstraint("sub");
            //CellRangeAddressList addressListSub = new CellRangeAddressList(1, 5, 5, 5);
            //XSSFDataValidation validationSub = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraintSub, addressListSub);
            //validation.SuppressDropDownArrow = true;
            //validation.ShowErrorBox = true;
            //sheet.AddValidationData(validationSub);



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow newRow = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    newRow.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                }

            }

            //FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            //用这种 上面方式 excel有可能打不开
            FileStream fs = File.Create(path);

            workbook.Write(fs);
            fs.Close();
            workbook.Close();
            return 1;
        }





        /// <summary>
        /// 从Excel中读入数据到DataTable中
        /// </summary>
        /// <param name="sourceFileNamePath">Excel文件的路径</param>
        /// <param name="sheetName">excel文件中工作表名称</param>
        /// <param name="IsHasColumnName">文件是否有列名</param>
        /// <returns>从Excel读取到数据的DataTable结果集</returns>
        public static DataTable ExcelToDataTable(string sourceFileNamePath, string sheetName, bool IsHasColumnName)
        {

            if (!File.Exists(sourceFileNamePath))
            {
                throw new ArgumentException("excel文件的路径不存在或者excel文件没有创建好");
            }

            if (sheetName == null || sheetName.Length == 0)
            {
                throw new ArgumentException("工作表sheet的名称不能为空");
            }

            //根据Excel文件的后缀名创建对应的workbook
            IWorkbook workbook = null;
            //打开文件
            FileStream fs = new FileStream(sourceFileNamePath, FileMode.Open, FileAccess.Read);
            if (sourceFileNamePath.IndexOf(".xlsx") > 0)
            {  //2007版本的excel
                workbook = new XSSFWorkbook(fs);
            }
            else if (sourceFileNamePath.IndexOf(".xls") > 0) //2003版本的excel
            {
                workbook = new HSSFWorkbook(fs);
            }
            else
            {
                return null;    //都不匹配或者传入的文件根本就不是excel文件，直接返回
            }




            //获取工作表sheet
            ISheet sheet = workbook.GetSheet(sheetName);
            //获取不到，直接返回
            if (sheet == null) return null;



            //开始读取的行号
            int StartReadRow = 0;
            DataTable targetTable = new DataTable();



            //表中有列名,则为DataTable添加列名
            if (IsHasColumnName)
            {
                //获取要读取的工作表的第一行
                IRow columnNameRow = sheet.GetRow(0);   //0代表第一行
                                                        //获取该行的列数(即该行的长度)
                int CellLength = columnNameRow.LastCellNum;

                //遍历读取
                for (int columnNameIndex = 0; columnNameIndex < CellLength; columnNameIndex++)
                {
                    //不为空，则读入
                    if (columnNameRow.GetCell(columnNameIndex) != null)
                    {
                        //获取该单元格的值
                        string cellValue = columnNameRow.GetCell(columnNameIndex).StringCellValue;
                        if (cellValue != null)
                        {
                            //为DataTable添加列名
                            targetTable.Columns.Add(new DataColumn(cellValue));
                        }
                    }
                }

                StartReadRow++;
            }



            ///开始读取sheet表中的数据

            //获取sheet文件中的行数
            int RowLength = sheet.LastRowNum;
            //遍历一行一行地读入
            for (int RowIndex = StartReadRow; RowIndex < RowLength; RowIndex++)
            {
                //获取sheet表中对应下标的一行数据
                IRow currentRow = sheet.GetRow(RowIndex);   //RowIndex代表第RowIndex+1行

                if (currentRow == null) continue;  //表示当前行没有数据，则继续
                                                   //获取第Row行中的列数，即Row行中的长度
                int currentColumnLength = currentRow.LastCellNum;

                //创建DataTable的数据行
                DataRow dataRow = targetTable.NewRow();
                //遍历读取数据
                for (int columnIndex = 0; columnIndex < currentColumnLength; columnIndex++)
                {
                    //没有数据的单元格默认为空
                    if (currentRow.GetCell(columnIndex) != null)
                    {
                        dataRow[columnIndex] = currentRow.GetCell(columnIndex);
                    }
                }
                //把DataTable的数据行添加到DataTable中
                targetTable.Rows.Add(dataRow);
            }


            //释放资源
            fs.Close();
            workbook.Close();

            return targetTable;
        }


        #region IDisposable 成员

        public void Dispose()
        {

        }

        #endregion
    }
}

