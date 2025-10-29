using System.Drawing;
using ClosedXML.Excel;
using StudentManagement5GoodTempp.DataAccess.Entity;

namespace StudentManagement5Good.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Chuyển đổi System.Drawing.Color sang XLColor của ClosedXML
        /// </summary>
        public static XLColor ToClosedXMLColor(this Color color)
        {
            return XLColor.FromArgb(color.A, color.R, color.G, color.B);
        }
        
        /// <summary>
        /// Chuyển đổi TrangThaiMinhChung sang XLColor
        /// </summary>
        public static XLColor ToClosedXMLColor(this TrangThaiMinhChung trangThai)
        {
            return trangThai.ToColor().ToClosedXMLColor();
        }
    }
}
