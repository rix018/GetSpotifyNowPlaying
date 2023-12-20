Public Class frmChecklist
    Public outChkTasks As New List(Of myTask)

    Private Sub frmChecklist_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim chkLocX As Integer = 17
        Dim chkLocY As Integer = 41

        Me.BackColor = Color.FromArgb(255, 0, 255, 0)

        For Each thistask As myTask In outChkTasks
            Dim thischeck As New CheckBox()

            AddHandler thischeck.CheckedChanged, AddressOf ChkStrike
            thischeck.Text = thistask.TaskDesc
            thischeck.Location = New Point(chkLocX, chkLocY)
            thischeck.Font = New Font("Nirmala UI", 9.75, FontStyle.Bold)
            thischeck.ForeColor = Color.White
            thischeck.AutoSize = True
            thischeck.Checked = thistask.TaskStatus

            Me.Controls.Add(thischeck)

            chkLocY += 27

        Next thistask
    End Sub

    Private Sub ChkStrike(sender As System.Object, e As System.EventArgs)
        If sender.checkstate Then
            sender.Font = New Font("Nirmala UI", 9.75, FontStyle.Strikeout)
        Else
            sender.Font = New Font("Nirmala UI", 9.75, FontStyle.Bold)
        End If
    End Sub
End Class