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

                meFormAdjustW(chkLocX + thischeck.Width, chkLocX)
            ElseIf thistask.TaskType = "Note" Then
                Dim thisminilabel As New Label()
                thisminilabel.Text = thistask.TaskDesc
                thisminilabel.Location = New Point(chkLocX + 16, chkLocY - 5)
                thisminilabel.Font = New Font("Nirmala UI", 8, FontStyle.Bold)
                thisminilabel.ForeColor = Color.White
                thisminilabel.AutoSize = True
                thisminilabel.Tag = checkIDX.ToString

                Me.Controls.Add(thisminilabel)

                meFormAdjustW(chkLocX + 16 + thisminilabel.Width, chkLocX + 16)
            Else
                Dim thislabel As New Label()
                thislabel.Text = thistask.TaskDesc
                thislabel.Location = New Point(chkLocX - 5, chkLocY)
                thislabel.Font = New Font("Nirmala UI", 12, FontStyle.Bold)
                thislabel.ForeColor = Color.White
                thislabel.AutoSize = True
                thislabel.Tag = checkIDX.ToString

                Me.Controls.Add(thislabel)

                meFormAdjustW(chkLocX - 5 + thislabel.Width, chkLocX - 5)
            End If

            If thistask.TaskType = "Note" Then
                chkLocY += 15
            Else
                chkLocY += 27
            End If

            checkIDX += 1

        Next thistask

        bUppate = True
    End Sub

    Private Sub meFormAdjustW(ByVal thislabelwidth As Integer, ByVal iAddWidth As Integer)
        If thislabelwidth > Me.Width Then
            Me.Width = thislabelwidth + (iAddWidth * 2)
        End If
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

                Dim sChecked As String = ""

                If thisTask.TaskType = "Task" Then
                    If IDXup = currentIDX Then
                        If sender.checked Then
                            sChecked = "Check"
                        End If
                    Else
                        If thisTask.TaskStatus Then
                            sChecked = "Check"
                        End If
                    End If
                End If

                frmMain.txtChecklist.Text &= sChecked & "|" & thisTask.TaskDesc & "|" & thisTask.TaskType

                currentIDX += 1
            Next

            UpdateText(Application.StartupPath & "\Checklist.txt", frmMain.txtChecklist.Text, False)

            Dim arrMyTask As New ArrayList
            myLoadTask(arrMyTask, outChkTasks)
        End If
    End Sub
End Class