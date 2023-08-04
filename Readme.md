# View Reference Add-in for Autodesk Inventor

The View Reference add-in creates references between parent and dependent views in Autodesk Inventor drawings. Text is added to the view callout in the parent view and then view label of the dependent view. References can be added to Details Views, Section Views, Projected Views, and Auxiliary Views. 

## Usage

The addin can either be used via the commmand buttons added to the Inventor UI or by using the addins API with your own code. 

## Installation

Add installation steps

## Using the API

To use the View Reference Add-in API the dll needs to be referenced. This can be done by either downloading the dll from the Releases or by adding the ViewReference NuGet package. The dll includes an extension method for Inventor.Aplication called `GetViewReferenceAddin()` that makes instantiating the `ViewReferenceAutomation` object easier. 

`ViewReferenceAutomation` includes the following methods that can be used:

| Method Name | Description |
| - | --- |
| `CreateReferences(ViewReferenceSettings)` | Adds references to all drawing views using the default settings |
| `CreateReferences(DrawingDocument, ViewReferenceSettings)` | Adds references to all drawing views using the provided settings |
| `CreateReferences(DrawingView, ViewReferenceSettings)` | Adds references to the one view with the provided settings |
| `RemoveReferences(DrawingDocument)` | Removes references from all drawing views |
| `RemoveReferences(DrawingView)` | Removes references from the one view |

```csharp
var dwgDoc = (DrawingDocument)inventorApp.Documents.Open(@"C:\Work\MyDrawing.idw");

var viewRefAddin = inventorApp.GetViewReferenceAddin();

//Uses Default ViewReferenceSettings
viewRefAddin.CreateReferences(dwgDoc);
```

### `ViewReferenceSettings`

`CalloutStyle`, `DetailViewLabelStyle`, `SectionViewLabelStyle`, `ProjectedViewStyle`, and `AuxiliaryViewLabelStyle` 

### Styling

View callouts and view labels use attribute tags to define the styling that should be used by the addin. The `AttributeTags` class contains all the possible tags that can be used.

#### Examples

##### Callout Styles

| Appears As | Styling String Text | Styling String Using Attribute Tags |
| - | --- | --- |
| B (2) | "<VIEW> (<VIEW SHEET #>)" | $"{AttributeTags.ViewName} ({AttributeTags.ViewSheetNumber})"
| B (Sh. 2) | "<VIEW> (Sh. <VIEW SHEET #>)" | $"{AttributeTags.ViewName} (Sh. {AttributeTags.ViewSheetNumber})" |
