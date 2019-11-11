using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeTest
{
    public static class Extension
    {

        public static void AddConstraint(this ISheet sheet, IWorkbook workbook, string name, string mula, int columnIndex, bool isCustom = false)
        {

            IName namedRange = workbook.CreateName();
            namedRange.NameName = name;
            XSSFDataValidationHelper dvHelper = new XSSFDataValidationHelper((XSSFSheet)sheet);
            XSSFDataValidationConstraint dvConstraint;
            // XSSFDataValidation validation;
            if (!isCustom)
            {
                namedRange.RefersToFormula = mula;//公式
                dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateFormulaListConstraint(namedRange.NameName);

            }
            else
            {
                //自定义
                dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateExplicitListConstraint(name.Split(','));
            }
            CellRangeAddressList addressList = new CellRangeAddressList(1, 10000, columnIndex, columnIndex);
            XSSFDataValidation validation = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, addressList);

            validation.SuppressDropDownArrow = true;
            validation.ShowErrorBox = true;
            sheet.AddValidationData(validation);
        }
    }
}
