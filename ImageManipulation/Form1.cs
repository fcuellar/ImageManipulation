using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulation
{
    public partial class Form1 : Form
    {
        Image newImage;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //new open file dialog with filters set for specific types
            OpenFileDialog openF = new OpenFileDialog();
            openF.Filter = "Image Files(*.jpg; *.png; *.bmp)|*.jpg;  *.png; *.bmp";

            //if opened successfully 
            if (openF.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                //opens and shows opened image
                pictureBox1.ImageLocation = openF.FileName;
                //creates a "copy" image from opened image
                newImage = Image.FromFile(openF.FileName);
                //assigned the file address from the opened file
                fileAddress.Text = openF.FileName;
                if(button2.Enabled==false || button3.Enabled==false)
                {
                    //resets the submit and resize buttons after opening new file
                    button2.Enabled = true;
                    button3.Enabled = true;
                }
                
            }
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //initializes the dropdown menus with text
            comboBox1.Items.Add("PNG");
            comboBox1.Items.Add("JPG");
            comboBox1.Items.Add("BMP");

            comboBox2.Items.Add("640x480");
            comboBox2.Items.Add("960x720");
            comboBox2.Items.Add("1024x768");
            comboBox2.Items.Add("Other..");
            


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //if no file opened
            
            if(fileAddress.Text.Equals(""))
                {
                MessageBox.Show("Please pick a File");
                
            }
            
            else
            {
                //removes the last 3 characters from the file address into a new address
                newAddress.Text = fileAddress.Text.Remove(fileAddress.TextLength - 3);
                //cases for which type of file was selected
                
                if (comboBox1.SelectedItem.Equals("PNG"))
                {
                    //image will be saved, adding the format to the end of the address and changing the actual image format
                    newImage.Save(newAddress.Text + "png", System.Drawing.Imaging.ImageFormat.Png);
                    //disables button, must open new file
                    button2.Enabled = false;
                    button3.Enabled = false;

                    MessageBox.Show("Image has been saved");

                }
                else if (comboBox1.SelectedItem.Equals("JPG"))
                {
                    newImage.Save(newAddress.Text + "jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    button2.Enabled = false;
                    button3.Enabled = false;

                    MessageBox.Show("Image has been saved");
                }
                else if (comboBox1.SelectedItem.Equals("BMP"))
                {
                    newImage.Save(newAddress.Text + "bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                    button2.Enabled = false;
                    button3.Enabled = false;

                    MessageBox.Show("Image has been saved");
                }
                else
                    MessageBox.Show("Please pick a New File Type");
            }
            




        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //custom resolution will only appear if its clicked on in the drop down menu
            if(comboBox2.SelectedIndex.Equals(3))
            {
                customH.Visible = true;
                customW.Visible = true;
                label2.Visible = true;
                label3.Visible = true;

            }
            else
            {
                customH.Visible = false;
                customW.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //different cases for pre-determined resolutions
            if (comboBox2.SelectedIndex.Equals(0))
            {
                //will call the resize method and disable the button
                newImage=resize(newImage, 640, 480);
                button3.Enabled = false;
                MessageBox.Show("Image has been resized");

            }
            else if (comboBox2.SelectedIndex.Equals(1))
            {
                newImage=resize(newImage, 960, 720);
                button3.Enabled = false;
                MessageBox.Show("Image has been resized");


            }
            else if (comboBox2.SelectedIndex.Equals(2))
            {

                newImage=resize(newImage, 1020, 768);
                button3.Enabled = false;
                MessageBox.Show("Image has been resized");



            }
            else if (comboBox2.SelectedIndex.Equals(3))
            {
                //custom resolution converts the w and the h from the specific text box
                int w = Convert.ToInt32(customW.Text);
                int h = Convert.ToInt32(customH.Text);             
                newImage=resize(newImage, w, h);
                button3.Enabled = false;
                MessageBox.Show("Image has been resized");


            }




        }
        //resize method taking image with  new resolution
        Image resize(Image image, int w, int h)
        {
            //new bitmap in pixels with the new resolution size
            Bitmap rsize = new Bitmap(w, h);
            //graphical surface to create new image one
            Graphics graphic = Graphics.FromImage(rsize);
            //new image created from the origin
            graphic.DrawImage(image, 0, 0, w, h);
            graphic.Dispose();
            return rsize;
        }
    }
}
