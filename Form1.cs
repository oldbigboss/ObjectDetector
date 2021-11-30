using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  
using System.IO;
using System.Security;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using Aspose.Imaging.Cloud.Sdk.Api;
using Aspose.Imaging.Cloud.Sdk.Model;
using Aspose.Imaging.Cloud.Sdk.Model.Requests;

namespace ObjectDetector
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\credits\\tough-flames-333022-0da9508cffee.json");

        }
        private string SelectedImagePath;
        
        private void SelectImg()
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Файлы изображений(*.png; *.jpg; *.jpeg; *.gif; *.bmp)| *.png; *.jpg; *.jpeg; *.gif; *.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {

                SelectedImagePath = open.FileName;
            }
            pictureBox1.Image = System.Drawing.Image.FromFile(SelectedImagePath);
        }  
  
     
        private void button1_Click(object sender, EventArgs e)
        {
            SelectImg();
        }

        private void button2_Click(object sender, EventArgs e) // google unpaid
        {
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            Google.Cloud.Vision.V1.Image ImgSelected = Google.Cloud.Vision.V1.Image.FromFile(SelectedImagePath);
            IReadOnlyList<EntityAnnotation> labels = client.DetectLabels(ImgSelected);
            string s1 = "";
            foreach (EntityAnnotation label in labels)
            {
                s1+= $"Вероятность: {(int)(label.Score * 100)}%; Описание: {label.Description}/n";
            }
            label2.Text = s1;
        }
        public void BoundsAnImageFromRequestBody() // aspose
        {
            // Input formats could be one of the following:
            // bmp, jpeg, and jpeg2000 
            
            using (FileStream inputImageStream = File.OpenRead(SelectedImagePath))
            {
                string method = "ssd";
                int threshold = 50;
                bool includeLabel = true;
                bool includeScore = true;
                string outPath = null;
                string storage = null; // We are using default Cloud Storage

                var request = new CreateObjectBoundsRequest(inputImageStream, method, threshold, includeLabel, includeScore, outPath, storage);

                // DetectedObjectList detectedObjectList = this.ImagingApi.GetObjectBounds(request);
                // Aspose.Imaging.Cloud.Sdk.Api.ImagingApi.Eq
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
