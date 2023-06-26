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
Imports InvViewReference

#End Region

Namespace RemoveOldViewReferences

    Public Class OldReferences

        Private ViewRefAdded As Boolean
        Private oWrite As System.IO.StreamWriter
        Private oRead As System.IO.StreamReader
        Private UserFolder = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData
        Private ViewRefType As String
        Private LabelType As String

        Public InvApp As Inventor.Application

        Public Sub Remove()

            'InvApp.AssemblyOptions.DeferUpdate = True

            If My.Computer.FileSystem.FileExists(UserFolder + "\V5E0I1W7R8E4F9E3R7E6N1C8E\VRSettings.txt") Then
                oRead = System.IO.File.OpenText(UserFolder + "\V5E0I1W7R8E4F9E3R7E6N1C8E\VRSettings.txt")
                While oRead.Peek <> -1
                    ViewRefType = oRead.ReadLine()
                End While
                oRead.Close()
            End If
            Dim oSheet As Sheet
            Dim oSheets As Sheets
            oSheets = InvApp.ActiveDocument.Sheets

            For Each oSheet In oSheets

                Dim oView As DrawingView
                Dim oViews As DrawingViews
                oViews = oSheet.DrawingViews

                For Each oView In oViews 'ViewLoop = 1 To oViewCount

                    Select Case oView.ViewType
                        Case "10502", "10503"

                            Dim CurrentViewRefType As String
                            Dim CurrentLabelType As String
                            Dim SavedDetailName As String = ""
                            Dim SavedParentViewSheet As String = ""

                            Dim oAttribSet As AttributeSet
                            'Dim oAttribSetLabel As AttributeSet
                            If oView.AttributeSets.NameIsUsed("ViewReference") = True Then

                                'Assign Attribute Values to Parameters
                                oAttribSet = oView.AttributeSets.Item("ViewReference")
                                CurrentViewRefType = oAttribSet.Item("ViewRefType").Value
                                CurrentLabelType = oAttribSet.Item("LabelType").Value
                                SavedDetailName = oAttribSet.Item("SavedDetailName").Value
                                SavedParentViewSheet = oAttribSet.Item("SavedParentViewSheet").Value

                                'Clear Attributes
                                oAttribSet.Item("ViewRefType").Value = ""
                                oAttribSet.Item("LabelType").Value = ""
                                oAttribSet.Item("SavedDetailName").Value = ""
                                oAttribSet.Item("SavedParentViewSheet").Value = ""

                            Else
                                'Check to See if Old Version of Reference is added
                                If InStr(oView.Label.FormattedText, "Sheet") <> 0 Then
                                    CurrentViewRefType = "Old Version"
                                    CurrentLabelType = "Old Version"
                                Else
                                    CurrentViewRefType = ""
                                    CurrentLabelType = ""
                                End If
                            End If

                            'rename detail view names to show up in detail call outs
                            Dim DetailName As String = oView.Name
                            DetailName = GetDetailName(DetailName, CurrentViewRefType)

                            oView.Name = DetailName

                            'Get Parent View Sheet Number
                            Dim ParentViewName As String = oView.ParentView.Parent.Name
                            Dim ParentViewSheet As String = ParentViewSheetNumber(ParentViewName)

                            Dim ViewLabel As String = oView.Label.FormattedText '= "SECTION <DrawingViewName/>-<DrawingViewName/><Delimiter/><Br/>"
                            Dim ViewType As ViewTypeEnum = oView.ViewType

                            If SavedDetailName = "" Then
                                SavedDetailName = DetailName
                            End If
                            If SavedParentViewSheet = "" Then
                                SavedParentViewSheet = ParentViewSheet
                            End If
                            'Remove View Label References
                            Dim NewLabel As String = RemoveLabelReference(CurrentLabelType, SavedParentViewSheet, SavedDetailName, ViewLabel, ViewType)
                            oView.Label.FormattedText = NewLabel

                    End Select

                Next



            Next

            'InvApp.AssemblyOptions.DeferUpdate = False

        End Sub
        Public Shared Function GetDetailName(ByVal DetailName As String, ByVal CurrentViewRefType As String)

            Dim NumberCharacter As Integer = FindFirstNumber(DetailName)
            Select Case CurrentViewRefType
                Case "Sh3Option" ' A (Sh. 3)
                    DetailName = Left(DetailName, (NumberCharacter - 7))
                Case "Sheet3Option" ' A (Sheet 3)
                    DetailName = Left(DetailName, (NumberCharacter - 9))
                Case "N3Option" ' A (3)
                    DetailName = Left(DetailName, (NumberCharacter - 3))
                Case "Dot3Option" ' A.3
                    DetailName = Left(DetailName, (NumberCharacter - 2))
                Case "N2Sh3Option" ' A1 SH3
                    DetailName = Left(DetailName, (NumberCharacter - 1))
                Case "NSh3Option" ' A SH3
                    DetailName = Left(DetailName, (NumberCharacter - 4))
                Case "Old Version"
                    'If Reference Was Added with old version
                    Dim dnposition = InStr(DetailName, " ")
                    If dnposition <> 0 Then
                        DetailName = Left(DetailName, dnposition - 1)
                    Else 'If Detail Name is the Dot3 Option
                        dnposition = InStr(DetailName, ".")
                        DetailName = Left(DetailName, dnposition - 1)
                    End If
                Case Else 'Aplies to views that were moved to different sheets before update
                    If InStr(DetailName, " SH") <> 0 Then 'A1 SH3  & A SH3 Options
                        If NumberCharacter < 5 Then
                            DetailName = Left(DetailName, (NumberCharacter - 1))
                        Else
                            DetailName = Left(DetailName, (NumberCharacter - 4))
                        End If
                    Else
                        DetailName = DetailName 'No Reference Added
                    End If

            End Select

            Return DetailName
        End Function
        Public Shared Function FindFirstNumber(ByVal StringCheck As String)

            Dim StringLength As Double = Len(StringCheck)
            Dim CurrentCharacter As String
            Dim FirstNumberCharacter As Integer
            For i = 1 To StringLength
                CurrentCharacter = Mid(StringCheck, i, 1)

                If IsNumeric(CurrentCharacter) = True Then
                    FirstNumberCharacter = i
                    Return FirstNumberCharacter

                    Exit Function
                End If
            Next

        End Function

        Public Shared Function ParentViewSheetNumber(ByVal PViewName As String)

            'Dim ParentViewName As String = oView.ParentView.Parent.Name
            Dim ParentViewNameLength As Double = Len(PViewName)
            Dim pvposition As Double = InStr(PViewName, ":")
            pvposition = ParentViewNameLength - pvposition
            Dim ParentViewSheet As String = Right(PViewName, pvposition)

            Return ParentViewSheet

        End Function

        Public Shared Function RemoveLabelReference(ByVal CurrentLabelType, ByVal SavedParentViewSheet, ByVal SavedDetailName, ByVal ViewLabel, ByVal ViewType)

            If CurrentLabelType = "" Then 'When views are moved to different sheets they dont have attributes
                If InStr(ViewLabel, "Sheet") <> 0 Then 'Sheet 2 Option
                    CurrentLabelType = "Sheet2Option"
                ElseIf InStr(ViewLabel, "(") Then ' (2) Option
                    CurrentLabelType = "P2POption"
                Else
                    CurrentLabelType = "N2Option" 'Orrin's Option
                End If
            End If

            Select Case CurrentLabelType
                Case "Sheet2Option", "Old Version"

                    If ViewType = "10502" Then 'detail view
                        Dim FirstSpace As Integer
                        FirstSpace = InStr(ViewLabel, " ")
                        If Left(ViewLabel, FirstSpace + 1) = SavedDetailName + "  " Then
                            ViewLabel = Replace(ViewLabel, SavedDetailName + "  ", "<DrawingViewName/>")
                        Else
                            ViewLabel = Replace(ViewLabel, " " + SavedDetailName + "  ", " " + "<DrawingViewName/>")
                        End If
                        Dim dvposition As Double = InStr(ViewLabel, "<Delimiter/>")
                        ViewLabel = Left(ViewLabel, dvposition - 1)


                    ElseIf ViewType = "10503" Then 'section view
                        ViewLabel = Replace(ViewLabel, SavedDetailName + "-" + SavedDetailName, "<DrawingViewName/>-<DrawingViewName/>")
                        Dim svposition As Double = InStr(ViewLabel, "<Delimiter/>")
                        ViewLabel = Left(ViewLabel, svposition - 1)

                    End If

                Case "N2Option"
                    If ViewType = "10502" Then 'detail view
                        Dim FirstSpace As Integer
                        FirstSpace = InStr(ViewLabel, " ")
                        If Left(ViewLabel, FirstSpace + 1) = SavedDetailName + SavedParentViewSheet + "  " Then
                            ViewLabel = Replace(ViewLabel, SavedDetailName + SavedParentViewSheet + "  ", "<DrawingViewName/>")
                        Else
                            ViewLabel = Replace(ViewLabel, " " + SavedDetailName + SavedParentViewSheet + "  ", " " + "<DrawingViewName/>")
                        End If



                    ElseIf ViewType = "10503" Then 'section view
                        ViewLabel = Replace(ViewLabel, SavedDetailName + SavedParentViewSheet + "-" + SavedDetailName + SavedParentViewSheet, "<DrawingViewName/>-<DrawingViewName/>")


                    End If

                Case "P2POption"
                    If ViewType = "10502" Then 'detail view
                        'Dim FirstSpace As Integer   'A_(2)_
                        'FirstSpace = InStr(ViewLabel, " ")
                        'If Left(ViewLabel, FirstSpace + 1) = DetailName + "  " Then
                        '    ViewLabel = Replace(ViewLabel, DetailName + "  ", "<DrawingViewName/>")
                        'Else
                        '    ViewLabel = Replace(ViewLabel, " " + DetailName + "  ", " " + "<DrawingViewName/>")
                        'End If
                        ViewLabel = Replace(ViewLabel, SavedDetailName + " (" + SavedParentViewSheet + ") ", "<DrawingViewName/>")

                    ElseIf ViewType = "10503" Then 'section view
                        ViewLabel = Replace(ViewLabel, SavedDetailName + "-" + SavedDetailName, "<DrawingViewName/>-<DrawingViewName/>")
                        ViewLabel = Replace(ViewLabel, " (" + SavedParentViewSheet + ") ", "")
                    End If

                    'Remove Parent Sheet Reference
                    ViewLabel = Replace(ViewLabel, " (" + SavedParentViewSheet + ") ", "")

            End Select



            Return ViewLabel

        End Function
    End Class

End Namespace
