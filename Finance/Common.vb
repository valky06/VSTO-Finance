Imports System.Data
Imports Microsoft.SqlServer.Management.IntegrationServices
Imports System.IO
Imports System.Data.SqlClient
Imports System.Collections.ObjectModel
'Imports Microsoft.Office.Interop

Public Module Common
    Public SqlCon As New OleDb.OleDbConnection

#Region "Factor"
    Function num2Fact(a As Integer, Nbcar As Integer) As String
        Return a.ToString.PadLeft(Nbcar, "0").Substring(0, Nbcar)
    End Function

    Function txt2Fact(s As String, Nbcar As Integer) As String
        s = s.ToUpper
        For i = 0 To s.Length - 1
            If Asc(s(i).ToString) < 48 Or Asc(s(i).ToString) > 93 Then s = s.Replace(s(i), " ")
        Next
        Return s.PadRight(Nbcar, " ").Substring(0, Nbcar)
    End Function

    Function mnt2Fact(m As Decimal, Nbcar As Integer) As String
        Return num2Fact(Math.Round(m, 2) * 100, Nbcar)
    End Function

    Function date2Fact(d As Date) As String
        Return d.ToString("ddMMyyyy")
    End Function

#End Region

#Region "Divers"

    Sub LienOuvre(leLien As String)
        System.Diagnostics.Process.Start(leLien)
    End Sub

    Sub DocOuvre(leDoc As String)
        System.Diagnostics.Process.Start(leDoc)
    End Sub

    Function Max(ByVal a As Integer, ByVal b As Integer) As Integer
        If a > b Then Return a Else Return b
    End Function

    Function Min(ByVal a As Integer, ByVal b As Integer) As Integer
        If a < b Then Return a Else Return b
    End Function

    Function MinDate(ByVal a As Date, ByVal b As Date) As Date
        If a < b Then Return a Else Return b
    End Function

    Function MaxDate(ByVal a As Date, ByVal b As Date) As Date
        If a < b Then Return b Else Return a
    End Function

    Public Function Nz(ByVal o As Object, ByVal valeurNull As Object) As String
        Try
            If IsDBNull(o) Or IsNothing(o) Then Return valeurNull Else Return o
        Catch ex As Exception
            Return valeurNull
        End Try
    End Function


    Function Num2sql(t As String) As String
        If Nz(t, "") = "" Then
            Return "0"
        Else
            Return Val(t.Replace(",", ".")).ToString.Replace(",", ".")
        End If
    End Function

    Function Num2txt(b As Double) As String
        Return b.ToString.Replace(".", ",")
    End Function

    Function Txt2num(t As String) As Decimal
        If t = "" Then
            Return 0
        Else
            Return Val(t.Replace(",", "."))
        End If
    End Function

    Function Sql2num(o As Object) As Decimal
        Dim a As Decimal
        a = Nz(o, 0)
        Return a
    End Function

    Function Txt2sql(s As Object) As String
        Return "'" & Nz(s, "").Replace("'", "''") & "'"
    End Function

    Function Date2sql(d As Date) As String
        If IsDBNull(d) Then
            Return "NULL"
        Else
            Return "'" & d.ToString("yyyy-MM-dd") & "'"
        End If
    End Function

    Function SqlDate(d As Windows.Forms.DateTimePicker) As String
        Return "'" & d.Value.ToString("yyyy-MM-dd") & "'"
    End Function

    Function Date2Grid(d As Object) As String
        Dim s As String = ""

        If Not IsDBNull(d) Then
            Dim laDate As Date = d
            If laDate.Year < 2099 Then s = laDate.ToString("dd/MM/yyyy")
        End If
        Return s
    End Function

    Function Date2Xl(d As Object) As String
        Dim s As String = ""

        If Not IsDBNull(d) Then
            Dim laDate As Date = d
            If laDate.Year < 2099 Then s = "'" & laDate.ToString("dd/MM/yyyy")
        End If
        Return s
    End Function

    Function FindeMois(d As Date) As Date
        Return d.AddDays(-d.Day + 1).AddMonths(1).AddDays(-1)
    End Function

    Function repNom(s As String) As String
        If s.Substring(s.Length - 1, 1) <> "\" Then s &= "\"
        Return s
    End Function

    Function Cell2Sql(a As Excel.Range) As String
        Select Case a.GetType.Name
            Case "String"
                Return Txt2sql(a.Value)
            Case "DateTime"
                Return Txt2sql(a.Value)
            Case "Boolean"
                Return a.Value
            Case Else
                Return Txt2sql(a.Value).Replace(",", ".")
        End Select
    End Function
#End Region

#Region "SQL"

    Sub ConnexionInit(ByVal strCon As String, ByRef consql As OleDb.OleDbConnection)
        Try
            ConnexionFerme(consql)
            consql.ConnectionString = strCon
        Catch ex As Exception
            Throw New Exception("Erreur d'initialisation de connexion")
        End Try
    End Sub

    Sub ConnexionFerme(ByRef consql As OleDb.OleDbConnection)
        Try
            If consql.State = ConnectionState.Open Then consql.Close()
        Catch ex As Exception
            Throw New Exception("Erreur fermeture connexion")
        End Try
    End Sub

    Public Function ConnexionTest(ByVal strCon As String) As Boolean
        Dim conSql As New OleDb.OleDbConnection
        Try
            If conSql.State = ConnectionState.Open Then conSql.Close()
            conSql.ConnectionString = strCon
            conSql.Open()
            conSql.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function SqlDo(ByVal s As String, ByVal consql As OleDb.OleDbConnection) As Boolean
        Dim lareq As New OleDb.OleDbCommand
        Try
            If consql.State <> ConnectionState.Open Then consql.Open()
            lareq.CommandText = s
            lareq.Connection = consql
            lareq.CommandType = CommandType.Text
            lareq.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message & s)
            Return False
            'Throw New Exception("Erreur Execution requête")
        End Try
    End Function

    Function SqlLit(ByVal s As String, ByVal consql As OleDb.OleDbConnection) As OleDb.OleDbDataReader
        Dim lareq As New OleDb.OleDbCommand

        Try
            If consql.State <> ConnectionState.Open Then consql.Open()
            lareq.CommandText = s
            lareq.Connection = consql
            lareq.CommandType = CommandType.Text
            Return lareq.ExecuteReader()
        Catch ex As Exception
            MsgBox(ex.Message & s)
            'Throw New Exception(ex.Message) 'TODO: vérifier la gestion de l'erreur
            Return Nothing
        End Try
    End Function


#End Region

#Region "Liste Combo"



    Public Class ListItem
        Public Value As Object
        Public Text As String

        Public Sub New(ByVal NewValue As Object, ByVal NewText As String)
            Value = NewValue
            Text = NewText
        End Sub

        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

    Public Sub ListRempli(ByVal leSql As String, ByVal laListe As Windows.Forms.ListBox, ByVal consql As OleDb.OleDbConnection)
        Dim leRs As OleDb.OleDbDataReader
        leRs = SqlLit(leSql, consql)
        While leRs.Read
            laListe.Items.Add(New ListItem(leRs(0), leRs(1)))
        End While
        leRs.Close()
    End Sub

    Public Sub ComboRempli(ByVal leSql As String, ByVal laCombo As Object, ByVal consql As OleDb.OleDbConnection, Optional ByVal AcceptVide As Boolean = True)
        Dim ligne As Boolean = False
        Dim lers As OleDb.OleDbDataReader
        Dim laValeur As Integer = 0

        '        If laCombo.SelectedIndex >= 0 Then laValeur = laCombo.text
        lers = SqlLit(leSql, consql)
        laCombo.Items.Clear()
        While lers.Read
            ligne = True
            If lers.FieldCount > 1 Then
                laCombo.Items.Add(New ListItem(lers(0), Nz(lers(1).ToString, "")))
            Else
                laCombo.Items.Add(lers(0))
            End If
        End While
        lers.Close()
        If Not AcceptVide Then laCombo.Enabled = ligne
        '        If laValeur <> 0 Then Call ComboSelectValue(laValeur, laCombo)
    End Sub

    Public Sub ComboSelectTxt(ByVal laValeur As String, ByVal laCombo As Windows.Forms.ComboBox)
        laCombo.SelectedIndex = -1
        laCombo.Text = ""
        For i = 0 To laCombo.Items.Count - 1
            If laCombo.Items(i).text = laValeur Then laCombo.SelectedIndex = i
        Next
    End Sub

    Public Sub ComboSelectValue(ByVal laValeur As String, ByVal laCombo As Windows.Forms.ComboBox)
        laCombo.SelectedIndex = -1
        laCombo.Text = ""
        For i = 0 To laCombo.Items.Count - 1
            If laCombo.Items(i).value = laValeur Then laCombo.SelectedIndex = i
        Next
    End Sub
#End Region

#Region "Formulaire"
    Public Class SQLchamp
        Public champNom As String
        Public champVal As String
        Public champTyp As Char
        Public champObl As Boolean

        Public Sub New(ByVal champ As Object, ByVal valeur As String, type As Char)
            champNom = champ
            champVal = valeur
            champTyp = type
        End Sub
    End Class

    Sub FormVide(b As Windows.Forms.Control)
        For Each c In b.Controls
            If c.tag <> "" Then
                Try
                    If TypeOf (c) Is Windows.Forms.TextBox Then c.text = ""
                    If TypeOf (c) Is Windows.Forms.ComboBox Then
                        c.items.clear()
                        c.selectedIndex = -1

                    End If

                    If TypeOf (c) Is Windows.Forms.GroupBox Then Call FormVide(c)
                Catch ex As Exception
                End Try
            End If
        Next
    End Sub

    Sub FormRempli(b As Windows.Forms.Control, sSql As String, ByVal consql As OleDb.OleDbConnection)
        Dim lers As OleDb.OleDbDataReader

        lers = SqlLit(sSql, consql)
        While lers.Read

            For Each c In b.Controls
                If c.tag <> "" Then
                    Try
                        If TypeOf (c) Is Windows.Forms.TextBox Then
                            c.text = Nz(lers(c.tag.split(",")(0)), "")
                        End If
                        If TypeOf (c) Is Windows.Forms.ComboBox Then
                            If c.tag.ToString.Contains(",t") Then
                                'Call ComboSelectValue(nz(lers(c.tag.split(",")(0)), 0), c)
                                c.text = Nz(lers(c.tag.split(",")(0)), "")
                            Else
                                Call ComboSelectValue(Nz(lers(c.tag.split(",")(0)), 0), c)
                            End If

                        End If
                        If TypeOf (c) Is Windows.Forms.CheckBox Then
                            c.checked = Nz(lers(c.tag.split(",")(0)), False)
                        End If

                        If TypeOf (c) Is Windows.Forms.DateTimePicker Then
                            If Nz(lers(c.tag), "") = "" Then
                                c.value = CDate("31/12/2100")
                                If c.showcheckbox Then c.checked = False
                            Else
                                c.value = lers(c.tag)
                                c.enabled = True
                                If c.showcheckbox Then c.checked = True
                            End If
                        End If

                    Catch ex As Exception
                        MsgBox(c.name & "-" & ex.Message)
                    End Try


                End If
            Next
        End While
        lers.Close()
    End Sub

    Function FormRecupereChamp(b As Windows.Forms.Control) As List(Of SQLchamp)
        Dim lesChamps As New List(Of SQLchamp)
        For Each c In b.Controls
            Try
                If TypeOf (c) Is Windows.Forms.TextBox Then
                    If c.tag.ToString.Split(",").Length > 1 Then
                        If c.tag.ToString.Split(",")(1).ToUpper.Contains("N") Then
                            lesChamps.Add(New SQLchamp(c.tag.ToString.Split(",")(0), "'" & Num2sql(c.text) & "'", c.tag.ToString.Split(",")(1)))
                        Else
                            lesChamps.Add(New SQLchamp(c.tag.ToString.Split(",")(0), "'" & Txt2sql(c.text) & "'", c.tag.ToString.Split(",")(1)))
                        End If
                    End If
                End If

                If TypeOf (c) Is Windows.Forms.ComboBox Then
                    If c.tag.ToString.Split(",").Length > 1 Then
                        If c.tag.ToString.Contains(",t") Then
                            lesChamps.Add(New SQLchamp(c.tag.ToString.Split(",")(0), "'" & c.text & "'", c.tag.ToString.Split(",")(1)))
                        Else
                            If c.selectedindex >= 0 Then
                                lesChamps.Add(New SQLchamp(c.tag.ToString.Split(",")(0), "'" & c.selecteditem.value & "'", c.tag.ToString.Split(",")(1)))
                            End If
                        End If

                    End If
                End If

                If TypeOf (c) Is Windows.Forms.CheckBox Then
                    If c.tag.ToString.Split(",").Length > 1 Then

                        lesChamps.Add(New SQLchamp(c.tag.ToString.Split(",")(0), IIf(c.checked, 1, 0), c.tag.ToString.Split(",")(1)))
                    End If
                End If

                If TypeOf (c) Is Windows.Forms.DateTimePicker Then
                    If c.enabled = False Or c.checked = False Then
                        lesChamps.Add(New SQLchamp(c.tag, "NULL", "d"))
                    Else
                        lesChamps.Add(New SQLchamp(c.tag, SqlDate(c), "d"))
                    End If
                End If

                If TypeOf (c) Is Windows.Forms.GroupBox Then lesChamps.AddRange(FormRecupereChamp(c))

            Catch ex As Exception
            End Try
        Next
        Return lesChamps
    End Function

    Function FormVerif(f As Windows.Forms.Control, erp As Windows.Forms.ErrorProvider) As Boolean
        Dim b As Boolean = True
        For Each c In f.Controls
            Try
                If TypeOf (c) Is Windows.Forms.GroupBox Then
                    If Not FormVerif(c, erp) Then b = False
                End If
                If c.tag <> "" Then
                    If c.tag.ToString.Contains(",") Then
                        If c.tag.ToString.Split(",")(1).Contains("o") Then
                            If TypeOf (c) Is Windows.Forms.TextBox Then
                                If c.text = "" Then
                                    erp.SetError(c, "Obligatoire")
                                    b = False
                                End If
                            End If
                            If TypeOf (c) Is Windows.Forms.ComboBox Then
                                If c.tag.ToString.Contains(",t") Then
                                    If c.text = "" Then
                                        erp.SetError(c, "Obligatoire")
                                        b = False
                                    End If
                                Else
                                    If c.selectedindex < 0 Then
                                        erp.SetError(c, "Obligatoire")
                                        b = False
                                    End If
                                End If

                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                Throw New Exception(ex.Message)
            End Try
        Next
        Return b
    End Function

    Function FormEnreg(b As Windows.Forms.Control, table As String, ByVal consql As OleDb.OleDbConnection) As Integer
        Dim sSqlAjoutChp As String = ""
        Dim sSqlAjoutVal As String = ""
        Dim sSqlModif As String = ""
        Dim sSql As String
        Dim lesChamps As New List(Of SQLchamp)
        Dim lidchamp As String = ""
        Dim lidval As Integer = 0
        Dim lers As OleDb.OleDbDataReader

        'vérifie les champs obligatoires

        'recupere les champs
        lesChamps = FormRecupereChamp(b)

        'créer les chaines sql en ajout et modif en meme temps et mémorise l'ID
        For Each c In lesChamps
            If c.champTyp = "k" Then
                lidchamp = c.champNom
                If c.champVal <> "" Then lidval = Val(c.champVal.Replace("'", ""))
            Else
                If c.champVal <> "" Then
                    sSqlAjoutChp &= c.champNom & ","
                    sSqlAjoutVal &= c.champVal & ","
                End If
                sSqlModif &= c.champNom & "=" & c.champVal & ","
            End If
        Next

        'supprime la derniere virgule de chaque chaine SQL
        sSqlModif = sSqlModif.Remove(sSqlModif.Length - 1, 1)
        sSqlAjoutChp = sSqlAjoutChp.Remove(sSqlAjoutChp.Length - 1, 1)
        sSqlAjoutVal = sSqlAjoutVal.Remove(sSqlAjoutVal.Length - 1, 1)

        'execute la requete
        If lidval <> 0 Then
            sSql = " update " & table & " set " & sSqlModif & " where " & lidchamp & "=" & lidval
        Else
            sSql = "insert into " & table & " (" & sSqlAjoutChp & ") values (" & sSqlAjoutVal & ")"
        End If
        SqlDo(sSql, consql)

        'recupere l'id du nouvel enreg
        If lidval = 0 Then
            sSql = "select max(" & lidchamp & ") from " & table
            lers = SqlLit(sSql, consql)
            While lers.Read
                lidval = lers(0)
            End While
            lers.Close()
        End If

        Return lidval


    End Function
#End Region



#Region "SSIS"
    Public Class SSISParam
        Public Nom As Object
        Public valeur As String
        Public type As String

        Public Sub New(ByVal leNom As Object, ByVal laValeur As String, leType As String)
            Nom = leNom
            valeur = laValeur
            type = leType ' PROJET / PACKAGE
        End Sub

        Public Overrides Function ToString() As String
            Return valeur
        End Function
    End Class

    Function SSISexecute(leProjet As String, lepackage As String) As Boolean
        Dim ExecOK As Boolean = True
        Dim leMSG As String = ""

        Try
            '"Data Source=" & My.Settings.SSISServer & ";Initial Catalog=master;Integrated Security=SSPI;Connection Timeout=300;"
            'Dim ssisConnection As New SqlConnection("Data Source=" & My.Settings.SSISServer & ";Initial Catalog=master;Connection Timeout=300;Persist Security Info=True;Password=Bgt56yhN;User ID=KEP\cssql2017;")

            Dim ssisConnection As New SqlConnection("Data Source=sqlc1;Initial Catalog=master;Password=M@iali;User ID=KEP\lvalcasara;Connection Timeout=300;")
            Dim ssisServer As New IntegrationServices(ssisConnection)
            Dim ssisPackage As PackageInfo = ssisServer.Catalogs("SSISDB").Folders("DataWH").Projects(leProjet).Packages(lepackage)
            Dim executionParameters As New Collection(Of PackageInfo.ExecutionValueParameterSet)
            'Dim executionParameter As New PackageInfo.ExecutionValueParameterSet
            '            executionParameter.ObjectType = 50
            '            executionParameter.ParameterName = "SYNCHRONIZED"
            '            executionParameter.ParameterValue = 1
            '            executionParameters.Add(executionParameter)



            Dim executionIdentifier As Long = ssisPackage.Execute(True, Nothing, executionParameters)
            Dim ExecutionOperation As ExecutionOperation = ssisServer.Catalogs("SSISDB").Executions(executionIdentifier)
            While Not ExecutionOperation.Completed
                ExecutionOperation.Refresh()
                System.Threading.Thread.Sleep(2000)
            End While

            For Each message As OperationMessage In ssisServer.Catalogs("SSISDB").Executions(executionIdentifier).Messages
                If message.MessageType = 120 Or message.MessageType = 140 Then ExecOK = False
                leMSG += Chr(10) + (message.MessageType.ToString() + ": " + message.Message)
            Next

            If Not ExecOK Then
                MsgBox(leMSG)
                '   F_Notif.tMSG.Text = leMSG
                '  F_Notif.ShowDialog()
            End If
        Catch ex As Exception
            ExecOK = False
            MsgBox(ex.Message)
        Finally
        End Try

        Return ExecOK
    End Function

#End Region

End Module
