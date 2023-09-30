<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.btnTimer = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtGetPauseMessage = New System.Windows.Forms.TextBox()
        Me.btnPauseMsgUpdate = New System.Windows.Forms.Button()
        Me.btnPauseMsgRefresh = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Coolvetica Rg", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "NOW PLAYING"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Caviar Dreams", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(201, 34)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "NOW PLAYING"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.QuitToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(104, 48)
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.QuitToolStripMenuItem.Text = "Quit"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Simple Spotify Snip"
        '
        'btnTimer
        '
        Me.btnTimer.Location = New System.Drawing.Point(12, 75)
        Me.btnTimer.Name = "btnTimer"
        Me.btnTimer.Size = New System.Drawing.Size(75, 23)
        Me.btnTimer.TabIndex = 2
        Me.btnTimer.Text = "Timer Start"
        Me.btnTimer.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(93, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "00:00:00"
        '
        'txtGetPauseMessage
        '
        Me.txtGetPauseMessage.Location = New System.Drawing.Point(15, 103)
        Me.txtGetPauseMessage.Multiline = True
        Me.txtGetPauseMessage.Name = "txtGetPauseMessage"
        Me.txtGetPauseMessage.Size = New System.Drawing.Size(383, 118)
        Me.txtGetPauseMessage.TabIndex = 4
        '
        'btnPauseMsgUpdate
        '
        Me.btnPauseMsgUpdate.Location = New System.Drawing.Point(15, 227)
        Me.btnPauseMsgUpdate.Name = "btnPauseMsgUpdate"
        Me.btnPauseMsgUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnPauseMsgUpdate.TabIndex = 5
        Me.btnPauseMsgUpdate.Text = "Update"
        Me.btnPauseMsgUpdate.UseVisualStyleBackColor = True
        '
        'btnPauseMsgRefresh
        '
        Me.btnPauseMsgRefresh.Location = New System.Drawing.Point(93, 227)
        Me.btnPauseMsgRefresh.Name = "btnPauseMsgRefresh"
        Me.btnPauseMsgRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnPauseMsgRefresh.TabIndex = 6
        Me.btnPauseMsgRefresh.Text = "Refresh"
        Me.btnPauseMsgRefresh.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 261)
        Me.Controls.Add(Me.btnPauseMsgRefresh)
        Me.Controls.Add(Me.btnPauseMsgUpdate)
        Me.Controls.Add(Me.txtGetPauseMessage)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnTimer)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.Text = "Simple Spotify Snip feat. Stream Tool"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents QuitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents btnTimer As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents txtGetPauseMessage As TextBox
    Friend WithEvents btnPauseMsgUpdate As Button
    Friend WithEvents btnPauseMsgRefresh As Button
End Class
