using System.Collections.Generic;

namespace Editor.XmlImporter
{
	public interface IWorkbook
	{
		string Path { get; }

		IEnumerable<IWorksheet> Worksheets { get; }
	}
}
