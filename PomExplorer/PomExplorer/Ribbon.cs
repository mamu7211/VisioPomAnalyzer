﻿using PomExplorer.PomAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using Office = Microsoft.Office.Core;
using Visio = Microsoft.Office.Interop.Visio;

// TODO:  Führen Sie diese Schritte aus, um das Element auf dem Menüband (XML) zu aktivieren:

// 1: Kopieren Sie folgenden Codeblock in die ThisAddin-, ThisWorkbook- oder ThisDocument-Klasse.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon1();
//  }

// 2. Erstellen Sie Rückrufmethoden im Abschnitt "Menübandrückrufe" dieser Klasse, um Benutzeraktionen
//    zu behandeln, z.B. das Klicken auf eine Schaltfläche. Hinweis: Wenn Sie dieses Menüband aus dem Menüband-Designer exportiert haben,
//    verschieben Sie den Code aus den Ereignishandlern in die Rückrufmethoden, und ändern Sie den Code für die Verwendung mit dem
//    Programmmodell für die Menübanderweiterung (RibbonX).

// 3. Weisen Sie den Steuerelementtags in der Menüband-XML-Datei Attribute zu, um die entsprechenden Rückrufmethoden im Code anzugeben.  

// Weitere Informationen erhalten Sie in der Menüband-XML-Dokumentation in der Hilfe zu Visual Studio-Tools für Office.


namespace PomExplorer
{
    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        public Ribbon()
        {
        }

        #region IRibbonExtensibility-Member

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("PomExplorer.Ribbon.xml");
        }

        #endregion

        #region Menübandrückrufe
        //Erstellen Sie hier Rückrufmethoden. Weitere Informationen zum Hinzufügen von Rückrufmethoden finden Sie unter "http://go.microsoft.com/fwlink/?LinkID=271226".

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        public void OnTextButton(Office.IRibbonControl control)
        {
            Visio.Page page = Globals.ThisAddIn.Application.ActivePage;

            page.DrawRectangle(1, 1, 3, 2);
        }

        public void OnLoadPom(Office.IRibbonControl control)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Maven pom.xml|pom.xml|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var objectParser = new MavenProjectParser();
                var sr = new StreamReader(ofd.FileName);
                var xml = sr.ReadToEnd();
                var project = objectParser.Parse(xml);

                if (project != null)
                {
                    var rect = CreateRect(project.Name, 0, 0, 3, 1);
                    var rect2 = CreateRect("ASDF", 0, 3, 3, 1);
                    
                    Connect(rect, rect2);
                }

            }
        }

        private Visio.Shape CreateRect(String name, double offsetX, double offsetY, double width, double height)
        {
            Visio.Page page = Globals.ThisAddIn.Application.ActivePage;

            var cx = page.PageSheet.CellsU["PageWidth"].ResultIU / 2;
            var cy = page.PageSheet.CellsU["PageHeight"].ResultIU / 2;

            var rect = page.DrawRectangle(cx + offsetX - width/2, cy * 2 - offsetY,cx+offsetX+width/2,cy*2-offsetY- height);
            rect.Text = name;
            return rect;
        }

        private void Connect(Visio.Shape shape1, Visio.Shape shape2)
        {
            Visio.Page page = Globals.ThisAddIn.Application.ActivePage;
            var cn = page.Application.ConnectorToolDataObject;
            var conn = page.Drop(cn, 3, 3) as Visio.Shape;
            var x2 = shape1.CellsSRC[1, 1, 0];  //(1,1,0); //.GlueTo(conn.Cell;
            var x3 = conn.CellsU["BeginX"];
            var x4 = shape2.CellsSRC[1, 1, 0];
            var x5 = conn.CellsU["EndX"];
            x3.GlueTo(x2);
            x5.GlueTo(x4);
        }

        #endregion

        #region Hilfsprogramme

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
