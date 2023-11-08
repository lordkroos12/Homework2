using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Homework2
{

    public partial class Form1 : Form
    { 
        Bitmap originalImage;
        public Form1()
        {
            InitializeComponent();
        }
       
        public static Bitmap DownscaleImage(Bitmap originalImage, double scaleFactorPercentage)
        {
            if (scaleFactorPercentage <= 0.0 || scaleFactorPercentage >= 100.0)
            {
                throw new ArgumentException("Scale factor percentage must be between 0 and 100.");
            }

            int newWidth = (int)(originalImage.Width * scaleFactorPercentage / 100.0);
            int newHeight = (int)(originalImage.Height * scaleFactorPercentage / 100.0);

            Bitmap resizedImage = new Bitmap(newWidth, newHeight);

            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {

                    int totalRed = 0;
                    int totalGreen = 0;
                    int totalBlue = 0;

                    int originalX = (int)(x * 1.0 / newWidth * originalImage.Width);
                    int originalY = (int)(y * 1.0 / newHeight * originalImage.Height);

                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {

                            Color pixelColor = originalImage.GetPixel(originalX + i, originalY + j);

                            totalRed += pixelColor.R;
                            totalGreen += pixelColor.G;
                            totalBlue += pixelColor.B;
                        }
                    }
                    int averageRed = totalRed / 4;
                    int averageGreen = totalGreen / 4;
                    int averageBlue = totalBlue / 4;

                    Color averageColor = Color.FromArgb(averageRed, averageGreen, averageBlue);

                    resizedImage.SetPixel(x, y, averageColor);
                }
            }
            return resizedImage;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd1 = new OpenFileDialog();
            fd1.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (fd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(fd1.FileName);
                originalImage = new Bitmap(fd1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double scaleFactorPercentage = double.Parse(textBox1.Text);
            MessageBox.Show("Your image is being processed");
            Bitmap resizedImage = DownscaleImage(originalImage, scaleFactorPercentage);
            pictureBox1.Image = resizedImage;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Jpeg;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                pictureBox1.Image.Save(sfd.FileName, format);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}