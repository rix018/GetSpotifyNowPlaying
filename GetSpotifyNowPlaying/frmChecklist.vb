Public Class frmChecklist
    Public outChkTasks As New List(Of myTask)
    Public bUppate As Boolean

    Private Sub frmChecklist_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim chkLocX As Integer = 17
        Dim chkLocY As Integer = 41
        Dim checkIDX As Integer = 0

        Me.BackColor = Color.FromArgb(255, 0, 255, 0)
        bUppate = False

        For Each thistask As myTask In outChkTasks
            If thistask.TaskType = "Task" Then
                Dim thischeck As New CheckBox()

                AddHandler thischeck.CheckedChanged, AddressOf ChkStrike
                thischeck.Text = thistask.TaskDesc
                thischeck.Location = New Point(chkLocX, chkLocY)
                thischeck.Font = New Font("Nirmala UI", 9.75, FontStyle.Bold)
                thischeck.ForeColor = Color.White
                thischeck.AutoSize = True
                thischeck.Checked = thistask.TaskStatus
                thischeck.Tag = checkIDX.ToString

                Me.Controls.Add(thischeck)
            Else
                Dim thislabel As New Label()
                thislabel.Text = thistask.TaskDesc
                thislabel.Location = New Point(chkLocX - 5, chkLocY)
                thislabel.Font = New Font("Nirmala UI", 12, FontStyle.Bold)
                thislabel.ForeColor = Color.White
                thislabel.AutoSize = True
                thislabel.Tag = checkIDX.ToString

                Me.Controls.Add(thislabel)
            End If

            chkLocY += 27
            checkIDX += 1

        Next thistask

        bUppate = True
    End Sub

    Private Sub ChkStrike(sender As System.Object, e As System.EventArgs)
        If sender.checkstate Then
            sender.Font = New Font("Nirmala UI", 9.75, FontStyle.Strikeout)
        Else
            sender.Font = New Font("Nirmala UI", 9.75, FontStyle.Bold)
        End If

        If bUppate Then
            Dim IDXup As Integer = CInt(sender.tag)
            Dim bChkState As Boolean = sender.Checked
            Dim arrNewTask As New List(Of myTask)
            Dim currentIDX As Integer = 0

            frmMain.txtChecklist.Text = ""

            For Each thisTask As myTask In outChkTasks
                If frmMain.txtChecklist.Text <> "" Then
                    frmMain.txtChecklist.Text = frmMain.txtChecklist.Text.TrimEnd
                    frmMain.txtChecklist.Text &= vbCrLf
                End If

                If thisTask.TaskType = "Task" Then
                    If IDXup = currentIDX Then
                        If sender.checked Then
                            frmMain.txtChecklist.Text &= "Check"
                        Else
                            frmMain.txtChecklist.Text &= ""
                        End If
                    Else
                        If thisTask.TaskStatus Then
                            frmMain.txtChecklist.Text &= "Check"
                        Else
                            frmMain.txtChecklist.Text &= ""
                        End If
                    End If
                End If

                frmMain.txtChecklist.Text &= "|" & thisTask.TaskDesc
                frmMain.txtChecklist.Text &= "|" & thisTask.TaskType

                currentIDX += 1
            Next

            UpdateText(Application.StartupPath & "\Checklist.txt", frmMain.txtChecklist.Text, False)

            outChkTasks.Clear()

            Dim arrMyTask As New ArrayList
            myLoadTask(arrMyTask, outChkTasks)

        End If
    End Sub
End Class