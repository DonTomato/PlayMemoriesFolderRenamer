using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlayMemoriesForderRenamer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting...");

			var currentPath = AppDomain.CurrentDomain.BaseDirectory;
			DirectoryInfo currentDir = new DirectoryInfo(currentPath);
			
			var dirs = currentDir.GetDirectories();

			try
			{
				foreach (var dir in dirs)
				{
					if (!Regex.IsMatch(dir.Name, @"\d+[-.]\d+[-.]\d\d\d\d.*"))
						continue;

					var m = Regex.Match(dir.Name, @"(\d+)[-.](\d+)[-.](\d\d\d\d)(.*)");

                    string year = m.Groups[3].Value;
                    string month = m.Groups[1].Value;
                    string day = m.Groups[2].Value;
                    string tail = m.Groups[4].Value;

                    if (month.Length == 1)
                        month = "0" + month;

                    if (day.Length == 1)
                        day = "0" + day;


                    var newPath = string.Format("{0}.{1}.{2}{3}", year, month, day, tail);

					var newDir = new DirectoryInfo(Path.Combine(dir.Parent.FullName, newPath));

					dir.MoveTo(newDir.FullName);

					Console.WriteLine(String.Format("Directory: {0};\t\tNew Directory: {1}", dir.Name, newPath));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine();
				Console.Write(ex.StackTrace);
			}

			Console.WriteLine("Press any key...");
			Console.ReadKey();
		}
	}
}
