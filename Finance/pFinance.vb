Imports System.Data
Public Class pFinance
    Dim init As Boolean = False
    Dim APP As Excel.Application = Globals.XLFinance.Application
    Dim leWS As Excel.Worksheet = Nothing


    Public Sub initialise()

        If Not init Then
            Try
                Dim APP As Excel.Application = Globals.XLFinance.Application
                APP.StatusBar = "Vues..."


                Me.init = True
                My.Settings.Reload()
                ConnexionFerme(SqlCon)
                ConnexionInit(My.Settings.VueConStr, SqlCon)
                Me.tInit.Text = "Connecté"
                APP.StatusBar = ""
            Catch ex As Exception
                Me.tInit.Text = "Non Connecté"
                APP.StatusBar = ""
            End Try
        End If
    End Sub

    Private Sub tInit_DoubleClick(sender As Object, e As EventArgs) Handles tInit.DoubleClick
        Dim a As String = InputBox("Mot de passe")
        If a = "!KEP" Then
            Dim frm As New fParam
            If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                init = False
                Call initialise()
            End If
        End If
    End Sub

    Private Sub pEx_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        init = False
        Call initialise()
    End Sub

    Sub OngletActive(aName As String)
        Try
            leWS = Nothing
            'cherche l'onglet Site
            For i = 1 To APP.Worksheets.Count
                If APP.Worksheets(i).name = aName Then leWS = APP.Worksheets(i)
            Next

            'Crée l'onglet si besoin
            If leWS Is Nothing Then
                APP.Worksheets.Add()
                APP.ActiveSheet.name = aName
                leWS = APP.ActiveSheet
            Else
                leWS.Select()
                leWS.Cells.Clear()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Sub ExtraitTable(laTable As String)
        Dim ssql As String
        Dim lers As OleDb.OleDbDataReader
        Dim curlig As Integer = 1
        Dim NbCol As Integer = 0
        Dim s As String

        Try
            Call OngletActive(laTable)

            'select c.name,cd.value from  sys.syscolumns as c
            'Left OUTER JOIN sys.extended_properties AS cd ON cd.major_id = c.id And cd.minor_id = c.colid And cd.name = 'MS_Description'
            'WHERE c.id = OBJECT_ID('Ilot') 

            APP.Columns("A:B").entirecolumn.hidden = True
            APP.Cells(1, 1).value = "HashCode"

            'Affiche les entetes de colonne
            ssql = "select c.name,cd.value from  sys.syscolumns as c Left OUTER JOIN sys.extended_properties AS cd ON cd.major_id = c.id And cd.minor_id = c.colid And cd.name = 'MS_Description' WHERE c.id = OBJECT_ID('" & laTable & "') order by colid"
            lers = SqlLit(ssql, SqlCon)
            While lers.Read
                NbCol += 1
                If Nz(lers("value"), "") = "" Then APP.Cells(1, NbCol + 1).value = lers("Name") Else APP.Cells(1, NbCol + 1).value = lers("value")
            End While
            lers.Close()

            'Affiche les enreg
            ssql = "Select * from " & laTable
            lers = SqlLit(ssql, SqlCon)
            While lers.Read
                curlig += 1
                s = ""
                For i = 1 To NbCol
                    APP.Cells(curlig, i + 1).value = lers(i - 1).ToString
                Next
                s = ""
                For i = 2 To NbCol
                    s &= APP.Cells(curlig, i + 1).value
                Next
                APP.Cells(curlig, 1).value = s.GetHashCode
            End While
            lers.Close()

            leWS.ListObjects.Add(Microsoft.Office.Interop.Excel.XlListObjectSourceType.xlSrcRange, leWS.Range(APP.Cells(1, 1), APP.Cells(curlig, NbCol + 1)),, Microsoft.Office.Interop.Excel.XlYesNoGuess.xlYes).Name = laTable


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub bSite_Click(sender As Object, e As EventArgs) Handles bSite.Click
        Call ExtraitTable("Site")
    End Sub

    Private Sub bIllot_Click(sender As Object, e As EventArgs) Handles bIllot.Click
        Call ExtraitTable("Ilot")
    End Sub

    Private Sub bCompte_Click(sender As Object, e As EventArgs) Handles bCompte.Click
        Call ExtraitTable("Compte")
    End Sub


    Sub Enreg(laTable As String)
        Dim ssql As String
        Dim lers As OleDb.OleDbDataReader
        Dim curligne As Integer = 1
        Dim NbCol As Integer = 0
        Dim LCHamp As New List(Of String)
        Dim HCode As String

        Try
            With APP.ActiveSheet
                'Mémorise les champs
                ssql = "Select Name FROM sys.columns WHERE object_id = OBJECT_ID('" & laTable & "') order by column_id"
                lers = SqlLit(ssql, SqlCon)
                While lers.Read
                    NbCol += 1
                    LCHamp.Add(lers(0).ToString)
                End While
                lers.Close()

                curligne = 2
                While Nz(.cells(curligne, 3).value, "") <> "" Or Nz(.cells(curligne, 2).value, "") <> ""
                    ssql = ""

                    If Nz(.cells(curligne, 3).value, "") <> "" Then 'si le 1er champ est non vide alors on est en ajout ou modif

                        'génère le Hcode pour comparaison
                        HCode = ""
                        For i = 2 To NbCol : HCode &= .cells(curligne, i + 1).value : Next
                        HCode = HCode.GetHashCode

                        If Nz(.cells(curligne, 1).value, "") <> HCode Then
                            'une modif est détectée
                            If Nz(.cells(curligne, 2).value, "") <> "" Then 'Lid n'est pas vide > en modif
                                ssql = "update " & laTable & " set "
                                For i = 2 To NbCol : ssql &= LCHamp(i - 1) & "=" & Cell2Sql(.cells(curligne, i + 1)) & " ," : Next
                                ssql = ssql.Remove(ssql.Length - 1, 1) & "  where " & LCHamp(0) & "=" & Cell2Sql(.cells(curligne, 2))
                            Else ' Insertion
                                ssql = "insert into " & laTable & " ("
                                For i = 2 To NbCol : ssql &= LCHamp(i - 1) & "," : Next i
                                ssql = ssql.Remove(ssql.Length - 1, 1) & " ) values ( "
                                For i = 2 To NbCol : ssql &= Cell2Sql(.cells(curligne, i + 1)) & " ," : Next i
                                ssql = ssql.Remove(ssql.Length - 1, 1) & " )"
                            End If
                        End If

                    Else
                        'suppression
                        ssql = "delete from " & laTable & "  where " & LCHamp(0) & "=" & Cell2Sql(.cells(curligne, 2))
                    End If
                    If ssql <> "" Then SqlDo(ssql, SqlCon)
                    curligne += 1
                End While

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub bEnreg_Click(sender As Object, e As EventArgs) Handles bEnreg.Click
        Select Case APP.ActiveSheet.name
            Case "Site"
                Call Enreg("Site")
                Call ExtraitTable("Site")
            Case "Ilot"
                Call Enreg("Ilot")
                Call ExtraitTable("Ilot")
            Case "Compte"
                Call Enreg("Compte")
                Call ExtraitTable("Compte")
        End Select
    End Sub

End Class
