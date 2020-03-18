<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class pImport
    Inherits System.Windows.Forms.UserControl

    'UserControl remplace la méthode Dispose pour nettoyer la liste des composants.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(pImport))
        Me.bEnreg = New System.Windows.Forms.Button()
        Me.dDAte = New System.Windows.Forms.DateTimePicker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tInit = New System.Windows.Forms.ToolStripStatusLabel()
        Me.i_info = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tTaux = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'bEnreg
        '
        Me.bEnreg.Location = New System.Drawing.Point(15, 95)
        Me.bEnreg.Name = "bEnreg"
        Me.bEnreg.Size = New System.Drawing.Size(149, 23)
        Me.bEnreg.TabIndex = 2
        Me.bEnreg.Text = "Enregistrer"
        Me.bEnreg.UseVisualStyleBackColor = True
        '
        'dDAte
        '
        Me.dDAte.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dDAte.Location = New System.Drawing.Point(50, 21)
        Me.dDAte.Name = "dDAte"
        Me.dDAte.Size = New System.Drawing.Size(114, 20)
        Me.dDAte.TabIndex = 3
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(22, 22)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tInit, Me.i_info})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 547)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(180, 27)
        Me.StatusStrip1.TabIndex = 26
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tInit
        '
        Me.tInit.DoubleClickEnabled = True
        Me.tInit.Name = "tInit"
        Me.tInit.Size = New System.Drawing.Size(24, 22)
        Me.tInit.Text = "init"
        '
        'i_info
        '
        Me.i_info.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.i_info.Image = CType(resources.GetObject("i_info.Image"), System.Drawing.Image)
        Me.i_info.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.i_info.IsLink = True
        Me.i_info.Name = "i_info"
        Me.i_info.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.i_info.Size = New System.Drawing.Size(141, 22)
        Me.i_info.Spring = True
        Me.i_info.Tag = ""
        Me.i_info.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Taux Devise -> €"
        '
        'tTaux
        '
        Me.tTaux.Location = New System.Drawing.Point(105, 48)
        Me.tTaux.Name = "tTaux"
        Me.tTaux.Size = New System.Drawing.Size(59, 20)
        Me.tTaux.TabIndex = 28
        Me.tTaux.Text = "1"
        Me.tTaux.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Date"
        '
        'pImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tTaux)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.dDAte)
        Me.Controls.Add(Me.bEnreg)
        Me.Name = "pImport"
        Me.Size = New System.Drawing.Size(180, 574)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bEnreg As Windows.Forms.Button
    Friend WithEvents dDAte As Windows.Forms.DateTimePicker
    Friend WithEvents StatusStrip1 As Windows.Forms.StatusStrip
    Friend WithEvents tInit As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents i_info As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents tTaux As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
End Class
