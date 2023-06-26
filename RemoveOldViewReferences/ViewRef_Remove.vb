#Region "Namespaces"

Imports System.Text
Imports System.Linq
Imports System.Xml
Imports System.Reflection
Imports System.ComponentModel
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Media.Imaging
Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Imports Inventor

#End Region

Namespace RemoveOldViewReferences

    Public Class ViewRef_Remove
        Public ViewRefAdded As Boolean
        Public oWrite As System.IO.StreamWriter
        Public oRead As System.IO.StreamReader
        Public UserFolder = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData
        Public ViewRefType As String
        Public DetailLabelType As String
        Public SectionLabelType As String
        Public AuxLabelType As String
        'Public oDoc As Document = AddinGlobal.InventorApp.ActiveDocument
        Private SavedViewRefType As String
        Private SavedDetailLabelType As String
        Private SavedSectionLabelType As String
        Private SavedAuxLabelType As String
        Private SavedDetailName As String
        Private SavedDetailSheetNum As String
        Private SavedDetailSheetName As String
        Private SavedParentViewSheetNum As String
        Private SavedParentViewSheetName As String
        Private DetailName As String
        Private DetailSheetNum As String
        Private DetailSheetName As String
        Private ParentViewSheetNum As String
        Private ParentViewSheetName As String
        Private SavedLabelType As String
        Private OldStyle As Boolean = False
        Private DefaultDetailText As String
        Private DefaultSectionText As String
        Private DefaultAuxText As String
        Private SavedLabelText As String
        Public TrialPeriodOver As Boolean
        Public ViewRefDisabled As Boolean

        Public InvApp As Inventor.Application

        ''' <summary>
        ''' 
        ''' </summary>

        Public Sub Remove_ViewRefs(Inventor As Inventor.Application)

            InvApp = Inventor


            'MessageBox.Show("This will remove view references!")

            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            'Get Default Label Text
            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

            SetDefaultLabels()
            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

            Dim oDoc As Document
            oDoc = InvApp.ActiveDocument

            Dim oSheet As Sheet
            Dim oSheets As Sheets
            oSheets = oDoc.Sheets

            For Each oSheet In oSheets

                Dim oView As DrawingView
                Dim oViews As DrawingViews
                oViews = oSheet.DrawingViews

                For Each oView In oViews

                    Select Case oView.ViewType
                        Case "10502", "10503", "10499" 'Only Change Detail Section and Auxillary Views

                            Try

                                'Reset Parameter Values
                                SavedViewRefType = ""
                                SavedDetailLabelType = ""
                                SavedDetailName = ""
                                SavedDetailSheetNum = ""
                                SavedDetailSheetName = ""
                                SavedParentViewSheetNum = ""
                                SavedParentViewSheetName = ""
                                DetailName = ""
                                DetailSheetNum = ""
                                DetailSheetName = ""
                                ParentViewSheetNum = ""
                                ParentViewSheetName = ""
                                SavedLabelText = ""
                                SavedSectionLabelType = ""
                                SavedAuxLabelType = ""

                                'Get Parameter Values from View Attributes
                                GetSavedAttributesFromView(oView)


                                '////////////////////////////////////////// CHECK FOR OLD REFERENCES & REMOVE \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                                'MessageBox.Show(oView.Label.FormattedText)
                                If SavedDetailLabelType = "" Or SavedSectionLabelType = "" Then
                                    If InStr(oView.Label.FormattedText, "<DrawingViewName/>") = 0 Then
                                        Dim OldRef As New OldReferences
                                        OldRef.InvApp = InvApp
                                        OldRef.Remove()
                                        'Exits interation and Continues to next
                                        Continue For
                                    End If
                                End If

                                '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\END CHECK FOR OLD REFERENCES & REMOVE ///////////////////////////////////////


                                '/////////////////////////////////////////////////// RENAME CALLOUTS \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

                                oView.Name = SavedDetailName

                                '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ END RENAME CALLOUTS ///////////////////////////////////////////////


                                'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                                'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX VIEW LABELS XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                                'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


                                '///////////////////////////////////////////////// REMOVE LABEL TYPE \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


                                Dim RemovedLabel As String

                                RemovedLabel = RemoveLabelReferences(oView, GetSavedLabelString(oView))

                                'MessageBox.Show("Removed Label = " & RemovedLabel)
                                oView.Label.FormattedText = RemovedLabel

                                '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ END REMOVE LABEL TYPE /////////////////////////////////////////////////


                            Catch ex As Exception

                            Finally
                                'ALWAYS CLEAR ATTRIBUTES

                                ClearAttributes(oView)

                            End Try



                    End Select
                Next

            Next


        End Sub

        Sub SaveAttributes(ByRef oView As DrawingView)

            Dim oAttribSet As AttributeSet
            oAttribSet = oView.AttributeSets.Item("ViewReference")

            oAttribSet.Item("ViewRefType").Value = ViewRefType
            oAttribSet.Item("DetailLabelType").Value = DetailLabelType
            oAttribSet.Item("SectionLabelType").Value = SectionLabelType
            oAttribSet.Item("AuxLabelType").Value = AuxLabelType
            oAttribSet.Item("DetailName").Value = DetailName
            oAttribSet.Item("ParentViewSheetNum").Value = ParentViewSheetNum
            oAttribSet.Item("ParentViewSheetName").Value = ParentViewSheetName
            oAttribSet.Item("DetailSheetNum").Value = DetailSheetNum
            oAttribSet.Item("DetailSheetName").Value = DetailSheetName
            oAttribSet.Item("SavedLabelText").Value = SavedLabelText

        End Sub

        Sub GetSavedAttributesFromView(ByRef oView As DrawingView)
            Dim oAttribSet As AttributeSet

            If oView.AttributeSets.NameIsUsed("ViewReference") = True Then

                oAttribSet = oView.AttributeSets.Item("ViewReference")
                'Check to see if Attributes Exist
                CreateAttributes(oView)

                'Get Attributes
                SavedViewRefType = oAttribSet.Item("ViewRefType").Value
                SavedDetailName = oAttribSet.Item("DetailName").Value
                SavedDetailSheetName = oAttribSet.Item("DetailSheetName").Value
                SavedDetailSheetNum = oAttribSet.Item("DetailSheetNum").Value
                SavedParentViewSheetNum = oAttribSet.Item("ParentViewSheetNum").Value
                SavedParentViewSheetName = oAttribSet.Item("ParentViewSheetName").Value
                SavedDetailLabelType = oAttribSet.Item("DetailLabelType").Value
                SavedSectionLabelType = oAttribSet.Item("SectionLabelType").Value

                Try
                    'this attribute was added on 10/21/2016, anything created before then won't have it
                    SavedLabelText = oAttribSet.Item("SavedLabelText").Value
                Catch
                End Try
                Try
                    'this attribute was added on 3/20/2018, anything created before then won't have it
                    SavedAuxLabelType = oAttribSet.Item("AuxLabelType").Value
                Catch
                End Try


            Else
                'Create Attribute Set
                oAttribSet = oView.AttributeSets.Add("ViewReference")
                CreateAttributes(oView)

            End If

            'Make Attributes Follow Views to Different Sheets
            oAttribSet.CopyWithOwner = True
        End Sub

        Function GetSavedLabelString(ByRef oView As DrawingView) As String
            'This is used to remove an existing label

            Dim LabelString As String

            Select Case oView.ViewType
                Case "10502"
                    'Detail
                    LabelString = CreateLabelString(SavedDetailLabelType, SavedDetailName, SavedDetailSheetNum, SavedDetailSheetName, SavedParentViewSheetNum,
                                        SavedParentViewSheetName)
                Case "10503"
                    'Section
                    LabelString = CreateLabelString(SavedSectionLabelType, SavedDetailName, SavedDetailSheetNum, SavedDetailSheetName, SavedParentViewSheetNum,
                                            SavedParentViewSheetName)
                Case "10499"
                    'Auxillary
                    LabelString = CreateLabelString(SavedAuxLabelType, SavedDetailName, SavedDetailSheetNum, SavedDetailSheetName, SavedParentViewSheetNum,
                                            SavedParentViewSheetName)
                Case Else
                    LabelString = "Unknown"
            End Select
        End Function

        Function GetParentViewSheetNumber(ByVal PViewName As String)

            'Dim ParentViewName As String = oView.ParentView.Parent.Name
            Dim ParentViewNameLength As Double = Len(PViewName)
            Dim pvposition As Double = InStr(PViewName, ":")
            pvposition = ParentViewNameLength - pvposition
            Dim ParentViewSheet As String = Right(PViewName, pvposition)

            Return ParentViewSheet

        End Function

        Function GetParentViewSheetName(ByVal PViewName As String)

            'Dim ParentViewNameLength As Double = Len(PViewName)
            'Dim pvposition As Double = InStr(PViewName, ":")
            'pvposition = ParentViewNameLength - pvposition
            Dim ParentViewSheet As String = Left(PViewName, (InStr(PViewName, ":") - 1))

            Return ParentViewSheet

        End Function

        Sub CreateAttributes(ByRef oView As DrawingView)

            Dim oAttribSet As AttributeSet = oView.AttributeSets.Item("ViewReference")

            Dim AttName As String

            For i = 1 To 10
                Select Case i
                    Case 1
                        AttName = "ViewRefType"
                    Case 2
                        AttName = "DetailName"
                    Case 3
                        AttName = "DetailSheetNum"
                    Case 4
                        AttName = "DetailSheetName"
                    Case 5
                        AttName = "ParentViewSheetNum"
                    Case 6
                        AttName = "ParentViewSheetName"
                    Case 7
                        AttName = "DetailLabelType"
                    Case 8
                        AttName = "SectionLabelType"
                    Case 9
                        AttName = "SavedLabelText"
                    Case 10
                        AttName = "AuxLabelType"
                    Case Else
                        AttName = "Blank"
                End Select

                If oAttribSet.NameIsUsed(AttName) = False Then
                    oAttribSet.Add(AttName, ValueTypeEnum.kStringType, "")
                End If
            Next

        End Sub

        Function CreateLabelString(ByRef LabelType As String, ByRef DetailName As String, ByRef DetailSheetNum As String, ByRef DetailSheetName As String,
                                      ByRef ParentViewSheetNum As String, ByRef ParentViewSheetName As String)

            Dim StartLabel As String
            StartLabel = LabelType

            If DetailName <> "" Then
                StartLabel = Replace(StartLabel, "<VIEW>", DetailName)
            End If
            If DetailSheetNum <> "" Then
                StartLabel = Replace(StartLabel, "<VIEW SHEET #>", DetailSheetNum)
            End If
            If DetailSheetName <> "" Then
                StartLabel = Replace(StartLabel, "<VIEW SHEET NAME>", DetailSheetName)
            End If
            If ParentViewSheetNum <> "" Then
                StartLabel = Replace(StartLabel, "<PARENT SHEET #>", ParentViewSheetNum)
            End If
            If ParentViewSheetName <> "" Then
                StartLabel = Replace(StartLabel, "<PARENT SHEET NAME>", ParentViewSheetName)
            End If

            Return StartLabel

        End Function

        Function RemoveLabelReferences(ByRef oView As DrawingView, ByRef LabelString As String)


            Dim ResultString As String = ""

            Dim ViewLabel As String = oView.Label.FormattedText

            If SavedLabelText <> "" Then 'This will be blank if it was created with old version
                ResultString = SavedLabelText

            Else
                If oView.ViewType = "10502" Then 'Detail
                    ResultString = Replace(ViewLabel, LabelString, DefaultDetailText) '"<DrawingViewName/>")
                ElseIf oView.ViewType = "10503" Then 'Section
                    ResultString = Replace(ViewLabel, LabelString, DefaultSectionText) '"<DrawingViewName/>-<DrawingViewName/>")
                ElseIf oView.ViewType = "10499" Then 'Auxillary
                    ResultString = Replace(ViewLabel, LabelString, DefaultAuxText) '"<DrawingViewName/>-<DrawingViewName/>")
                End If
            End If


            Return ResultString

        End Function

        Sub SetDefaultLabels()

            Dim oDoc As Document
            oDoc = InvApp.ActiveDocument

            Dim oStylesManager As DrawingStylesManager
            oStylesManager = oDoc.StylesManager

            Dim oStandard As DrawingStandardStyle
            oStandard = oStylesManager.ActiveStandardStyle

            ' Get the view label defaults for draft view
            Dim sPrefix As String = "", bVisible As Boolean, bConstrainToBorder As Boolean, bPlaceBelowView As Boolean

            Call oStandard.GetViewLabelDefaults("10502", sPrefix, bVisible, DefaultDetailText, bConstrainToBorder, bPlaceBelowView)
            Call oStandard.GetViewLabelDefaults("10503", sPrefix, bVisible, DefaultSectionText, bConstrainToBorder, bPlaceBelowView)
            Call oStandard.GetViewLabelDefaults("10499", sPrefix, bVisible, DefaultAuxText, bConstrainToBorder, bPlaceBelowView)

            Dim ViewLength As Integer
            ViewLength = Len("<DrawingViewName/>")

            DefaultDetailText = Mid(DefaultDetailText, InStr(DefaultDetailText, "<DrawingViewName/>"), InStrRev(DefaultDetailText, "<DrawingViewName/>") - InStr(DefaultDetailText, "<DrawingViewName/>") + ViewLength)
            DefaultSectionText = Mid(DefaultSectionText, InStr(DefaultSectionText, "<DrawingViewName/>"), InStrRev(DefaultSectionText, "<DrawingViewName/>") - InStr(DefaultSectionText, "<DrawingViewName/>") + ViewLength)
            DefaultAuxText = Mid(DefaultAuxText, InStr(DefaultAuxText, "<DrawingViewName/>"), InStrRev(DefaultAuxText, "<DrawingViewName/>") - InStr(DefaultAuxText, "<DrawingViewName/>") + ViewLength)


        End Sub

        Function GetViewText(ByRef oView As DrawingView) As String
            'this returns the text including any overrides 

            Dim ViewLength As Integer
            ViewLength = Len("<DrawingViewName/>")

            Dim FormattedText As String
            FormattedText = oView.Label.FormattedText

            Return Mid(FormattedText, InStr(FormattedText, "<DrawingViewName/>"), InStrRev(FormattedText, "<DrawingViewName/>") - InStr(FormattedText, "<DrawingViewName/>") + ViewLength)

            'Select Case oView.ViewType
            '    Case "10502"
            '        'Detail View
            '        Return Mid(FormattedText, InStr(FormattedText, "<DrawingViewName/>"), InStrRev(FormattedText, "<DrawingViewName/>") - InStr(FormattedText, "<DrawingViewName/>") + ViewLength)
            '    Case "10503"
            '        'Section View
            '        Return Mid(FormattedText, InStr(FormattedText, "<DrawingViewName/>"), InStrRev(FormattedText, "<DrawingViewName/>") - InStr(FormattedText, "<DrawingViewName/>") + ViewLength)
            'End Select

        End Function

        Function get_OccurrenceCount(ByRef FindString, ByRef SourceString) As Integer

            Dim SourceStringLength, FindStringLength, newString, TotalOccCount As Integer

            SourceStringLength = Len(SourceString)
            FindStringLength = Len(FindString)
            newString = Replace(SourceString, FindString, "")

            TotalOccCount = (SourceStringLength - Len(newString)) / FindStringLength

            Return TotalOccCount

        End Function

        Sub ClearAttributes(ByRef oView As DrawingView)
            If oView.AttributeSets.NameIsUsed("ViewReference") = True Then
                Dim oAttribSet As AttributeSet

                oAttribSet = oView.AttributeSets.Item("ViewReference")

                oAttribSet.Item("ViewRefType").Value = ""
                oAttribSet.Item("DetailLabelType").Value = ""
                oAttribSet.Item("SectionLabelType").Value = ""
                oAttribSet.Item("AuxLabelType").Value = ""
                oAttribSet.Item("DetailName").Value = ""
                oAttribSet.Item("ParentViewSheetNum").Value = ""
                oAttribSet.Item("ParentViewSheetName").Value = ""
                oAttribSet.Item("DetailSheetNum").Value = ""
                oAttribSet.Item("DetailSheetName").Value = ""
                oAttribSet.Item("SavedLabelText").Value = ""

            End If
        End Sub

    End Class
End Namespace
