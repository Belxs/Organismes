using Microsoft.VisualBasic.FileIO;
using OfficeOpenXml;
using System;
using System.IO;
using System.Threading;


namespace FileToExcel
{

    class Head
    {
        public static string path, pathresult, pathdeep, nameresult, namedeep;
        public static FileInfo file,deepfile;
        public static string analise,deepanalise;
        public static string[] allfiles;
        static void Main(string[] args)
        {
            string nam = "result[" + DateTime.Now.TimeOfDay.ToString() + "]";
            string newnam = "";
            for (int i = 0; i < nam.Length; i++)
            {
                char s = nam[i];
                switch (nam[i])
                {
                    case '.':
                        s = '_';
                        break;
                    case ':':
                        s = '-';
                        break;
                    case ' ':
                        s = '_';
                        break;
                }
                newnam += s;
            }

            path = "organizmes_Data\\StreamingAssets\\";
            file = new FileInfo(path+"ExcelResults" + "\\" + newnam+".xlsx");
            deepfile = new FileInfo(path+"ExcelResults" + "\\" +"deep"+ newnam + ".xlsx");
            allfiles = Directory.GetFiles(path+"Analises");
            bool r=false, d=false;
            string buffer;
            buffer = GetAnalise();
            if (buffer != "")
            {
                analise = File.ReadAllText(buffer);
                nameresult = FileSystem.GetName(buffer);
                pathresult = path + "Analises" +"\\"+ nameresult;
                
                r = true;
            }
            buffer = GetDeepAnalise();
            if (buffer != "")
            {
                deepanalise = File.ReadAllText(buffer);
                namedeep = FileSystem.GetName(buffer);
                pathdeep = path + "Analises" + "\\" + namedeep;
                
                d = true;
            }
           // Console.WriteLine(path);
            if (r)
            {
                OutPutResult();
                Console.WriteLine("Good result!");
                FileSystem.RenameFile(pathresult, "Good" + nameresult);
            }
            if (d)
            {
                OutPutDeepResult();
                Console.WriteLine("Good deepresult!");
                
                FileSystem.RenameFile(pathdeep, "Good" + namedeep);
            }
            Console.WriteLine("End");
            Thread.Sleep(1000);
        }
        public static string GetAnalise()
        {
            for (int i = 0; i < allfiles.Length; i++)
            {
               
                    if (FileSystem.GetName(allfiles[i])[0] == 'r')

                        return allfiles[i];
                
            }
            
            return "";
        }
        public static string GetDeepAnalise()
        {
            for (int i = 0; i < allfiles.Length; i++)
            {
               
                    if (FileSystem.GetName(allfiles[i])[0] == 'd')
                    
                        return allfiles[i];
                    
                
            }
            return "";
        }
        public static void OutPutDeepResult()
        {
            ExcelPackage ExcelPkg = new ExcelPackage();
            ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

            // ExcelRange Rng = wsSheet1.Cells;

            //Indirectly access ExcelTableCollection class
            //  ExcelTable table = wsSheet1.Tables.Add(Rng, "none");
            //table.Name = "tblSalesman";
            wsSheet1.SetValue(1, 2, "XFP");
            wsSheet1.SetValue(1, 3, "XQP");
            wsSheet1.SetValue(1, 4, "XWP");
            wsSheet1.SetValue(1, 5, "XEP");
            wsSheet1.SetValue(1, 6, "XFA");
            wsSheet1.SetValue(1, 7, "XQA");
            wsSheet1.SetValue(1, 8, "XWA");
            wsSheet1.SetValue(1, 9, "XEA");
            wsSheet1.SetValue(1, 10, "CFA");
            wsSheet1.SetValue(1, 11, "CQA");
            wsSheet1.SetValue(1, 12, "CWA");
            wsSheet1.SetValue(1, 13, "CEA");
            wsSheet1.SetValue(1, 14, "HG");
            wsSheet1.SetValue(1, 15, "QG");
            wsSheet1.SetValue(1, 16, "LG");
            wsSheet1.SetValue(1, 17, "VFA");
            wsSheet1.SetValue(1, 18, "VQA");
            wsSheet1.SetValue(1, 19, "VWA");
            wsSheet1.SetValue(1, 20, "VEA");

            int row = 2, colum = 1; bool read = false; string value = "";
            //Directly access ExcelTableCollection class
            for (int i = 0; i < deepanalise.Length; i++)
            {

                if ((deepanalise[i] == '|') || (deepanalise[i] == '\n')) read = false;

                if (read)
                {
                    value += deepanalise[i];
                }
                else
                {

                    if (value != "")
                    {
                        float num = float.Parse(value)*10;
                        if (Math.Abs(num) > 9000) num = 0;
                        wsSheet1.SetValue(row, colum,num);
                        colum++;
                        value = "";
                        read = false;

                    }





                }

                if (deepanalise[i] == ':') read = true;
                if (deepanalise[i] == '\n')
                {
                    row++;
                    colum = 1;


                }

            }

            //table.ShowHeader = false;
            //table.ShowFilter = true;
            //table.ShowTotal = true;
            
            ExcelPkg.SaveAs(deepfile);


        }
        public static void OutPutResult()
        {
            ExcelPackage ExcelPkg = new ExcelPackage();
            ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

            // ExcelRange Rng = wsSheet1.Cells;

            //Indirectly access ExcelTableCollection class
            //  ExcelTable table = wsSheet1.Tables.Add(Rng, "none");
            //table.Name = "tblSalesman";
            wsSheet1.SetValue(1, 1, "Время");
            wsSheet1.SetValue(1, 2, "Всего организмов");
            wsSheet1.SetValue(1, 3, "Еда");
            wsSheet1.SetValue(1, 4, "Вода");
            wsSheet1.SetValue(1, 5, "Самцы");
            wsSheet1.SetValue(1, 6, "Самки");
            wsSheet1.SetValue(1, 7, "Гермафродиты");
            wsSheet1.SetValue(1, 8, "Бесполые");
            wsSheet1.SetValue(1, 9, "Температура");

            int row = 2, colum = 1; bool read = false; string value = "";
            //Directly access ExcelTableCollection class
            for (int i = 0; i < analise.Length; i++)
            {
               
                if ((analise[i] == '|') || (analise[i] == '\n')) read = false;

                if (read)
                {
                    value += analise[i];
                }
                else
                {

                    if (value != "")
                    {
                        wsSheet1.SetValue(row, colum,float.Parse(value));
                        colum++;
                        value = "";
                        read = false;
                        
                    }
                    
                    
                      

                    
                }
               
                if (analise[i] == ':') read = true;
                if (analise[i] == '\n')
                {
                    row++;
                    colum = 1;


                }

            }

            //table.ShowHeader = false;
            //table.ShowFilter = true;
            //table.ShowTotal = true;
            ExcelPkg.SaveAs(file);


        }
    }
}
