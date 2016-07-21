using System;
using System.Collections.Generic;
using Visio = Microsoft.Office.Interop.Visio;
using VisCellIndicies = Microsoft.Office.Interop.Visio.VisCellIndices;
using VisSectionIndices = Microsoft.Office.Interop.Visio.VisSectionIndices;
using VisRowIndicies = Microsoft.Office.Interop.Visio.VisRowIndices;
using PomExplorer.Maven;

namespace PomExplorer.VisioDrawing
{
    class PomPainter
    {

        private Dictionary<String, Visio.Shape> _artifactsAlreadyDrawn = new Dictionary<string, Visio.Shape>();

        private Visio.Page _page;
        private double _rectWidth = 2.5;
        private double _rectHeight = 0.5;
        private double _spacing = 0.5;
        private double _centerHor;
        private double _centerVert;
        private double _pageTop;
        private String _fontSize = "9pt";
        private XmlProjectObjectModel _project;

        private PomPainterStyle _style;

        public PomPainter(Visio.Page page, XmlProjectObjectModel project)
        {
            _page = page;
            _centerHor = _page.PageSheet.CellsU["PageWidth"].ResultIU / 2;
            _pageTop = _page.PageSheet.CellsU["PageHeight"].ResultIU;
            _centerVert = _pageTop / 2;
            _project = project;
        }

        public void Paint(PomPainterStyle style)
        {
            _style = style;

            //if (_project.Shape == null)
            //{
            //    _project.Shape = CreateRect(_project, 0, _rectHeight * 2);
            //}
            DrawProject(_project);
        }

        public void DrawProject(XmlProjectObjectModel project)
        {

            if (_style == PomPainterStyle.PomDependencies || _style == PomPainterStyle.PomHierarchy) DrawDependencies(project);

            if (_style == PomPainterStyle.PomHierarchy) DrawModules(project);
        }

        private void DrawModules(XmlProjectObjectModel project)
        {
            foreach (var module in project.Modules)
            {
                //if (module.Project.Shape == null)
                //{
                //    module.Project.Shape = CreateRect(module.Project, _centerHor, _centerVert);
                //}

                //Connect(project.Shape, module.Project.Shape);

                //DrawProject(module.Project);
            }
        }

        private void DrawDependencies(XmlProjectObjectModel project)
        {
            foreach (var dependency in project.Dependencies)
            {
                var rectDependency = CreateRect(dependency, _centerHor, _centerVert);
                //Connect(project.Shape, rectDependency);
            }
        }

        private double getBottom(Visio.Shape shape)
        {
            return shape.CellsU["PinY"].ResultIU+shape.CellsU["Height"].ResultIU;
        }

        private Visio.Shape CreateRect(XmlArtifact artifact, double offsetX, double offsetY)
        {
            if (!_artifactsAlreadyDrawn.ContainsKey(artifact.ArtifactKey))
            {
                _artifactsAlreadyDrawn.Add(artifact.ArtifactKey, CreateRect(artifact.ArtifactSummary, offsetX, offsetY, _rectWidth, _rectHeight));
            }

            return _artifactsAlreadyDrawn[artifact.ArtifactKey];
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

            // https://msdn.microsoft.com/en-us/library/office/ff767991.aspx

            var anchorShape1 = shape1.CellsSRC[1, 1, 0];
            var beginConnector = connector.CellsU["BeginX"];
            var anchorShape2 = shape2.CellsSRC[1, 1, 0];
            var endConnector = connector.CellsU["EndX"];

            beginConnector.GlueTo(anchorShape1);
            endConnector.GlueTo(anchorShape2);

            connector.CellsSRC[(short)VisSectionIndices.visSectionObject, (short)VisRowIndicies.visRowLine, (short)VisCellIndicies.visLineEndArrow].FormulaU = "13";
            connector.CellsSRC[(short)VisSectionIndices.visSectionObject, (short)VisRowIndicies.visRowShapeLayout, (short)VisCellIndicies.visLineEndArrow].FormulaU = "1";
            connector.CellsSRC[(short)VisSectionIndices.visSectionObject, (short)VisRowIndicies.visRowShapeLayout, (short)VisCellIndicies.visSLOLineRouteExt].FormulaU = "16";
        }
    }
}
