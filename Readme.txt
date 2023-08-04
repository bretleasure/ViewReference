# View Reference Add-in for Autodesk Inventor

The View Reference add-in creates references between parent and dependent views in Autodesk Inventor drawings. Text is added to the view callout in the parent view and then view label of the dependent view. References can be added to Details Views, Section Views, Projected Views, and Auxiliary Views. 

## Usage

The addin can either be used via the commmand buttons added to the Inventor UI or by using the addins API with your own code. 

## Installation

## Using the API

To use the View Reference Add-in API the dll needs to be referenced. This can be done by either downloading the dll from the Releases or by adding the ViewReference NuGet package. The dll includes an extension method for Inventor.Aplication called `GetViewReferenceAddin()` that makes instantiating the `ViewReferenceAutomation` object easier. 

`ViewReferenceAutomation` includes the following methods that can be used:

* `CreateReferences(ViewReferenceSettings)`
* `CreateReferences(DrawingDocument, ViewReferenceSettings)`
* `CreateReferences(DrawingView, ViewReferenceSettings)'
* 'RemoveReferences(DrawingDocument)`
* 'RemoveReferences(DrawingView)'

