using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement5GoodTempp.Winform.PanelDesign
{
    public class GradientPanel : Panel
    {
        public Color gradientTop { get; set; }
        public Color gradientBottom { get; set; }  

        public GradientPanel()
        {
            Resize += GradientPanel_Resize;
        }

        private void GradientPanel_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush linear = new LinearGradientBrush(
                ClientRectangle,
                gradientTop,
                gradientBottom,
                90F);

            Graphics g = e.Graphics;

            g.FillRectangle(linear, ClientRectangle);


            base.OnPaint(e);
        }
    }
}
