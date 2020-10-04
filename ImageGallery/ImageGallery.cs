using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using ImageGallery.Properties;
using C1.Win.C1Tile;

namespace ImageGallery
{
    public partial class ImageGallery : Form
    {

        public SplitContainer splitContainer1 = new SplitContainer();
        public TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
        public Panel panel1 = new Panel();
        public Panel panel2 = new Panel();
        public Panel panel3 = new Panel();
        public TextBox textBox1 = new TextBox();
        public PictureBox searchimg = new PictureBox();
        public PictureBox exportimg = new PictureBox();
        public C1.Win.C1Tile.C1TileControl tileControl1 = new C1.Win.C1Tile.C1TileControl();
        public C1.Win.C1Tile.Group group1 = new C1.Win.C1Tile.Group();
        public C1.C1Pdf.C1PdfDocument pdfDocument1 = new C1.C1Pdf.C1PdfDocument();
        public StatusStrip statusStrip1 = new System.Windows.Forms.StatusStrip();
        public ToolStripProgressBar progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
        public C1.Win.C1Tile.Tile img1 = new Tile();
        public C1.Win.C1Tile.Tile img2 = new Tile();
        
        DataFetcher datafetch1 = new DataFetcher();
        List<ImageItem> imagesList1 = new List<ImageItem>();
        int checkedItems = 0;

        private void ImageGallery_Load(object sender, EventArgs e)
        {
            void controls1()
            {
                this.Text = "Image Gallery";
                this.MaximizeBox = false;
                this.Size = new System.Drawing.Size(780, 780);
                this.MaximumSize = new System.Drawing.Size(810, 810);
                this.ShowIcon = false;
                this.Controls.Add(splitContainer1);

                this.splitContainer1.Dock = DockStyle.Fill;
                this.splitContainer1.Panel1.Show();
                this.splitContainer1.Panel2.Show();
                this.splitContainer1.SplitterDistance = 40;
                this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
                this.splitContainer1.Visible = true;
                this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
                this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
                this.splitContainer1.IsSplitterFixed = true;
                this.splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
                this.splitContainer1.Panel2.Controls.Add(tileControl1);
                this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);

                this.tableLayoutPanel1.ColumnCount = 3;
                this.tableLayoutPanel1.Dock = DockStyle.Fill;
                this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
                this.tableLayoutPanel1.RowCount = 1;
                this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 40);
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
                this.tableLayoutPanel1.Controls.Add(panel1, 1, 0);
                this.tableLayoutPanel1.Controls.Add(panel2, 2, 0);
                this.tableLayoutPanel1.Controls.Add(panel3, 0, 0);

                this.panel1.Location = new System.Drawing.Point(477, 0);
                this.panel1.Size = new System.Drawing.Size(287, 40);
                this.panel1.Dock = DockStyle.Fill;
                this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tablecol2Paint);
                this.panel1.Controls.Add(textBox1);

                this.textBox1.Name = "_searchBox";
                this.textBox1.BorderStyle = 0;
                this.textBox1.Dock = DockStyle.Fill;
                this.textBox1.Location = new System.Drawing.Point(16, 9);
                this.textBox1.Size = new System.Drawing.Size(244, 16);
                this.textBox1.Text = "Search Image";
                this.textBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);

                this.panel2.Location = new System.Drawing.Point(479, 12);
                this.panel2.Margin = new System.Windows.Forms.Padding(2, 12, 45, 12);
                this.panel2.Size = new System.Drawing.Size(40, 16);
                this.panel2.TabIndex = 1;
                this.panel2.Controls.Add(searchimg);

                this.searchimg.Name = "_search";
                this.searchimg.Image = global::ImageGallery.Properties.Resource.searchicon;
                this.searchimg.Dock = DockStyle.Left;
                this.searchimg.Location = new System.Drawing.Point(0, 0);
                this.searchimg.Margin = new System.Windows.Forms.Padding(0);
                this.searchimg.Size = new System.Drawing.Size(40, 16);
                this.searchimg.SizeMode = PictureBoxSizeMode.Zoom;
                this.searchimg.BorderStyle = BorderStyle.FixedSingle;
                this.searchimg.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
                this.searchimg.Click += new System.EventHandler(this.searchimg_Click);
                

                this.panel3.Dock = DockStyle.Fill;
                this.panel3.TabIndex = 1;
                this.panel3.Controls.Add(exportimg);
                this.panel3.TabIndex = 2;

                this.exportimg.Name = "_exportImage";
                this.exportimg.Image = global::ImageGallery.Properties.Resource.exporticon;
                this.exportimg.Location = new System.Drawing.Point(29, 3);
                this.exportimg.Size = new System.Drawing.Size(135, 28);
                this.exportimg.SizeMode = PictureBoxSizeMode.StretchImage;
                this.exportimg.Click += new System.EventHandler(this.exportimg_Click);
                this.exportimg.Visible = false;
                this.exportimg.BorderStyle = BorderStyle.FixedSingle;
                this.exportimg.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2Paint);

                this.tileControl1.CellHeight = 78;
                this.tileControl1.CellSpacing = 11;
                this.tileControl1.CellWidth = 78;
                this.tileControl1.Name = "tileControl1";
                this.tileControl1.Dock = DockStyle.Fill;
                this.tileControl1.Size = new System.Drawing.Size(764, 718);
                this.tileControl1.SurfacePadding = new System.Windows.Forms.Padding(12, 4, 12, 4);
                this.tileControl1.SwipeDistance = 20;
                this.tileControl1.SwipeRearrangeDistance = 98;
                this.tileControl1.Groups.Add(this.group1);
                this.tileControl1.TileChecked += new System.EventHandler<C1.Win.C1Tile.TileEventArgs>(this.tileChecked);
                this.tileControl1.TileUnchecked += new System.EventHandler<C1.Win.C1Tile.TileEventArgs>(this.tileUnchecked);
                this.tileControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.tileControl1Paint);
                this.tileControl1.AllowChecking = true;
                this.tileControl1.Orientation = LayoutOrientation.Vertical;

                this.statusStrip1.Visible = false;
                this.statusStrip1.Dock = DockStyle.Bottom;
                this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.progressBar1 });
                this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;

                this.group1.Name = "Group1";
                this.group1.Tiles.Add(this.img1);
                this.group1.Tiles.Add(this.img2);
                this.img1.Image = global::ImageGallery.Properties.Resource.img1;
                this.img2.Image = global::ImageGallery.Properties.Resource.img2;

            }
            controls1();
            
        }

        public ImageGallery()
        {   
            
            InitializeComponent();
        }

        private async void searchimg_Click(object sender, EventArgs e)
        {
            
                statusStrip1.Visible = true;
                imagesList1 = await
                datafetch1.GetImageData(textBox1.Text);
                AddTiles(imagesList1);
                statusStrip1.Visible = false;
            
        }

        private void AddTiles(List<ImageItem> imageList1)
        {
            tileControl1.Groups[0].Tiles.Clear();

            foreach (var imageitem in imageList1)
            {
                Tile tile = new Tile();
                tile.HorizontalSize = 2;
                tile.VerticalSize = 2;
                tileControl1.Groups[0].Tiles.Add(tile);
                Image img = Image.FromStream(new MemoryStream(imageitem.Base64));
                Template tl = new Template();
                ImageElement ie = new ImageElement();
                ie.ImageLayout = ForeImageLayout.Stretch;
                tl.Elements.Add(ie);
                tile.Template = tl;
                tile.Image = img;
            }
        }



        private void exportimg_Click(object sender, EventArgs e)
        {
            List<Image> images = new List<Image>();
            foreach (Tile tile in tileControl1.Groups[0].Tiles)
            {
                if (tile.Checked)
                {
                    images.Add(tile.Image);
                }
            }
            ConvertToPdf(images);
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "pdf";
            saveFile.Filter = "PDF files (*.pdf)|*.pdf*";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {

                pdfDocument1.Save(saveFile.FileName);

            }

        }
        private void ConvertToPdf(List<Image> images)
        {
            RectangleF rect = pdfDocument1.PageRectangle;
            bool firstPage = true;
            foreach (var selectedimg in images)
            {
                if (!firstPage)
                {
                    pdfDocument1.NewPage();
                }
                firstPage = false;
                rect.Inflate(-72, -72);
                pdfDocument1.DrawImage(selectedimg, rect);
            }

        }

        public void tablecol2Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle r = textBox1.Bounds;
            r.Inflate(3, 3);
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
        }

        public void pictureBox2Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle r = new Rectangle(exportimg.Location.X, exportimg.Location.Y, exportimg.Width,
            exportimg.Height);
            r.X -= 29;
            r.Y -= 3;
            r.Width--;
            r.Height--;
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
            e.Graphics.DrawLine(p, new Point(0, 43), new
            Point(this.Width, 43));
        }

        private void tileChecked(object sender, C1.Win.C1Tile.TileEventArgs e)
        {   
            checkedItems = checkedItems + 1;
            exportimg.Visible = true;
        }

        private void tileUnchecked(object sender, C1.Win.C1Tile.TileEventArgs e)
        {
            checkedItems = checkedItems - 1;
            exportimg.Visible = (checkedItems > 0);
        }

        private void tileControl1Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawLine(p, 0, 43, 800, 43);
        }

    }
}   
