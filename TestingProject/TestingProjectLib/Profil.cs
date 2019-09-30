using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ProfilDrogi
{
    public static class Profil
    {
        public static Image Print(List<Element> elements, int imgWidth, int imgHeight, float scalePen, float scaleFont)
        {
            Bitmap bmp = new Bitmap(imgWidth,imgHeight);
            
            using (Graphics graphicsObj = Graphics.FromImage(bmp))
            {
                    graphicsObj.Clear(Color.FromKnownColor(KnownColor.Window));

                    //zmienna do liczenia sumy elementów szerokości
                    float sumOfElements = 0; 

                    // for remembering width of last element
                    float itemWidth = 0;

                    //value for height of the road
                    float heightRoad = (float)imgHeight / 2;

                    //variables for logic operations
                    int countElem = 0;
                    float scaleLongStr = 0;
                    
                    foreach (Element item in elements) 
                    {
                        sumOfElements += item.Width ;
                    }

                    //scalowanie
                    float scale = imgWidth / (sumOfElements + 3);
                    float sumOfWidths = sumOfElements * scale + scalePen*elements.Count();
                    
                    //int for starting rectangle place
                    float locationStart=(imgWidth-sumOfWidths)/2;
                    
                    
                    foreach (Element item in elements)
                    {

                        if (item.Name.Length > 18)
                        {
                            scaleLongStr = heightRoad + scale / 2;
                            break;
                        }
                        else
                        {
                            scaleLongStr = heightRoad + scale / 3;
                        }

                    }
                
                    //iterate through elements
                    foreach (Element item in elements) 
                    {
                        if(countElem>=1){
                                locationStart += itemWidth+scalePen;
                        }
                        else if(countElem>=elements.Count())
                        {
                                Console.WriteLine("Error in locationStart allocation if statement");
                        }
                        itemWidth = item.Width*scale;

                        //15 ma sztywno teraz wysokosc mozliwe zmiany // rzutowanie jawne czy convert.toint32???
                        Rectangle rectangle = new Rectangle((int)(Math.Round(locationStart)), (int)(Math.Round(heightRoad)), (int)(Math.Round(itemWidth)), 15);

                        //Arial font may be changed
                        //writing informations about element
                        using (Font arialFont = new Font("Arial",scaleFont))
                        {
                            RectangleF drawRect;
                            RectangleF drawRect2;
                            
                            //rectangle for wraping text for item
                            drawRect = new RectangleF(locationStart, scaleLongStr, itemWidth, 100);
                            drawRect2 = new RectangleF(locationStart, heightRoad - scale / 2, itemWidth, 50);
                                
                            //alignment to center for text strings
                                StringFormat sf = new StringFormat();
                                sf.LineAlignment = StringAlignment.Center;
                                sf.Alignment = StringAlignment.Center;
                             
                            //drawing information about elements
                            graphicsObj.DrawString(item.Name+"\n"+item.Surface, arialFont, Brushes.Black, drawRect, sf);
                            graphicsObj.DrawString(item.Width.ToString("f1").Replace(",",".") +" m", arialFont, Brushes.Black, drawRect2, sf);
                            
                        }

                        // new pen for every rectangle
                        using (Pen myPen = new Pen(Color.Black, scalePen))
                        {
                            //line border for the road * (Math.PI)) / 180
                            if(countElem==0){
                                graphicsObj.DrawLine(myPen, locationStart - scale / 2, heightRoad - scalePen / 2, locationStart + sumOfWidths + scale / 2, heightRoad - scalePen / 2);
                                graphicsObj.DrawLine(myPen, 0,heightRoad + (float)(Math.Tan((30* (Math.PI)) / 180)*100),locationStart - scale / 2,heightRoad - scalePen/2);
                                graphicsObj.DrawLine(myPen,imgWidth,heightRoad + (float)(Math.Tan((30 * (Math.PI)) / 180) * 100),locationStart + sumOfWidths + scale / 2,heightRoad - scalePen/2);
                            }

                            //drawing rectangle
                            graphicsObj.DrawRectangle(myPen, rectangle);
                                
                            //filling rectangle depending on Hachure
                                    if(item.Hachure==3){
                                        using (HatchBrush hBrush = new HatchBrush(HatchStyle.DiagonalCross, Color.Black, Color.White))
                                        {
                                            graphicsObj.FillRectangle(hBrush, rectangle);
                                        }
                                    }
                                        //additional style for filling rectangle
                                    else if (item.Hachure == 4)
                                    {
                                        using (HatchBrush hBrush = new HatchBrush(HatchStyle.DiagonalBrick, Color.Black, Color.White))
                                        {
                                            graphicsObj.FillRectangle(hBrush, rectangle);
                                        }
                                    }
                                        //additional style for filling rectangle
                                    else if (item.Hachure == 5)
                                    {
                                        using (HatchBrush hBrush = new HatchBrush(HatchStyle.Divot, Color.Black, Color.White))
                                        {
                                            graphicsObj.FillRectangle(hBrush, rectangle);
                                        }
                                    }
                                    else if (item.Hachure == 2)
                                    {
                                        using (HatchBrush hBrush = new HatchBrush(HatchStyle.Cross, Color.Black, Color.White))
                                        {
                                            graphicsObj.FillRectangle(hBrush, rectangle);
                                        }
                                    }
                                    else if (item.Hachure == 1)
                                    {
                                        try
                                        {
                                            using (SolidBrush blackBrush = new SolidBrush(Color.Black))
                                            {
                                                // custom dash style pattern length of: line, space ,dot, line
                                                myPen.DashPattern = new float[] { 3.0F, 3.0F, 1.0F, 3.0F };

                                                graphicsObj.DrawLine(myPen, locationStart + itemWidth / 2, 1, locationStart + itemWidth / 2, imgHeight - 1);

                                                graphicsObj.FillRectangle(blackBrush, rectangle);
                                            }
                                        }catch(ArgumentException ex){
                                            Console.WriteLine(ex);
                                        }
                                    }
                                        // 0 zostawic puste a reszta bedzie bledem juz tutaj
                                        //
                                    else if (item.Hachure != 0)
                                    {
                                        Console.WriteLine("Error in hatchstyle check");
                                    }
                                }

                        //pogrubienie lini pomiedzy elementami
                        using (Pen myPen2 = new Pen(Color.Black, scalePen + 2))
                        {
                            graphicsObj.DrawLine(myPen2, locationStart - scalePen / 2, heightRoad, locationStart - scalePen / 2, heightRoad + 15+scalePen/2);
                            if (countElem == elements.Count()-1)
                            {
                                graphicsObj.DrawLine(myPen2, locationStart + itemWidth + scalePen, heightRoad, locationStart + itemWidth + scalePen, heightRoad + 15 + scalePen / 2);
                            }
                        }
                            countElem++;
                        }
                    }
            return bmp;
        }
    }
}