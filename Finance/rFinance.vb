Option Explicit On
Imports System.IO
Imports Microsoft.Office.Tools.Ribbon
Imports System.Windows.Forms
Imports Office = Microsoft.Office.Tools
Imports Excel = Microsoft.Office.Interop.Excel

Public Class rFinance


    Private Sub Button1_Click(sender As Object, e As RibbonControlEventArgs) Handles Button1.Click
        Dim ctp As Office.CustomTaskPane
        Dim w = Globals.XLFinance.Application.ActiveWindow
        Dim PaneTrouve As Boolean = False
        Dim PaneName As String = "Paramètres Finance"

        For Each pane In Globals.XLFinance.CustomTaskPanes
            Try
                If pane.Window.Hwnd = w.Hwnd And pane.Title = PaneName Then
                    pane.Visible = Not pane.Visible
                    PaneTrouve = True
                End If
            Catch
            End Try
        Next

        If PaneTrouve = False Then
            ctp = Globals.XLFinance.CustomTaskPanes.Add(New pFinance, PaneName)
            ctp.Visible = True
        End If
    End Sub
End Class
