Public Class fParam
    Private Sub fParam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Settings.Reload()
        pGrid1.SelectedObject = My.Settings
        Me.Text = "Parameters " & My.Application.Info.Version.ToString
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ConnexionTest(My.Settings.VueConStr) Then
            MsgBox("Connexion OK")
        Else
            MsgBox("Erreur connexion")

        End If
    End Sub

    Private Sub bAnnul_Click(sender As Object, e As EventArgs) Handles bAnnul.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub bOK_Click(sender As Object, e As EventArgs) Handles bOK.Click
        My.Settings.Save()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class