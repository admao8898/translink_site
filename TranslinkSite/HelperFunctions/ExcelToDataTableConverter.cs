﻿using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace TranslinkSite.HelperFunctions
{
    public class ExcelToDataTableConverter
    {
        //using code from 
        // https://stackoverflow.com/questions/33465284/how-to-get-data-from-every-single-cell

        public static DataTable ImportSheet(string fileName)
        {            
            var datatable = new DataTable();
            //Gets file from desired directory not debug/bin folder
            string parentOfStartupPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../TestFiles/")); 
            string filePath = parentOfStartupPath + fileName;
            var workbook = new XLWorkbook(filePath);
            var xlWorksheet = workbook.Worksheet(1);
            var range = xlWorksheet.Range(xlWorksheet.FirstCellUsed(), xlWorksheet.LastCellUsed());

            var col = range.ColumnCount();
            var row = range.RowCount();

            //if a datatable already exists, clear the existing table 
            datatable.Clear();
            for (var i = 1; i <= col; i++)
            {
                var column = xlWorksheet.Cell(1, i);
                datatable.Columns.Add(column.Value.ToString());
            }

            var firstHeadRow = 0;
            foreach (var item in range.Rows())
            {
                if (firstHeadRow != 0)
                {
                    var array = new object[col];
                    for (var y = 1; y <= col; y++)
                    {
                        array[y - 1] = item.Cell(y).Value;
                    }

                    datatable.Rows.Add(array);
                }
                firstHeadRow++;
            }
            return datatable;
        }
    }
}
