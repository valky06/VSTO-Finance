Imports System.Data
Public Class pFinance
    Dim init As Boolean = False
    Dim APP As Excel.Application = Globals.XLFinance.Application
    Dim leWS As Excel.Worksheet = Nothing

    Function Cell2Txt(ACell As Excel.Range) As String
        Return Txt2sql(ACell.Value)
    End Function
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

    Private Sub bSite_Click(sender As Object, e As EventArgs) Handles bSite.Click
        Dim sSQL As String = ""
        Dim lers As OleDb.OleDbDataReader
        Dim curlig As Integer = 1

        Try
            Call OngletActive("Sites")

            APP.Columns("A:B").entirecolumn.hidden = True
            APP.Cells(1, 1).value = "Id"
            APP.Cells(1, 2).value = "HashCode"
            APP.Cells(1, 3).value = "Code Site"
            APP.Cells(1, 4).value = "Nom Site"

            lers = SqlLit("SELECT SiteId,SiteCode,SiteNom FROM Site order by SiteId", SqlCon)
            While lers.Read
                curlig += 1
                APP.Cells(curlig, 1).value = lers("SiteId").ToString
                APP.Cells(curlig, 2).value = (lers("SiteCode").ToString + lers("SiteNom").ToString).GetHashCode
                APP.Cells(curlig, 3).value = lers("SiteCode").ToString
                APP.Cells(curlig, 4).value = lers("SiteNom").ToString
            End While
            lers.Close()
            leWS.ListObjects.Add(Microsoft.Office.Interop.Excel.XlListObjectSourceType.xlSrcRange, leWS.Range("$A$1:$D$" & curlig),, Microsoft.Office.Interop.Excel.XlYesNoGuess.xlYes).Name = "Sites"

        Catch ex As Exception
            MsgBox(ex.Message & Chr(10) & sSQL)
        End Try

    End Sub

    Sub Sites_Enreg()
        Dim ssql As String


        Dim curligne As Integer = 2
        With APP.ActiveSheet

            While Nz(.cells(curligne, 3).value, "") <> "" Or Nz(.cells(curligne, 1).value, "") <> ""
                ssql = ""
                If Nz(.cells(curligne, 3).value, "") <> "" Then 'si le 1er champ est non vide alors on est en ajout ou modif
                    If Nz(.cells(curligne, 2).value, "") <> (Nz(.cells(curligne, 3).value, "") + Nz(.cells(curligne, 4).value, "")).GetHashCode.ToString Then
                        'une modif est détectée
                        If Nz(.cells(curligne, 1).value, "") <> "" Then
                            'Mise à jour
                            ssql = "update Site set SiteCode=" & Cell2Txt(.cells(curligne, 3)) & ", sitenom=" & Cell2Txt(.cells(curligne, 4)) & " where siteid=" & Cell2Txt(.cells(curligne, 1))
                        Else
                            'Insertion
                            ssql = "insert into Site (SiteCode,sitenom) values (" & Cell2Txt(.cells(curligne, 3)) & "," & Cell2Txt(.cells(curligne, 4)) & ")"
                        End If
                    End If

                Else
                    'suppression
                    ssql = "delete from Site where siteid=" & Cell2Txt(.cells(curligne, 1))
                End If
                If ssql <> "" Then SqlDo(ssql, SqlCon)
                curligne += 1
            End While
            Call bSite_Click(Nothing, Nothing)
        End With


    End Sub

    Sub Ilots_Enreg()
        'IlotNom,IlotAffec,IlotAffectCUOM
        Dim ssql As String
        Dim curligne As Integer = 2

        With APP.ActiveSheet
            While Nz(.cells(curligne, 3).value, "") <> "" Or Nz(.cells(curligne, 1).value, "") <> ""
                ssql = ""
                If Nz(.cells(curligne, 3).value, "") <> "" Then 'si le 1er champ est non vide alors on est en ajout ou modif
                    If Nz(.cells(curligne, 2).value, "") <> (Nz(.cells(curligne, 3).value, "") + Nz(.cells(curligne, 4).value, "") + Nz(.cells(curligne, 5).value, "")).GetHashCode.ToString Then
                        'une modif est détectée
                        If Nz(.cells(curligne, 1).value, "") <> "" Then
                            'Mise à jour
                            ssql = "update Ilot set IlotNom=" & Cell2Txt(.cells(curligne, 3)) & ", IlotAffec=" & Cell2Txt(.cells(curligne, 4)) & ", IlotAffectCUOM =" & Cell2Txt(.cells(curligne, 5)) & " where IlotId=" & Cell2Txt(.cells(curligne, 1))
                        Else
                            'Insertion
                            ssql = "insert into Ilot (IlotNom,IlotAffec,IlotAffectCUOM) values ('" & Cell2Txt(.cells(curligne, 3)) & "," & Cell2Txt(.cells(curligne, 4)) & "," & Cell2Txt(.cells(curligne, 5)) & ")"
                        End If
                    End If
                Else
                    'suppression
                    ssql = "delete from ilot where IlotId=" & Cell2Txt(.cells(curligne, 1))
                End If
                If ssql <> "" Then SqlDo(ssql, SqlCon)
                curligne += 1
            End While
            Call bIllot_Click(Nothing, Nothing)
        End With
    End Sub


    Sub Comptes_Enreg()
        'SELECT CptId,CptCode,CptNom,CptSIG,CptExploit,CptCategorie,CptResultat,CptRgt,CptResultat2,CptResultat3 FROM Compte

        Dim ssql As String
        Dim curligne As Integer = 2

        With APP.ActiveSheet
            While Nz(.cells(curligne, 3).value, "") <> "" Or Nz(.cells(curligne, 1).value, "") <> ""
                ssql = ""
                If Nz(.cells(curligne, 3).value, "") <> "" Then 'si le 1er champ est non vide alors on est en ajout ou modif
                    If Nz(.cells(curligne, 2).value, "") <> (Nz(.cells(curligne, 3).value, "") + Nz(.cells(curligne, 4).value, "") + Nz(.cells(curligne, 5).value, "") _
                    + Nz(.cells(curligne, 6).value, "") + Nz(.cells(curligne, 7).value, "") + Nz(.cells(curligne, 8).value, "") + Nz(.cells(curligne, 9).value, "") _
                    + Nz(.cells(curligne, 10).value, "") + Nz(.cells(curligne, 11).value, "")).GetHashCode.ToString Then
                        'une modif est détectée
                        If Nz(.cells(curligne, 1).value, "") <> "" Then
                            'Mise à jour
                            ssql = "update Compte set CptCode=" & Cell2Txt(.cells(curligne, 3)) _
                                & ", CptNom=" & Cell2Txt(.cells(curligne, 4)) _
                                & ", CptSIG=" & Cell2Txt(.cells(curligne, 5)) _
                                & ", CptExploit=" & Cell2Txt(.cells(curligne, 6)) _
                                & ", CptCategorie=" & Cell2Txt(.cells(curligne, 7)) _
                                & ", CptResultat=" & Cell2Txt(.cells(curligne, 8)) _
                                & ", CptRgt=" & Cell2Txt(.cells(curligne, 9)) _
                                & ", CptResultat2=" & Cell2Txt(.cells(curligne, 10)) _
                                & ", CptResultat3=" & Cell2Txt(.cells(curligne, 11)) _
                                & " where CptID=" & Cell2Txt(.cells(curligne, 1))
                        Else
                            'Insertion
                            ssql = "insert into Compte (CptCode,CptNom,CptSIG,CptExploit,CptCategorie,CptResultat,CptRgt,CptResultat2,CptResultat3) values (" _
                                & Cell2Txt(.cells(curligne, 3)) & "," & Cell2Txt(.cells(curligne, 4)) & "," & Cell2Txt(.cells(curligne, 5)) _
                                & "," & Cell2Txt(.cells(curligne, 6)) & "," & Cell2Txt(.cells(curligne, 7)) & "," & Cell2Txt(.cells(curligne, 8)) _
                                & "," & Cell2Txt(.cells(curligne, 9)) & "," & Cell2Txt(.cells(curligne, 10)) & "," & Cell2Txt(.cells(curligne, 11)) _
                                & ")"
                        End If
                    End If

                Else
                    'suppression
                    ssql = "delete from compte where Cptid=" & Cell2Txt(.cells(curligne, 1))
                End If
                If ssql <> "" Then SqlDo(ssql, SqlCon)
                curligne += 1
            End While
            Call bCompte_Click(Nothing, Nothing)
        End With
    End Sub


    Private Sub bEnreg_Click(sender As Object, e As EventArgs) Handles bEnreg.Click

        '        If APP.ActiveSheet.listobjects.count < 1 Then Exit Sub
        '        For i = 3 To APP.ActiveSheet.listobjects(1).range.columns.count
        '        Next i

        Dim curlig As Integer = 2
        Select Case APP.ActiveSheet.name
            Case "Sites" : Call Sites_Enreg()
            Case "Ilots" : Call Ilots_Enreg()
            Case "Comptes" : Call Comptes_Enreg()
        End Select

        'While Nz(APP.Cells(curlig, 3).value, "") <> ""
        'APP.Cells(curlig, 5).value = (APP.Cells(curlig, 3).value.ToString + APP.Cells(curlig, 4).value.ToString).GetHashCode
        'curlig += 1
        '        End While
    End Sub

    Private Sub bIllot_Click(sender As Object, e As EventArgs) Handles bIllot.Click
        Dim sSQL As String = ""
        Dim lers As OleDb.OleDbDataReader
        Dim curlig As Integer = 1

        Try
            Call OngletActive("Ilots")
            leWS.Activate()

            APP.Columns("A:B").entirecolumn.hidden = True
            APP.Cells(1, 1).value = "Id"
            APP.Cells(1, 2).value = "HashCode"
            APP.Cells(1, 3).value = "Ilot"
            APP.Cells(1, 4).value = "Affect."
            APP.Cells(1, 5).value = "Affect CUOM"
            lers = SqlLit("SELECT IlotId,IlotNom,IlotAffec,IlotAffectCUOM FROM Ilot", SqlCon)
            While lers.Read
                curlig += 1
                APP.Cells(curlig, 1).value = lers("IlotId").ToString
                APP.Cells(curlig, 2).value = (lers("IlotNom").ToString + lers("IlotAffec").ToString + lers("IlotAffectCUOM").ToString).GetHashCode
                APP.Cells(curlig, 3).value = lers("IlotNom").ToString
                APP.Cells(curlig, 4).value = lers("IlotAffec").ToString
                APP.Cells(curlig, 5).value = lers("IlotAffectCUOM").ToString
            End While
            lers.Close()
            leWS.ListObjects.Add(Microsoft.Office.Interop.Excel.XlListObjectSourceType.xlSrcRange, leWS.Range("$A$1:$E$" & curlig),, Microsoft.Office.Interop.Excel.XlYesNoGuess.xlYes).Name = "Ilots"

        Catch ex As Exception
            MsgBox(ex.Message & Chr(10) & sSQL)
        End Try
    End Sub

    Private Sub bCompte_Click(sender As Object, e As EventArgs) Handles bCompte.Click
        'SELECT CptId,CptCode,CptNom,CptSIG,CptExploit,CptCategorie,CptResultat,CptRgt,CptResultat2,CptResultat3 FROM Compte
        Dim sSQL As String = ""
        Dim lers As OleDb.OleDbDataReader
        Dim curlig As Integer = 1

        Try
            Call OngletActive("Comptes")

            APP.Columns("A:B").entirecolumn.hidden = True
            APP.Columns("C").ColumnWidth = 10
            APP.Columns("D:K").ColumnWidth = 30

            APP.Cells(1, 1).value = "Id"
            APP.Cells(1, 2).value = "HashCode"
            APP.Cells(1, 3).value = "Compte"
            APP.Cells(1, 4).value = "Nom"
            APP.Cells(1, 5).value = "SIG"
            APP.Cells(1, 6).value = "Cpt Exploit"
            APP.Cells(1, 7).value = "Catégorie"
            APP.Cells(1, 8).value = "Résultat"
            APP.Cells(1, 9).value = "Rgt"
            APP.Cells(1, 10).value = "Résultat 2"
            APP.Cells(1, 11).value = "Résultat 3"
            lers = SqlLit("SELECT CptId,CptCode,CptNom,CptSIG,CptExploit,CptCategorie,CptResultat,CptRgt,CptResultat2,CptResultat3 FROM Compte", SqlCon)
            While lers.Read
                curlig += 1
                APP.Cells(curlig, 1).value = lers("CptId").ToString
                APP.Cells(curlig, 2).value = (lers("CptCode").ToString + lers("CptNom").ToString + lers("CptSIG").ToString _
                    + lers("CptExploit").ToString + lers("CptCategorie").ToString + lers("CptResultat").ToString _
                    + lers("CptRgt").ToString + lers("CptResultat2").ToString + lers("CptResultat3").ToString).GetHashCode
                APP.Cells(curlig, 3).value = lers("CptCode").ToString
                APP.Cells(curlig, 4).value = lers("CptNom").ToString
                APP.Cells(curlig, 5).value = lers("CptSIG").ToString
                APP.Cells(curlig, 6).value = lers("CptExploit").ToString
                APP.Cells(curlig, 7).value = lers("CptCategorie").ToString
                APP.Cells(curlig, 8).value = lers("CptResultat").ToString
                APP.Cells(curlig, 9).value = lers("CptRgt").ToString
                APP.Cells(curlig, 10).value = lers("CptResultat2").ToString
                APP.Cells(curlig, 11).value = lers("CptResultat3").ToString
            End While
            lers.Close()
            leWS.ListObjects.Add(Microsoft.Office.Interop.Excel.XlListObjectSourceType.xlSrcRange, leWS.Range("$A$1:$K$" & curlig),, Microsoft.Office.Interop.Excel.XlYesNoGuess.xlYes).Name = "Comptes"

        Catch ex As Exception
            MsgBox(ex.Message & Chr(10) & sSQL)
        End Try
    End Sub
End Class
