using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visio = Microsoft.Office.Interop.Visio;
using VisCellIndicies = Microsoft.Office.Interop.Visio.VisCellIndices;
using VisSectionIndices = Microsoft.Office.Interop.Visio.VisSectionIndices;
using PomExplorer.PomAccess;
using System.IO;

namespace PomExplorer.VisioDrawing
{
    class PomPainter
    {

        private Visio.Page _page;
        private double _rectWidth = 2.5;
        private double _rectHeight = 0.5;
        private double _spacing = 0.5;
        private double _centerHor;
        private double _centerVert;
        private double _pageTop;
        private String _fontSize = "9pt";
        private MavenProject _project;

        public PomPainter(Visio.Page page, MavenProject project)
        {
            _page = page;
            _centerHor = _page.PageSheet.CellsU["PageWidth"].ResultIU / 2;
            _pageTop = _page.PageSheet.CellsU["PageHeight"].ResultIU;
            _centerVert = _pageTop / 2;

        }

        public void Paint()
        {
            var rectProject = CreateRect(_project.ArtifactSummary, 0, _rectHeight * 2);
            drawDependencies(_project, rectProject);
        }

        private void drawDependencies(MavenProject project, Visio.Shape rectProject)
        {
            if (project.Dependencies.Count == 0) return;

            var fullWidth = project.Dependencies.Count * _rectWidth + (project.Dependencies.Count - 1) * _spacing;
            var oneWidth = fullWidth / project.Dependencies.Count;
            var currentOffset = -(project.Dependencies.Count / 2.0);

            foreach (var dependency in project.Dependencies)
            {
                var rectDependency = CreateRect(dependency.ArtifactSummary, _centerHor + currentOffset, _centerVert);
                Connect(rectProject, rectDependency);
                currentOffset += oneWidth;
            }

            foreach(var module in project.Modules)
            {
                var xml = new StreamReader(module.ModuleFileName).ReadToEnd();
                var moduleProject = new MavenProjectParser().Parse(xml);
            }
        }

        private Visio.Shape CreateRect(String name, double offsetX, double offsetY)
        {
            return CreateRect(name, offsetX, offsetY, _rectWidth, _rectHeight);
        }

        private Visio.Shape CreateRect(String name, double offsetX, double offsetY, double width, double height)
        {
            Visio.Page page = Globals.ThisAddIn.Application.ActivePage;
            var rect = page.DrawRectangle(_centerHor + offsetX - width / 2, _pageTop - offsetY, _centerHor + offsetX + width / 2, _pageTop - offsetY - height);
            var textFont = rect.CellsSRC[(short)VisSectionIndices.visSectionCharacter, 0, (short)VisCellIndicies.visCharacterSize];
            var textAlign = rect.CellsSRC[(short)VisSectionIndices.visSectionCharacter, 0, (short)VisCellIndicies.visHorzAlign];
            textFont.FormulaU = _fontSize;
            rect.Text = name;
            textAlign.FormulaU = "0";
            return rect;
        }

        private void Connect(Visio.Shape shape1, Visio.Shape shape2)
        {
            var cn = _page.Application.ConnectorToolDataObject;
            var connector = _page.Drop(cn, 3, 3) as Visio.Shape;

            var anchorShape1 = shape1.CellsSRC[1, 1, 0];
            var beginConnector = connector.CellsU["BeginX"];
            var anchorShape2 = shape2.CellsSRC[1, 1, 0];
            var endConnector = connector.CellsU["EndX"];

            beginConnector.GlueTo(anchorShape1);
            endConnector.GlueTo(anchorShape2);
        }
    }
}
