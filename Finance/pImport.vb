Public Class pImport

    Dim init As Boolean = False
    Dim APP As Excel.Application = Globals.XLFinance.Application
    Dim leWS As Excel.Worksheet = Nothing


    Public Sub initialise()

        If Not init Then
            Try
                Dim APP As Excel.Application = Globals.XLFinance.Application
                APP.StatusBar = "Init..."
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


    Private Sub pImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.dDAte.Value = Now.AddDays(-Now.Day)
        Call initialise()
    End Sub

    Private Sub bEnreg_Click(sender As Object, e As EventArgs) Handles bEnreg.Click
        Dim APP As Excel.Application = Globals.XLFinance.Application
        Dim b As Boolean = True
        Dim curligne As Integer
        Dim s() As String = ("Centre,Nature,Compte,Intitulé du compte,Débit,Crédit,Solde").Split(",")
        Dim ssql As String


        curligne = 4
        For i = 2 To 8
            If APP.Cells(curligne, i).value <> s(i - 2) Then b = False
        Next i

        If Not b Then
            MsgBox("La feuille de données ne semble pas correcte", MsgBoxStyle.OkOnly)
        Else
            If MsgBox("Enregsitrer les données comptables ?", MsgBoxStyle.OkCancel) Then
                curligne = 5
                While Nz(APP.Cells(curligne, 2).value, "") <> ""
                    If APP.Cells(curligne, 2).value.ToString.Length = 3 AndAlso Nz(APP.Cells(curligne, 2).value, "") <> Nz(APP.Cells(curligne - 1, 2).value, "") Then
                        ssql = "delete from comptaAna where month(dateCompta)=" & Me.dDAte.Value.Month & "  and year(DateCompta)=" & Me.dDAte.Value.Year & " and Centre='" & APP.Cells(curligne, 2).value & "'"
                        SqlDo(ssql, SqlCon)
                        MsgBox(ssql)
                    End If
                    curligne += 1
                End While


                curligne = 5
                While Nz(APP.Cells(curligne, 2).value, "") <> ""
                    APP.StatusBar = "Ligne " & curligne
                    If APP.Cells(curligne, 2).value.ToString.Length = 3 Then
                        ssql = "insert into comptaAna (Centre,Nature,Compte,Intitule,Debit,Credit,DateCompta,DateImport,TauxDevise) values (" _
                            & Cell2Sql(APP.Cells(curligne, 2)) & "," & Cell2Sql(APP.Cells(curligne, 3)) & "," & Cell2Sql(APP.Cells(curligne, 4)) _
                            & "," & Cell2Sql(APP.Cells(curligne, 5)) & "," & Cell2Sql(APP.Cells(curligne, 6)) & "," & Cell2Sql(APP.Cells(curligne, 7)) _
                            & "," & Date2sql(Me.dDAte.Value) & "," & Date2sql(Now) & "," & Num2sql(Me.tTaux.Text) & ")"
                        SqlDo(ssql, SqlCon)
                    End If
                    curligne += 1
                    APP.StatusBar = ""
                End While
                MsgBox("Enregistrement terminé ")
            End If
        End If
    End Sub
End Class
