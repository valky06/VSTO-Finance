<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class pFinance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(pFinance))
        Me.label10 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tInit = New System.Windows.Forms.ToolStripStatusLabel()
        Me.i_info = New System.Windows.Forms.ToolStripStatusLabel()
        Me.bEnreg = New System.Windows.Forms.Button()
        Me.bCompte = New System.Windows.Forms.Button()
        Me.bIllot = New System.Windows.Forms.Button()
        Me.bSite = New System.Windows.Forms.Button()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'label10
        '
        Me.label10.AutoSize = True
        Me.label10.Location = New System.Drawing.Point(0, -14)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(59, 13)
        Me.label10.TabIndex = 9
        Me.label10.Text = "Extractions"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(22, 22)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tInit, Me.i_info})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 609)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(156, 27)
        Me.StatusStrip1.TabIndex = 25
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
        Me.i_info.Size = New System.Drawing.Size(117, 22)
        Me.i_info.Spring = True
        Me.i_info.Tag = ""
        Me.i_info.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'bEnreg
        '
        Me.bEnreg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bEnreg.Location = New System.Drawing.Point(16, 192)
        Me.bEnreg.Name = "bEnreg"
        Me.bEnreg.Size = New System.Drawing.Size(124, 33)
        Me.bEnreg.TabIndex = 27
        Me.bEnreg.Text = "Enregistrer"
        Me.bEnreg.UseVisualStyleBackColor = True
        '
        'bCompte
        '
        Me.bCompte.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bCompte.Image = Global.TEMPLATE.My.Resources.Resources.compte_fw
        Me.bCompte.Location = New System.Drawing.Point(16, 115)
        Me.bCompte.Name = "bCompte"
        Me.bCompte.Size = New System.Drawing.Size(124, 45)
        Me.bCompte.TabIndex = 26
        Me.bCompte.Text = "Comptes"
        Me.bCompte.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.bCompte.UseVisualStyleBackColor = True
        '
        'bIllot
        '
        Me.bIllot.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bIllot.Image = Global.TEMPLATE.My.Resources.Resources.ilot_fw
        Me.bIllot.Location = New System.Drawing.Point(16, 64)
        Me.bIllot.Name = "bIllot"
        Me.bIllot.Size = New System.Drawing.Size(124, 45)
        Me.bIllot.TabIndex = 26
        Me.bIllot.Text = "Ilots"
        Me.bIllot.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.bIllot.UseVisualStyleBackColor = True
        '
        'bSite
        '
        Me.bSite.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bSite.Image = Global.TEMPLATE.My.Resources.Resources.sites_fw
        Me.bSite.Location = New System.Drawing.Point(16, 13)
        Me.bSite.Name = "bSite"
        Me.bSite.Size = New System.Drawing.Size(124, 45)
        Me.bSite.TabIndex = 26
        Me.bSite.Text = "Sites"
        Me.bSite.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.bSite.UseVisualStyleBackColor = True
        '
        'pFinance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.bEnreg)
        Me.Controls.Add(Me.bCompte)
        Me.Controls.Add(Me.bIllot)
        Me.Controls.Add(Me.bSite)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.label10)
        Me.Name = "pFinance"
        Me.Size = New System.Drawing.Size(156, 636)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents label10 As Windows.Forms.Label
    Friend WithEvents StatusStrip1 As Windows.Forms.StatusStrip
    Friend WithEvents tInit As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents i_info As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents bSite As Windows.Forms.Button
    Friend WithEvents bIllot As Windows.Forms.Button
    Friend WithEvents bCompte As Windows.Forms.Button
    Friend WithEvents bEnreg As Windows.Forms.Button
End Class
