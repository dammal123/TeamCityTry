using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProfilDrogi;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;

namespace PrzekrojDrogi
{
    public class PrzekrojConsole
    {
        
        public static void Main(string[] args)
        {
            //size of picture
            int width = 0;
            int height = 0;

            //width of borders
            float scalePen = 0.0f;
            float scaleFont = 0.0f;

            // paths of files to save pictures
            string pathDirectory = Directory.GetCurrentDirectory();
            
            //instructions
            Console.WriteLine("\nInstruction:\n Arguments possible to set:\n 1. *.csv file to load data from.\n2. Width of the image.\n3. Height of the image.\n4. Border width of recantangles.\n5. Size of the font used.\nDefault values: (three predefined lists of elements), 2000, 700, 2.0, 16.0\n");
            Console.ReadLine();
            // Using default values depending on amount of arguments from input
            if (args.Count() > 0) {
                //loading list of elements from csv file
                List<Element> listCsvElem = new List<Element>();
                try
                {
                    using (StreamReader strRead = new StreamReader(args[0]))
                    {
                        int lineCounter = 0;
                        while(strRead.Peek() >= 0){
                            lineCounter++;
                            string[] strLineVal = strRead.ReadLine().Split(',');
                            if (strLineVal.Count() == 4 )
                            {
                                try
                                {
                                    listCsvElem.Add(new Element(strLineVal[0], strLineVal[1], float.Parse(strLineVal[2], CultureInfo.InvariantCulture), Convert.ToInt32(strLineVal[3])));
                                }
                                catch (Exception) 
                                {
                                    Console.WriteLine("Invalid data format in line: " + lineCounter);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Line: "+lineCounter+" has invalid data.");
                            }
                        }
                    }
                if (args.Count() > 1)
                {
                    width = Convert.ToInt32(args[1]);
                    if (args.Count() > 2)
                    {
                        height = Convert.ToInt32(args[2]);
                        if (args.Count() > 3)
                        {
                            scalePen = float.Parse(args[3], CultureInfo.InvariantCulture);
                            if (args.Count() > 4)
                            {
                                scaleFont = float.Parse(args[4], CultureInfo.InvariantCulture);
                            }
                            else {
                                scaleFont = 16.0f;
                            }
                        }
                        else {
                            scalePen = 2.0f;
                            scaleFont = 16.0f;
                        }
                    }
                    else {
                        height = 700;
                        scalePen = 2.0f;
                        scaleFont = 16.0f;
                    }
                }
                else {
                    width = 2000;
                    height = 700;
                    scalePen = 2.0f;
                    scaleFont = 16.0f;                
                }

                //path to save image to

                //changing parameters for 2nd and 3rd image
                string filecsv = pathDirectory + "\\" + DateTime.Now.ToString("d") + "_" + "ImageCsv1.png";
                Profil.Print(listCsvElem, width, height, scalePen, scaleFont).Save(filecsv, ImageFormat.Png);
                Profil.Print(listCsvElem, width + width / 2, height + height / 2, scalePen + 1, scaleFont + 5).Save(filecsv.Replace("ImageCsv1.png", "ImageCsv2.png"), ImageFormat.Png);
                Profil.Print(listCsvElem, width * 2, height * 2, scalePen + 2, scaleFont + 10).Save(filecsv.Replace("ImageCsv1.png", "ImageCsv3.png"), ImageFormat.Png);
                Console.WriteLine("Saved files to: "+filecsv);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("One or more of argument is invalid.");
                }
            }
            else{
                width = 2000;
                height = 700;
                scalePen = 2.0f;
                scaleFont = 21.0f;

                //list of elements to print
                //name, name for type of surface, width, type of filling
                List<Element> listElem1 = new List<Element>{

                new Element("Chodnik","KK",1.0f,2),
                new Element("Chodnik ssssssssss", "KK", 1.0f, 2),
                new Element("Jezdnia ddddddddd", "MB", 2.0f, 1),
                new Element("Jezdnia ddddddddd" , "MB", 2.0f, 0),
                new Element("Zatoka ffffffffff", "MZ", 1.0f,3)
                };

                string file1 = pathDirectory + "\\" + DateTime.Now.ToString("d") + "_" + "testImage1.png";
                Profil.Print(listElem1, width, height, scalePen, scaleFont).Save(file1.Replace("file" + ":", ""), ImageFormat.Png);


                List<Element> listElem2 = new List<Element>{

                new Element("Zatoka a","MB",2.7f,3),
                new Element("Chodnik sssqs", "KK", 2.5f, 2),
                new Element("Jezdnia dasdd", "MB", 5.0f, 1),
                new Element("Chodnik ddddgd" , "KK", 2.5f, 2)
                };

                string file2 = pathDirectory + "\\" + DateTime.Now.ToString("d") + "_" + "testImage2.png";
                Profil.Print(listElem2, width, height, scalePen, scaleFont).Save(file2, ImageFormat.Png);

                List<Element> listElem3 = new List<Element>{

                new Element("Zatoka asdsadsagfdda","KK",1.0f,3),
                new Element("Chodnik sssssdsfdsfsssss", "KK", 1.0f, 2),
                new Element("Jezdnia ddddddfsfdsdddd", "MB", 2.0f, 1),
                new Element("Jezdnia ddddgfdgfddddd" , "MB", 2.0f, 0),
                new Element("Zatoka ffffgfgfffffff", "MZ", 1.0f,3),
                new Element("Zatoka ffffgfgfffffff", "KK", 1.0f,4),
                new Element("Zatoka ffffgfgfffffff", "MZ", 1.0f,5)
                };

                string file3 = pathDirectory + "\\" + DateTime.Now.ToString("d") + "_" + "testImage3.png";
                Profil.Print(listElem3, width, height, scalePen, scaleFont).Save(file3, ImageFormat.Png);
            }
        }
    }
}
