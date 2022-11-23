using Kompas6API3D5COM;
using Kompas6API5;
using Kompas6Constants;
using KompasAPI7;
using Thread = System.Threading.Thread;


////ПОДКЛЮЧЕНИЕ К КОМПАСУ
//var kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");

//await Task.Run(() =>
//{
//    s_kompasObject = (KompasObject)Activator.CreateInstance(kompasType);
//    s_kompasObject.ActivateControllerAPI();
//    s_kompasObject.Visible = true;
//});

////СОЗДАНИЕ ДОКУМЕНТА
//Document3D doc3D = (Document3D)s_kompasObject.Document3D();
//doc3D.Create(false, true);

////СОЗДАНИЕ СКЕТЧА
//ksPart topPart = (ksPart)_document.GetPart(_topPartType);

//ksEntity drawEntity = (ksEntity)topPart.NewEntity(5);

//ksSketchDefinition sketchDefinition = (ksSketchDefinition)drawEntity.GetDefinition();

//ksEntity entityPlane = (ksEntity)topPart.GetDefaultEntity(1);

//sketchDefinition.SetPlane(entityPlane);
//drawEntity.Create();

//return sketchDefinition;

////СОЗДАНИЕ ЭСКИЗА КОЛЬЦА.
//ksDocument2D flatDocument = (ksDocument2D)sketchDefinition.BeginEdit();
//flatDocument.ksCircle(0, 0, radius, 1);
//sketchDefinition.EndEdit();


IApplication s_kompasApplication;

IKompasAPIObject s_kompasObject;

// ПОДКЛЮЧЕНИЕ К КОМПАСУ
var kompasType = Type.GetTypeFromProgID("KOMPAS.Application.7");


s_kompasObject = (IKompasAPIObject)Activator.CreateInstance(kompasType);
s_kompasApplication = s_kompasObject.Application;
s_kompasApplication.Visible = true;

Console.WriteLine("Успешное подключение");

// СОЗДАНИЕ ДОКУМЕНТА
Thread.Sleep(6000);
var activeDocument = (IKompasDocument3D)s_kompasApplication.ActiveDocument;
if (activeDocument is null
    || !activeDocument.Type.Equals(DocumentTypeEnum.ksDocumentPart))
{
    var newDocument = (IKompasDocument3D)s_kompasApplication.Documents.Add(DocumentTypeEnum.ksDocumentPart);
    s_kompasApplication.ActiveDocument = newDocument;
    activeDocument = newDocument;
}

Console.WriteLine("Документ создан");

var topPart = (IPart7)activeDocument.TopPart;
var container = (IModelContainer)topPart;
ISketchs sketches = container.Sketchs;

Console.WriteLine("Режим редактирования + IModelContainer");

ISketch newSketch = sketches.Add();


