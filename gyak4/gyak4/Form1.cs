using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace gyak4
{


    public partial class Form1 : Form
    {
        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül

        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;
        
        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();

        }

        private void LoadData()
        {
            Flats = context.Flats.ToList();
        }

        private void CreateExcel()
        {
            try 
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;
                CreateTable();
                xlApp.Visible = true;
                xlApp.UserControl = true;

            }

            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void CreateTable()
        {
            string[] headers = new string[] {"Kód","Eladó","Oldal","Kerület","Lift","Szobák száma","Alapterület (m2)","Ár (mFt)","Négyzetméter ár (Ft/m2)"};
            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i+1] = headers[i];
            }
        }

    }
}
