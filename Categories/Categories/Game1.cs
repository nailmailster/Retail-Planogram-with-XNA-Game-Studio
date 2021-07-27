using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Net;
//using Microsoft.Xna.Framework.Storage;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using WinForms = System.Windows.Forms;
using System.IO;
using System.Text;
using System.Threading;

//using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

using XELibrary;

namespace Categories
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region variables
        GraphicsDeviceManager graphics;

        #region 3D definitions
        private Matrix projection;
        private Matrix view;
        private Matrix world;
        public Matrix worldMatrix = Matrix.Identity;

        private Vector3 cameraPosition = new Vector3(0.0f, 6.5f, 19.0f);
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUpVector = Vector3.Up;

        private VertexBuffer vertexBuffer;
        private BasicEffect effect;
        #endregion

        SpriteBatch spriteBatch;

        private List<clsSprite> mySprites;
        private List<int> selSprites;
        private List<good> myGoods;
        private List<int> selGoods;

        public Texture2D texture, texturesel;
        public Texture2D item, itemsel;
        public Texture2D scrollup, scrolldown;
        public Texture2D infotexture;
        public Texture2D pleasewait;
        private SpriteFont gameFont, catFont, tinyFont, middleFont;
        private MouseState mouseState, prevMouseState;

        protected TimeSpan elapsedTime = TimeSpan.Zero;

        private WinForms.Label lblFPS;
        private WinForms.Button cmdClick;
        private WinForms.Button cmdSave;
        private WinForms.Button cmdLoad;
        private WinForms.Button cmdClear;
        private WinForms.Button cmdZoomIn;
        private WinForms.Button cmdZoomOut;
        private WinForms.Button cmdViewport;
        private WinForms.Button cmdReport;
        private WinForms.Button cmdDReport;
        private WinForms.Button cmdImage;
        private WinForms.Button cmdSortByCode;
        private WinForms.Button cmdSortByDescription;
        private WinForms.Button cmdSortByDefault;
        private WinForms.Button cmdQtySum;
        private InputForm inputForm = new InputForm();
        private WinForms.ContextMenuStrip cmsSprites;
        public string catName;

        Texture2D pixel;
        public List<Vector2> vectors;
        public Color colour;

        bool nodeIsDrawing;
        int parent;

        float scale = 1, prevScale = 1;

        bool isAdding = false;

        float angle = 0;

        Viewport leftVP, rightVP, originVP;
        bool portable = false;

        public int currentSprite = -1;
        public int ig = -1;

        public int firstLine = 0;

        public bool report = false;
        public bool reporting = false;
        public bool report2 = false;

        public bool print = false;

        private float spriteAngle = 0.0f;

        private bool onButton = false;

        private bool f1 = false, f2 = false;
        private KeyboardState prevKeyboardState;

        private float dx, dy;

        private bool f5 = true;
        private int curLevel = 1, floatingLevel = 1;
        private bool contextMenu = false;
        private int f5_i = -1;

        private bool freezed = false;
        private bool f7 = false;

        //private FPS fps;

        private Effect fireEffect;

        private Random rand = new Random();

        private Texture2D hotSpotTexture;
        private Texture2D fire;
        private Rectangle titleSafeArea;

        private RenderTarget2D renderTarget1;
        private RenderTarget2D renderTarget2;

        private int offset = -128;
        private Color[] colors = {
            Color.Black,
            Color.Yellow,
            Color.White,
            Color.Red,
            Color.Orange,
            new Color(255, 255, 128)
        };

        private bool fireIsOn = false;

        private WinForms.OpenFileDialog oFD = new WinForms.OpenFileDialog();

        private String reportFile;

        #endregion variables

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private void ExcelReport()
        {
            //Excel._Application oXL;
            //Excel._Workbook oWB;
            //Excel._Worksheet oSheet;
            //Excel.Range oRng;

            //if (!report)
            //{
            //    WinForms.MessageBox.Show("Переведите приложение в режим отображения отчета");
            //    //WinForms.MessageBox.Show(string.Format("Click on button {0}", sender));
            //    return;
            //}

            //reporting = true;

            //oXL = new Excel.Application();
            //oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
            //oSheet = (Excel._Worksheet)oWB.ActiveSheet;

            //int column = 0, parent1 = 0, parent2 = 0, parent3 = 0, parent4 = 0, parent5 = 0, parent6 = 0, parent7 = 0, parent8 = 0, parent9 = 0, parent10 = 0, row = 0, level = 0, prevlevel = 0, i;
            //if (selGoods.Count > 0)
            //{
            //    for (int j = 0; j < selGoods.Count; j++)
            //    {
            //        column = 1;
            //        row++;
            //        oSheet.Cells[row, column] = myGoods[selGoods[j]].code;
            //        oSheet.Cells[row, column + 1] = myGoods[selGoods[j]].description;
            //        oSheet.Cells[row, column + 2] = myGoods[selGoods[j]].sum;
            //        oSheet.Cells[row, column + 3] = Math.Round(myGoods[selGoods[j]].percent, 2);
            //    }
            //}
            //if (selGoods.Count == 0)
            //{
            //    for (i = 0; i < mySprites.Count; i++)
            //    {
            //        if (mySprites[i].parent == -1)
            //        {
            //            parent1 = i;
            //            break;
            //        }
            //    }
            //    for (i = 0; i < mySprites.Count; i++)
            //    {
            //        if (mySprites[i].parent == parent1)
            //        {
            //            level = 1;
            //            column = 1;
            //            row++;
            //            oSheet.Cells[row, column] = mySprites[i].catName;
            //            oSheet.Cells[row, column + 1] = mySprites[i].sum;
            //            oSheet.Cells[row, column + 2] = Math.Round(mySprites[i].percent, 2);

            //            parent2 = i;
            //            for (int i2 = 0; i2 < mySprites.Count; i2++)
            //            {
            //                if (mySprites[i2].parent == parent2)
            //                {
            //                    level = 2;
            //                    column = 2;
            //                    row++;
            //                    oSheet.Cells[row, column] = mySprites[i2].catName;
            //                    oSheet.Cells[row, column + 1] = mySprites[i2].sum;
            //                    oSheet.Cells[row, column + 2] = Math.Round(mySprites[i2].percent, 2);

            //                    parent3 = i2;
            //                    for (int i3 = 0; i3 < mySprites.Count; i3++)
            //                    {
            //                        if (mySprites[i3].parent == parent3)
            //                        {
            //                            level = 3;
            //                            column = 3;
            //                            row++;
            //                            oSheet.Cells[row, column] = mySprites[i3].catName;
            //                            oSheet.Cells[row, column + 1] = mySprites[i3].sum;
            //                            oSheet.Cells[row, column + 2] = Math.Round(mySprites[i3].percent, 2);
            //                            for (int j = 0; j < myGoods.Count; j++)
            //                            {
            //                                if (myGoods[j].index == i3)
            //                                {
            //                                    level = 11;
            //                                    column = 11;
            //                                    row++;
            //                                    oSheet.Cells[row, column] = myGoods[j].description;
            //                                    oSheet.Cells[row, column + 1] = myGoods[j].sum;
            //                                    oSheet.Cells[row, column + 2] = Math.Round(myGoods[j].percent, 2);
            //                                }
            //                            }

            //                            parent4 = i3;
            //                            for (int i4 = 0; i4 < mySprites.Count; i4++)
            //                            {
            //                                if (mySprites[i4].parent == parent4)
            //                                {
            //                                    level = 4;
            //                                    column = 4;
            //                                    row++;
            //                                    oSheet.Cells[row, column] = mySprites[i4].catName;
            //                                    oSheet.Cells[row, column + 1] = mySprites[i4].sum;
            //                                    oSheet.Cells[row, column + 2] = Math.Round(mySprites[i4].percent, 2);

            //                                    parent5 = i4;
            //                                    for (int i5 = 0; i5 < mySprites.Count; i5++)
            //                                    {
            //                                        if (mySprites[i5].parent == parent5)
            //                                        {
            //                                            level = 5;
            //                                            column = 5;
            //                                            row++;
            //                                            oSheet.Cells[row, column] = mySprites[i5].catName;
            //                                            oSheet.Cells[row, column + 1] = mySprites[i5].sum;
            //                                            oSheet.Cells[row, column + 2] = Math.Round(mySprites[i5].percent, 2);
            //                                            for (int j = 0; j < myGoods.Count; j++)
            //                                            {
            //                                                if (myGoods[j].index == i5)
            //                                                {
            //                                                    level = 11;
            //                                                    column = 11;
            //                                                    row++;
            //                                                    oSheet.Cells[row, column] = myGoods[j].description;
            //                                                    oSheet.Cells[row, column + 1] = myGoods[j].sum;
            //                                                    oSheet.Cells[row, column + 2] = Math.Round(myGoods[j].percent, 2);
            //                                                }
            //                                            }

            //                                            parent6 = i5;
            //                                            for (int i6 = 0; i6 < mySprites.Count; i6++)
            //                                            {
            //                                                if (mySprites[i6].parent == parent6)
            //                                                {
            //                                                    level = 6;
            //                                                    column = 6;
            //                                                    row++;
            //                                                    oSheet.Cells[row, column] = mySprites[i6].catName;
            //                                                    oSheet.Cells[row, column + 1] = mySprites[i6].sum;
            //                                                    oSheet.Cells[row, column + 2] = Math.Round(mySprites[i6].percent, 2);

            //                                                    parent7 = i6;
            //                                                    for (int i7 = 0; i7 < mySprites.Count; i7++)
            //                                                    {
            //                                                        if (mySprites[i7].parent == parent7)
            //                                                        {
            //                                                            level = 7;
            //                                                            column = 7;
            //                                                            row++;
            //                                                            oSheet.Cells[row, column] = mySprites[i7].catName;
            //                                                            oSheet.Cells[row, column + 1] = mySprites[i7].sum;
            //                                                            oSheet.Cells[row, column + 2] = Math.Round(mySprites[i7].percent, 2);

            //                                                            parent8 = i7;
            //                                                            for (int i8 = 0; i8 < mySprites.Count; i8++)
            //                                                            {
            //                                                                if (mySprites[i8].parent == parent8)
            //                                                                {
            //                                                                    level = 8;
            //                                                                    column = 8;
            //                                                                    row++;
            //                                                                    oSheet.Cells[row, column] = mySprites[i8].catName;
            //                                                                    oSheet.Cells[row, column + 1] = mySprites[i8].sum;
            //                                                                    oSheet.Cells[row, column + 2] = Math.Round(mySprites[i8].percent, 2);

            //                                                                    parent9 = i8;
            //                                                                    for (int i9 = 0; i9 < mySprites.Count; i9++)
            //                                                                    {
            //                                                                        if (mySprites[i9].parent == parent9)
            //                                                                        {
            //                                                                            level = 9;
            //                                                                            column = 9;
            //                                                                            row++;
            //                                                                            oSheet.Cells[row, column] = mySprites[i9].catName;
            //                                                                            oSheet.Cells[row, column + 1] = mySprites[i9].sum;
            //                                                                            oSheet.Cells[row, column + 2] = Math.Round(mySprites[i9].percent, 2);

            //                                                                            parent10 = i9;
            //                                                                            for (int i10 = 0; i10 < mySprites.Count; i10++)
            //                                                                            {
            //                                                                                if (mySprites[i10].parent == parent10)
            //                                                                                {
            //                                                                                    level = 10;
            //                                                                                    column = 10;
            //                                                                                    row++;
            //                                                                                    oSheet.Cells[row, column] = mySprites[i10].catName;
            //                                                                                    oSheet.Cells[row, column + 1] = mySprites[i10].sum;
            //                                                                                    oSheet.Cells[row, column + 2] = Math.Round(mySprites[i10].percent, 2);
            //                                                                                    for (int j = 0; j < myGoods.Count; j++)
            //                                                                                    {
            //                                                                                        if (myGoods[j].index == i10)
            //                                                                                        {
            //                                                                                            level = 11;
            //                                                                                            column = 11;
            //                                                                                            row++;
            //                                                                                            oSheet.Cells[row, column] = mySprites[i10].catName;
            //                                                                                            oSheet.Cells[row, column + 1] = mySprites[i10].sum;
            //                                                                                            oSheet.Cells[row, column + 2] = Math.Round(mySprites[i10].percent, 2);
            //                                                                                        }
            //                                                                                    }
            //                                                                                }
            //                                                                            }
            //                                                                        }
            //                                                                    }
            //                                                                }
            //                                                            }
            //                                                        }
            //                                                    }
            //                                                }
            //                                            }
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //selSprites.Clear();
            //selSprites.Add(parent);
            //for (int i = 0; i < mySprites.Count; i++)
            //    if (IsDescendantOfSprite(parent, i))
            //        selSprites.Add(i);

            //oSheet.Cells[1, 1] = "First Name";
            //oSheet.Cells[1, 2] = "Last Name";
            //oSheet.Cells[1, 3] = "Full Name";
            //oSheet.Cells[1, 4] = "Salary";

            //oSheet.get_Range("A1", "D1").Font.Bold = true;
            //oSheet.get_Range("A1", "D1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //reporting = false;
            //oXL.Visible = true;
        }

        private void cmdClick_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(
                delegate()
                {
                    ExcelReport();
                }
            );
            thread.Start();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (WinForms.MessageBox.Show("Записать схему?", "Сохранение схемы", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            //if (WinForms.MessageBox.Show("You are going to save your changes. Sure?:))", "Saving data", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                StreamWriter sw = new StreamWriter("cats.txt", false);
                for (int i = 0; i < mySprites.Count; i++)
                {
                    sw.WriteLine((int)Math.Round(mySprites[i].position.X, 0));
                    sw.WriteLine((int)Math.Round(mySprites[i].position.Y, 0));
                    sw.WriteLine(mySprites[i].catName);
                    sw.WriteLine(mySprites[i].parent);
                }
                sw.Close();
                sw = new StreamWriter("goods.txt", false);
                for (int i = 0; i < myGoods.Count; i++)
                {
                    sw.WriteLine(myGoods[i].code);
                    sw.WriteLine(myGoods[i].description);
                    sw.WriteLine(myGoods[i].index);
                }
                sw.Close();
            }
        }

        private void CheckLevel()
        {
            Vector2 stringSize, maxStringSize;
            Vector2 infoSize, maxInfoSize;
            int i;

            for (i = 0; i < mySprites.Count; i++)
                if (mySprites[i].parent == -1)
                    mySprites[i].level = 1;
            for (i = 0; i < mySprites.Count; i++)
                if (mySprites[i].parent != -1)
                    if (mySprites[mySprites[i].parent].level == 1)
                        mySprites[i].level = 2;
            for (i = 0; i < mySprites.Count; i++)
                if (mySprites[i].parent != -1)
                    if (mySprites[mySprites[i].parent].level == 2)
                        mySprites[i].level = 3;
            for (i = 0; i < mySprites.Count; i++)
                if (mySprites[i].parent != -1)
                    if (mySprites[mySprites[i].parent].level == 3)
                        mySprites[i].level = 4;
            for (i = 0; i < mySprites.Count; i++)
                if (mySprites[i].parent != -1)
                    if (mySprites[mySprites[i].parent].level == 4)
                        mySprites[i].level = 5;
            for (i = 0; i < mySprites.Count; i++)
                if (mySprites[i].parent != -1)
                    if (mySprites[mySprites[i].parent].level == 5)
                        mySprites[i].level = 6;
            for (i = 0; i < mySprites.Count; i++)
                if (mySprites[i].parent != -1)
                    if (mySprites[mySprites[i].parent].level == 6)
                        mySprites[i].level = 7;
            for (i = 0; i < mySprites.Count; i++)
                if (mySprites[i].parent != -1)
                    if (mySprites[mySprites[i].parent].level == 7)
                        mySprites[i].level = 8;
            for (i = 0; i < mySprites.Count; i++)
                if (mySprites[i].parent != -1)
                    if (mySprites[mySprites[i].parent].level == 8)
                        mySprites[i].level = 9;
            for (i = 0; i < mySprites.Count; i++)
                if (mySprites[i].parent != -1)
                    if (mySprites[mySprites[i].parent].level == 9)
                        mySprites[i].level = 10;

            for (int level = 1; level <= 10; level++)
            {
                stringSize = new Vector2(0, 0);
                maxStringSize = new Vector2(0, 0);
                infoSize = new Vector2(0, 0);
                maxInfoSize = new Vector2(0, 0);

                for (i = 0; i < mySprites.Count; i++)
                {
                    if (mySprites[i].level == level)
                    {
                        if (report)
                        {
                            stringSize = middleFont.MeasureString(mySprites[i].catName + Convert.ToString(Math.Round(mySprites[i].percent2, 2)));
                            stringSize.X += 8;
                            infoSize = middleFont.MeasureString(Convert.ToString(Math.Round(mySprites[i].percent2, 2)));
                            if (infoSize.Length() > maxInfoSize.Length())
                                maxInfoSize = infoSize;
                        }
                        else
                        {
                            stringSize = middleFont.MeasureString(mySprites[i].catName);
                            stringSize.X += 4;
                        }
                        if (stringSize.Length() > maxStringSize.Length())
                            maxStringSize = stringSize;
                    }
                    for (int j = 0; j < mySprites.Count; j++)
                    {
                        if (mySprites[j].level == level)
                        {
                            mySprites[j].size2.X = maxStringSize.X;
                            mySprites[j].infoSize.X = maxInfoSize.X;
                        }
                    }
                }
            }
            for (i = 0; i < mySprites.Count; i++)
            {
                switch (mySprites[i].level)
                {
                    case 1:
                        mySprites[i].position2.X = graphics.PreferredBackBufferWidth - GetLevelWidth(1) - 5;
                        break;
                    case 2:
                        mySprites[i].position2.X = graphics.PreferredBackBufferWidth - GetLevelWidth(1) - GetLevelWidth(2) - 5 * 2;
                        break;
                    case 3:
                        mySprites[i].position2.X = graphics.PreferredBackBufferWidth - GetLevelWidth(1) - GetLevelWidth(2) - GetLevelWidth(3) - 5 * 3;
                        break;
                    case 4:
                        mySprites[i].position2.X = graphics.PreferredBackBufferWidth - GetLevelWidth(1) - GetLevelWidth(2) - GetLevelWidth(3) - GetLevelWidth(4) - 5 * 4;
                        break;
                    case 5:
                        mySprites[i].position2.X = graphics.PreferredBackBufferWidth - GetLevelWidth(1) - GetLevelWidth(2) - GetLevelWidth(3) - GetLevelWidth(4) - GetLevelWidth(5) - 5 * 5;
                        break;
                    case 6:
                        mySprites[i].position2.X = graphics.PreferredBackBufferWidth - GetLevelWidth(1) - GetLevelWidth(2) - GetLevelWidth(3) - GetLevelWidth(4) - GetLevelWidth(5) - GetLevelWidth(6) - 5 * 6;
                        break;
                    case 7:
                        mySprites[i].position2.X = graphics.PreferredBackBufferWidth - GetLevelWidth(1) - GetLevelWidth(2) - GetLevelWidth(3) - GetLevelWidth(4) - GetLevelWidth(5) - GetLevelWidth(6) - GetLevelWidth(7) - 5 * 7;
                        break;
                    case 8:
                        mySprites[i].position2.X = graphics.PreferredBackBufferWidth - GetLevelWidth(1) - GetLevelWidth(2) - GetLevelWidth(3) - GetLevelWidth(4) - GetLevelWidth(5) - GetLevelWidth(6) - GetLevelWidth(7) - GetLevelWidth(8) - 5 * 8;
                        break;
                    case 9:
                        mySprites[i].position2.X = graphics.PreferredBackBufferWidth - GetLevelWidth(1) - GetLevelWidth(2) - GetLevelWidth(3) - GetLevelWidth(4) - GetLevelWidth(5) - GetLevelWidth(6) - GetLevelWidth(7) - GetLevelWidth(8) - GetLevelWidth(9) - 5 * 9;
                        break;
                    case 10:
                        mySprites[i].position2.X = graphics.PreferredBackBufferWidth - GetLevelWidth(1) - GetLevelWidth(2) - GetLevelWidth(3) - GetLevelWidth(4) - GetLevelWidth(5) - GetLevelWidth(6) - GetLevelWidth(7) - GetLevelWidth(8) - GetLevelWidth(9) - GetLevelWidth(10) - 5 * 10;
                        break;
                    default:
                        break;
                }
            }
            for (i = 0; i < mySprites.Count; i++)
            {
                int counter = 0;
                for (int j = 0; j < mySprites.Count; j++)
                {
                    if (mySprites[j].parent == i)
                    {
                        mySprites[j].position2.Y = 50 + counter * 23;
                        counter++;
                    }
                }
            }
            for (i = 0; i < mySprites.Count; i++)
            {
                int counter = 0;
                for (int j = 0; j < mySprites.Count; j++)
                {
                    if (mySprites[j].parent == -1)
                    {
                        mySprites[j].position2.Y = 50 + counter * 23;
                        counter++;
                    }
                }
            }
        }

        private float GetLevelWidth(int level)
        {
            for (int i = 0; i < mySprites.Count; i++)
                if (mySprites[i].level == level)
                    return mySprites[i].size2.X;
            return 0;
        }

        private void SelectParents()
        {
            int i;
            int selectedSprite = 0;
            int maxLevel = 0;

            for (i = 0; i < mySprites.Count; i++)
            {
                if (mySprites[i].visible)
                    if (mySprites[i].level > maxLevel)
                    {
                        maxLevel = mySprites[i].level;
                        selectedSprite = i;
                    }
            }

            if (selectedSprite != -1)
            {
                for (i = 0; i < mySprites.Count; i++)
                {
                    if (mySprites[selectedSprite].level == 2)
                    {
                        if (mySprites[selectedSprite].parent == i)
                            mySprites[i].texture = texturesel;
                    }
                    if (mySprites[selectedSprite].level == 3)
                    {
                        if (mySprites[selectedSprite].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[selectedSprite].parent].parent == i)
                            mySprites[i].texture = texturesel;
                    }
                    if (mySprites[selectedSprite].level == 4)
                    {
                        if (mySprites[selectedSprite].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[selectedSprite].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                    }
                    if (mySprites[selectedSprite].level == 5)
                    {
                        if (mySprites[selectedSprite].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[selectedSprite].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                    }
                    if (mySprites[selectedSprite].level == 6)
                    {
                        if (mySprites[selectedSprite].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[selectedSprite].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                    }
                    if (mySprites[selectedSprite].level == 7)
                    {
                        if (mySprites[selectedSprite].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[selectedSprite].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                    }
                    if (mySprites[selectedSprite].level == 8)
                    {
                        if (mySprites[selectedSprite].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[selectedSprite].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                    }
                    if (mySprites[selectedSprite].level == 9)
                    {
                        if (mySprites[selectedSprite].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[selectedSprite].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                    }
                    if (mySprites[selectedSprite].level == 10)
                    {
                        if (mySprites[selectedSprite].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[selectedSprite].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                        if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent].parent].parent].parent == i)
                            mySprites[i].texture = texturesel;
                    }
                }
            }
        }
        
        private void CheckVisibility(int selectedSprite)
        {
            int i;

            for (i = 0; i < mySprites.Count; i++)
            {
                if (mySprites[i].parent == selectedSprite)
                {
                    floatingLevel = mySprites[i].level;
                    mySprites[i].visible = true;
                }
                else
                {
                    if (mySprites[i].level > floatingLevel || mySprites[i].level > curLevel)
                        mySprites[i].visible = false;
                }
            }
            
            if (selectedSprite != -1)
            {
                for (i = 0; i < mySprites.Count; i++)
                {
                    if (mySprites[selectedSprite].parent == i)
                        mySprites[i].visible = true;

                    if (mySprites[selectedSprite].parent == mySprites[i].parent)
                        mySprites[i].visible = true;

                    if (mySprites[selectedSprite].level == 3)
                        if (mySprites[i].level == 2)
                            if (mySprites[mySprites[selectedSprite].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;

                    if (mySprites[selectedSprite].level == 4)
                    {
                        if (mySprites[i].level == 3)
                            if (mySprites[mySprites[selectedSprite].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 2)
                            if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 1)
                            if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                    }

                    if (mySprites[selectedSprite].level == 5)
                    {
                        if (mySprites[i].level == 4)
                            if (mySprites[mySprites[selectedSprite].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 3)
                            if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 2)
                            if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                    }

                    if (mySprites[selectedSprite].level == 6)
                    {
                        if (mySprites[i].level == 5)
                            if (mySprites[mySprites[selectedSprite].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 4)
                            if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 3)
                            if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 2)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                    }

                    if (mySprites[selectedSprite].level == 7)
                    {
                        if (mySprites[i].level == 6)
                            if (mySprites[mySprites[selectedSprite].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 5)
                            if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 4)
                            if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 3)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 2)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                    }

                    if (mySprites[selectedSprite].level == 8)
                    {
                        if (mySprites[i].level == 7)
                            if (mySprites[mySprites[selectedSprite].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 6)
                            if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 5)
                            if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 4)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 3)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 2)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                    }

                    if (mySprites[selectedSprite].level == 9)
                    {
                        if (mySprites[i].level == 8)
                            if (mySprites[mySprites[selectedSprite].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 7)
                            if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 6)
                            if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 5)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 4)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 3)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 2)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                    }

                    if (mySprites[selectedSprite].level == 10)
                    {
                        if (mySprites[i].level == 9)
                            if (mySprites[mySprites[selectedSprite].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 8)
                            if (mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 7)
                            if (mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 6)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 5)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 4)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 3)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                        if (mySprites[i].level == 2)
                            if (mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[mySprites[selectedSprite].parent].parent].parent].parent].parent].parent].parent].parent].parent == mySprites[i].parent)
                                mySprites[i].visible = true;
                    }
                }
            }
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            string x, y, catName, parent;
            StreamReader sr;

            mySprites.Clear();
            myGoods.Clear();

            reportFile = null;
            oFD.ShowDialog();
            if (reportFile != null)
            {
                if (reportFile.Substring(reportFile.Length - 8) != "cats.txt")
                    WinForms.MessageBox.Show(oFD.FileName + " - неверный файл схемы");
                sr = new StreamReader(reportFile);
                while (!sr.EndOfStream)
                {
                    x = sr.ReadLine();
                    y = sr.ReadLine();
                    catName = sr.ReadLine();
                    parent = sr.ReadLine();
                    mySprites.Add(new clsSprite(texture, new Vector2(Convert.ToInt16(x), Convert.ToInt16(y)), new Vector2(50, 15)));
                    mySprites[mySprites.Count - 1].catName = catName;
                    mySprites[mySprites.Count - 1].parent = Convert.ToInt16(parent);
                    mySprites[mySprites.Count - 1].firstLine = 0;
                }
                sr.Close();
                CheckLevel();
                CheckVisibility(-1);
                LoadGoods();
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            portable = false;
            report = false;
            mySprites.Clear();
            for (int i = 0; i < myGoods.Count; i++)
                myGoods[i].index = -1;
        }

        private void cmdZoomIn_Click(object sender, EventArgs e)
        {
            scale += 0.5f;
        }

        private void cmdZoomOut_Click(object sender, EventArgs e)
        {
            scale -= 0.5f;
        }

        private void cmdViewport_Click(object sender, EventArgs e)
        {
            portable = !portable;
            if (f5)
            {
                if (portable)
                {
                    for (int i = 0; i < mySprites.Count; i++)
                        mySprites[i].position2.X -= 380;
                }
                else
                {
                    for (int i = 0; i < mySprites.Count; i++)
                        mySprites[i].position2.X += 380;
                }
            }
        }

        private void cmdReport_Click(object sender, EventArgs e)
        {
            if (!report)
            {
                reportFile = null;
                oFD.ShowDialog();
                if (reportFile != null)
                {
                    if (reportFile.Substring(reportFile.Length - 10) != "report.txt")
                        WinForms.MessageBox.Show(oFD.FileName + " is not correct report file");
                    report = !report;
                    if (report)
                    {
                        Window.Title = reportFile;
                        if (/*myGoods.Count > 0 && */mySprites.Count > 0)
                        {
                            reporting = true;
                            Thread thread = new Thread(
                                delegate()
                                {
                                    LoadReport(false);
                                }
                            );
                            thread.Start();
                        }
                        else
                            report = !report;
                    }
                    else
                        Window.Title = "Categories";
                    CheckLevel();
                }
                else
                {
                    report = false;
                    Window.Title = "Categories";
                }
            }
            else
                report = false;
        }

        private void cmdDReport_Click(object sender, EventArgs e)
        {
            if (!report)
            {
                reportFile = null;
                oFD.ShowDialog();
                if (reportFile != null)
                {
                    report = !report;
                    if (report)
                    {
                        Window.Title = reportFile;
                        if (/*myGoods.Count > 0 && */mySprites.Count > 0)
                        {
                            reporting = true;
                            Thread thread = new Thread(
                                delegate()
                                {
                                    LoadReport(false);
                                }
                            );
                            thread.Start();
                        }
                        else
                            report = !report;
                    }
                    else
                        Window.Title = "Categories";
                    CheckLevel();
                }
                else
                {
                    report = false;
                    Window.Title = "Categories";
                }
            }
            else
                report = false;

            if (!report2)
            {
                reportFile = null;
                oFD.ShowDialog();
                if (reportFile != null)
                {
                    report2 = !report2;
                    if (report2)
                    {
                        Window.Title += " - " + reportFile;
                        if (/*myGoods.Count > 0 && */mySprites.Count > 0)
                        {
                            reporting = true;
                            Thread thread = new Thread(
                                delegate()
                                {
                                    LoadReport(true);
                                }
                            );
                            thread.Start();
                        }
                        else
                            report2 = !report2;
                    }
                    //else
                    //    Window.Title = "Categories";
                    CheckLevel();
                }
                else
                {
                    report2 = false;
                    //Window.Title = "Categories";
                }
            }
            else
                report2 = false;
        }

        void oFD_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            reportFile = oFD.FileName;
        }

        private void cmdImage_Click(object sender, EventArgs e)
        {
            print = true;
        }

        private void cmdSortByCode_Click(object sender, EventArgs e)
        {
            if (myGoods == null)
                return;
            myGoods.Sort(CompareGoodsCodes);
            RefreshGoodsList(0);
        }

        private void cmdSortByDescription_Click(object sender, EventArgs e)
        {
            if (myGoods == null)
                return;
            myGoods.Sort(CompareGoodsDescriptions);
            RefreshGoodsList(0);
        }

        private void cmdSortByDefault_Click(object sender, EventArgs e)
        {
            if (myGoods == null)
                return;
            myGoods.Sort(CompareGoodsCodes);
        }

        private void cmdQtySum_Click(object sender, EventArgs e)
        {
            f7 = !f7;
            if (f7)
                cmdQtySum.Text = "SUM";
            else
                cmdQtySum.Text = "QTY";
            RefreshReport();
            CheckLevel();
        }

        private void lblFPS_Click(object sender, EventArgs e)
        {
            currentSprite = -1;
        }

        private static int CompareGoodsCodes(good x, good y)
        {
            return string.Compare(x.code, y.code);
        }

        private static int CompareGoodsDescriptions(good x, good y)
        {
            return string.Compare(x.description, y.description);
        }

        public bool IsMouseOnMySprites(MouseState mouseState)
        {
            for (int i = 0; i < mySprites.Count; i++)
                if (f1)
                {
                    if (mouseState.X >= mySprites[i].position.X * scale
                        && mouseState.X <= (mySprites[i].position.X * scale + mySprites[i].size.X)
                        && mouseState.Y >= mySprites[i].position.Y * scale
                        && mouseState.Y <= (mySprites[i].position.Y * scale + mySprites[i].size.Y))
                        return true;
                }
                else if (f5)
                {
                    if (mouseState.X >= mySprites[i].position2.X * scale
                        && mouseState.X <= (mySprites[i].position2.X * scale + mySprites[i].size2.X)
                        && mouseState.Y >= mySprites[i].position2.Y * scale
                        && mouseState.Y <= (mySprites[i].position2.Y * scale + mySprites[i].size.Y))
                        return true;
                }
                else
                {
                    if (mouseState.X >= mySprites[i].position.X * scale
                        && mouseState.X <= (mySprites[i].position.X * scale + mySprites[i].size.X * scale)
                        && mouseState.Y >= mySprites[i].position.Y * scale
                        && mouseState.Y <= (mySprites[i].position.Y * scale + mySprites[i].size.Y * scale))
                        return true;
                }
            return false;
        }

        public bool IsMouseOnMySprite(MouseState mouseState, int i)
        {
            if (f1)
            {
                if (mouseState.X >= mySprites[i].position.X * scale
                    && mouseState.X <= (mySprites[i].position.X * scale + mySprites[i].size.X)
                    && mouseState.Y >= mySprites[i].position.Y * scale
                    && mouseState.Y <= (mySprites[i].position.Y * scale + mySprites[i].size.Y))
                    return true;
            }
            else if (f5)
            {
                if (mouseState.X >= mySprites[i].position2.X * scale
                    && mouseState.X <= (mySprites[i].position2.X * scale + mySprites[i].size2.X * scale)
                    && mouseState.Y >= mySprites[i].position2.Y * scale
                    && mouseState.Y <= (mySprites[i].position2.Y * scale + mySprites[i].size.Y * scale))
                    return true;
            }
            else
            {
                if (mouseState.X >= mySprites[i].position.X * scale
                    && mouseState.X <= (mySprites[i].position.X * scale + mySprites[i].size.X * scale)
                    && mouseState.Y >= mySprites[i].position.Y * scale
                    && mouseState.Y <= (mySprites[i].position.Y * scale + mySprites[i].size.Y * scale))
                    return true;
            }
            return false;
        }

        public bool IsVectorOnMySprites(Vector2 v2)
        {
            for (int i = 0; i < mySprites.Count; i++)
                if (v2.X >= mySprites[i].position.X * scale
                    && v2.X <= (mySprites[i].position.X * scale + mySprites[i].size.X * scale)
                    && v2.Y >= mySprites[i].position.Y * scale
                    && v2.Y <= (mySprites[i].position.Y * scale + mySprites[i].size.Y * scale))
                    return true;
            return false;
        }

        public bool IsPlaceForNewSprite(MouseState mouseState)
        {
            Vector2 v2 = new Vector2(mouseState.X, mouseState.Y);
            if (IsVectorOnMySprites(v2)
                || IsVectorOnMySprites(v2 + new Vector2(50, 0))
                || IsVectorOnMySprites(v2 + new Vector2(50, 15))
                || IsVectorOnMySprites(v2 + new Vector2(0, 15)))
                return false;
            return true;
        }

        public int MySpriteMouseOn(MouseState mouseState)
        {
            for (int i = 0; i < mySprites.Count; i++)
            {
                if (f1)
                {
                    if (mouseState.X >= mySprites[i].position.X * scale
                        && mouseState.X <= (mySprites[i].position.X * scale + mySprites[i].size.X)
                        && mouseState.Y >= mySprites[i].position.Y * scale
                        && mouseState.Y <= (mySprites[i].position.Y * scale + mySprites[i].size.Y))
                        return i;
                }
                else if (f5)
                {
                    if (mouseState.X >= mySprites[i].position2.X * scale
                        && mouseState.X <= (mySprites[i].position2.X * scale + mySprites[i].size2.X)
                        && mouseState.Y >= mySprites[i].position2.Y * scale
                        && mouseState.Y <= (mySprites[i].position2.Y * scale + mySprites[i].size.Y)
                        && mySprites[i].visible)
                        return i;
                }
                else
                {
                    if (mouseState.X >= mySprites[i].position.X * scale
                        && mouseState.X <= (mySprites[i].position.X * scale + mySprites[i].size.X * scale)
                        && mouseState.Y >= mySprites[i].position.Y * scale
                        && mouseState.Y <= (mySprites[i].position.Y * scale + mySprites[i].size.Y * scale))
                        return i;
                }
            }
            return -1;
        }

        public bool IsDescendantOfSprite(int parent, int i)
        {
            int j = i;
            
            while (mySprites[j].parent != -1)
            {
                if (mySprites[j].parent == parent)
                    return true;
                else
                    j = mySprites[j].parent;
            }
            return false;
        }

        public void FillDescendants(int parent)
        {
            selSprites.Clear();
            selSprites.Add(parent);
            for (int i = 0; i < mySprites.Count; i++)
                if (IsDescendantOfSprite(parent, i))
                    selSprites.Add(i);
        }

        public void LoadGoods()
        {
            string code, description, index;
            
            StreamReader sr = new StreamReader("goods.txt");
            int i = 0;
            while (!sr.EndOfStream)
            {
                code = sr.ReadLine();
                description = sr.ReadLine();
                index = sr.ReadLine();
                myGoods.Add(new good());
                myGoods[myGoods.Count - 1].code = code;
                myGoods[myGoods.Count - 1].description = description;
                myGoods[myGoods.Count - 1].texture = item;
                if (index != null)
                {
                    if (index.Length > 0)
                        myGoods[myGoods.Count - 1].index = Convert.ToInt16(index);
                    else
                        myGoods[myGoods.Count - 1].index = -1;
                }
                else
                    myGoods[myGoods.Count - 1].index = -1;
                if (myGoods[myGoods.Count - 1].index == currentSprite)
                    myGoods[myGoods.Count - 1].position = new Vector2(5, i * 16 + 15);
                myGoods[myGoods.Count - 1].size = new Vector2(370, 15);
                myGoods[myGoods.Count - 1].colour = Color.White;
                myGoods[myGoods.Count - 1].active = false;
                if (myGoods[myGoods.Count - 1].index == currentSprite)
                    i++;
            }
            sr.Close();
        }

        public void LoadReport(bool delta)
        {
            string ver, code, description, sum, percent = "0", qty = "0", percent2, dpercent = "0", dqty = "0";
            int i;
            bool found;
            int version = 1;
            bool firstLine = true;

            if (reportFile == null)
                return;
            StreamReader sr = new StreamReader(reportFile);
            //StreamReader sr = new StreamReader("report.txt");
            for (i = 0; i < myGoods.Count; i++)
            {
                if (!delta)
                {
                    myGoods[i].active = false;
                    myGoods[i].sum = 0;
                    myGoods[i].percent = 0;
                    myGoods[i].qty = 0;
                    myGoods[i].percent2 = 0;
                }
                else
                {
                    myGoods[i].dactive = false;
                    myGoods[i].dsum = 0;
                    myGoods[i].dpercent = 0;
                    myGoods[i].dqty = 0;
                    myGoods[i].dpercent2 = 0;
                }
            }
            while (!sr.EndOfStream)
            {
                if (firstLine)
                {
                    ver = sr.ReadLine();
                    if (ver == "2")
                    {
                        version = Convert.ToInt32(ver);
                        code = sr.ReadLine();
                    }
                    else
                        code = ver;
                    firstLine = false;
                }
                else
                {
                    code = sr.ReadLine();
                }
                description = sr.ReadLine();


                //Byte[] msg = Encoding.UTF8.GetBytes(description.ToCharArray());
                //msg = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, msg);
                //StringBuilder sb = new StringBuilder();
                //for (i = 0; i < msg.Length; i++)
                //    sb.Append(msg[i]);
                //description = sb.ToString();

                sum = sr.ReadLine();
                if (!delta)
                    percent = sr.ReadLine();
                else
                {
                    dpercent = sr.ReadLine();
                }

                if (version == 2)
                {
                    if (!delta)
                    {
                        qty = sr.ReadLine();
                        //percent2 = sr.ReadLine();
                    }
                    else
                    {
                        dqty = sr.ReadLine();
                        //dpercent2 = sr.ReadLine();
                    }
                }
                else
                {
                    if (!delta)
                    {
                        qty = "0";
                        //percent2 = "0";
                    }
                    else
                    {
                        dqty = "0";
                        //dpercent2 = "0";
                    }
                }
                found = false;
                for (i = 0; i < myGoods.Count; i++)
                {
                    if (myGoods[i].code == code)
                    {
                        myGoods[i].active = true;
                        if (!delta)
                        {
                            myGoods[i].sum = (float)Convert.ToDouble(sum);
                            myGoods[i].percent = (float)Convert.ToDouble(percent);
                            if (version == 2)
                            {
                                myGoods[i].qty = Convert.ToInt32(qty);
                                //myGoods[i].percent2 = (float)Convert.ToDouble(percent2);
                            }
                        }
                        else
                        {
                            myGoods[i].dsum = (float)Convert.ToDouble(sum);
                            myGoods[i].dpercent = (float)Convert.ToDouble(dpercent);
                            if (version == 2)
                            {
                                myGoods[i].dqty = Convert.ToInt32(dqty);
                                //myGoods[i].dpercent2 = (float)Convert.ToDouble(percent2);
                            }
                        }
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    //StreamWriter sw = new StreamWriter("goods.txt", true);
                    //sw.WriteLine(code);
                    //sw.WriteLine(description);
                    //sw.WriteLine("-1");
                    //sw.Close();
                    myGoods.Add(new good());
                    myGoods[myGoods.Count - 1].code = code;
                    myGoods[myGoods.Count - 1].description = description;
                    myGoods[myGoods.Count - 1].index = -1;
                    myGoods[myGoods.Count - 1].texture = item;
                    myGoods[myGoods.Count - 1].size = new Vector2(370, 15);
                    myGoods[myGoods.Count - 1].colour = Color.White;
                    myGoods[myGoods.Count - 1].active = true;
                    if (!delta)
                        myGoods[myGoods.Count - 1].active = true;
                    else
                        myGoods[myGoods.Count - 1].dactive = true;
                }
            }
            sr.Close();
            RefreshReport();
            CheckLevel();
            float totalQty = 0.0f;
            for (i = 0; i < myGoods.Count; i++)
            {
                totalQty += myGoods[i].qty;
            }
            for (i = 0; i < myGoods.Count; i++)
            {
                myGoods[i].percent2 = (float)Convert.ToDouble(myGoods[i].qty / (totalQty / 100));
            }
            float dtotalQty = 0.0f;
            for (i = 0; i < myGoods.Count; i++)
            {
                dtotalQty += myGoods[i].dqty;
            }
            for (i = 0; i < myGoods.Count; i++)
            {
                myGoods[i].dpercent2 = (float)Convert.ToDouble(myGoods[i].dqty / (dtotalQty / 100));
            }
            RefreshReport();
            CheckLevel();
            reporting = false;
        }

        public void RefreshReport()
        {
            int i;
            int cur;

            for (i = 0; i < mySprites.Count; i++)
            {
                mySprites[i].sum = 0;
                mySprites[i].percent = 0;
                mySprites[i].qty = 0;
                mySprites[i].qtyPercent = 0;

                mySprites[i].dsum = 0;
                mySprites[i].dpercent = 0;
                mySprites[i].dqty = 0;
                mySprites[i].dqtyPercent = 0;
                
                mySprites[i].active = false;
            }

            for (i = 0; i < myGoods.Count; i++)
            {
                if (myGoods[i].active)
                {
                    if (myGoods[i].index != -1)
                    {
                        mySprites[myGoods[i].index].sum += myGoods[i].sum;
                        mySprites[myGoods[i].index].percent += myGoods[i].percent;

                        mySprites[myGoods[i].index].dsum += myGoods[i].dsum;
                        mySprites[myGoods[i].index].dpercent += myGoods[i].dpercent;
                        
                        //  тест на отрицательный процент
                        if (myGoods[i].percent < 0)
                            WinForms.MessageBox.Show(myGoods[i].description + " " + myGoods[i].code);

                        mySprites[myGoods[i].index].qty += myGoods[i].qty;
                        mySprites[myGoods[i].index].qtyPercent += myGoods[i].percent2;

                        mySprites[myGoods[i].index].dqty += myGoods[i].dqty;
                        mySprites[myGoods[i].index].dqtyPercent += myGoods[i].dpercent2;

                        mySprites[myGoods[i].index].active = true;
                        cur = myGoods[i].index;
                        while (mySprites[cur].parent != -1)
                        {
                            cur = mySprites[cur].parent;
                            mySprites[cur].sum += myGoods[i].sum;
                            mySprites[cur].percent += myGoods[i].percent;

                            mySprites[cur].dsum += myGoods[i].dsum;
                            mySprites[cur].dpercent += myGoods[i].dpercent;
                            
                            //  тест на отрицательный процент
                            if (myGoods[i].percent < 0)
                                WinForms.MessageBox.Show(myGoods[i].description + " " + myGoods[i].code);
                            mySprites[cur].qty += myGoods[i].qty;
                            mySprites[cur].qtyPercent += myGoods[i].percent2;

                            mySprites[cur].dqty += myGoods[i].dqty;
                            mySprites[cur].dqtyPercent += myGoods[i].dpercent2;

                            mySprites[cur].active = true;
                        }
                    }
                }
            }

            //  в следующем цикле необходимо в зависимости от настроек отображать не только % суммы, но и % количества
            for (i = 0; i < mySprites.Count; i++)
            {
                if (!f7)
                {
                    if (mySprites[i].parent != -1)
                    {
                        if (mySprites[mySprites[i].parent].sum != 0)
                            mySprites[i].percent2 = mySprites[i].sum / (mySprites[mySprites[i].parent].sum / 100);
                        else
                            mySprites[i].percent2 = 0;

                        if (mySprites[mySprites[i].parent].dsum != 0)
                            mySprites[i].dpercent2 = mySprites[i].dsum / (mySprites[mySprites[i].parent].dsum / 100);
                        else
                            mySprites[i].dpercent2 = 0;
                    }
                    else
                    {
                        mySprites[i].percent2 = mySprites[i].percent;

                        mySprites[i].dpercent2 = mySprites[i].dpercent;
                    }
                }
                else
                {
                    if (mySprites[i].parent != -1)
                    {
                        if ((float)(mySprites[mySprites[i].parent].qty / 100) != 0.0f)
                            mySprites[i].percent2 = (float)(mySprites[i].qty) / ((float)(mySprites[mySprites[i].parent].qty / 100));
                        else
                            mySprites[i].percent2 = 0;

                        if ((float)(mySprites[mySprites[i].parent].dqty / 100) != 0.0f)
                            mySprites[i].dpercent2 = (float)(mySprites[i].dqty) / ((float)(mySprites[mySprites[i].parent].dqty / 100));
                        else
                            mySprites[i].dpercent2 = 0;
                    }
                    else
                    {
                        mySprites[i].percent2 = mySprites[i].qtyPercent;

                        mySprites[i].dpercent2 = mySprites[i].dqtyPercent;
                    }
                }
            }
        }

        public void RefreshGoodsList(int delta)
        {
            int i = 0;
            int z;
            int previousSprite, nextSprite;

            if (currentSprite == -1)
                z = firstLine;
            else
                z = mySprites[currentSprite].firstLine;
            previousSprite = z;
            nextSprite = z;
            for (i = z - 1; i >= 0; i--)
            {
                if (myGoods[i].index == currentSprite)
                {
                    previousSprite = i;
                    break;
                }
                previousSprite = z;
            }
            for (i = z + 1; i < myGoods.Count; i++)
            {
                if (myGoods[i].index == currentSprite)
                {
                    nextSprite = i;
                    break;
                }
                nextSprite = z;
            }
            if (delta == 1)
            {
                if (currentSprite == -1)
                    firstLine = nextSprite;
                else
                    mySprites[currentSprite].firstLine = nextSprite;
            }
            else if (delta == -1)
            {
                if (currentSprite == -1)
                    firstLine = previousSprite;
                else
                    mySprites[currentSprite].firstLine = previousSprite;
            }

            i = 0;
            if (currentSprite == -1)
                z = firstLine;
            else
                z = mySprites[currentSprite].firstLine;
            for (int j = z; j < myGoods.Count; j++)
            {
                if (myGoods[j].index == currentSprite)
                {
                    myGoods[j].position = new Vector2(5, i * 16 + 15);
                    myGoods[j].oldPosition = new Vector2(5, i * 16 + 15);
                    i++;
                    //if (i > Convert.ToInt16(Math.Round(graphics.PreferredBackBufferHeight / 19f)) /*42*/)
                    if (i > Convert.ToInt16(Math.Round(graphics.GraphicsDevice.DisplayMode.Height / 19f)) /*42*/)
                        break;
                }
            }
        }

        public bool IsMouseOnMyGood(MouseState mouseState, int i)
        {
            if (mouseState.X - (graphics.PreferredBackBufferWidth - 380) >= myGoods[i].position.X
                && mouseState.X - (graphics.PreferredBackBufferWidth - 380) <= (myGoods[i].position.X + myGoods[i].size.X)
                && mouseState.Y >= myGoods[i].position.Y
                && mouseState.Y <= (myGoods[i].position.Y + myGoods[i].size.Y))
                return true;
            return false;
        }

        private void cmd_Enter(object sender, EventArgs e)
        {
            onButton = true;
            //WinForms.MessageBox.Show(string.Format("Entered button {0}", sender));
        }

        private void cmd_Leave(object sender, EventArgs e)
        {
            onButton = false;
            //WinForms.MessageBox.Show(string.Format("Left button {0}", sender));
        }

        private void cmsOpening(object sender, EventArgs e)
        {
            contextMenu = true;
        }

        private void cmsClosing(object sender, EventArgs e)
        {
            contextMenu = false;
        }

        void cmsSprites_ItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == cmsSprites.Items[0])
            {
                for (int zz = 0; zz < selGoods.Count; zz++)
                {
                    myGoods[selGoods[zz]].index = f5_i;
                    myGoods[selGoods[zz]].position = myGoods[selGoods[zz]].oldPosition;
                }
                selGoods.Clear();
                ig = -1;
                RefreshGoodsList(0);
                RefreshReport();
            }
            if (e.ClickedItem == cmsSprites.Items[1])
            {
                isAdding = true;
                mySprites.Add(new clsSprite(texture, new Vector2(mouseState.X / scale, mouseState.Y / scale), new Vector2(50, 15)));
                mySprites[mySprites.Count - 1].parent = -1;
                catName = "NONAME";
                mySprites[mySprites.Count - 1].catName = catName;
                StreamWriter sw = new StreamWriter("catname.txt");
                sw.WriteLine(catName);
                sw.Close();
                inputForm.ShowDialog();
                StreamReader sr = new StreamReader("catname.txt");
                catName = sr.ReadLine();
                sr.Close();
                mySprites[mySprites.Count - 1].catName = catName;
                isAdding = false;
                if (catName == "" || catName == "NONAME")
                    mySprites.RemoveAt(mySprites.Count - 1);
                else
                {
                    mySprites[mySprites.Count - 1].parent = f5_i;
                    CheckLevel();
                    CheckVisibility(f5_i);
                }
            }
            else if (e.ClickedItem == cmsSprites.Items[2])
            {
                isAdding = true;
                mySprites.Add(new clsSprite(texture, new Vector2(mouseState.X / scale, mouseState.Y / scale), new Vector2(50, 15)));
                mySprites[mySprites.Count - 1].parent = -1;
                catName = "NONAME";
                mySprites[mySprites.Count - 1].catName = catName;
                StreamWriter sw = new StreamWriter("catname.txt");
                sw.WriteLine(catName);
                sw.Close();
                inputForm.ShowDialog();
                StreamReader sr = new StreamReader("catname.txt");
                catName = sr.ReadLine();
                sr.Close();
                mySprites[mySprites.Count - 1].catName = catName;
                isAdding = false;
                if (catName == "" || catName == "NONAME")
                    mySprites.RemoveAt(mySprites.Count - 1);
                else
                {
                    mySprites[mySprites.Count - 1].parent = mySprites[f5_i].parent;
                    CheckLevel();
                    CheckVisibility(f5_i);
                }
            }
            else if (e.ClickedItem == cmsSprites.Items[3])
            {
                if (!isAdding)
                {
                    isAdding = true;
                    catName = mySprites[f5_i].catName;
                    StreamWriter sw = new StreamWriter("catname.txt");
                    sw.WriteLine(catName);
                    sw.Close();
                    inputForm.ShowDialog();
                    StreamReader sr = new StreamReader("catname.txt");
                    catName = sr.ReadLine();
                    sr.Close();
                    mySprites[f5_i].catName = catName;
                    isAdding = false;
                }
            }
            else if (e.ClickedItem == cmsSprites.Items[4])
            {
                float zs = 0;
                for (int x = 0; x < myGoods.Count; x++)
                {
                    if (myGoods[x].index == f5_i)
                        zs += myGoods[x].sum;
                }
                if (WinForms.MessageBox.Show(Convert.ToString(Convert.ToDecimal(zs)) + " Удалить категорию и все ее связи?", "Удаление категории", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                //if (WinForms.MessageBox.Show(Convert.ToString(Convert.ToDecimal(zs)) + " You're about to remove category and its links permanently?", "Deleting", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    for (int j = 0; j < mySprites.Count; j++)
                    {
                        if (mySprites[j].parent == f5_i)
                            mySprites[j].parent = -1;
                        else if (mySprites[j].parent > f5_i)
                            mySprites[j].parent--;
                    }
                    for (int j = 0; j < myGoods.Count; j++)
                    {
                        if (myGoods[j].index == f5_i)
                            myGoods[j].index = -1;
                        else if (myGoods[j].index > f5_i)
                            myGoods[j].index--;
                    }
                    mySprites.RemoveAt(f5_i);
                    f5_i = MySpriteMouseOn(mouseState);
                    CheckLevel();
                    CheckVisibility(f5_i);
                    currentSprite = -1;
                }
            }
        }

        private int GetMiddleSprite()
        {
            float minX = 7777777, minY = 7777777, maxX = -7777777, maxY = -7777777;
            int i;
            for (i = 0; i < mySprites.Count; i++)
            {
                if (mySprites[i].parent != -1)
                    if (mySprites[mySprites[i].parent].parent != -1)
                        if (mySprites[mySprites[mySprites[i].parent].parent].parent != -1)
                            continue;
                if (mySprites[i].position.X < minX)
                    minX = mySprites[i].position.X;
                if (mySprites[i].position.Y < minY)
                    minY = mySprites[i].position.Y;
                if (mySprites[i].position.X + mySprites[i].size.X > maxX)
                    maxX = mySprites[i].position.X + mySprites[i].size.X;
                if (mySprites[i].position.Y + mySprites[i].size.Y > maxY)
                    maxY = mySprites[i].position.Y + mySprites[i].size.Y;
            }
            float midX = (minX + maxX) / 2, midY = (minY + maxY) / 2;
            int midS = 0;
            float minD = 7777777, delta;
            for (i = 0; i < mySprites.Count; i++)
            {
                if (mySprites[i].parent != -1)
                    if (mySprites[mySprites[i].parent].parent != -1)
                        if (mySprites[mySprites[mySprites[i].parent].parent].parent != -1)
                            continue;
                delta = Math.Abs(mySprites[i].position.X + mySprites[i].size.X / 2 - midX)
                    + Math.Abs(mySprites[i].position.Y + mySprites[i].size.Y / 2 - midY);
                if (Math.Abs(delta) < minD)
                {
                    minD = Math.Abs(delta);
                    midS = i;
                }
            }
            return midS;
        }

        public static Rectangle GetTitleSafeArea(GraphicsDevice graphicsDevice,
        float percent)
        {
            Rectangle retval = new Rectangle(graphicsDevice.Viewport.X,
            graphicsDevice.Viewport.Y,
            graphicsDevice.Viewport.Width,
            graphicsDevice.Viewport.Height);
            return retval;
        }

        protected override void Initialize()
        {
            #region 3D definitions
            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width /
                (float)graphics.GraphicsDevice.Viewport.Height;
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio,
                1.0f, 1000.0f, out projection);
            Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget,
                ref cameraUpVector, out view);
            world = Matrix.Identity;
            #endregion

            #region ButtonsLabelsDefinition
            cmdSave = new WinForms.Button();
            cmdSave.Left = 0;
            cmdSave.Width = 60;
            cmdSave.Height = 30;
            cmdSave.Text = "ЗАПИСЬ\nСХЕМЫ";
            cmdSave.Click += new EventHandler(cmdSave_Click);
            cmdSave.Enter += new EventHandler(cmd_Enter);
            cmdSave.Leave += new EventHandler(cmd_Leave);

            cmdLoad = new WinForms.Button();
            cmdLoad.Left = 60;
            cmdLoad.Width = 70;
            cmdLoad.Height = 30;
            cmdLoad.Text = "ЗАГРУЗКА\nСХЕМЫ";
            cmdLoad.Click += new EventHandler(cmdLoad_Click);
            //cmdLoad.Enter += new EventHandler(cmd_Enter);
            //cmdLoad.Leave += new EventHandler(cmd_Leave);

            cmdClear = new WinForms.Button();
            cmdClear.Left = 130;
            cmdClear.Width = 70;
            cmdClear.Height = 30;
            cmdClear.Text = "ОЧИСТКА\nСХЕМЫ";
            cmdClear.Click += new EventHandler(cmdClear_Click);

            cmdZoomIn = new WinForms.Button();
            cmdZoomIn.Left = 200;
            cmdZoomIn.Width = 50;
            cmdZoomIn.Text = "ЗУМ +";
            cmdZoomIn.Click += new EventHandler(cmdZoomIn_Click);

            cmdZoomOut = new WinForms.Button();
            cmdZoomOut.Left = 250;
            cmdZoomOut.Width = 50;
            cmdZoomOut.Text = "ЗУМ -";
            cmdZoomOut.Click += new EventHandler(cmdZoomOut_Click);

            cmdViewport = new WinForms.Button();
            cmdViewport.Left = 300;
            cmdViewport.Width = 65;
            cmdViewport.Text = "СПИСОК";
            cmdViewport.Click += new EventHandler(cmdViewport_Click);

            cmdReport = new WinForms.Button();
            cmdReport.Left = 365;
            cmdReport.Width = 70;
            cmdReport.Height = 30;
            cmdReport.Text = "ЗАГРУЗКА\nОТЧЕТА";
            cmdReport.Click += new EventHandler(cmdReport_Click);

            cmdDReport = new WinForms.Button();
            cmdDReport.Left = 435;
            cmdDReport.Width = 75;
            cmdDReport.Height = 30;
            cmdDReport.Text = "ДВОЙНОЙ\nОТЧЕТ";
            cmdDReport.Click += new EventHandler(cmdDReport_Click);

            cmdImage = new WinForms.Button();
            cmdImage.Left = 510;
            cmdImage.Width = 50;
            cmdImage.Text = "ФОТО";
            cmdImage.Click += new EventHandler(cmdImage_Click);

            cmdSortByCode = new WinForms.Button();
            cmdSortByCode.Left = 560;
            cmdSortByCode.Width = 40;
            cmdSortByCode.Text = "КОД";
            cmdSortByCode.Click += new EventHandler(cmdSortByCode_Click);

            cmdSortByDescription = new WinForms.Button();
            cmdSortByDescription.Left = 600;
            cmdSortByDescription.Width = 65;
            cmdSortByDescription.Text = "НАИМ-ИЕ";
            cmdSortByDescription.Click += new EventHandler(cmdSortByDescription_Click);

            cmdSortByDefault = new WinForms.Button();
            cmdSortByDefault.Left = 665;
            cmdSortByDefault.Width = 85;
            cmdSortByDefault.Text = "УМОЛЧАНИЕ";
            cmdSortByDefault.Click += new EventHandler(cmdSortByDefault_Click);

            lblFPS = new WinForms.Label();
            lblFPS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblFPS.Left = 750;
            lblFPS.Text = "NO CATEGORY";
            lblFPS.BackColor = System.Drawing.Color.Yellow;
            lblFPS.ForeColor = System.Drawing.Color.Blue;
            lblFPS.Click += new EventHandler(lblFPS_Click);
            lblFPS.Visible = true;

            cmdClick = new WinForms.Button();
            cmdClick.Left = 851;
            cmdClick.Width = 45;
            cmdClick.Text = "Excel";
            cmdClick.Click += new EventHandler(cmdClick_Click);
            //cmdClick.Visible = false;

            cmdQtySum = new WinForms.Button();
            cmdQtySum.Left = 896;
            cmdQtySum.Width = 40;
            cmdQtySum.Text = "КОЛ";
            cmdQtySum.Click += new EventHandler(cmdQtySum_Click);
            //cmdQtySum.Visible = false;
#endregion

            cmsSprites = new WinForms.ContextMenuStrip();
            cmsSprites.Items.Add("Move selected goods into current category");
            cmsSprites.Items.Add("Add child category");
            cmsSprites.Items.Add("Add current level category");
            cmsSprites.Items.Add("Rename current category");
            cmsSprites.Items.Add("Delete category (child categories will be moved to the root)");
            cmsSprites.Opening += new System.ComponentModel.CancelEventHandler(cmsOpening);
            cmsSprites.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(cmsClosing);
            cmsSprites.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(cmsSprites_ItemClicked);

            WinForms.Control mainWindow = WinForms.Control.FromHandle(Window.Handle);
            mainWindow.Controls.Add(lblFPS);
            mainWindow.Controls.Add(cmdClick);
            mainWindow.Controls.Add(cmdSave);
            mainWindow.Controls.Add(cmdLoad);
            mainWindow.Controls.Add(cmdClear);
            mainWindow.Controls.Add(cmdZoomIn);
            mainWindow.Controls.Add(cmdZoomOut);
            mainWindow.Controls.Add(cmdViewport);
            mainWindow.Controls.Add(cmdReport);
            mainWindow.Controls.Add(cmdDReport);
            mainWindow.Controls.Add(cmdImage);
            mainWindow.Controls.Add(cmdSortByCode);
            mainWindow.Controls.Add(cmdSortByDescription);
            mainWindow.Controls.Add(cmdSortByDefault);
            mainWindow.Controls.Add(cmdQtySum);

            mouseState = Mouse.GetState();
            prevMouseState = mouseState;

            vectors = new List<Vector2>();
            selSprites = new List<int>();
            selGoods = new List<int>();

            oFD.FileOk += new System.ComponentModel.CancelEventHandler(oFD_FileOk);

            Viewport vp = new Viewport();
            vp.X = 0;
            vp.Y = 0;
            vp.Width = 100;
            vp.Height = 100;

            //fps = new FPS(this);
            //Components.Add(fps);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //graphics.GraphicsDevice.RenderState.CullMode = CullMode.None;

            graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width - 7;
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height - 57;
            graphics.ApplyChanges();
            WinForms.Control mainWindow = WinForms.Control.FromHandle(Window.Handle);
            mainWindow.Left = 0;
            mainWindow.Top = 0;
            IsMouseVisible = true;
            mySprites = new List<clsSprite>();
            texture = Content.Load<Texture2D>("catfolder");
            texturesel = Content.Load<Texture2D>("catfoldersel");
            item = Content.Load<Texture2D>("item");
            itemsel = Content.Load<Texture2D>("itemsel");
            scrollup = Content.Load<Texture2D>("scrollup");
            scrolldown = Content.Load<Texture2D>("scrolldown");
            infotexture = Content.Load<Texture2D>("info");
            pleasewait = Content.Load<Texture2D>("pleasewait");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            originVP = graphics.GraphicsDevice.Viewport;

            rightVP = graphics.GraphicsDevice.Viewport;
            rightVP.Width = 380;
            rightVP.Height = graphics.PreferredBackBufferHeight;
            rightVP.X = graphics.PreferredBackBufferWidth - 380;

            leftVP = graphics.GraphicsDevice.Viewport;
            leftVP.Width = graphics.PreferredBackBufferWidth - 380;

            tinyFont = Content.Load<SpriteFont>("tinyfont");
            catFont = Content.Load<SpriteFont>("SpriteFont1");
            middleFont = Content.Load<SpriteFont>("spritefont9");
            gameFont = Content.Load<SpriteFont>("font");

            #region 3D
            effect = new BasicEffect(graphics.GraphicsDevice);
            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / graphics.GraphicsDevice.Viewport.Height;
            effect.View = Matrix.CreateLookAt(new Vector3(0.0f, 2.0f, 2.0f), Vector3.Zero,
                                  Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                               MathHelper.ToRadians(45.0f),
                               aspectRatio, 1.0f, 10.0f);
            effect.LightingEnabled = false;

            VertexPositionColor[] vertices = new VertexPositionColor[2];
            vertices[0] = new VertexPositionColor(new Vector3(-1, 0.0f, -1.0f), Color.White);
            vertices[1] = new VertexPositionColor(new Vector3(1, 0.0f, -1.0f), Color.White);
            vertexBuffer = new VertexBuffer(graphics.GraphicsDevice, 2 * VertexPositionColor.SizeInBytes,
                                            BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(vertices);
            #endregion

            pixel = new Texture2D(graphics.GraphicsDevice, 1, 1, 1, TextureUsage.None, SurfaceFormat.Color);
            Color[] pixels = new Color[1];
            colour = Color.White;
            pixels[0] = colour;
            pixel.SetData<Color>(pixels);

            myGoods = new List<good>();

            GraphicsDevice device = graphics.GraphicsDevice;

            titleSafeArea = GetTitleSafeArea(device, .8f);
            hotSpotTexture = CreateTexture(4, 1);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fireEffect = Content.Load<Effect>(@"Effects\Fire");

            renderTarget1 = new RenderTarget2D(device, device.Viewport.Width,
                device.Viewport.Height / 2 + 150, 1, device.DisplayMode.Format,
                device.PresentationParameters.MultiSampleType,
                device.PresentationParameters.MultiSampleQuality);

            renderTarget2 = new RenderTarget2D(device, device.Viewport.Width,
                device.Viewport.Height / 2 + 150, 1, device.DisplayMode.Format,
                device.PresentationParameters.MultiSampleType,
                device.PresentationParameters.MultiSampleQuality);

            fire = null;
        }

        private Texture2D CreateTexture(int width, int height)
        {
            Texture2D texture = new Texture2D(graphics.GraphicsDevice, width, height, 1,
                TextureUsage.None, SurfaceFormat.Color);

            int pixelCount = width * height;
            Color[] pixelData = new Color[pixelCount];

            for (int i = 0; i < pixelCount; i++)
                pixelData[i] = Color.White;

            texture.SetData(pixelData);

            return (texture);
        }

        protected override void UnloadContent()
        {
            spriteBatch.Dispose();

            renderTarget1.Dispose();
            renderTarget2.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            int i;

            if (IsActive == false)
            {
                IsMouseVisible = false;
                return;
            }
            else
            {
                if (!IsMouseVisible)
                    IsMouseVisible = true;
            }
            if (inputForm.Visible)
                return;
            //if (onButton)
            //    return;

            if (f5)
            {
                cmdZoomIn.Enabled = false;
                cmdZoomOut.Enabled = false;
                //cmdReport.Enabled = false;
            }
            else
            {
                cmdZoomIn.Enabled = true;
                cmdZoomOut.Enabled = true;
                //cmdReport.Enabled = true;
            }

            KeyboardState keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            #region Keyboard
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                if (WinForms.MessageBox.Show("Завершить работу приложения без сохранения?", "Завершение работы", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                //if (WinForms.MessageBox.Show("Are you sure you want to exit?", "Finishing application", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    this.Exit();
            }
            for (i = 0; i < mySprites.Count; i++)
            {
                if (keyboardState.IsKeyDown(Keys.Down))
                    if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                    {
                        if (!f5)
                            mySprites[i].position.Y += 20f;
                        //else
                            // mySprites[i].position2.Y += 20f;
                    }
                    else
                    {
                        if (!f5)
                            mySprites[i].position.Y += 3f;
                        //else
                            // mySprites[i].position2.Y += 3f;
                    }
                if (keyboardState.IsKeyDown(Keys.Up))
                    if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                    {
                        if (!f5)
                            mySprites[i].position.Y -= 20f;
                        //else
                            // mySprites[i].position2.Y -= 20f;
                    }
                    else
                    {
                        if (!f5)
                            mySprites[i].position.Y -= 3f;
                        //else
                            // mySprites[i].position2.Y -= 3f;
                    }
                if (keyboardState.IsKeyDown(Keys.Right))
                    if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                    {
                        if (!f5)
                            mySprites[i].position.X += 20f;
                        else
                            mySprites[i].position2.X += 20f;
                    }
                    else
                    {
                        if (!f5)
                            mySprites[i].position.X += 3f;
                        else
                            mySprites[i].position2.X += 3f;
                    }
                if (keyboardState.IsKeyDown(Keys.Left))
                    if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                    {
                        if (!f5)
                            mySprites[i].position.X -= 20f;
                        else
                            mySprites[i].position2.X -= 20f;
                    }
                    else
                    {
                        if (!f5)
                            mySprites[i].position.X -= 3f;
                        else
                            mySprites[i].position2.X -= 3f;
                    }
            }
            for (i = 0; i < vectors.Count; i++)
            {
                if (keyboardState.IsKeyDown(Keys.Down))
                    if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                        vectors[i] += new Vector2(0, 20f);
                    else
                        vectors[i] += new Vector2(0, 3f);
                if (keyboardState.IsKeyDown(Keys.Up))
                    if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                        vectors[i] -= new Vector2(0, 20f);
                    else
                        vectors[i] -= new Vector2(0, 3f);
                if (keyboardState.IsKeyDown(Keys.Right))
                    if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                        vectors[i] += new Vector2(20f, 0);
                    else
                        vectors[i] += new Vector2(3f, 0);
                if (keyboardState.IsKeyDown(Keys.Left))
                    if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                        vectors[i] -= new Vector2(20f, 0);
                    else
                        vectors[i] -= new Vector2(3f, 0);
            }
            if (keyboardState.IsKeyUp(Keys.LeftControl) && keyboardState.IsKeyUp(Keys.RightControl))
            {
                if (nodeIsDrawing)
                {
                    nodeIsDrawing = false;
                    i = MySpriteMouseOn(mouseState);
                    if (i == -1)
                    {
                        vectors.RemoveAt(vectors.Count - 1);
                        vectors.RemoveAt(vectors.Count - 1);
                    }
                    else
                    {
                        mySprites[i].parent = parent;
                        parent = -1;
                        vectors.RemoveAt(vectors.Count - 1);
                        vectors.RemoveAt(vectors.Count - 1);
                    }
                }
            }
            if (keyboardState.IsKeyDown(Keys.F11))
            {
                angle -= 0.00001f;
            }
            if (keyboardState.IsKeyDown(Keys.F12))
            {
                angle += 0.00001f;
            }
            if (keyboardState.IsKeyDown(Keys.F1) && prevKeyboardState.IsKeyUp(Keys.F1))
            {
                f1 = !f1;
                if (f1)
                {
                    if (f2)
                    {
                        scale = prevScale;
                        f2 = false;
                    }
                    prevScale = scale;
                    scale = 0.3f;

                    int midS = GetMiddleSprite();
                    dx = (graphics.GraphicsDevice.DisplayMode.Width / 2 - mySprites[midS].position.X);
                    dx += (mySprites[midS].position.X + dx) + graphics.GraphicsDevice.DisplayMode.Width / 2;
                    dy = (graphics.GraphicsDevice.DisplayMode.Height / 2 - mySprites[midS].position.Y);
                    dy += (mySprites[midS].position.Y + dy) + graphics.GraphicsDevice.DisplayMode.Height / 2;
                    for (int j = 0; j < mySprites.Count; j++)
                    {
                        mySprites[j].position.X += dx;
                        mySprites[j].position.Y += dy;
                    }
                }
                else
                    scale = prevScale;
            }
            if (keyboardState.IsKeyDown(Keys.F2) && prevKeyboardState.IsKeyUp(Keys.F2))
            {
                //f2 = !f2;
                //if (f2)
                //{
                //    if (f1)
                //    {
                //        scale = prevScale;
                //        f1 = false;
                //    }
                //    prevScale = scale;
                //    scale = 0.5f;
                //}
                //else
                //    scale = prevScale;
            }
            if (keyboardState.IsKeyDown(Keys.F5) && prevKeyboardState.IsKeyUp(Keys.F5))
            {
                f5 = !f5;
            }
            if (keyboardState.IsKeyDown(Keys.F7) && prevKeyboardState.IsKeyUp(Keys.F7))
            {
                f7 = !f7;
                RefreshReport();
                CheckLevel();
            }
            if (keyboardState.IsKeyDown(Keys.F) && prevKeyboardState.IsKeyUp(Keys.F))
            {
                fireIsOn = !fireIsOn;
            }
            #endregion

            #region selection
            for (i = 0; i < mySprites.Count; i++)
            {
                if ((mouseState.X < (graphics.PreferredBackBufferWidth - 380) && portable) || !portable)
                {
                    if (IsMouseOnMySprite(mouseState, i))
                    {
                        if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
                        {
                            FillDescendants(i);
                            for (int j = 0; j < selSprites.Count; j++)
                                mySprites[selSprites[j]].texture = texturesel;
                            break;
                        }
                        else
                        {
                            if (!contextMenu)   //  если не поп-ап меню
                            {
                                if (!freezed)   //  если не зафиксировано
                                {
                                    mySprites[i].texture = texturesel;
                                    if (f5 && mySprites[i].visible)
                                        CheckVisibility(i);
                                }
                            }
                        }
                    }
                    else
                    {
                        mySprites[i].texture = texture;
                    }
                }
                else
                {
                    mySprites[i].texture = texture;
                }
            }
            #endregion

            if ((portable && mouseState.X < (graphics.PreferredBackBufferWidth - 380)) || !portable)
            {
                #region new sprite
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    if (!f5)
                    {
                        if (!isAdding)
                        {
                            if (IsPlaceForNewSprite(mouseState))
                            {
                                isAdding = true;
                                mySprites.Add(new clsSprite(texture, new Vector2(mouseState.X / scale, mouseState.Y / scale), new Vector2(50, 15)));
                                mySprites[mySprites.Count - 1].parent = -1;
                                catName = "NONAME";
                                mySprites[mySprites.Count - 1].catName = catName;
                                StreamWriter sw = new StreamWriter("catname.txt");
                                sw.WriteLine(catName);
                                sw.Close();
                                inputForm.ShowDialog();
                                StreamReader sr = new StreamReader("catname.txt");
                                catName = sr.ReadLine();
                                sr.Close();
                                mySprites[mySprites.Count - 1].catName = catName;
                                isAdding = false;
                                if (catName == "" || catName == "NONAME")
                                    mySprites.RemoveAt(mySprites.Count - 1);
                                else
                                {
                                    CheckLevel();
                                    CheckVisibility(mySprites.Count - 1);
                                }
                            }
                        }
                    }
                    else // if (f5)
                    {
                        i = MySpriteMouseOn(mouseState);
                        f5_i = i;
                        if (i != -1)
                        {
                            mySprites[i].texture = texturesel;
                            cmsSprites.Show(mouseState.X, mouseState.Y);
                        }
                    }
                }
                #endregion

                i = MySpriteMouseOn(mouseState);

                #region Mouse
                if (mouseState.MiddleButton == ButtonState.Pressed)
                {
                    if (prevMouseState.MiddleButton == ButtonState.Released)
                    {
                        if (f5)
                        {
                            if (IsMouseOnMySprites(mouseState))
                                freezed = !freezed;
                        }
                    }
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (prevMouseState.LeftButton == ButtonState.Released)
                    {
                        //  left freeze
                        //if (f5)
                        //{
                        //    if (IsMouseOnMySprites(mouseState))
                        //        freezed = !freezed;
                        //}
                        if (!keyboardState.IsKeyDown(Keys.LeftControl) && !keyboardState.IsKeyDown(Keys.RightControl))
                        {
                            if (i != -1)
                            {
                                if (!f1)
                                {
                                    if (elapsedTime.Milliseconds < 200)
                                    {
                                        elapsedTime = TimeSpan.Zero;
                                        currentSprite = i;
                                        RefreshGoodsList(0);
                                    }
                                    mySprites[i].pressed = true;
                                    mySprites[i].released = false;
                                }
                                else
                                {
                                    f1 = false;
                                    scale = prevScale;
                                    dx = (graphics.GraphicsDevice.DisplayMode.Width / 2 - (mySprites[i].position.X + mySprites[i].size.X / 2));
                                    dy = (graphics.GraphicsDevice.DisplayMode.Height / 2 - mySprites[i].position.Y + mySprites[i].size.Y / 2);
                                    for (int j = 0; j < mySprites.Count; j++)
                                    {
                                        mySprites[j].position.X += dx;
                                        mySprites[j].position.Y += dy;
                                    }
                                }
                            }
                        }
                        else // if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                        {
                            if (i != -1)
                            {
                                parent = i;
                                nodeIsDrawing = true;
                                vectors.Add(new Vector2(mouseState.X, mouseState.Y));
                                vectors.Add(new Vector2(mouseState.X, mouseState.Y));
                            }
                        }
                        if (keyboardState.IsKeyDown(Keys.LeftAlt) || keyboardState.IsKeyDown(Keys.RightAlt))
                        {
                            if (i != -1)
                            {
                                for (int j = 0; j < mySprites.Count; j++)
                                    if (mySprites[j].parent == i)
                                    {
                                        mySprites[j].parent = -1;
                                        CheckLevel();
                                        CheckVisibility(j);
                                    }
                            }
                        }
                        else if (keyboardState.IsKeyDown(Keys.Delete))
                        {
                            if (i != -1)
                            {
                                float zs = 0;
                                for (int x = 0; x < myGoods.Count; x++)
                                {
                                    if (myGoods[x].index == i)
                                        zs += myGoods[x].sum;
                                }
                                if (WinForms.MessageBox.Show(Convert.ToString(Convert.ToDecimal(zs)) + " Удалить категорию и все ее связи?", "Удаление категории", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                                //if (WinForms.MessageBox.Show(Convert.ToString(Convert.ToDecimal(zs)) + " You're about to remove category and its links permanently?", "Deleting", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    for (int j = 0; j < mySprites.Count; j++)
                                    {
                                        if (mySprites[j].parent == i)
                                            mySprites[j].parent = -1;
                                        else if (mySprites[j].parent > i)
                                            mySprites[j].parent--;
                                    }
                                    for (int j = 0; j < myGoods.Count; j++)
                                    {
                                        if (myGoods[j].index == i)
                                            myGoods[j].index = -1;
                                        else if (myGoods[j].index > i)
                                            myGoods[j].index--;
                                    }
                                    mySprites.RemoveAt(i);
                                    i = MySpriteMouseOn(mouseState);
                                    CheckLevel();
                                    CheckVisibility(i);
                                    currentSprite = -1;
                                }
                            }
                        }
                        else if (keyboardState.IsKeyDown(Keys.Tab))
                        {
                            if (i != -1)
                            {
                                if (!isAdding)
                                {
                                    isAdding = true;
                                    catName = mySprites[i].catName;
                                    StreamWriter sw = new StreamWriter("catname.txt");
                                    sw.WriteLine(catName);
                                    sw.Close();
                                    inputForm.ShowDialog();
                                    StreamReader sr = new StreamReader("catname.txt");
                                    catName = sr.ReadLine();
                                    sr.Close();
                                    mySprites[i].catName = catName;
                                    isAdding = false;
                                    CheckLevel();
                                    CheckVisibility(i);
                                }
                            }
                        }
                    }
                    else // if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                        {
                            if (nodeIsDrawing)
                                vectors[vectors.Count - 1] = new Vector2(mouseState.X, mouseState.Y);
                        }
                    }
                }

                if (mouseState.LeftButton == ButtonState.Released)
                {
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                        {
                            if (nodeIsDrawing)
                            {
                                nodeIsDrawing = false;
                                if (i == -1)
                                {
                                    vectors.RemoveAt(vectors.Count - 1);
                                    vectors.RemoveAt(vectors.Count - 1);
                                }
                                else
                                {
                                    mySprites[i].parent = parent;
                                    if (mySprites[i].parent == i)
                                        mySprites[i].parent = -1;
                                    parent = -1;
                                    vectors.RemoveAt(vectors.Count - 1);
                                    vectors.RemoveAt(vectors.Count - 1);
                                    CheckLevel();
                                    CheckVisibility(i);
                                }
                            }
                            else
                            {
                                if (ig != -1)
                                {
                                    for (int zz = 0; zz < selGoods.Count; zz++)
                                    {
                                        myGoods[selGoods[zz]].index = i;
                                        myGoods[selGoods[zz]].position = myGoods[selGoods[zz]].oldPosition;
                                    }
                                    selGoods.Clear();
                                    myGoods[ig].index = i;
                                    myGoods[ig].position = myGoods[ig].oldPosition;
                                    ig = -1;
                                    RefreshGoodsList(0);
                                    RefreshReport();
                                }
                            }
                        }
                        else
                        {
                            if (i != -1)
                            {
                                elapsedTime += gameTime.ElapsedGameTime;
                                mySprites[i].pressed = false;
                                mySprites[i].released = true;
                                if (ig != -1)
                                {
                                    myGoods[ig].index = i;
                                    myGoods[ig].position = myGoods[ig].oldPosition;
                                    ig = -1;
                                    RefreshGoodsList(0);
                                    RefreshReport();
                                }
                            }
                            else
                            {
                                if (ig != -1)
                                {
                                    myGoods[ig].position = myGoods[ig].oldPosition;
                                    ig = -1;
                                }
                            }
                        }
                    }
                }
                if (i != -1)
                {
                    if (mySprites[i].pressed)
                    {
                        if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
                        {
                            for (int j = 0; j < selSprites.Count; j++)
                            {
                                mySprites[selSprites[j]].position.X += (mouseState.X - prevMouseState.X) / scale;
                                mySprites[selSprites[j]].position.Y += (mouseState.Y - prevMouseState.Y) / scale;
                            }
                        }
                        else
                        {
                            mySprites[i].position.X += (mouseState.X - prevMouseState.X) / scale;
                            mySprites[i].position.Y += (mouseState.Y - prevMouseState.Y) / scale;
                        }
                    }
                }
                else
                {
                    for (i = 0; i < mySprites.Count; i++)
                    {
                        mySprites[i].pressed = false;
                        mySprites[i].released = true;
                    }
                }
                #endregion
            }
            if ((portable && ig == -1) || (portable && ig != -1))
            //if ((portable && mouseState.X >= 900 && ig == -1) || (portable && ig != -1))
            {
                #region selection
                for (i = 0; i < myGoods.Count; i++)
                {
                    if (IsMouseOnMyGood(mouseState, i))
                    {
                        myGoods[i].texture = itemsel;
                        myGoods[i].colour = Color.Yellow;
                    }
                    else
                    {
                        myGoods[i].texture = item;
                        myGoods[i].colour = Color.White;
                    }
                }
                #endregion
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    int counter = 0;
                    int fl = 0;

                    if (currentSprite == -1)
                        fl = firstLine;
                    else
                        fl = mySprites[currentSprite].firstLine;
                    if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                    {
                        for (int j = fl; j < myGoods.Count; j++)
                        {
                            if (myGoods[j].index == currentSprite)
                            {
                                if (IsMouseOnMyGood(mouseState, j))
                                {
                                    ig = j;
                                    bool found = false;
                                    for (i = 0; i < selGoods.Count; i++)
                                    {
                                        if (selGoods[i] == ig)
                                        {
                                            selGoods.RemoveAt(i);
                                            myGoods[ig].position = myGoods[ig].oldPosition;
                                            found = true;
                                            break;
                                        }
                                    }
                                    if (!found)
                                    {
                                        selGoods.Add(ig);
                                        myGoods[ig].oldPosition = myGoods[ig].position;
                                        myGoods[ig].texture = itemsel;
                                        myGoods[ig].colour = Color.Yellow;
                                    }
                                    break;
                                }
                                counter++;
                                if (counter > Convert.ToInt16(Math.Round(graphics.GraphicsDevice.DisplayMode.Height / 19f)) /*42*/)
                                    break;
                            }
                            ig = -1;
                        }
                    }
                    else
                    {
                        if (mouseState.X >= (graphics.PreferredBackBufferWidth - 380))
                            if (mouseState.Y > 13 && mouseState.Y < graphics.GraphicsDevice.DisplayMode.Height - 75 - 14 - 10)
                                selGoods.Clear();
                        for (int j = fl; j < myGoods.Count; j++)
                        {
                            if (myGoods[j].index == currentSprite)
                            {
                                if (IsMouseOnMyGood(mouseState, j))
                                {
                                    ig = j;
                                    myGoods[ig].oldPosition = myGoods[ig].position;
                                    break;
                                }
                                counter++;
                                if (counter > Convert.ToInt16(Math.Round(graphics.GraphicsDevice.DisplayMode.Height / 19f)) /*42*/)
                                    break;
                            }
                            ig = -1;
                        }
                    }
                    if (mouseState.X >= (graphics.PreferredBackBufferWidth - 380) && mouseState.Y < 13)
                    {
                        RefreshGoodsList(-1);
                    }
                    if (mouseState.X >= (graphics.PreferredBackBufferWidth - 380) && mouseState.Y > graphics.GraphicsDevice.DisplayMode.Height - 75 - 14 - 10)
                    {
                        RefreshGoodsList(1);
                    }
                }

                if (mouseState.RightButton == ButtonState.Released && prevMouseState.RightButton == ButtonState.Pressed)
                {
                    int counter = 0;
                    int fl = 0;

                    if (currentSprite == -1)
                        fl = firstLine;
                    else
                        fl = mySprites[currentSprite].firstLine;
                    for (int j = fl; j < myGoods.Count; j++)
                    {
                        if (myGoods[j].index == currentSprite)
                        {
                            if (IsMouseOnMyGood(mouseState, j))
                            {
                                ig = j;
                                bool found = false;
                                for (i = 0; i < selGoods.Count; i++)
                                {
                                    if (selGoods[i] == ig)
                                    {
                                        selGoods.RemoveAt(i);
                                        myGoods[ig].position = myGoods[ig].oldPosition;
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    selGoods.Add(ig);
                                    myGoods[ig].oldPosition = myGoods[ig].position;
                                    myGoods[ig].texture = itemsel;
                                    myGoods[ig].colour = Color.Yellow;
                                }
                                break;
                            }
                            counter++;
                            if (counter > Convert.ToInt16(Math.Round(graphics.GraphicsDevice.DisplayMode.Height / 19f)) /*42*/)
                                break;
                        }
                        ig = -1;
                    }
                    if (mouseState.X >= (graphics.PreferredBackBufferWidth - 380) && mouseState.Y < 13)
                    {
                        RefreshGoodsList(-1);
                    }
                    if (mouseState.X >= (graphics.PreferredBackBufferWidth - 380) && mouseState.Y > graphics.GraphicsDevice.DisplayMode.Height - 75 - 14 - 10)
                    {
                        RefreshGoodsList(1);
                    }
                }
                
                
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (ig != -1)
                    {
                        for (i = 0; i < selGoods.Count; i++)
                        {
                            myGoods[selGoods[i]].position.X += mouseState.X - prevMouseState.X;
                            myGoods[selGoods[i]].position.Y += mouseState.Y - prevMouseState.Y;
                        }
                        myGoods[ig].position.X += mouseState.X - prevMouseState.X;
                        myGoods[ig].position.Y += mouseState.Y - prevMouseState.Y;
                    }
                }
                if (mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (ig != -1)
                    {
                        for (i = 0; i < selGoods.Count; i++)
                        {
                            myGoods[selGoods[i]].position = myGoods[selGoods[i]].oldPosition;
                        }
                        myGoods[ig].position = myGoods[ig].oldPosition;
                    }
                    ig = -1;
                }
            }

            prevMouseState = mouseState;
            prevKeyboardState = keyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Vector2 v1, v2;
            Vector2 stringSize;

            v1 = new Vector2(0, 0);
            v2 = v1;
            stringSize = new Vector2(0, 0);

            if (!f5)
            {
                #region !f5
                if (portable)
                {
                    graphics.GraphicsDevice.Viewport = rightVP;
                    graphics.GraphicsDevice.Clear(Color.Navy);
                    graphics.GraphicsDevice.Viewport = leftVP;
                    graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                }
                else
                {
                    graphics.GraphicsDevice.Viewport = originVP;
                    if (!print)
                        graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    else
                        graphics.GraphicsDevice.Clear(Color.White);
                }

                spriteAngle += 0.1f;
                //angle += 0.001f;
                //spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
                spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None, Matrix.Identity * Matrix.CreateScale(1) * Matrix.CreateRotationZ(angle) * Matrix.CreateTranslation(0, 0, 0));
                for (int i = 0; i < mySprites.Count; i++)
                {
                    if (f1)
                    {
                        if (mySprites[i].parent != -1)
                            if (mySprites[mySprites[i].parent].parent != -1)
                                if (mySprites[mySprites[mySprites[i].parent].parent].parent != -1)
                                    continue;
                    }
                    if (f2)
                    {
                        if (mySprites[i].parent != -1)
                            if (mySprites[mySprites[i].parent].parent != -1)
                                if (mySprites[mySprites[mySprites[i].parent].parent].parent != -1)
                                    if (mySprites[mySprites[mySprites[mySprites[i].parent].parent].parent].parent != -1)
                                        continue;
                    }
                    stringSize = catFont.MeasureString(mySprites[i].catName);
                    stringSize.X += 4;
                    mySprites[i].size.X = stringSize.X;
                    if (mySprites[i].parent != -1)
                    {
                        v1.X = (mySprites[i].position.X + mySprites[i].size.X / 2) * scale;
                        v1.Y = (mySprites[i].position.Y + 7) * scale;
                        v2.X = (mySprites[mySprites[i].parent].position.X + mySprites[mySprites[i].parent].size.X / 2) * scale;
                        v2.Y = (mySprites[mySprites[i].parent].position.Y + 7) * scale;
                        if ((v1.X < 0 || v1.Y < 0) && (v2.X < 0 || v2.Y < 0))
                            continue;
                        if ((v1.X >= graphics.GraphicsDevice.Viewport.Width || v1.Y >= graphics.GraphicsDevice.Viewport.Height)
                            && (v2.X >= graphics.GraphicsDevice.Viewport.Width || v2.Y >= graphics.GraphicsDevice.Viewport.Height))
                            continue;
                        if (!print)
                        {
                            if (mySprites[mySprites[i].parent].parent == -1)
                                DrawLine(v1, v2, Color.White, 7);
                            else if (mySprites[mySprites[mySprites[i].parent].parent].parent == -1)
                                DrawLine(v1, v2, Color.White, 5);
                            else if (mySprites[mySprites[mySprites[mySprites[i].parent].parent].parent].parent == -1)
                                DrawLine(v1, v2, Color.White, 3);
                            else if (mySprites[mySprites[mySprites[mySprites[mySprites[i].parent].parent].parent].parent].parent == -1)
                                DrawLine(v1, v2, Color.White, 2);
                            else if (mySprites[mySprites[i].parent].parent != -1)
                                DrawLine(v1, v2, Color.White, 1);
                        }
                        else
                        {
                            if (mySprites[mySprites[i].parent].parent == -1)
                                DrawLine(v1, v2, Color.Gray, 7);
                            else if (mySprites[mySprites[mySprites[i].parent].parent].parent == -1)
                                DrawLine(v1, v2, Color.Gray, 5);
                            else if (mySprites[mySprites[mySprites[mySprites[i].parent].parent].parent].parent == -1)
                                DrawLine(v1, v2, Color.Gray, 3);
                            else if (mySprites[mySprites[mySprites[mySprites[mySprites[i].parent].parent].parent].parent].parent == -1)
                                DrawLine(v1, v2, Color.Gray, 2);
                            else if (mySprites[mySprites[i].parent].parent != -1)
                                DrawLine(v1, v2, Color.Gray, 1);
                        }
                    }
                }
                for (int i = 0; i < mySprites.Count; i++)
                {
                    if ((int)(mySprites[i].position.X * scale) >= graphics.GraphicsDevice.Viewport.Width || (int)(mySprites[i].position.Y * scale) >= graphics.GraphicsDevice.Viewport.Height)
                        continue;
                    if ((int)(mySprites[i].position.X * scale) + (int)(mySprites[i].size.X * scale) < 0 || (int)(mySprites[i].position.Y * scale) + (int)(mySprites[i].size.Y * scale) < 0)
                        continue;

                    if (i == currentSprite && portable)
                        spriteBatch.Draw(mySprites[i].texture,
                            new Rectangle((int)(mySprites[i].position.X * scale + mySprites[i].size.X * scale / 2), (int)(mySprites[i].position.Y * scale + mySprites[i].size.Y * scale / 2), (int)(mySprites[i].size.X * scale), (int)(15 * scale)),
                            new Rectangle((int)(mySprites[i].position.X * scale), (int)(mySprites[i].position.Y * scale), (int)(mySprites[i].size.X * scale), (int)(15 * scale)),
                            Color.White,
                            spriteAngle,
                            new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                            SpriteEffects.None,
                            0.5f);
                    else
                    {
                        if (f1)
                        {
                            if (mySprites[i].parent != -1)
                                if (mySprites[mySprites[i].parent].parent != -1)
                                    if (mySprites[mySprites[mySprites[i].parent].parent].parent != -1)
                                        continue;
                            spriteBatch.Draw(mySprites[i].texture,
                                new Rectangle((int)(mySprites[i].position.X * scale),
                                    (int)(mySprites[i].position.Y * scale),
                                    (int)(mySprites[i].size.X),
                                    (int)(15)),
                                Color.White);
                        }
                        else if (f2)
                        {
                            if (mySprites[i].parent != -1)
                                if (mySprites[mySprites[i].parent].parent != -1)
                                    if (mySprites[mySprites[mySprites[i].parent].parent].parent != -1)
                                        if (mySprites[mySprites[mySprites[mySprites[i].parent].parent].parent].parent != -1)
                                            continue;
                        }
                        else
                        {
                            #region Разные размеры
                            ////  категории разного уровня - разным размером
                            //if (mySprites[i].parent == -1)
                            //    spriteBatch.Draw(mySprites[i].texture,
                            //        new Rectangle((int)((mySprites[i].position.X - 5 * 2) * scale),
                            //        (int)((mySprites[i].position.Y - 5 * 2) * scale),
                            //        (int)((mySprites[i].size.X + 5 * 2) * scale),
                            //        (int)((15 + 5 * 2) * scale)),
                            //        Color.White);
                            //else if (mySprites[mySprites[i].parent].parent == -1)
                            //    spriteBatch.Draw(mySprites[i].texture,
                            //        new Rectangle((int)((mySprites[i].position.X - 4 * 2) * scale),
                            //        (int)((mySprites[i].position.Y - 4 * 2) * scale),
                            //        (int)((mySprites[i].size.X + 4 * 2) * scale),
                            //        (int)((15 + 4 * 2) * scale)),
                            //        Color.White);
                            //else if (mySprites[mySprites[mySprites[i].parent].parent].parent == -1)
                            //    spriteBatch.Draw(mySprites[i].texture,
                            //        new Rectangle((int)((mySprites[i].position.X - 3 * 2) * scale),
                            //        (int)((mySprites[i].position.Y - 3 * 2) * scale),
                            //        (int)((mySprites[i].size.X + 3 * 2) * scale),
                            //        (int)((15 + 3 * 2) * scale)),
                            //        Color.White);
                            //else if (mySprites[mySprites[mySprites[mySprites[i].parent].parent].parent].parent == -1)
                            //    spriteBatch.Draw(mySprites[i].texture,
                            //        new Rectangle((int)((mySprites[i].position.X - 2 * 2) * scale),
                            //        (int)((mySprites[i].position.Y - 2 * 2) * scale),
                            //        (int)((mySprites[i].size.X + 2 * 2) * scale),
                            //        (int)((15 + 2 * 2) * scale)),
                            //        Color.White);
                            //else if (mySprites[mySprites[mySprites[mySprites[mySprites[i].parent].parent].parent].parent].parent == -1)
                            //    spriteBatch.Draw(mySprites[i].texture,
                            //        new Rectangle((int)((mySprites[i].position.X - 1 * 2) * scale),
                            //        (int)((mySprites[i].position.Y - 1 * 2) * scale),
                            //        (int)((mySprites[i].size.X + 1 * 2) * scale),
                            //        (int)((15 + 1 * 2) * scale)),
                            //        Color.White);
                            //else if (mySprites[mySprites[i].parent].parent != -1)
                            //    spriteBatch.Draw(mySprites[i].texture,
                            //        new Rectangle((int)(mySprites[i].position.X * scale),
                            //        (int)(mySprites[i].position.Y * scale),
                            //        (int)(mySprites[i].size.X * scale),
                            //        (int)(15 * scale)),
                            //        Color.White);
                            #endregion Разные размеры
                            spriteBatch.Draw(mySprites[i].texture,
                                new Rectangle((int)(mySprites[i].position.X * scale),
                                    (int)(mySprites[i].position.Y * scale),
                                    (int)(mySprites[i].size.X * scale),
                                    (int)(15 * scale)),
                                Color.White);
                        }
                    }
                    if (mySprites[i].catName != null)
                    {
                        if (i == currentSprite && portable)
                        {
                            if (scale < 1f)
                                spriteBatch.DrawString(tinyFont,
                                    mySprites[i].catName,
                                    mySprites[i].position * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                    Color.Blue,
                                    spriteAngle,
                                    new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                    1,
                                    SpriteEffects.None,
                                    0.999f);
                            else if (scale < 2.0f)
                                spriteBatch.DrawString(catFont,
                                    mySprites[i].catName,
                                    mySprites[i].position * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                    Color.Blue,
                                    spriteAngle,
                                    new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                    1,
                                    SpriteEffects.None,
                                    0.999f);
                            else if (scale < 3.0f)
                                spriteBatch.DrawString(middleFont,
                                    mySprites[i].catName,
                                    mySprites[i].position * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                    Color.Blue,
                                    spriteAngle,
                                    new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                    1,
                                    SpriteEffects.None,
                                    0.999f);
                            else
                                spriteBatch.DrawString(gameFont,
                                    mySprites[i].catName,
                                    mySprites[i].position * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                    Color.Blue,
                                    spriteAngle,
                                    new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                    1,
                                    SpriteEffects.None,
                                    0.999f);
                        }
                        else
                        {
                            if (scale < 1f)
                            {
                                if (f1)
                                    spriteBatch.DrawString(catFont, mySprites[i].catName, mySprites[i].position * scale + new Vector2(2 * scale, 2 * scale), Color.Blue);
                                else
                                    spriteBatch.DrawString(tinyFont, mySprites[i].catName, mySprites[i].position * scale + new Vector2(2 * scale, 2 * scale), Color.Blue);
                            }
                            else if (scale < 2.0f)
                                spriteBatch.DrawString(catFont, mySprites[i].catName, mySprites[i].position * scale + new Vector2(2 * scale, 2 * scale), Color.Blue);
                            else if (scale < 3.0f)
                                spriteBatch.DrawString(middleFont, mySprites[i].catName, mySprites[i].position * scale + new Vector2(2 * scale, 2 * scale), Color.Blue);
                            else
                                spriteBatch.DrawString(gameFont, mySprites[i].catName, mySprites[i].position * scale + new Vector2(2 * scale, 2 * scale), Color.Blue);
                        }
                    }
                    if (report2)
                    {
                        if (report2 && mySprites[i].active)
                        {
                            if (i == currentSprite && portable)
                                spriteBatch.Draw(infotexture,
                                    new Rectangle((int)(mySprites[i].position.X * scale + mySprites[i].size.X * scale / 2), (int)((mySprites[i].position.Y + 15) * scale + mySprites[i].size.Y * scale / 2), (int)(mySprites[i].size.X * scale), (int)(15 * scale)),
                                    new Rectangle((int)(mySprites[i].position.X * scale), (int)(mySprites[i].position.Y * scale), (int)(mySprites[i].size.X * scale), (int)(15 * scale)),
                                    Color.White,
                                    spriteAngle,
                                    new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                    SpriteEffects.None,
                                    0.5f);
                            else
                                spriteBatch.Draw(infotexture,
                                    new Rectangle((int)(mySprites[i].position.X * scale),
                                    (int)((mySprites[i].position.Y + 15) * scale),
                                    (int)(mySprites[i].size.X * scale),
                                    (int)(15 * scale)),
                                    Color.White);
                            if (i == currentSprite && portable)
                            {
                                if (scale < 1f)
                                    spriteBatch.DrawString(tinyFont,
                                        Convert.ToString(Math.Round(mySprites[i].percent - mySprites[i].dpercent, 2)),
                                        (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        Color.White,
                                        spriteAngle,
                                        new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        1,
                                        SpriteEffects.None,
                                        0.999f);
                                else if (scale < 2.0f)
                                    spriteBatch.DrawString(catFont,
                                        Convert.ToString(Math.Round(mySprites[i].percent - mySprites[i].dpercent, 2)),
                                        (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        Color.White,
                                        spriteAngle,
                                        new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        1,
                                        SpriteEffects.None,
                                        0.999f);
                                else if (scale < 3.0f)
                                    spriteBatch.DrawString(middleFont,
                                        Convert.ToString(Math.Round(mySprites[i].percent - mySprites[i].dpercent, 2)),
                                        (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        Color.White,
                                        spriteAngle,
                                        new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        1,
                                        SpriteEffects.None,
                                        0.999f);
                                else
                                    spriteBatch.DrawString(gameFont,
                                        Convert.ToString(Math.Round(mySprites[i].percent - mySprites[i].dpercent, 2)),
                                        (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        Color.White,
                                        spriteAngle,
                                        new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        1,
                                        SpriteEffects.None,
                                        0.999f);
                            }
                            else
                            {
                                if (scale < 1f)
                                    spriteBatch.DrawString(tinyFont, Convert.ToString(Math.Round(mySprites[i].percent - mySprites[i].dpercent, 2)), (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale), Color.White);
                                else if (scale < 2.0f)
                                    spriteBatch.DrawString(catFont, Convert.ToString(Math.Round(mySprites[i].percent - mySprites[i].dpercent, 2)), (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale), Color.White);
                                else if (scale < 3.0f)
                                    spriteBatch.DrawString(middleFont, Convert.ToString(Math.Round(mySprites[i].percent - mySprites[i].dpercent, 2)), (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale), Color.White);
                                else
                                    spriteBatch.DrawString(gameFont, Convert.ToString(Math.Round(mySprites[i].percent - mySprites[i].dpercent, 2)), (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale), Color.White);
                            }
                        }
                    }
                    else if (report)
                    {
                        if (report && mySprites[i].active)
                        {
                            if (i == currentSprite && portable)
                                spriteBatch.Draw(infotexture,
                                    new Rectangle((int)(mySprites[i].position.X * scale + mySprites[i].size.X * scale / 2), (int)((mySprites[i].position.Y + 15) * scale + mySprites[i].size.Y * scale / 2), (int)(mySprites[i].size.X * scale), (int)(15 * scale)),
                                    new Rectangle((int)(mySprites[i].position.X * scale), (int)(mySprites[i].position.Y * scale), (int)(mySprites[i].size.X * scale), (int)(15 * scale)),
                                    Color.White,
                                    spriteAngle,
                                    new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                    SpriteEffects.None,
                                    0.5f);
                            else
                                spriteBatch.Draw(infotexture,
                                    new Rectangle((int)(mySprites[i].position.X * scale),
                                    (int)((mySprites[i].position.Y + 15) * scale),
                                    (int)(mySprites[i].size.X * scale),
                                    (int)(15 * scale)),
                                    Color.White);
                            if (i == currentSprite && portable)
                            {
                                if (scale < 1f)
                                    spriteBatch.DrawString(tinyFont,
                                        Convert.ToString(Math.Round(mySprites[i].percent, 2)),
                                        (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        Color.White,
                                        spriteAngle,
                                        new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        1,
                                        SpriteEffects.None,
                                        0.999f);
                                else if (scale < 2.0f)
                                    spriteBatch.DrawString(catFont,
                                        Convert.ToString(Math.Round(mySprites[i].percent, 2)),
                                        (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        Color.White,
                                        spriteAngle,
                                        new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        1,
                                        SpriteEffects.None,
                                        0.999f);
                                else if (scale < 3.0f)
                                    spriteBatch.DrawString(middleFont,
                                        Convert.ToString(Math.Round(mySprites[i].percent, 2)),
                                        (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        Color.White,
                                        spriteAngle,
                                        new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        1,
                                        SpriteEffects.None,
                                        0.999f);
                                else
                                    spriteBatch.DrawString(gameFont,
                                        Convert.ToString(Math.Round(mySprites[i].percent, 2)),
                                        (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        Color.White,
                                        spriteAngle,
                                        new Vector2(mySprites[i].size.X * scale / 2, mySprites[i].size.Y * scale / 2),
                                        1,
                                        SpriteEffects.None,
                                        0.999f);
                            }
                            else
                            {
                                if (scale < 1f)
                                    spriteBatch.DrawString(tinyFont, Convert.ToString(Math.Round(mySprites[i].percent, 2)), (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale), Color.White);
                                else if (scale < 2.0f)
                                    spriteBatch.DrawString(catFont, Convert.ToString(Math.Round(mySprites[i].percent, 2)), (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale), Color.White);
                                else if (scale < 3.0f)
                                    spriteBatch.DrawString(middleFont, Convert.ToString(Math.Round(mySprites[i].percent, 2)), (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale), Color.White);
                                else
                                    spriteBatch.DrawString(gameFont, Convert.ToString(Math.Round(mySprites[i].percent, 2)), (mySprites[i].position + new Vector2(0, 17)) * scale + new Vector2(2 * scale, 2 * scale), Color.White);
                            }
                        }
                    }
                }
                for (int i = 0; i < vectors.Count - 1; i += 2)
                    DrawLine(vectors[i], vectors[i + 1], Color.White, 1);
                spriteBatch.DrawString(middleFont, Convert.ToString(mouseState.X) + ", " + Convert.ToString(mouseState.Y), new Vector2(2, 2), Color.White);
                if (reporting)
                    spriteBatch.Draw(pleasewait, new Vector2(graphics.PreferredBackBufferWidth / 2 - 319 / 2, graphics.PreferredBackBufferHeight / 2 - 82 / 2), Color.White);
                spriteBatch.End();

                if (portable)
                {
                    int j, counter = 0;
                    graphics.GraphicsDevice.Viewport = rightVP;
                    graphics.GraphicsDevice.Clear(Color.DarkGray);
                    spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None, Matrix.Identity * Matrix.CreateScale(1) * Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(0, 0, 0));
                    spriteBatch.Draw(scrollup, new Vector2(0, 1), Color.White);
                    spriteBatch.Draw(scrolldown, new Vector2(0, graphics.GraphicsDevice.DisplayMode.Height - 75 - 14 - 10), Color.White);
                    if (currentSprite == -1)
                        j = firstLine;
                    else
                        j = mySprites[currentSprite].firstLine;
                    for (int i = j; i < myGoods.Count; i++)
                    {
                        if (myGoods[i].index == currentSprite)
                        {
                            bool found = false;
                            for (int z = 0; z < selGoods.Count; z++)
                            {
                                if (selGoods[z] == i)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (i != ig && !found)
                            {
                                spriteBatch.Draw(myGoods[i].texture, myGoods[i].position, Color.White);
                                if (myGoods[i].active)
                                {
                                    string str1, str2;
                                    spriteBatch.DrawString(catFont, myGoods[i].code + " " + myGoods[i].description, myGoods[i].position + new Vector2(2, 2), myGoods[i].colour);
                                    if (myGoods[i].sum > 0)
                                    {
                                        str1 = Convert.ToString(Math.Round(myGoods[i].percent, 2));
                                        while (str1.Length < 4)
                                            str1 = str1 + " ";
                                        if (myGoods[i].index != -1)
                                        {
                                            str1 += " / ";
                                            str2 = Convert.ToString(Math.Round(myGoods[i].sum / (mySprites[myGoods[i].index].sum / 100), 2));
                                            while (str2.Length < 5)
                                                str2 = str2 + " ";
                                        }
                                        else
                                            str2 = "";
                                        stringSize = catFont.MeasureString(str1 + str2);
                                        spriteBatch.DrawString(catFont, str1 + str2, myGoods[i].position + new Vector2(myGoods[i].size.X - stringSize.X - 2, 2), myGoods[i].colour);
                                    }
                                }
                                else
                                    spriteBatch.DrawString(catFont, myGoods[i].code + " " + myGoods[i].description, myGoods[i].position + new Vector2(2, 2), Color.LightGray);
                            }
                            counter++;
                            if (counter > Convert.ToInt16(Math.Round(graphics.GraphicsDevice.DisplayMode.Height / 19f)) /*42*/)
                                break;
                        }
                    }
                    spriteBatch.End();

                    graphics.GraphicsDevice.Viewport = originVP;
                    spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None, Matrix.Identity * Matrix.CreateScale(1) * Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(0, 0, 0));
                    for (int z = 0; z < selGoods.Count; z++)
                    {
                        spriteBatch.Draw(itemsel, myGoods[selGoods[z]].position + new Vector2((graphics.PreferredBackBufferWidth - 380), 0), Color.White);
                        spriteBatch.DrawString(catFont, myGoods[selGoods[z]].code + " " + myGoods[selGoods[z]].description, myGoods[selGoods[z]].position + new Vector2(2, 2) + new Vector2((graphics.PreferredBackBufferWidth - 380), 0), Color.Yellow);
                    }
                    if (ig != -1)
                    {
                        spriteBatch.Draw(myGoods[ig].texture, myGoods[ig].position + new Vector2((graphics.PreferredBackBufferWidth - 380), 0), Color.White);
                        if (myGoods[ig].active)
                            spriteBatch.DrawString(catFont, myGoods[ig].code + " " + myGoods[ig].description, myGoods[ig].position + new Vector2(2, 2) + new Vector2((graphics.PreferredBackBufferWidth - 380), 0), myGoods[ig].colour);
                        else
                            spriteBatch.DrawString(catFont, myGoods[ig].code + " " + myGoods[ig].description, myGoods[ig].position + new Vector2(2, 2) + new Vector2((graphics.PreferredBackBufferWidth - 380), 0), Color.LightGray);
                    }
                    spriteBatch.End();
                }
                #region 3D
                //effect.World = worldMatrix * Matrix.CreateRotationZ(MathHelper.ToRadians(30));
                ////graphics.GraphicsDevice.VertexDeclaration = new VertexDeclaration(graphics.GraphicsDevice, VertexPositionColor.VertexElements);
                ////graphics.GraphicsDevice.Vertices[0].SetSource(vertexBuffer, 0, VertexPositionColor.SizeInBytes);
                //effect.Begin();
                //foreach (EffectPass CurrentPass in effect.CurrentTechnique.Passes)
                //{
                //    CurrentPass.Begin();
                ////    graphics.GraphicsDevice.DrawPrimitives(PrimitiveType.LineList, 0, 1);
                //    CurrentPass.End();
                //}
                //effect.End();
                #endregion
                #endregion !f5
            }
            else // if (f5)
            {
                if (!contextMenu)
                {
                    if (fireIsOn)
                    {
                        //  fire
                        GraphicsDevice device = graphics.GraphicsDevice;
                        device.SetRenderTarget(0, renderTarget1);
                        device.Clear(Color.TransparentBlack);

                        spriteBatch.Begin();

                        if (fire != null)
                            spriteBatch.Draw(fire, Vector2.Zero, Color.White);

                        for (int i = 0; i < device.Viewport.Width / hotSpotTexture.Width; i++)
                            spriteBatch.Draw(hotSpotTexture,
                                new Vector2(i * hotSpotTexture.Width,
                                device.Viewport.Height - hotSpotTexture.Height),
                                colors[rand.Next(colors.Length)]);

                        spriteBatch.End();

                        device.SetRenderTarget(0, null);

                        device.SetRenderTarget(0, renderTarget2);

                        fireEffect.Begin();
                        spriteBatch.Begin(SpriteBlendMode.None, SpriteSortMode.Immediate, SaveStateMode.None);

                        EffectPass pass = fireEffect.CurrentTechnique.Passes[0];
                        pass.Begin();
                        spriteBatch.Draw(renderTarget1.GetTexture(),
                            new Rectangle(0, offset, device.Viewport.Width,
                            device.Viewport.Height - offset), Color.White);
                        spriteBatch.End();
                        pass.End();
                        fireEffect.End();

                        device.SetRenderTarget(0, null);

                        device.Clear(Color.TransparentBlack);

                        fire = renderTarget2.GetTexture();

                        spriteBatch.Begin(SpriteBlendMode.Additive);

                        spriteBatch.Draw(fire, titleSafeArea, Color.White);
                        spriteBatch.Draw(fire, titleSafeArea, Color.White);
                        spriteBatch.Draw(fire, titleSafeArea, Color.White);
                    }
//---------------------
                    if (portable)
                    {
                        graphics.GraphicsDevice.Viewport = rightVP;
                        graphics.GraphicsDevice.Clear(Color.Navy);
                        graphics.GraphicsDevice.Viewport = leftVP;
                        if (!fireIsOn)
                            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    }
                    else
                    {
                        graphics.GraphicsDevice.Viewport = originVP;
                        if (!fireIsOn)
                        {
                            if (!print)
                                graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                            else
                                graphics.GraphicsDevice.Clear(Color.White);
                        }
                    }

                    SelectParents();
                    if (!fireIsOn)
                    {
                        spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None, Matrix.Identity * Matrix.CreateScale(1) * Matrix.CreateRotationZ(angle) * Matrix.CreateTranslation(0, 0, 0));
                    }
                    for (int i = 0; i < mySprites.Count; i++)
                    {
                        if (mySprites[i].visible)
                        {
                            if (mySprites[i].parent != -1)
                            {
                                v1.X = (mySprites[i].position2.X) * scale;
                                v1.Y = (mySprites[i].position2.Y) * scale;
                                v2.X = (mySprites[mySprites[i].parent].position2.X) * scale;
                                v2.Y = (mySprites[mySprites[i].parent].position2.Y) * scale;
                                if ((v1.X < 0 || v1.Y < 0) && (v2.X < 0 || v2.Y < 0))
                                    continue;
                                if ((v1.X >= graphics.GraphicsDevice.Viewport.Width || v1.Y >= graphics.GraphicsDevice.Viewport.Height)
                                    && (v2.X >= graphics.GraphicsDevice.Viewport.Width || v2.Y >= graphics.GraphicsDevice.Viewport.Height))
                                    continue;
                            }

                            if (fireIsOn)
                            {
                                if (mySprites[i].texture == texturesel)
                                {
                                    spriteBatch.Draw(mySprites[i].texture,
                                        new Rectangle((int)(mySprites[i].position2.X * scale),
                                        (int)(mySprites[i].position2.Y * scale),
                                        (int)(mySprites[i].size2.X),
                                        (int)(20)),
                                        Color.White);   //  вместо White
                                    spriteBatch.DrawString(middleFont, mySprites[i].catName,
                                        mySprites[i].position2 * scale + new Vector2(2 * scale, 2 * scale),
                                        Color.White);  //  вместо Blue
                                }
                                else
                                {
                                    spriteBatch.Draw(texturesel,
                                        new Rectangle((int)(mySprites[i].position2.X * scale),
                                        (int)(mySprites[i].position2.Y * scale),
                                        (int)(mySprites[i].size2.X),
                                        (int)(20)),
                                        Color.Tomato);   //  вместо White
                                    spriteBatch.DrawString(middleFont, mySprites[i].catName,
                                        mySprites[i].position2 * scale + new Vector2(2 * scale, 2 * scale),
                                        Color.Yellow);
                                }
                                if (report2)
                                {
                                    if (mySprites[i].texture == texturesel)
                                    {
                                        spriteBatch.Draw(infotexture,
                                            new Rectangle((int)(mySprites[i].position2.X * scale + mySprites[i].size2.X * scale - mySprites[i].infoSize.X * scale - 4),
                                            (int)(mySprites[i].position2.Y * scale),
                                            (int)(mySprites[i].infoSize.X + 4),
                                            (int)(20)),
                                            Color.White); //  вместо White
                                        spriteBatch.DrawString(middleFont,
                                            Convert.ToString(Math.Round(mySprites[i].percent2 - mySprites[i].dpercent2, 2)),
                                            mySprites[i].position2 * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size2.X * scale - mySprites[i].infoSize.X * scale - 4, 0),
                                            Color.White);
                                    }
                                    else
                                    {
                                        spriteBatch.Draw(infotexture,
                                            new Rectangle((int)(mySprites[i].position2.X * scale + mySprites[i].size2.X * scale - mySprites[i].infoSize.X * scale - 4),
                                            (int)(mySprites[i].position2.Y * scale),
                                            (int)(mySprites[i].infoSize.X + 4),
                                            (int)(20)),
                                            Color.White); //  вместо White
                                        spriteBatch.DrawString(middleFont,
                                            Convert.ToString(Math.Round(mySprites[i].percent2 - mySprites[i].dpercent2, 2)),
                                            mySprites[i].position2 * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size2.X * scale - mySprites[i].infoSize.X * scale - 4, 0),
                                            Color.White);   //  вместо White
                                    }
                                }
                                else if (report)
                                {
                                    if (mySprites[i].texture == texturesel)
                                    {
                                        spriteBatch.Draw(infotexture,
                                            new Rectangle((int)(mySprites[i].position2.X * scale + mySprites[i].size2.X * scale - mySprites[i].infoSize.X * scale - 4),
                                            (int)(mySprites[i].position2.Y * scale),
                                            (int)(mySprites[i].infoSize.X + 4),
                                            (int)(20)),
                                            Color.White); //  вместо White
                                        spriteBatch.DrawString(middleFont,
                                            Convert.ToString(Math.Round(mySprites[i].percent2, 2)),
                                            mySprites[i].position2 * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size2.X * scale - mySprites[i].infoSize.X * scale - 4, 0),
                                            Color.White);
                                    }
                                    else
                                    {
                                        spriteBatch.Draw(infotexture,
                                            new Rectangle((int)(mySprites[i].position2.X * scale + mySprites[i].size2.X * scale - mySprites[i].infoSize.X * scale - 4),
                                            (int)(mySprites[i].position2.Y * scale),
                                            (int)(mySprites[i].infoSize.X + 4),
                                            (int)(20)),
                                            Color.White); //  вместо White
                                        spriteBatch.DrawString(middleFont,
                                            Convert.ToString(Math.Round(mySprites[i].percent2, 2)),
                                            mySprites[i].position2 * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size2.X * scale - mySprites[i].infoSize.X * scale - 4, 0),
                                            Color.White);   //  вместо White
                                    }
                                }
                            }
                            else
                            {
                                    spriteBatch.Draw(mySprites[i].texture,
                                        new Rectangle((int)(mySprites[i].position2.X * scale),
                                        (int)(mySprites[i].position2.Y * scale),
                                        (int)(mySprites[i].size2.X),
                                        (int)(20)),
                                        Color.White);
                                    spriteBatch.DrawString(middleFont, mySprites[i].catName,
                                        mySprites[i].position2 * scale + new Vector2(2 * scale, 2 * scale),
                                        Color.Blue);
                                if (report2)
                                {
                                    spriteBatch.Draw(infotexture,
                                        new Rectangle((int)(mySprites[i].position2.X * scale + mySprites[i].size2.X * scale - mySprites[i].infoSize.X * scale - 4),
                                        (int)(mySprites[i].position2.Y * scale),
                                        (int)(mySprites[i].infoSize.X + 4),
                                        (int)(20)),
                                        Color.White);
                                    spriteBatch.DrawString(middleFont,
                                        Convert.ToString(Math.Round(mySprites[i].percent2 - mySprites[i].dpercent2, 2)),
                                        mySprites[i].position2 * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size2.X * scale - mySprites[i].infoSize.X * scale - 4, 0),
                                        Color.White);
                                }
                                else if (report)
                                {
                                    spriteBatch.Draw(infotexture,
                                        new Rectangle((int)(mySprites[i].position2.X * scale + mySprites[i].size2.X * scale - mySprites[i].infoSize.X * scale - 4),
                                        (int)(mySprites[i].position2.Y * scale),
                                        (int)(mySprites[i].infoSize.X + 4),
                                        (int)(20)),
                                        Color.White);
                                    spriteBatch.DrawString(middleFont,
                                        Convert.ToString(Math.Round(mySprites[i].percent2, 2)),
                                        mySprites[i].position2 * scale + new Vector2(2 * scale, 2 * scale) + new Vector2(mySprites[i].size2.X * scale - mySprites[i].infoSize.X * scale - 4, 0),
                                        Color.White);
                                }
                            }
                        }
                    }
                    if (reporting)
                        spriteBatch.Draw(pleasewait, new Vector2(graphics.PreferredBackBufferWidth / 2 - 319 / 2, graphics.PreferredBackBufferHeight / 2 - 82 / 2), Color.White);
                    //if (fireIsOn)
                        spriteBatch.End();
//---------------------

                    //spriteBatch.End();
                    //  fire
                }
                if (portable)
                {
                    int j, counter = 0;
                    graphics.GraphicsDevice.Viewport = rightVP;
                    graphics.GraphicsDevice.Clear(Color.DarkGray);
                    spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None, Matrix.Identity * Matrix.CreateScale(1) * Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(0, 0, 0));
                    spriteBatch.Draw(scrollup, new Vector2(0, 1), Color.White);
                    spriteBatch.Draw(scrolldown, new Vector2(0, graphics.GraphicsDevice.DisplayMode.Height - 75 - 14 - 10), Color.White);
                    if (currentSprite == -1)
                        j = firstLine;
                    else
                        j = mySprites[currentSprite].firstLine;
                    for (int i = j; i < myGoods.Count; i++)
                    {
                        if (myGoods[i].index == currentSprite)
                        {
                            bool found = false;
                            for (int z = 0; z < selGoods.Count; z++)
                            {
                                if (selGoods[z] == i)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (i != ig && !found)
                            {
                                spriteBatch.Draw(myGoods[i].texture, myGoods[i].position, Color.White);
                                if (myGoods[i].active)
                                {
                                    string str1, str2;
                                    spriteBatch.DrawString(catFont, myGoods[i].code + " " + myGoods[i].description, myGoods[i].position + new Vector2(2, 2), myGoods[i].colour);
                                    if (report2)
                                    {
                                        if (myGoods[i].sum > 0 || myGoods[i].dsum > 0)
                                        {
                                            str1 = Convert.ToString(Math.Round(myGoods[i].percent - myGoods[i].dpercent, 2));
                                            while (str1.Length < 4)
                                                str1 = str1 + " ";
                                            if (myGoods[i].index != -1)
                                            {
                                                str1 += " / ";
                                                str2 = Convert.ToString(Math.Round((myGoods[i].sum - myGoods[i].dsum) / ((mySprites[myGoods[i].index].sum - mySprites[myGoods[i].index].dsum) / 100), 2));
                                                while (str2.Length < 5)
                                                    str2 = str2 + " ";
                                            }
                                            else
                                                str2 = "";
                                            stringSize = catFont.MeasureString(str1 + str2);
                                            spriteBatch.DrawString(catFont, str1 + str2, myGoods[i].position + new Vector2(myGoods[i].size.X - stringSize.X - 2, 2), myGoods[i].colour);
                                        }
                                    }
                                    else if (report)
                                    {
                                        if (myGoods[i].sum > 0)
                                        {
                                            str1 = Convert.ToString(Math.Round(myGoods[i].percent, 2));
                                            while (str1.Length < 4)
                                                str1 = str1 + " ";
                                            if (myGoods[i].index != -1)
                                            {
                                                str1 += " / ";
                                                str2 = Convert.ToString(Math.Round(myGoods[i].sum / (mySprites[myGoods[i].index].sum / 100), 2));
                                                while (str2.Length < 5)
                                                    str2 = str2 + " ";
                                            }
                                            else
                                                str2 = "";
                                            stringSize = catFont.MeasureString(str1 + str2);
                                            spriteBatch.DrawString(catFont, str1 + str2, myGoods[i].position + new Vector2(myGoods[i].size.X - stringSize.X - 2, 2), myGoods[i].colour);
                                        }
                                    }
                                }
                                else
                                    spriteBatch.DrawString(catFont, myGoods[i].code + " " + myGoods[i].description, myGoods[i].position + new Vector2(2, 2), Color.LightGray);
                            }
                            counter++;
                            if (counter > Convert.ToInt16(Math.Round(graphics.GraphicsDevice.DisplayMode.Height / 19f)) /*42*/)
                                break;
                        }
                    }
                    spriteBatch.End();

                    graphics.GraphicsDevice.Viewport = originVP;
                    spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None, Matrix.Identity * Matrix.CreateScale(1) * Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(0, 0, 0));
                    for (int z = 0; z < selGoods.Count; z++)
                    {
                        spriteBatch.Draw(itemsel, myGoods[selGoods[z]].position + new Vector2((graphics.PreferredBackBufferWidth - 380), 0), Color.White);
                        spriteBatch.DrawString(catFont, myGoods[selGoods[z]].code + " " + myGoods[selGoods[z]].description, myGoods[selGoods[z]].position + new Vector2(2, 2) + new Vector2((graphics.PreferredBackBufferWidth - 380), 0), Color.Yellow);
                    }
                    //if (ig != -1)
                    //{
                    //    spriteBatch.Draw(myGoods[ig].texture, myGoods[ig].position + new Vector2((graphics.PreferredBackBufferWidth - 380), 0), Color.White);
                    //    if (myGoods[ig].active)
                    //        spriteBatch.DrawString(catFont, myGoods[ig].code + " " + myGoods[ig].description, myGoods[ig].position + new Vector2(2, 2) + new Vector2((graphics.PreferredBackBufferWidth - 380), 0), myGoods[ig].colour);
                    //    else
                    //        spriteBatch.DrawString(catFont, myGoods[ig].code + " " + myGoods[ig].description, myGoods[ig].position + new Vector2(2, 2) + new Vector2((graphics.PreferredBackBufferWidth - 380), 0), Color.LightGray);
                    //}
                    spriteBatch.End();
                }
            }

            base.Draw(gameTime);
            if (print)
            {
                ResolveTexture2D resolveTexture = new ResolveTexture2D(
                    graphics.GraphicsDevice,
                    graphics.GraphicsDevice.PresentationParameters.BackBufferWidth,
                    graphics.GraphicsDevice.PresentationParameters.BackBufferHeight,
                    1,
                    graphics.GraphicsDevice.PresentationParameters.BackBufferFormat);
                graphics.GraphicsDevice.ResolveBackBuffer(resolveTexture);
                resolveTexture.Save("image.bmp", ImageFileFormat.Bmp);
                print = false;
            }
        }

        public void DrawLine(Vector2 vector1, Vector2 vector2, Color colour, int width)
        {
            float distance = Vector2.Distance(vector1, vector2);
            Vector2 length = vector2 - vector1;
            length.Normalize();
            int count = (int)Math.Round(distance);
            for (int x = 0; x < count; ++x)
            {
                vector1 += length;
                spriteBatch.Draw(pixel, new Vector2(0, 0) + vector1, null, colour, 0, Vector2.Zero, new Vector2(width, width), SpriteEffects.None, 0);
            }
        }
    }
}
