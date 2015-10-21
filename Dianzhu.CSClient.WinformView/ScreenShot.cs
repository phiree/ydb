using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Teboscreen
{
    public delegate void Captured(Image image);
    class ScreenShot
    {

        public event Captured Captured;
        
        public   void CaptureImage(bool showCursor, Size curSize, Point curPos, Point SourcePoint, Point DestinationPoint, Rectangle SelectionRectangle )
        {

            using (Bitmap bitmap = new Bitmap(SelectionRectangle.Width, SelectionRectangle.Height))
            {

                using (Graphics g = Graphics.FromImage(bitmap))
                {

                    g.CopyFromScreen(SourcePoint, DestinationPoint, SelectionRectangle.Size);

                    if (showCursor)
                    {

                        Rectangle cursorBounds = new Rectangle(curPos, curSize);
                        Cursors.Default.Draw(g, cursorBounds);

                    }

                }
 
                    Image img = (Image)bitmap;
                Captured(img);

            
                
 

            }

        }
    }
}