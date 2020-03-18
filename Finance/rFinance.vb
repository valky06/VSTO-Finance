Option Explicit On
Imports Microsoft.Office.Tools.Ribbon
Imports Office = Microsoft.Office.Tools
Imports System.Data

'SELECT  h.instance_id,   j.name, h.step_id, h.step_name, h.run_date, h.run_time, h.run_status, h.run_duration
'FROM         msdb.dbo.sysjobhistory As h
'inner join msdb.dbo.sysjobs As j On j.job_id=h.job_id
'WHERE j.name = 'BI_FInance'
'And h.step_id<>0
''And run_date>= cast(convert(nvarchar(10),getdate(),112) as int)
'order by run_date asc

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

    Private Sub Button2_Click(sender As Object, e As RibbonControlEventArgs) Handles Button2.Click 'Centre	Nature	Compte	Intitulé du compte	Débit	Crédit	Solde
        Dim ctp As Office.CustomTaskPane
        Dim w = Globals.XLFinance.Application.ActiveWindow
        Dim PaneTrouve As Boolean = False
        Dim PaneName As String = "Import Quadra"

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
            ctp = Globals.XLFinance.CustomTaskPanes.Add(New pImport, PaneName)
            ctp.Visible = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As RibbonControlEventArgs) Handles Button3.Click
        Dim APP As Excel.Application = Globals.XLFinance.Application
        Dim ConSqlc1 As New OleDb.OleDbConnection

        Call ConnexionInit("Provider=SQLOLEDB.1;Persist Security Info=True;Password=SilmoMacro;User ID=ZCBN;Server=sqlc1;Database=msdb;", ConSqlc1)
        APP.StatusBar = "Traitement en cours"
        If Not SqlDo("exec sp_start_job  'BI_Finance'", ConSqlc1) Then
            MsgBox("Erreur dans le Traitement")
        Else
            MsgBox("Traitement mis en route sur le serveur.")
        End If
        '    If SSISexecute("10 - ControleGestion", "F_Compta.dtsx") Then MsgBox("Traitement terminé !")
        APP.StatusBar = ""
    End Sub

End Class
