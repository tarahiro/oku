using Editor.XmlImporter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReportImporter
{
    //--------------------------------------------------------------------
    // ì«Ç›çûÇ›
    //--------------------------------------------------------------------

    [MenuItem("Assets/Tables/Import Report", false, 10)]
    static void ImportMenuItem()
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        Import();

        stopwatch.Stop();
    }

    public static void Import()
    {
        var topicData = LoadReportData("ImportData/Report/Report.xml");

        XmlImporter.ExportList("Data/Report",topicData);
    }

    static List<ReportMasterDataList.ReportMasterData> LoadReportData(string path)
    {
        var book = XmlImporter.ImportWorkbook(path);
        var sheet = book.TryGetWorksheet("Report");

        List<ReportMasterDataList.ReportMasterData> reportMasterDataList = new List<ReportMasterDataList.ReportMasterData>();

        for (int row = 0; row < sheet.Height; ++row)
        {
            if (int.TryParse(sheet[row, 0].String, out int localIndex))
            {
                reportMasterDataList.Add(LoadReport(sheet, row));
            }
        }

        return reportMasterDataList;
    }

    //è´óàìIÇ…ï ÉNÉâÉXÇ…âfÇµÇΩÇ¢
    static ReportMasterDataList.ReportMasterData LoadReport(IWorksheet sheet, int row)
    {
        return new ReportMasterDataList.ReportMasterData(
                sheet[row, 1].String,
                sheet[row, 2].String,
                sheet[row, 3].String,
                int.Parse(sheet[row, 4].String),
                int.Parse(sheet[row, 5].String)
            );
    }
}
