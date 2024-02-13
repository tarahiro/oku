using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEditor;

namespace Editor.XmlImporter
{
    /// <summary>
    /// Xmlをインポートするための共通処理を実装したクラスです。
    /// </summary>
    public static class XmlImporter
    {
		class Workbook : IWorkbook
		{
			public Workbook(string path)
			{
				XNamespace o = "urn:schemas-microsoft-com:office:office";
				XNamespace x = "urn:schemas-microsoft-com:office:excel";
				XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

				using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					using (var stream = new StreamReader(fileStream))
					{
						var worksheets = new List<IWorksheet>();

						var rootElement = XElement.Load(stream);
						foreach (var worksheetElement in rootElement.Elements(ss + "Worksheet"))
						{
							worksheets.Add(new Worksheet(worksheetElement));
						}

						Path = path;
						Worksheets = worksheets;
					}
				}
			}

			public string Path { get; }
			public IEnumerable<IWorksheet> Worksheets { get; }
		}

		class Worksheet : IWorksheet
		{
			readonly string[,] m_Cells;

			public Worksheet(XElement worksheetElement)
			{
				XNamespace o = "urn:schemas-microsoft-com:office:office";
				XNamespace x = "urn:schemas-microsoft-com:office:excel";
				XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

				var tableElement = worksheetElement.Element(ss + "Table");
				var width = int.Parse(tableElement.Attribute(ss + "ExpandedColumnCount").Value);
				var height = int.Parse(tableElement.Attribute(ss + "ExpandedRowCount").Value);
				var cells = new string[height, width];

				var row = 0;
				foreach (var rowElement in tableElement.Elements(ss + "Row"))
				{
					var rowIndexAttribute = rowElement.Attribute(ss + "Index");
					if (rowIndexAttribute != null)
					{
						row = int.Parse(rowIndexAttribute.Value) - 1;
					}

					var column = 0;
					foreach (var cellElement in rowElement.Elements(ss + "Cell"))
					{
						var cellIndexAttribute = cellElement.Attribute(ss + "Index");
						if (cellIndexAttribute != null)
						{
							column = int.Parse(cellIndexAttribute.Value) - 1;
						}

						var dataElement = cellElement.Element(ss + "Data");
						if (dataElement != null)
						{
							cells[row, column] = dataElement.Value;
						}

						++column;
					}

					++row;
				}

				Name = worksheetElement.Attribute(ss + "Name").Value;
				m_Cells = cells;
			}

			public string Name { get; }
			public int Width { get { return m_Cells.GetLength(1); } }
			public int Height { get { return m_Cells.GetLength(0); } }
			public TableCell this[int row, int column] { get { return new TableCell(m_Cells[row, column]); } }
		}

		// ワークブックをインポート（pathはUnityプロジェクトフォルダからの相対パス）
		public static IWorkbook ImportWorkbook(string path)
		{
			return new Workbook(path);
		}

		//まずはReportData用に書き、後日一般化
		// リストをアセットにエクスポート
		public static void ExportList(string resourceDataPath, List<ReportMasterDataList.ReportMasterData> records)
		{
			string path = "Assets/Resources/" + resourceDataPath + ".asset";
			var asset = AssetDatabase.LoadAssetAtPath<ReportMasterDataList>(path);
			// 存在しなかったら作成
			if (asset == null)
			{
				var directoryPath = Path.GetDirectoryName(path);

				// フォルダがなければ生成
				if (!Directory.Exists(directoryPath))
				{
					Directory.CreateDirectory(directoryPath);
				}

				asset = ScriptableObject.CreateInstance<ReportMasterDataList>();
				AssetDatabase.CreateAsset(asset, path);
			}

			asset.SetList = records;
			asset.hideFlags = HideFlags.NotEditable;

			EditorUtility.SetDirty(asset);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

    }
}
