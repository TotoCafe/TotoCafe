using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using ZXing;

/// <summary>
/// Summary description for QrCode
/// </summary>
public class QrCode
{
    public string QrString { get; set; }
    public string CompanyName { get; set; }
    public string TableName { get; set; }
    public System.Drawing.Image QrImage { get; set; }

	public QrCode()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void GenerateQrImage()
    {
        System.Drawing.Image QrImage;
        IBarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options =
            {
                Width = 150,
                Height = 150
            }
        };
        var result = writer.Write(this.QrString);

        using (Bitmap bitMap = new Bitmap(result))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                QrImage = System.Drawing.Image.FromStream(ms);
            }
        }
        this.QrImage = QrImage;
    }
}