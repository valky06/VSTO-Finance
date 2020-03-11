<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fParam
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.bOK = New System.Windows.Forms.Button()
        Me.bAnnul = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'pGrid1
        '
        Me.pGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pGrid1.HelpVisible = False
        Me.pGrid1.Location = New System.Drawing.Point(12, 12)
        Me.pGrid1.Name = "pGrid1"
        Me.pGrid1.Size = New System.Drawing.Size(588, 239)
        Me.pGrid1.TabIndex = 18
        Me.pGrid1.ToolbarVisible = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(227, 257)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(95, 26)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "Test Connexion"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'bOK
        '
        Me.bOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bOK.BackColor = System.Drawing.SystemColors.Control
        Me.bOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.bOK.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bOK.ForeColor = System.Drawing.Color.Black
        Me.bOK.Location = New System.Drawing.Point(525, 257)
        Me.bOK.Name = "bOK"
        Me.bOK.Size = New System.Drawing.Size(75, 26)
        Me.bOK.TabIndex = 15
        Me.bOK.Text = "OK"
        Me.bOK.UseVisualStyleBackColor = False
        '
        'bAnnul
        '
        Me.bAnnul.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bAnnul.BackColor = System.Drawing.SystemColors.Control
        Me.bAnnul.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.bAnnul.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.bAnnul.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bAnnul.ForeColor = System.Drawing.Color.Black
        Me.bAnnul.Location = New System.Drawing.Point(12, 257)
        Me.bAnnul.Name = "bAnnul"
        Me.bAnnul.Size = New System.Drawing.Size(75, 26)
        Me.bAnnul.TabIndex = 14
        Me.bAnnul.Text = "Annuler"
        Me.bAnnul.UseVisualStyleBackColor = False
        '
        'fParam
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(612, 295)
        Me.Controls.Add(Me.pGrid1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.bOK)
        Me.Controls.Add(Me.bAnnul)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "fParam"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Paramètres"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pGrid1 As Windows.Forms.PropertyGrid
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents bOK As Windows.Forms.Button
    Friend WithEvents bAnnul As Windows.Forms.Button
End Class
